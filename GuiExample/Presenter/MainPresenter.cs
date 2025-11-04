// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainPresenter.cs" company="FocalSpec Ltd">
// FocalSpec Ltd 2016-.
// </copyright>
// <summary>
// Main presenter of the application.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Reflection;
using FocalSpec.FsApiNet.Model;
using FocalSpec.GuiExample.Model;
using FocalSpec.GuiExample.Model.Camera;
using FocalSpec.GuiExample.Model.Export;
using FocalSpec.GuiExample.View;
using Timer = System.Timers.Timer;

namespace FocalSpec.GuiExample.Presenter
{
    /// <summary>
    /// Controls the application.
    /// </summary>
    public class MainPresenter
    {
        /// <summary>
        /// Latest point cloud received from the <code>_cameraManager</code>.
        /// </summary>
        private List<ProfileInfo> _latestProfiles = new List<ProfileInfo>();

        /// <summary>
        /// Latest raw image received from the <code>_cameraManager</code>.
        /// </summary>
        private RawImage _latestRawImage;

        /// <summary>
        /// Provides point clouds.
        /// </summary>
        private readonly CameraManager _cameraManager;

        /// <summary>
        /// Maintains sensor parameters.
        /// </summary>
        private readonly ApplicationSettings _applicationSettings;

        /// <summary>
        /// Sensor parameters.
        /// </summary>
        private readonly SensorParameterStore _parameters;

        /// <summary>
        /// Regularly updates the UI.
        /// </summary>
        private Timer _updateViewTimer;
        private Timer _updateRawImageTimer;
        private readonly object _profileLock = new object();

        /// <summary>
        /// The view of the application.
        /// </summary>
        private IMainView _mainView;

        /// <summary>
        /// The batch mode presenter.
        /// </summary>
        // ReSharper disable once NotAccessedField.Local
        private BatchModePresenter _batchModePresenter;
        public BatchModePresenter BatchMode => _batchModePresenter;

        /// <summary>
        /// Attaches console to the application. Useful in debugging modules that log into console.
        /// </summary>
        /// <param name="dwProcessId">Process ID.</param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPresenter" /> class. Constructor.
        /// </summary>
        /// <param name="mainView">The main view.</param>
        /// <param name="showMainView">Asks the UI framework to activate UI.</param>
        public MainPresenter(IMainView mainView, Action showMainView)
        {
            AttachConsole(-1);

            _parameters = SensorParameterStore.GetInstance();

            for (int i = 0; i < 20; i++)
            {
                _latestProfiles.Add(new ProfileInfo(i - 1, null));
            }

            try
            {
                _applicationSettings = ApplicationSettings.LoadFromFile();
            }
            catch (ArgumentException e)
            {
                MessageBox.Show($"Error in reading {Defines.ApplicationSettingsFile} :\n\n{e.Message}", @"Load application settings");
                mainView.Close();
                return;
            }

	        _cameraManager = new CameraManager(_applicationSettings);
            _cameraManager.OnCameraSelectionEvent += CameraManagerOnCameraSelectionEvent;

	        var cameraStatus = _cameraManager.InitializeCamera();

            switch (cameraStatus)
            {
                case CameraStatusCode.Ok:
                    break;
                case CameraStatusCode.CameraErrorInvalidCalibrationFile:
                case CameraStatusCode.CameraErrorCalibFileAttributeInvalid:
                case CameraStatusCode.CameraErrorSensorCalibrationFileNotSet:
                    MessageBox.Show(
                        $"Calibration files are not correct.\n\nError code {cameraStatus}\n\nPlease, select correct calibration files and restart program",
                        @"Select calibration files");
                    break;
                case CameraStatusCode.CameraErrorSelftestGrabbingFailed:
                    MessageBox.Show(
                        "Please check that the VEVO filter driver is correctly installed and enabled on the ethernet card.\nFrom Ethernet adapter enable Jumbo Frames by setting the value 9014 Bytes for the Packet size and set Receive Buffer Size to 2048.",
                        @"Camera self test failed");
                    break;
                case CameraStatusCode.InstantionFailed:
                case CameraStatusCode.NotConnected:
                case CameraStatusCode.ImageReceptionNotInstantiated:
                case CameraStatusCode.CameraNotFound:
                case CameraStatusCode.CameraErrorNoLicense:
                case CameraStatusCode.CameraErrorLoadDllFailed:
                case CameraStatusCode.CameraErrorApiAlreadyLoaded:
                case CameraStatusCode.CameraErrorMissingLibraryFiles:
                case CameraStatusCode.CameraErrorInvalidSoftwareVersion:
                 /*   MessageBox.Show($"Could not initialize camera.\n\nError code {cameraStatus}", @"Initialize sensor");
                    _cameraManager.Close();
                    return;*/
                default:
                    break;
            }

            _applicationSettings.SaveToFile();

            InitializeMainView(mainView, _applicationSettings);

            SetUpdateViewTimer(true);
            SetRawImageTimer(false);

            _cameraManager.OnPointCloudReceivedEvent += CameraManagerOnPointCloudReceivedEvent;
            _cameraManager.OnRawImageReceivedEvent += CameraManagerOnRawImageReceivedEvent;

            _batchModePresenter = new BatchModePresenter(_cameraManager, mainView, _applicationSettings);

            Session.ViewMode = ViewMode.RealTime;
            showMainView();
        }
        public BatchModePresenter GetBatchModePresenter()
        {
            return _batchModePresenter;
        }

