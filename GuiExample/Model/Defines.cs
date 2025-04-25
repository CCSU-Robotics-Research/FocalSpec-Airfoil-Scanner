// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CameraManager.cs" company="FocalSpec Ltd">
// FocalSpec Ltd 2016-
// </copyright>
// <summary>
// General constant defines.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
 
namespace FocalSpec.GuiExample.Model
{
    /// <summary>        
    /// Application specific constants.
    /// </summary>
    class Defines
    {  
        /// <summary>
        /// The width of the FocalSpec sensor in px.
        /// </summary>
        public const int DefaultSensorWidth = 2048;

        /// <summary>
        /// Max. FocalSpec sensor height [px].
        /// </summary>
        public const int MaxSensorHeight = 1088;

	    /// <summary>
	    /// Max. FocalSpec sensor height [px].
	    /// </summary>
	    public const int MaxHsSensorHeight = 1400;
	
        /// <summary> 
        /// Default LED pulse width in µs.
        /// </summary>
        public const int DefaultPulseWidth = 20;

        /// <summary>
        /// Default max. LED pulse width in µs used by the Automatic Gain Control (AGC) if supported by the HW.
        /// </summary>
        public const int DefaultMaxPulseWidth = 100;

        /// <summary>
        /// UI update interval in ms.
        /// </summary>
        public const int UiUpdateInterval = 200;

        /// <summary>
        /// The maximum number of point clouds in the application reception queue.
        /// </summary>
        public const int QueueMaxLength = 30000;

        /// <summary>
        /// Maximum UI window height in mm.
        /// </summary>
        public const int MinUiWindowSize = 1;

        /// <summary>
        /// Minimum UI window height in mm.
        /// </summary>
        public const int MaxUiWindowSize = 20;

        /// <summary>
        /// Default UI window height in mm.
        /// </summary>
        public const int DefaultUiWindowSize = 3;

        /// <summary>
        /// Default pulse divider for external triggering pulse.
        /// </summary>
        public const int DefaultPulseDivider = 1;


        /// <summary>
        /// Default Jumbo Frame packet size.
        /// </summary>
        public const int DefaultJumboFrameMtu = 9014;

        /// <summary>
        /// Default triggering source for external trigger.
        /// </summary>
        public const int DefaultTriggerSource = 1;

        /// <summary>
        /// Default triggering source disabled.
        /// </summary>
        public const int DefaultTriggerDisableSource = 0;

        /// <summary>
        /// /// Default state of Image Height Z-calibarated Zero Position adjust.
        /// </summary>
        public const bool HeightZeroAdjustState = true;

        /// <summary>
        /// Default sensor trigger frequency in Hz.
        /// </summary>
        public const int DefaultTriggerFrequency = 300;

        /// <summary>
        /// Default FIR Length.
        /// </summary>
        public const int DefaultFirLength = 16;

        /// <summary>
        /// Default Average FIR Length.
        /// </summary>
        public const int DefaultAverFirLength = 16;

        /// <summary>
        /// Default Detection Filter Length.
        /// </summary>
        public const int DefaultDetectionFilter = 16;

        /// <summary>
        /// Default Average Intensity Filter Length.
        /// </summary>
        public const int DefaultAverageIntensityFilter = 16;

        /// <summary>
        /// Default Intensity Threshold.
        /// </summary>
        public const int DefaultThreshold = 24;

	    /// <summary>
	    /// Default Maximum point count per profile.
	    /// </summary>
	    public const int DefaultMaxPointCount = 20000;

        /// <summary>
        /// Default point cloud ASC file additional attributes on batch save output format.
        /// 0: x, y, z coordinates, no additional attribute.
        /// 1: x, y, z coordinates, + 4th is intensity in float format.
        /// 2: x, y, z coordinates, + 4th is gray scale value in RGB format.
        /// </summary>
        public const int DefaultPointCloudAscFormat = 0;
        
        /// <summary>
        /// Sensor trigger frequency value when using external triggering.
        /// </summary>
        public const int ExternalTriggering = 0;

        /// <summary>
        /// Path to the application data folder.
        /// </summary>
        public const string FsAppDataDir = @"\FocalSpec\\GuiExample\\";

        public const string RecipeFolder = "C:\\FocalSpec\\SDK Recipes\\";

        /// <summary>
        /// Name of the application settings file.
        /// </summary>
        public const string ApplicationSettingsFile = "GuiExampleSettings.json";

        /// <summary>
        /// Micrometer unit.
        /// </summary>
        public const int PeakUnitMicrometer = 1;

        /// <summary>
        /// Sensor discovery timeout in ms.
        /// </summary>
        public const int SensorDiscoveryTimeout = 2000;

        /// <summary>
        /// Default IPv4 address of the sensor.
        /// </summary>
        public const string DefaultIpAddress = null;

        /// <summary>
        /// Default Z calibration file.
        /// </summary>
        public const string DefaultZCalibrationFile = null;

        /// <summary>
        /// Default X calibration file.
        /// </summary>
        public const string DefaultXCalibrationFile = null;

        /// <summary>
        /// File externsion for the calibration data files.
        /// </summary>
        public const string CalibrationFileExt = ".dat";

        /// <summary>
        /// Scale to convert profile data from [µm] to [mm].
        /// </summary>
        public const double ProfileScale = 1.0 / 1000.0;

        /// <summary>
        /// Min. target intensity for good profile signal. This depends on the target.
        /// </summary>
        public const float TargetIntensityMin = 70.0f;

        /// <summary>
        /// Max. target intensity for good profile signal. This depends on the target.
        /// </summary>
        public const float TargetIntensityMax = 90.0f;

        /// <summary>
        /// AGC target grayscale intensity.
        /// </summary>
        public const float DefaultAgcTargetIntensity = 80.0f;

        /// <summary>
        /// AGC target intensity margin for UI controls.
        /// </summary>
        public const float AgcMargin = 10.0f;

        /// <summary>
        /// Default number of profile in a batch.
        /// </summary>
        public const int DefaultBatchLength = 5000;

        /// <summary>
        /// Default sensor trigger mode in batch.
        /// </summary>
        public const TriggerMode DefaultBatchTriggerMode = TriggerMode.External;

        /// <summary>
        /// Default line speed [m/min] in batch mode when internal sensor triggering is used.
        /// </summary>
        public const double DefaultLineSpeedInBatch = 1.0;

        /// <summary>
        /// Default scan step length [mm] in batch mode when external sensor triggering is used.
        /// </summary>
        public const double DefaultScanStepLength = 0.5;

        /// <summary>
        /// Default AGC state.
        /// </summary>
        public const bool DefaultAgcState = false;

        public static double DefaultCurrent { get; set; } = 1.0;
        public static double DefaultGain { get; set; } = 1.0;
        public static double DefaultGainHs { get; set; } = 2.0;

        public const int DefaultSaveRate = 1;

        public const bool DefaultHdrEnabled = false;
        public const float DefaultHdrKp1Pos = 95;
        public const float DefaultHdrVLow2 = 33;

        public const int DefaultExpectedCameraCount = 1;
    }
}
