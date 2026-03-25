using System;
using System.IO;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using FocalSpec.GuiExample.View;
using FocalSpec.GuiExample.Presenter;
using System.Collections.Generic;
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

        /* Options for table sequence */
        private const string SaveScan01 = @"C:\Users\Public\Downloads\Scan01.asc";
        private const string SaveScan02 = @"C:\Users\Public\Downloads\Scan02.asc";
        private const string SaveScan03 = @"C:\Users\Public\Downloads\Scan03.asc";
        private const string SaveScan04 = @"C:\Users\Public\Downloads\Scan04.asc";
        private const string SaveScan05 = @"C:\Users\Public\Downloads\Scan05.asc";
        private const string SaveScan06 = @"C:\Users\Public\Downloads\Scan06.asc";
        private const string SaveScan07 = @"C:\Users\Public\Downloads\Scan07.asc";
        private const string SaveScan08 = @"C:\Users\Public\Downloads\Scan08.asc";
        private const string SaveScan09 = @"C:\Users\Public\Downloads\Scan09.asc";
        private const string SaveScan10 = @"C:\Users\Public\Downloads\Scan10.asc";
        private const string SaveScan11 = @"C:\Users\Public\Downloads\Scan11.asc";
        private const string SaveScan12 = @"C:\Users\Public\Downloads\Scan12.asc";
        private const string SaveScan13 = @"C:\Users\Public\Downloads\Scan13.asc";
        private const string SaveScan14 = @"C:\Users\Public\Downloads\Scan14.asc";
        private const string SaveScan15 = @"C:\Users\Public\Downloads\Scan15.asc";
        private const string SaveScan16 = @"C:\Users\Public\Downloads\Scan16.asc";
        private const string SaveScan17 = @"C:\Users\Public\Downloads\Scan17.asc";
        private const string SaveScan18 = @"C:\Users\Public\Downloads\Scan18.asc";
        private const string SaveScan19 = @"C:\Users\Public\Downloads\Scan19.asc";
        private const string SaveScan20 = @"C:\Users\Public\Downloads\Scan20.asc";

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
            if (speed < 0 || speed > 200) {
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

        // TODO: Kept if needed for a different function.
        private void AppendTimingLog(string message)
        {
            string path = @"C:\Users\Public\Downloads\start_time_log.txt";
            File.AppendAllText(path, $"{DateTime.Now.ToString("HH:mm:ss.fff")} | {message}{Environment.NewLine}");
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
                    
                    controllerWaiting.Value = new Bool(true);
                    funcCall.StringValue = "\"\"";
                    //AppendTimingLog("Start RAPID Clicked");
                    controller.Rapid.Start();
                }
                _ = RunSequenceLoop();
                //AppendTimingLog("Robot Motion Running");
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

            new() { RapidFunctionName="NIMS_BeforeScanMotion" }, 
            new() { RapidFunctionName="Scan01PreScan" },

            new() { RapidFunctionName="Scan01TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan02PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan01 },

            new() { RapidFunctionName="Scan02TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan03PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan02 },

            new() { RapidFunctionName="Scan03TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan04PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan03 },

            new() { RapidFunctionName="Scan04TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan05PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan04 },

            new() { RapidFunctionName="Scan05TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan06PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan05 },

            new() { RapidFunctionName="Scan06TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan07PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan06 },

            new() { RapidFunctionName="Scan07TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan08PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan07 },

            new() { RapidFunctionName="Scan08TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan09PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan08 },

            new() { RapidFunctionName="Scan09TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan10PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan09 },

            new() { RapidFunctionName="Scan10TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan11PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan10 },

            new() { RapidFunctionName="Scan11TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan12PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan11 },

            new() { RapidFunctionName="Scan12TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan13PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan12 },

            new() { RapidFunctionName="Scan13TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan14PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan13 },

            new() { RapidFunctionName="Scan14TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan15PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan14 },

            new() { RapidFunctionName="Scan15TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan16PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan15 },

            new() { RapidFunctionName="Scan16TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan17PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan16 },

            new() { RapidFunctionName="Scan17TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan18PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan17 },

            new() { RapidFunctionName="Scan18TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan19PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan18 },

            new() { RapidFunctionName="Scan19TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="Scan20PreScan", Action=UiAction.StopAndSave, SavePath=SaveScan19 },

            new() { RapidFunctionName="Scan20TakeScan", Action=UiAction.StartScan },
            new() { RapidFunctionName="ScanToStand", Action=UiAction.StopAndSave, SavePath=SaveScan20 },

            new() { RapidFunctionName="NIMS_Place" }

            // If needed, add more scan steps before ScanToStand and adjust UiAction pipeline accordingly

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