        private void SetUpdateViewTimer(bool enabled)
        {
            if (enabled)
            {
                if (_updateViewTimer == null)
                    _updateViewTimer = new Timer(Defines.UiUpdateInterval);
                _updateViewTimer.Elapsed += _updateViewTimer_Elapsed;
                _updateViewTimer.Enabled = true;
            }
            else
            {
                if (_updateViewTimer != null)
                    _updateViewTimer.Enabled = false;
            }
        }

        private void SetRawImageTimer(bool enabled)
        {
            if (enabled)
            {
                if (_updateRawImageTimer == null)
                    _updateRawImageTimer = new Timer(Defines.UiUpdateInterval);
                _updateRawImageTimer.Elapsed += _updateRawImageTimer_Elapsed;
                _updateRawImageTimer.Enabled = true;
            }
            else
            {
                if (_updateRawImageTimer != null)
                    _updateRawImageTimer.Enabled = false;
            }
        }

        /// <summary>
        /// Asks calibration files from the user and stores the paths to the settings.
        /// </summary>
        /// <param name="setupView">Provides file selection interface.</param>
        /// <param name="settings">Stores the calibration file paths.</param>
        private bool RequestCalibrationFiles(ISetupView setupView, ApplicationSettings settings)
        {
            if (settings.ZCalibrationFile != null)
                setupView.ZCalibrationFilePath = settings.ZCalibrationFile;
            if (settings.XCalibrationFile != null)
                setupView.XCalibrationFilePath = settings.XCalibrationFile;

            if (!setupView.ShowModal())
            {
                return false;
            }

            if (!setupView.IsZCalibrationFileSet || !setupView.IsXCalibrationFileSet)
            {
                return false;
            }

            settings.ZCalibrationFile = setupView.ZCalibrationFilePath;
            settings.XCalibrationFile = setupView.XCalibrationFilePath;

            return true;
        }

