// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMainView.cs" company="FocalSpec Ltd">
// FocalSpec Ltd 2015-
// </copyright>
// <summary>
// Interface definition for the main view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using FocalSpec.GuiExample.Model;
using FocalSpec.GuiExample.Model.BatchMode;
using FocalSpec.GuiExample.Model.Camera;
using FocalSpec.GuiExample.Model.Export;

namespace FocalSpec.GuiExample.View
{
    /// <summary>
    /// Delegate definition for applying sensor settings.
    /// </summary>
    /// <param name="ledPulseWidth">LED pulse width [µs].</param>
    /// <param name="maxLedPulseWidth">Max. LED pulse width [µs] used by the Automatic Gain Control if supported by the HW.</param>
    /// <param name="freq">The frequency [Hz].</param>
    /// <param name="isExternalPulsingEnabled">If true, then parameter freq. is only the intended target frequency.</param>
    /// <param name="enableAgc">If true, AGC enable is requested.</param>
    /// <param name="agcTargetIntensity">AGC target intensity</param>
    /// <param name="firLength">Fir length</param>
    /// <param name="averFirLength">Averaging fir length</param>
    /// <param name="threshold">Peak core threshold</param>
    /// <param name="hdr">HDR enabled/disabled</param>
    /// <param name="vLow2">vLow2</param>
    /// <param name="vLow3">vLow3</param>
    /// <param name="kp1Pos">kp1Pos</param>
    /// <param name="kp2Pos">kp2Pos</param>
    /// <param name="minThickness">Minimum thickness</param>
    /// <param name="materialType">Material type</param>
    /// <param name="detectionSensitivity">Detection sensitivity</param>
    public delegate void ApplySensorSettingsHandler(float ledPulseWidth, int? maxLedPulseWidth, int freq, bool isExternalPulsingEnabled, 
        bool enableAgc, float? agcTargetIntensity, ref int firLength, ref int averFirLength, ref int detection, ref int averageIntensity, ref int threshold, 
        bool hdr, float vLow2, float vLow3, float kp1Pos, float kp2Pos, float minThickness, int materialType, int detectionSensitivity);

    public delegate void ApplyFilterSettingsHandler(ref int firLength, ref int averFirLength, ref int detection, ref int averageIntensity, ref int threshold
        , float minThickness, int materialType, int detectionSensitivity);

    /// <summary>
    /// Delegate definition for applying layer specific settings.
    /// </summary>
    public delegate void SetLayerSpecificSettingsHandler();

    /// <summary>
    /// Delegate definition for applying UI settings.
    /// </summary>
    /// <param name="profileWindowHeight">Profile window height [mm].</param>
    public delegate void ApplyUiSettingsHandler(int profileWindowHeight);

    /// <summary>
    /// Delegate definition for applying UI settings.
    /// </summary>
    /// <param name="layerMinThickness">Layer min thickness</param>
    public delegate void ApplyPeakDetectionSettingsHandler(float layerMinThickness);

    /// <summary>
    /// Handler definition for exporting data
    /// .</summary>
    /// <param name="file">The filename and path for the target.</param>
    /// <param name="layer">Selected layer.</param>
    public delegate void ExportDataRequestedHandler(string file, int layer);

    public delegate void ProfileLayerSelectedHandler(ExportLayer layer, int index);

    public delegate void ShowRawImageHandler(bool rawImageMode);

    public delegate void ExportRawImageHandler(string file, System.Drawing.Image image);

    public delegate void RequestCalibrationFilesHandler();

    public delegate void FilterHandler(bool noiseRemoval, double averageZ = -1, double averageIntensity = -1, int medianZ = -1, int medianIntensity = -1, 
        double reSample = -1, int peakXFilter = -1, double fillGapMax = -1, bool trimEdges = false);

    public delegate void RefractiveIndexHandler();

    public delegate void ThicknessModeHandler(bool thickness);

    public delegate void LoadRecipeHandler(string recipe);

    public delegate void SaveRecipeHandler(string recipe);

    /// <summary>
    /// Handler, called when the window is closing.
    /// </summary>
    public delegate void WindowClosingHandler();

    /// <summary>
    /// Handler, called when the window is shown for the first time.
    /// </summary>
    public delegate void WindowShownHandler();

    public class ProfileInfo
    {
        public int Layer;
        public Profile Profile;
        public ProfileInfo(int layer, Profile profile) {Layer = layer; Profile = profile;}
    }

    /// <summary>
    /// Interface definition for the main view.
    /// </summary>
    public interface IMainView
    {
        /// <summary>
        /// Event triggers when user wants to change sensor settings.
        /// </summary>
        event ApplySensorSettingsHandler OnApplySensorSettings;

        event ApplyFilterSettingsHandler OnApplyFilterSettings;

        /// <summary>
        /// Event triggers when user wants to change sensor settings.
        /// </summary>
        event SetLayerSpecificSettingsHandler OnSetLayerSpecificSettings;

