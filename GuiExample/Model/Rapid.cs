using System;
using System.Windows.Forms;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.RapidDomain;
using FocalSpec.GuiExample.View;
using FocalSpec.GuiExample.Presenter;

namespace Rapid
{
       
    class TaskWaiter
    {

        public async System.Threading.Tasks.Task WaitSeconds(int miliseconds)
        {
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

        private bool _run;

        public
        RapidData data5;
        RapidData data6;
        RapidData data7;
        RapidData data8;
        RapidData controllerWaiting;
        RapidData controllerScannerEnable;
        RapidData funcCall;
        RapidData axis6Allowed;

        TaskWaiter taskWaiter = new TaskWaiter();
        public MainView mainView;
        
        public decimal waittime = 100;
        string s;
        public string IP;
        public Controller controller = null;

        public bool _waiting = true;
        public bool _scannerEnable = true;
        public bool PhotosComplete = false;
        public RapidFunctions(MainView _form1)
        {
            this.mainView = _form1;

        }

       

      
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
        public void ConnectController(ListViewItem CTRLSelect)
        {
            try
            {
                ListViewItem item = CTRLSelect;
                if (item.Tag != null)
                {
                    ControllerInfo controllerInfo = (ControllerInfo)item.Tag;
                    if (controllerInfo.Availability == Availability.Available)
                    {
                        this.controller = ControllerFactory.CreateFrom(controllerInfo);
                    }
                    else
                    {
                        MessageBox.Show("Selected controller not available.");
                    }
                }

            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Unexpected error occurred: " + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }
        }

        public void Start()
        {
            try
            {
                if (controller.OperatingMode == ControllerOperatingMode.Auto)
                {
                    controllerWaiting = controller.Rapid.GetRapidData("T_ROB1", "TRob1Main", "extern_wait");
                    controllerWaiting.ValueChanged += new EventHandler<DataValueChangedEventArgs>(ControllerWaitCheck);
                    controllerScannerEnable = controller.Rapid.GetRapidData("T_ROB1", "TRob1Main", "scannerEnable");
                    //controllerScannerEnable.ValueChanged += new EventHandler<DataValueChangedEventArgs>(ScannerEnableCheck);
                    tasks = controller.Rapid.GetTasks();
                    tasks[0].ProgramPointerChanged += new EventHandler<ProgramPositionEventArgs>(ProgramPointer_Changed);

                    axis6Allowed = controller.Rapid.GetRapidData("T_ROB1", "TRob1Main", "axis6Allowed");

                    using (m = Mastership.Request(controller.Rapid))
                    {
                        // Perform operation
                        tasks[0].ResetProgramPointer();
                        controller.Rapid.Start();
                        funcCall = controller.Rapid.GetRapidData("T_ROB1", "TRob1Main", "funcCall");
                        funcCall.StringValue = "\"\"";
                    }

                }
                else
                {
                    MessageBox.Show(
                        "Automatic mode is required to start execution from a remote client.");

                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);
            }

        }

        // Stop Everything
        public void Stop()
        {
            try
            {
                //Stop the Send data while loop
                _run = false;

                if (controller != null)
                {
                    if (controller.Rapid.ExecutionStatus ==
                           ABB.Robotics.Controllers.RapidDomain.ExecutionStatus.Running)
                    {

                        try
                        {
                            using (m = Mastership.Request(controller))
                            {
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

                }



            }
            catch (System.Exception ex)
            {

                MessageBox.Show("An Error Has Occurred: " + ex.Message);
                mainView.LogMessage(ex.Message + ex.Source + ex.StackTrace);


            }


        }
        public void Continue()
        {
            Bool _waiting = (Bool)controllerWaiting.Value;
            if (_waiting == true)
            {
                //Things to do when robot is waiting 
                using (m = Mastership.Request(controller))
                {
                    controllerWaiting.Value = new Bool(false);
                }
            }
        }


        public async void PhotoSequence()
        {
            int sequenceStep = 0;
            int numOfPhotoSteps = 13;
            string hmm = funcCall.StringValue;
            
            try
            {


                while (sequenceStep < numOfPhotoSteps)
                {

                    if (_waiting == true)
                    {
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
                    }
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

        private void ControllerWaitCheck(object sender, DataValueChangedEventArgs e)
        {
            _waiting = (Bool)controllerWaiting.Value;
            if (_waiting == true)
            {
                
            }

        }
    }
}