        /// <summary>
        /// Initializes the main view.
        /// </summary>
        /// <param name="mainView">The main view on initialize.</param>
        /// <param name="settings">Active settings.</param>
        private void InitializeMainView(IMainView mainView, ApplicationSettings settings)
        {
            _mainView = mainView;

            // get parameter support status before LoadSensorParameters()
            _cameraManager.IsHdrSupported = _cameraManager.IsParameterSupported(SensorParameter.HdrEnabled);
            _mainView.IsHdrSupported = _cameraManager.IsHdrSupported;
            _mainView.IsHsCamera = _cameraManager.IsHsCamera;

            _mainView.LoadSettings(settings);
            _mainView.LoadSensorParameters(_cameraManager.IsAgcSupported);
            _mainView.OnApplySensorSettings += MainViewOnApplySensorSettings;
            _mainView.OnApplyFilterSettings += ApplyFilterSettingsHandler;

            _mainView.OnSetLayerSpecificSettings += MainViewOnSetLayerSpecificSettings;
            _mainView.OnApplyUiSettings += MainViewOnApplyUiSettings;
            _mainView.OnExportProfileCsv += _mainView_OnExportProfileCsv;
            _mainView.OnProfileLayerSelected += MainViewOnProfileLayerSelected;
            _mainView.OnShowRawImage += MainViewOnShowRawImage;
            _mainView.OnExportRawImage += MainViewOnExportRawImage;
            _mainView.OnRequestCalibrationFiles += MainViewOnRequestCalibrationFiles;
            _mainView.OnFilter += MainViewOnFilter;
            _mainView.OnSetRefractiveIndexes += MainViewOnSetRefractiveIndexes;
            _mainView.OnSetThicknessMode += MainViewOnSetThicknessMode;
            _mainView.OnLoadRecipe += MainViewOnLoadRecipe;
            _mainView.OnSaveRecipe += MainViewOnSaveRecipe;

            _cameraManager.IsXFilterSupported = _cameraManager.IsParameterSupported(SensorParameter.PeakXFilter);
            _mainView.IsXFilterSupported = _cameraManager.IsXFilterSupported;

            _mainView.IsSignalDetectionFilterSupported = _cameraManager.IsParameterSupported(SensorParameter.SignalDetectionFilterLength);
            _mainView.LayerIntensityTypeSupported = _cameraManager.IsParameterSupported(SensorParameter.LayerIntensityType);

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
	        string assemblyVersion = $"{version.Major}.{version.Minor}.{version.Build}";

	        string fsSystemInfo = $"FSSDK GuiExample v. {assemblyVersion}";
	       /* if(_cameraManager.CameraVersion.Length > 0)
		        fsSystemInfo += $" | Firmware v. {_cameraManager.CameraVersion}";
	        if(_cameraManager.CameraSn.Length > 0)
		        fsSystemInfo += $" | CameraId {_cameraManager.CameraSn}";
	        if(_cameraManager.DeviceSerialNumber.Length > 0)
		        fsSystemInfo += $" | LCI sensor S/N: {_cameraManager.DeviceSerialNumber}";*/

            SetTargetIntensityLimits();

            _mainView.SelectRecipe(_applicationSettings.LastRecipe);

            _mainView.OnWindowShown += () =>
            {
				_mainView.SetTitle(fsSystemInfo);
            };

            _mainView.OnWindowClosing += () =>
            {
                try
                {
                    _parameters.LedPulseWidth = _cameraManager.CurrentLedPulseWidth;
                    settings.LastRecipe = _cameraManager.RecipeFileName;
                }
                catch (Exception)
                {
                    // ignored
                }

                settings.SaveToFile();

                _cameraManager.Close();

                SetUpdateViewTimer(false);
            };
        }

        private void SetTargetIntensityLimits()
        {
            if (_cameraManager.IsAgcSupported && _cameraManager.IsAgcEnabled)
                _mainView.SetTargetIntensityLimits(_parameters.AgcTargetIntensity - Defines.AgcMargin, _parameters.AgcTargetIntensity + Defines.AgcMargin);
            else
                _mainView.SetTargetIntensityLimits(Defines.TargetIntensityMin, Defines.TargetIntensityMax);
        }

        private void MainViewOnProfileLayerSelected(ExportLayer layer, int index)
        {
            _cameraManager.SelectedLayerIndex = index;
            _cameraManager.SelectedLayer = layer;
        }

        /// <summary>
        /// Writes selected data into a CSV file in another thread.
        /// </summary>
        /// <param name="file">The filename for saving data.</param>
        /// <param name="layer">Selected layer.</param>
        private void _mainView_OnExportProfileCsv(string file, int layer)
        {
            new Thread(() => 
            {
                var profiles = new List<Profile>();
                lock (_profileLock)
                {
                    foreach (var profile in _latestProfiles)
                        profiles.Add(profile.Profile);
                }

                ExportCsv.Export(file, profiles, layer);
            }).Start();
        }