        /// <summary>
        /// Event triggers when user wants to change UI settings.
        /// </summary>
        event ApplyUiSettingsHandler OnApplyUiSettings;

        /// <summary>
        /// Event triggers when user wants to export profile data as CSV.
        /// </summary>
        event ExportDataRequestedHandler OnExportProfileCsv;

        event ProfileLayerSelectedHandler OnProfileLayerSelected;

        event ShowRawImageHandler OnShowRawImage;

        event ExportRawImageHandler OnExportRawImage;

        event RequestCalibrationFilesHandler OnRequestCalibrationFiles;

        event FilterHandler OnFilter;

        event RefractiveIndexHandler OnSetRefractiveIndexes;

        event ThicknessModeHandler OnSetThicknessMode;

        event LoadRecipeHandler OnLoadRecipe;

        event SaveRecipeHandler OnSaveRecipe;

        /// <summary>
        /// Event triggers when user is closing window.
        /// </summary>
        event WindowClosingHandler OnWindowClosing;

        /// <summary>
        /// Event triggers when the window is shown for the first time.
        /// </summary>
        event WindowShownHandler OnWindowShown;

        /// <summary>
        /// Gets the batch mode control instance.
        /// </summary>
        IBatchModeView BatchView { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the batch visualizer is visible.
        /// </summary>
        bool IsBatchVisualizerVisible { get; set; }

        /// <summary>
        /// Sets the current LED pulse width in µs. This is used also when Automatic Gain Control (AGC) is active.
        /// </summary>
        float CurrentLedPulseWidth { set; }

        /// <summary>
        /// Gets or sets a value indicating whether the camera is High Speed
        /// </summary>
        bool IsHsCamera { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Peak X Filter is supported.
        /// </summary>
        bool IsXFilterSupported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the HDR is supported.
        /// </summary>
        bool IsHdrSupported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the signal detection filter is supported.
        /// </summary>
        bool IsSignalDetectionFilterSupported { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Layer Specific Parameters are supported.
        /// </summary>
        bool LayerIntensityTypeSupported { get; set; }

        /// <summary>
        /// Minimum layer id.
        /// </summary>
        int MinLayerId { get; }

        /// <summary>
        /// Fills in the UI controls from settings.
        /// </summary>
        /// <param name="settings">Settings to read values from.</param>
        void LoadSettings(ApplicationSettings settings);

        /// <summary>
        /// Fills in the UI controls with sensor parameters.
        /// </summary>
        /// <param name="isAgcSupported">Is AGC supported by the current hardware.</param>
        void LoadSensorParameters(bool isAgcSupported);

        /// <summary>
        /// Select recipe for UI.
        /// </summary>
        /// <param name="lastRecipe">Last used recipe as a default value for selection</param>
        void SelectRecipe(string lastRecipe = null);

        /// <summary>
        /// Sets the target intensity range.
        /// </summary>
        /// <param name="min">The min. value for the target intensity of the profile.</param>
        /// <param name="max">The max. value for the target intensity of the profile.</param>
        void SetTargetIntensityLimits(float min, float max);

        /// <summary>
        /// Sets the profile.
        /// </summary>
        /// <param name="profiles">Profile to show.</param>
        void SetProfile(List<ProfileInfo> profiles);

        /// <summary>
        /// Sets the raw image.
        /// </summary>
        /// <param name="rawImage">Raw image to show.</param>
        void SetRawImage(RawImage rawImage);

        /// <summary>
        /// Sets the window title.
        /// </summary>
        /// <param name="title">The window title. </param>
        void SetTitle(string title);

        /// <summary>
        /// Displays a message.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="caption">Message caption.</param>
        void ShowMessage(string message, string caption);

        /// <summary>
        /// Ask (yes/no) from user.
        /// </summary>
        /// <param name="message">Message text.</param>
        /// <param name="caption">Message caption.</param>
        /// <returns>true if user selected yes, false if no.</returns>
        bool Ask(string message, string caption);

        /// <summary>
        /// Closes the view.
        /// </summary>
        void Close();

        /// <summary>
        /// Shows RecordContainer in BatchVisualizer2DUc.
        /// </summary>
        /// <param name="recordContainer">RecordContainer object.</param>
        /// <param name="batchConf">BatchConfiguration object.</param>
        /// <param name="averagePixelWidth">Average pixel width [mm].</param>
        void ShowRecording(RecordContainer recordContainer, BatchConfiguration batchConf, double averagePixelWidth);

        /// <summary>
        /// Clears image from BatchVisualizer2DUc.
        /// </summary>
        void ClearBatchVisualizer();

        /// <summary>
        /// Enables/disables AveragingFir selection
        /// </summary>
        void EnableAveragingFir(bool enable);

        /// <summary>
        /// Informs UI of ViewMode change
        /// </summary>
        void ViewModeChanged(ViewMode mode);
    }
}