using System;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using FocalSpec.GuiExample.View;
using FocalSpec.GuiExample.Presenter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rapid
{
       
    class TaskWaiter{
        public async System.Threading.Tasks.Task WaitSeconds(int miliseconds){
            await System.Threading.Tasks.Task.Delay(miliseconds);
        }
    }
    
    class RapidFunctions
    {
        ABB.Robotics.Controllers.Controller objController;
        private NetworkScanner objNetworkWatcher = null;
        private static MainPresenter _mainPresenter;
        public static MainPresenter MainPresenter => _mainPresenter;

        public ABB.Robotics.Controllers.RapidDomain.Task[] tasks = null;
        Mastership m;

        public
        RapidData controllerWaiting; // Useful
        RapidData funcCall; // Useful

        TaskWaiter taskWaiter = new TaskWaiter();
        public MainView mainView;

        public decimal waittime = 100; // Should probably be a const or something
        public string IP;
        public Controller controller = null;

        public bool _waiting = true; // Useful although is it used in the best way possible?
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

        public void Start(){
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

                //Mastership to control RAPID execution
                using (m = Mastership.Request(controller)) {
                    // Perform operation
                    tasks[0].ResetProgramPointer();
                    controller.Rapid.Start();
                    funcCall.StringValue = "\"\"";
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }

        }

        // Stop Everything
        public void Stop(){
            try{
                //no controller nothing stops
                if (controller != null) {
                    return;
                }
                //If robot isn't currently executing, skip stop
                if (controller.Rapid.ExecutionStatus != ExecutionStatus.Running) {
                    return;
                }
                //Attempt to stop using Mastership
                try {
                    using (m = Mastership.Request(controller)) {
                        //Ensures Mastership is released automatically on exit
                        m.ReleaseOnDispose = true;

                        controller.Rapid.Stop(ABB.Robotics.Controllers.RapidDomain.StopMode.Immediate);
                        tasks[0].ResetProgramPointer();
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
       
        public async void PhotoSequence() {
            /*Option if table sequence used*/
            var steps = BuildPhotoSequence();
            int sequenceStep = 0;
            int numOfPhotoSteps = steps.Count;

            try
            {
                while (sequenceStep < numOfPhotoSteps)
                {
                    if (!_waiting)
                    {
                        await taskWaiter.WaitSeconds(25);
                        continue;
                    }
                    using (m = Mastership.Request(controller))
                    {
                        var step = steps[sequenceStep];
                        ApplyUiAction(step);
                        //Push next RAPID Func Call and Clear wait
                        SetFuncCall(step.RapidFunctionName);
                        ClearWaitAndProceed();

                        //RAPID proceeds,ProgramPointerChanged will fire as it runs.
                    }
                    sequenceStep++;
                    await taskWaiter.WaitSeconds(25);
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
        /*{
            int sequenceStep = 0;
            int numOfPhotoSteps = 13;

            try{
                while (sequenceStep < numOfPhotoSteps)
                {
                    //skip loop until RAPID signals it's waiting for next command
                    if (!_waiting)
                    {
                        await taskWaiter.WaitSeconds(25);
                        continue;
                    }
                    using (m = Mastership.Request(controller))
                    {
                        switch (sequenceStep)
                        {
                            case 0:
                                funcCall.StringValue = "\"XpertsPickUp\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 1:
                                funcCall.StringValue = "\"XpertsMoveFromPickUpToSensor\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 2:
                                funcCall.StringValue = "\"XpertsRightEdgePreScan\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 3:
                                mainView.getBatchMode().TriggerClearLogic();
                                mainView.getBatchMode().TriggerStartLogic();
                                funcCall.StringValue = "\"XpertsRightEdgeTakeScan\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 4:
                                mainView.getBatchMode().TriggerStopLogic();
                                mainView.getBatchMode().TriggerSaveLogic("C:\\Users\\Public\\Downloads\\RightEdge.asc");
                                funcCall.StringValue = "\"XpertsBackEdgePreScan\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 5:
                                mainView.getBatchMode().TriggerClearLogic();
                                mainView.getBatchMode().TriggerStartLogic();
                                funcCall.StringValue = "\"XpertsBackEdgeTakeScan\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;

                                break;
                            case 6:
                                mainView.getBatchMode().TriggerStopLogic();
                                mainView.getBatchMode().TriggerSaveLogic("C:\\Users\\Public\\Downloads\\BackEdge.asc");
                                funcCall.StringValue = "\"XpertsLeftEdgePreScan\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;

                                break;
                            case 7:
                                mainView.getBatchMode().TriggerClearLogic();
                                mainView.getBatchMode().TriggerStartLogic();
                                funcCall.StringValue = "\"XpertsLeftEdgeTakeScan\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 8:
                                mainView.getBatchMode().TriggerStopLogic();
                                mainView.getBatchMode().TriggerSaveLogic("C:\\Users\\Public\\Downloads\\LeftEdge.asc");
                                funcCall.StringValue = "\"XpertsFrontEdgePreScan\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 9:
                                mainView.getBatchMode().TriggerClearLogic();
                                mainView.getBatchMode().TriggerStartLogic();
                                funcCall.StringValue = "\"XpertsFrontEdgeTakeScan\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 10:
                                mainView.getBatchMode().TriggerStopLogic();
                                mainView.getBatchMode().TriggerSaveLogic("C:\\Users\\Public\\Downloads\\FrontEdge.asc");
                                funcCall.StringValue = "\"ScanToStand\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 11:
                                funcCall.StringValue = "\"DropItem\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 12:
                                funcCall.StringValue = "\"GoToInitialState\"";
                                controllerWaiting.Value = new Bool(false);
                                _waiting = false;
                                break;
                            case 99:
                                break;
                            default:
                                return;
                        }
                    }
                    sequenceStep++;
                    await taskWaiter.WaitSeconds(25);
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
        }*/
        /*Option to use table sequence instead of switch*/
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