        /// <summary>
        /// This is where the sensor data gets processed. It is a good practice to store the data in a queue and process it in another thread to ensure smooth performance.
        /// </summary>
        /// <param name="profile">Sensor profile to process.</param>
        private void CameraManagerOnPointCloudReceivedEvent(Profile profile)
        {
            lock (_profileLock)
            {
                _latestProfiles[profile.LayerId + 1].Profile = profile;
                _mainView.SetProfile(_latestProfiles);
            }
        }

        /// <summary>
        /// This is where the raw image data gets processed
        /// </summary>
        /// <param name="rawImage">Raw image to process.</param>
        private void CameraManagerOnRawImageReceivedEvent(RawImage rawImage)
        {
            _latestRawImage = rawImage;
        }

        /// <summary>
        /// Handles camera selection
        /// </summary>
        /// <param name="cameraIds">List of camera ids.</param>
        private void CameraManagerOnCameraSelectionEvent(List<string> cameraIds)
        {
            var cameraSelectionView = new CameraSelectionView(cameraIds, _applicationSettings);
            cameraSelectionView.ShowDialog();
            _cameraManager.CameraId = cameraSelectionView.Camera;
        }

        /// <summary>
        /// Writes parameters to sensor and saves the values.
        /// </summary>
        /// <param name="ledPulseWidth">The LED pulse width [µs] to write to the sensor.</param>
        /// <param name="maxLedPulseWidth">The max. LED pulse width [µs] to write to the sensor if Automatic Gain Control is supported. null otherwise.</param>
        /// <param name="freq">The frequency [Hz] to write to the sensor.</param>
        /// <param name="isExternalPulsingEnabled">If true, then parameter freq. is only the intended target frequency.</param>
        /// <param name="enableAgc">If true, AGC enable is requested.</param>
        /// <param name="agcTargetIntensity">AGC target intensity [0.0f-255.0f] if AGC is in use. null otherwise.</param>
        /// <param name="firLength">Fir length</param>
        /// <param name="averFirLength">Averaging fir length</param>
        /// <param name="detection">Filter to reduce noise at low peak values</param>
        /// <param name="averageIntensity">Filter for averaging intensity values of the detected peaks</param>
        /// <param name="threshold">Peak core threshold</param>
        /// <param name="hdr">HDR enabled/disabled</param>
        /// <param name="vLow2">vLow2</param>
        /// <param name="vLow3">vLow3</param>
        /// <param name="kp1Pos">kp1Pos</param>
        /// <param name="kp2Pos">kp2Pos</param>
        /// <param name="minThickness">Minimum thickness</param>
        /// <param name="materialType">Material type</param>
        /// <param name="detectionSensitivity">Detection sensitivity</param>
        private void MainViewOnApplySensorSettings(float ledPulseWidth, int? maxLedPulseWidth, int freq, bool isExternalPulsingEnabled, 
            bool enableAgc, float? agcTargetIntensity, ref int firLength, ref int averFirLength, ref int detection, ref int averageIntensity,ref int threshold, 
            bool hdr, float vLow2, float vLow3, float kp1Pos, float kp2Pos, float minThickness, int materialType, int detectionSensitivity)
        {
            if (ledPulseWidth <= 0)
            {
                _mainView.ShowMessage("LED pulse width must be greater than 0 µs", "LED Pulse Width");
                return;                
            }

	        _cameraManager.StopGrabbing(100);
            _cameraManager.EnableAgc(enableAgc);

            // HDR needs to be set before setting pulse width, frequency etc. because it's used when calculating timing values
            if (_mainView.IsHdrSupported)
            {
                _parameters.HdrEnabled = hdr;
                _parameters.HdrVLow2 = vLow2;
                _parameters.HdrVLow3 = vLow3;
                _parameters.HdrKp1Pos = kp1Pos;
                _parameters.HdrKp2Pos = kp2Pos;
                _cameraManager.SetHdr(hdr, vLow2, vLow3, kp1Pos, kp2Pos);
            }

            try
            {
                _cameraManager.SetPulseWidth(ledPulseWidth, maxLedPulseWidth);
            }
            catch (ArgumentException)
            {
                // ReSharper disable once PossibleInvalidOperationException
                _mainView.ShowMessage($"LED pulse {ledPulseWidth} must be smaller than max. LED pulse {maxLedPulseWidth.Value}", "Parameterize Automatic Gain Control (AGC)");
                return;
            }

            _cameraManager.SetFreq(freq, isExternalPulsingEnabled);

            _mainView.CurrentLedPulseWidth = ledPulseWidth;
            _parameters.Freq = freq;
            _parameters.IsExternalPulsingEnabled = isExternalPulsingEnabled;
            _parameters.IsAgcEnabled = enableAgc;

            if (agcTargetIntensity.HasValue)
            {
	            if (Math.Abs(_parameters.AgcTargetIntensity - agcTargetIntensity.Value) > float.Epsilon)
	            {
		            _cameraManager.SetAgcTargetIntensity(agcTargetIntensity.Value);
	            }

                _parameters.AgcTargetIntensity = agcTargetIntensity.Value;    
            }

            ApplyFilterSettingsHandler(ref firLength, ref averFirLength, ref detection,
                ref averageIntensity, ref threshold, minThickness, materialType, detectionSensitivity);

            Session.RealTimeCameraSetting.Freq = freq;
            Session.RealTimeCameraSetting.IsExternalPulsingEnabled = isExternalPulsingEnabled;

            SetTargetIntensityLimits();
	        _cameraManager.StartGrabbing(10);
        }

