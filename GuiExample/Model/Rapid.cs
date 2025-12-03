using System;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using FocalSpec.GuiExample.View;
using FocalSpec.GuiExample.Presenter;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace Rapid
{

    class RapidFunctions
    {
        ABB.Robotics.Controllers.Controller objController;
        private NetworkScanner objNetworkWatcher = null;
        private static MainPresenter _mainPresenter;
        public static MainPresenter MainPresenter => _mainPresenter;

        public ABB.Robotics.Controllers.RapidDomain.Task[] tasks = null;
        Mastership m;

        public
        RapidData controllerWaiting; 
        RapidData funcCall; 
        RapidData travelSpeed;
        RapidData scanSpeed;

        public MainView mainView;

        // public string IP;
        public Controller controller = null;

        public bool _waiting = true;

        //sequence state
        private bool sequenceRunning;
        private int sequenceStep;
        private List<SequenceStep> steps;
        private CancellationTokenSource sequenceCounts;

        public RapidFunctions(MainView _form1)
        {
            this.mainView = _form1;

        }
        /*Options for table sequence*/
        private const string SaveRightEdge = @"C:\Users\Public\Downloads\RightEdge.asc";
        private const string SaveBackEdge  = @"C:\Users\Public\Downloads\BackEdge.asc";
        private const string SaveLeftEdge  = @"C:\Users\Public\Downloads\LeftEdge.asc";
        private const string SaveFrontEdge = @"C:\Users\Public\Downloads\FrontEdge.asc";

        //Updates RAPID funcCall for robot to know which routine to execute next
        private void SetFuncCall(string name) => funcCall.StringValue = $"\"{name}\"";

        private void ClearWaitAndProceed(){
            //release wait condition
            controllerWaiting.Value=new Bool(false);

            //marks robot no longer waiting
            _waiting=false;
        }

        private void StartScanUi(){
            //Access batch controller form main view
            var batch=mainView.getBatchMode();

            //clear previously buffered data
            batch.TriggerClearLogic();

            //Begin new scan
            batch.TriggerStartLogic();
        }

        private void StopAndSaveUi(string path){
            var batch=mainView.getBatchMode();
            batch.TriggerStopLogic();

            //if valid file path, save scan data
            if(!string.IsNullOrWhiteSpace(path)){
                batch.TriggerSaveLogic(path);
            }
        }

        private bool WaitGate()
        {
            if (tasks == null || tasks.Length == 0) return false;
            var pp = tasks[0].ProgramPointer;
            if (pp == null || controllerWaiting == null) return false;
            return pp.Routine == "ControllerWait" && (Bool)controllerWaiting.Value == true;
        }
        private void SafeProceed(string nextFunc)
        {
            if (!WaitGate())
                return;
            SetFuncCall(nextFunc);
            ClearWaitAndProceed();
        }


        // Network stuff should probably be it's own module? Decoupled design anyone?
        //Controller Scanner
        //Scan for and add controllers to the list
        public ControllerInfoCollection ScanControllers()
        {
            try
            {
                // Create A Robo Studio Controller Connector
                this.objNetworkWatcher = new NetworkScanner();
                objNetworkWatcher.Scan();
                ControllerInfoCollection controllersList = objNetworkWatcher.Controllers;
                return controllersList;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                return null;
            }

        }

        //Connect to the Robo Studio Controller
        public void ConnectController(ListViewItem CTRLSelect){
            try{
                ListViewItem item = CTRLSelect;
                //if not item or valid tag exit
                if (item.Tag is not ControllerInfo info) {
                    MessageBox.Show("Invalid selection.");
                    return;
                }
                //exit if no controller availability
                if (info.Availability != Availability.Available) {
                    MessageBox.Show("Selected controller not available.");
                    return;
                }
                //Connect to ABB controller using standalone mode (connects directly to robot without RobotStudio)
                controller = Controller.Connect(info, ConnectionType.Standalone, false);
            }
            catch (System.Exception ex){
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }
        }

        public void SetTravelSpeed(int speed)
        {
            // TODO: Investigate why speed isn't always set before running
            // Shouldn't be possible because of the input limitations, but just in case
            if (speed < 0 || speed > 250) {
                return;
            }

            // Accessing rapid data types requires this dance
            travelSpeed = controller.Rapid.GetRapidData("T_ROB1", "TRob1Main", "travelSpeed");
            RapidDataType rdt = controller.Rapid.GetRapidDataType("T_ROB1", "TRob1Main", "travelSpeed");
            UserDefined speedData = new UserDefined(rdt);
            speedData = (UserDefined) travelSpeed.Value;
            // Ok so, first of all if you try to use FillFromString it says it's obsolete
            // And then it tells you to use FillFromString2, 2!!!!, they literally just made a new version lmfao
            speedData.FillFromString2("[" + speed + ",500,5000,1000]");
            using (m = Mastership.Request(controller))
            {
                travelSpeed.Value = speedData;
            }
        }

        public void SetScanSpeed(int speed)
        {
            if (speed < 0 || speed > 50)
            {
                return;
            }

            scanSpeed = controller.Rapid.GetRapidData("T_ROB1", "TRob1Main", "scanSpeed");
            RapidDataType rdt = controller.Rapid.GetRapidDataType("T_ROB1", "TRob1Main", "scanSpeed");
            UserDefined speedData = new UserDefined(rdt);
            speedData = (UserDefined) scanSpeed.Value;
            speedData.FillFromString2("[" + speed + ",500,5000,1000]");
            using (m = Mastership.Request(controller))
            {
                scanSpeed.Value = speedData;
            }
        }

        public async void Start(){
            try{
                if (controller == null) {
                    MessageBox.Show("No controller connected.");
                    return;
                }
                if (controller.OperatingMode != ControllerOperatingMode.Auto) {
                    MessageBox.Show("Automatic mode required to start.");
                    return;
                }
                //Get handles to variables
                controllerWaiting = controller.Rapid.GetRapidData("T_ROB1", "TRob1Main", "extern_wait");
                funcCall = controller.Rapid.GetRapidData("T_ROB1", "TRob1Main", "funcCall");

                //Retrieve all active tasks
                tasks = controller.Rapid.GetTasks();
                if (tasks == null || tasks.Length == 0) {
                    MessageBox.Show("No RAID tasks available.");
                    return;
                }
                //Monitor events whenever RAPID program pointer moves (PP moves when RAPID executes new instruction or enters/exits routine)
                tasks[0].ProgramPointerChanged += new EventHandler<ProgramPositionEventArgs>(ProgramPointer_Changed); // When would the PP change?

                steps = BuildPhotoSequence();
                sequenceStep = 0;
                //Mastership to control RAPID execution
                using (m = Mastership.Request(controller))
                {
                    //Make sure RAPID is stopped before resetting PP
                    if (controller.Rapid.ExecutionStatus == ExecutionStatus.Running)
                    {
                        controller.Rapid.Stop(StopMode.Immediate);
                        //Wait until it's completely stopped
                        while(controller.Rapid.ExecutionStatus != ExecutionStatus.Stopped)
                        {
                            Thread.Sleep(50);
                        }
                    }
                    controllerWaiting.Value = new Bool(true);
                    funcCall.StringValue = "\"\"";

                    // Perform operation
                    tasks[0].ResetProgramPointer();
                    controller.Rapid.Start();
                }
                await System.Threading.Tasks.Task.Delay(100);
                _ = RunSequenceLoop();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }

        }

        // Stop Everything
        public void Stop(bool immediate = false) {
            try{
                //no controller nothing stops
                if (controller == null) {
                    sequenceCounts?.Cancel();
                    sequenceRunning = false;
                    return;
                }
                //If robot isn't currently executing, skip stop
                if (controller.Rapid.ExecutionStatus != ExecutionStatus.Running) {
                    return;
                }
                //Attempt to stop using Mastership
                try {
                    using (m = Mastership.Request(controller))
                    {
                        if (immediate)
                        {
                            controller.Rapid.Stop(StopMode.Immediate);
                        } else
                        {
                            controller.Rapid.Stop(StopMode.Cycle);
                        }
                    }

                    if (tasks != null && tasks.Length > 0)
                    {
                        tasks[0].ProgramPointerChanged -= ProgramPointer_Changed;
                    }
                }
                catch (System.InvalidOperationException ex)
                {
                    MessageBox.Show("Mastership is held by another client." + ex.Message);
                    mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Unexpected error occurred: " + ex.Message);
                    mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
                }

            }
            catch (System.Exception ex)
            {

                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);


            }


        }

        public void Resume()
        {
            try
            {
                if (controller == null)
                {
                    return;
                }

                if (controller.OperatingMode != ControllerOperatingMode.Auto)
                {
                    MessageBox.Show("Switch controller to Auto to step.");
                    return;
                }
                if (sequenceRunning)
                {
                    if (!WaitGate())
                    {
                        using (Mastership.Request(controller))
                        {
                            if(controller.Rapid.ExecutionStatus != ExecutionStatus.Running)
                            {
                                controller.Rapid.Start();
                                mainView.LogMessage("RAPID to reach wait gate");
                            }
                            else
                            {
                                mainView.LogMessage("Already Running");
                            }
                        }
                    }
                    else
                    {
                        mainView.LogMessage("At gate");
                    }
                    return;
                }
                if (WaitGate())
                {
                    _ = RunSequenceLoop();
                    mainView.LogMessage("Resume sequence");
                    return;
                }
                // if program is stopped (not at gate), resume motion to reach the gate
                using (Mastership.Request(controller))
                {
                    if (controller.Rapid.ExecutionStatus != ExecutionStatus.Running)
                    {
                        controller.Rapid.Start();
                        mainView.LogMessage("Started RAPID to reach next wait gate…");
                    }
                    else
                    {
                        mainView.LogMessage("Not at wait gate yet");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error continuing: " + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }
        }

        public async System.Threading.Tasks.Task RunSequenceLoop()
        {
            if (steps == null || steps.Count == 0)
            {
                steps = BuildPhotoSequence();
                if (sequenceStep < 0 || sequenceStep >= steps.Count) sequenceStep = 0;
            }
            if (sequenceRunning) return;

            sequenceRunning = true;
            sequenceCounts?.Cancel();           // kill any old token
            sequenceCounts = new CancellationTokenSource();

            try
            {
                while (sequenceStep < steps.Count && !sequenceCounts.IsCancellationRequested)
                {
                    if (!WaitGate())
                    {
                        await System.Threading.Tasks.Task.Delay(50, sequenceCounts.Token);
                        continue;
                    }

                    using (Mastership.Request(controller))
                    {
                        var step = steps[sequenceStep];
                        ApplyUiAction(step);
                        SafeProceed(step.RapidFunctionName);   // sets funcCall + extern_wait := FALSE
                    }

                    sequenceStep++;
                    await System.Threading.Tasks.Task.Delay(50, sequenceCounts.Token);
                }
            }
            catch (OperationCanceledException) { /* expected on Stop */ }
            finally
            {
                sequenceRunning = false;
                sequenceCounts?.Dispose();
                sequenceCounts = null;
            }
        }



        public async void PhotoSequence() {
            steps = BuildPhotoSequence();
            sequenceStep = 0;
            int numOfPhotoSteps = steps.Count;
            if (sequenceRunning) 
            {
                return;
            }
            sequenceRunning = true;
            sequenceCounts = new CancellationTokenSource();

            try
            {
                while (sequenceStep < numOfPhotoSteps && !sequenceCounts.IsCancellationRequested)
                {
                    if (!WaitGate())
                    {
                        await System.Threading.Tasks.Task.Delay(50, sequenceCounts.Token);
                        continue;
                    }
                    using (m = Mastership.Request(controller))
                    {
                        var step = steps[sequenceStep];
                        ApplyUiAction(step);
                        SafeProceed(step.RapidFunctionName);
                    }
                    sequenceStep++;
                    await System.Threading.Tasks.Task.Delay(50, sequenceCounts.Token);
                }
            }
            catch (System.InvalidOperationException ex)
            {
                MessageBox.Show("Mastership is held by another client." + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }
            finally
            {
                sequenceRunning = false;
            }
        }
        
        private enum UiAction { None, StartScan, StopAndSave }
        private sealed class SequenceStep
        {
            public string RapidFunctionName { get; init; }
            public UiAction Action { get; init; } = UiAction.None;
            public string SavePath { get; init; }
            public override string ToString() => RapidFunctionName;
        }
        private List<SequenceStep> BuildPhotoSequence() => new(){
            new() {RapidFunctionName="XpertsPickUp"},
            new() {RapidFunctionName="XpertsMoveFromPickUpToSensor"},
            new() {RapidFunctionName="XpertsRightEdgePreScan"},

            new() {RapidFunctionName="XpertsRightEdgeTakeScan", Action=UiAction.StartScan},
            new() {RapidFunctionName="XpertsBackEdgePreScan", Action=UiAction.StopAndSave, SavePath=SaveRightEdge},

            new() {RapidFunctionName="XpertsBackEdgeTakeScan", Action=UiAction.StartScan},
            new() {RapidFunctionName="XpertsLeftEdgePreScan", Action=UiAction.StopAndSave, SavePath=SaveBackEdge},

            new() {RapidFunctionName="XpertsLeftEdgeTakeScan", Action=UiAction.StartScan},
            new() {RapidFunctionName="XpertsFrontEdgePreScan", Action=UiAction.StopAndSave, SavePath=SaveLeftEdge},

            new() {RapidFunctionName="XpertsFrontEdgeTakeScan", Action=UiAction.StartScan},
            new() {RapidFunctionName="ScanToStand", Action=UiAction.StopAndSave, SavePath=SaveFrontEdge},

            new() {RapidFunctionName="DropItem"},
            new() {RapidFunctionName="GoToInitialState"}
        };

        private void ApplyUiAction(SequenceStep step)
        {
            switch (step.Action)
            {
                case UiAction.StartScan:
                    StartScanUi();
                    break;
                case UiAction.StopAndSave:
                    StopAndSaveUi(step.SavePath);
                    break;
                case UiAction.None:
                default:
                    break;
            }
        }


        // Event Handlers
        private void ProgramPointer_Changed(object sender, ProgramPositionEventArgs e)
        {
            // The Below Line Logs the program pointer row for debugging
            mainView.LogMessage(tasks[0].ProgramPointer.Range.Begin.Row.ToString());
            _waiting = (Bool)controllerWaiting.Value;
            if (tasks[0].ProgramPointer.Routine == "ControllerWait" && _waiting == true)
            {
                mainView.LogMessage("Waiting");
            }
        }
    }
}