        private void ApplyFilterSettingsHandler(ref int firLength, ref int averFirLength, ref int detection,
            ref int averageIntensity, ref int threshold, float minThickness, int materialType, int detectionSensitivity)
        {
            _parameters.FirLength = firLength;
            if (averFirLength >= 0)
                _parameters.AverFirLength = averFirLength;
            if (detection >= 0)
                _parameters.DetectionFilter = detection;
            if (averageIntensity >= 0)
                _parameters.AverageIntensityFilter = averageIntensity;
            _parameters.Threshold = threshold;

            _cameraManager.SetMinimumThickness(minThickness);

            if (materialType >= 0 && detectionSensitivity >= 0)
            {
                _cameraManager.SetPeakDetectionParameters((MaterialType)materialType, (DetectionSensitivity)detectionSensitivity);

                // Read automatically set values
                _parameters.FirLength = firLength = _cameraManager.GetFirLength();
                if (!_mainView.IsSignalDetectionFilterSupported)
                    _parameters.AverFirLength = averFirLength = _cameraManager.GetAveragingFirLength();
                if (_mainView.IsSignalDetectionFilterSupported)
                {
                    _cameraManager.GetSignalDetectionFilterLength(ref detection, ref averageIntensity);
                    _parameters.DetectionFilter = detection;
                    _parameters.AverageIntensityFilter = averageIntensity;
                }
                _parameters.Threshold = threshold = _cameraManager.GetThreshold();
            }
            else
            {
                _cameraManager.SetFirLength(firLength);
                if (!_mainView.IsSignalDetectionFilterSupported && averFirLength >= 0)
                {
                    _mainView.EnableAveragingFir(_cameraManager.SetAveragingFirLength(averFirLength) == CameraStatusCode.Ok);
                }
                if (_mainView.IsSignalDetectionFilterSupported && detection >= 0 && averageIntensity >= 0)
                {
                    _cameraManager.SetSignalDetectionFilterLength(detection, averageIntensity);
                }
                _cameraManager.SetThreshold(threshold);
            }

        }

        /// <summary>
        /// Sets layer specific settings.
        /// </summary>
        private void MainViewOnSetLayerSpecificSettings()
        {
            _cameraManager.SetLayerSpecificSettings();
        }

        /// <summary>
        /// Saves the values.
        /// </summary>
        /// <param name="profileWindowHeight">Profile window height [mm]</param>
        private void MainViewOnApplyUiSettings(int profileWindowHeight)
        {
            _applicationSettings.UiWindowHeight = profileWindowHeight;
        }

        /// <summary>
        /// Saves the values.
        /// </summary>
        /// <param name="rawImageMode">Sets raw image mode on/off</param>
        private void MainViewOnShowRawImage(bool rawImageMode)
        {
            _cameraManager.SetRawImageMode(rawImageMode, _parameters.Freq, _parameters.IsExternalPulsingEnabled);
            SetRawImageTimer(rawImageMode);
            SetUpdateViewTimer(!rawImageMode);
        }

        /// <summary>
        /// Saves the raw image.
        /// </summary>
        /// <param name="file">The output file.</param>
        /// <param name="image">The bitmap image to be exported.</param>
        private void MainViewOnExportRawImage(string file, Image image)
        {
            ExportImage.SaveToPictureFile(file, image);
        }

        /// <summary>
        /// This method is called at fixed interval for updating the view.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Elapsed event information.</param>
        private void _updateViewTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Session.ViewMode == ViewMode.Batch) return;

            //lock (_profileLock)
            //{
            //    _mainView.SetProfile(_latestProfiles);
            //}

            // Refresh LED pulse width shown in the UI only if we are using Automatic Gain Control (AGC).
            if (_cameraManager.IsAgcSupported && _cameraManager.IsAgcEnabled)
            {
                _mainView.CurrentLedPulseWidth = _cameraManager.CurrentLedPulseWidth;
            }

            if (!_cameraManager.IsConnected())
            {
               _mainView.ShowMessage("Connection to sensor lost", "Connection Status");
            }
        }

        /// <summary>
        /// This method is called at fixed interval for updating the raw image.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Elapsed event information.</param>
        private void _updateRawImageTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (Session.ViewMode != ViewMode.RawImage || _latestRawImage == null)
                return;

            if (!_cameraManager.IsConnected())
            {
                _mainView.ShowMessage("Connection to sensor lost", "Connection Status");
            }
            _mainView.SetRawImage(_latestRawImage);
        }

        private void MainViewOnRequestCalibrationFiles()
        {
            try
            {
                if (RequestCalibrationFiles(new SetupView() {StartPosition = FormStartPosition.CenterParent},
                    _applicationSettings))
                {
                    _mainView.ShowMessage("New settings require program restart", "Setup");
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        /// <summary>
        /// Writes filter values to sensor.
        /// </summary>
        /// <param name="noiseRemoval">Noise removal on/off.</param>
        /// <param name="averageZ">Average Z in µm.</param>
        /// <param name="averageIntensity">Average intensity in µm.</param>
        /// <param name="medianZ">Median Z in pixels.</param>
        /// <param name="medianIntensity">Median intensity in pixels.</param>
        /// <param name="reSample">Re-sample X-resolution in µm.</param>
        /// <param name="peakXFilter">Peak X-filter length</param>
        /// <param name="fillGapMax">Fill gap max in µm.</param>
        /// <param name="trimEdges">Trim edges on/off.</param>
        private void MainViewOnFilter(bool noiseRemoval, double averageZ, double averageIntensity, int medianZ, int medianIntensity, double reSample, 
            int peakXFilter, double fillGapMax, bool trimEdges)
        {
            _cameraManager.SetFilterValues(noiseRemoval, averageZ, averageIntensity, medianZ, medianIntensity, reSample, peakXFilter, fillGapMax, trimEdges);
        }

        /// <summary>
        /// Sets refractive indexes to camera.
        /// </summary>
        private void MainViewOnSetRefractiveIndexes()
        {
            _cameraManager.SetRefractiveIndexes();
        }

        /// <summary>
        /// Sets thickness mode to camera.
        /// </summary>
        /// <param name="thickness">Thickness mode on off</param>
        private void MainViewOnSetThicknessMode(bool thickness)
        {
            _cameraManager.SetThicknessMode(thickness);
        }

        /// <summary>
        /// Load camera parameters recipe
        /// </summary>
        /// <param name="recipe">Recipe file name</param>
        private void MainViewOnLoadRecipe(string recipe)
        {
            _cameraManager.LoadRecipe(recipe);

            _mainView.LoadSensorParameters(_cameraManager.IsAgcSupported);
        }

        /// <summary>
        /// Save camera parameters recipe
        /// </summary>
        /// <param name="recipe">Recipe file name</param>
        private void MainViewOnSaveRecipe(string recipe)
        {
            _cameraManager.SaveRecipe(recipe);
            _applicationSettings.LastRecipe = recipe;
        }
    }
}