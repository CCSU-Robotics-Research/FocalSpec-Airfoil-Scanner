// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainView.cs" company="FocalSpec Ltd">
// FocalSpec Ltd 2015
// </copyright>
// <summary>
// Implementation of the MainView using WinForms.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Threading;
using ABB.Robotics.Controllers;
using Adapters;
using FocalSpec.FsApiNet.Model;
using FocalSpec.GuiExample.Annotations;
using FocalSpec.GuiExample.Model;
using FocalSpec.GuiExample.Model.BatchMode;
using FocalSpec.GuiExample.Model.Camera;
using FocalSpec.GuiExample.Model.Export;
using Rapid;
using RobotStudio.Services.RobApi.Transport.RobAPI1Direct;
using Cursor = System.Windows.Forms.Cursor;

namespace FocalSpec.GuiExample.View
{
    /// <summary>
    /// Code-behind of the main view.
    /// </summary>
    public partial class MainView : Form, IMainView
    {
        /// <summary>
        /// Raw image panel - created dynamically
        /// </summary>
        private ElementHost _elementHostRawImage;
        private RawImageViewer _rawImageViewer;

        /// <summary>
        /// Sensor parameters.
        /// </summary>
        private readonly SensorParameterStore _parameters;

        /// <summary>
        /// Point cloud to show.
        /// </summary>
        private readonly List<Profile> _profiles = new List<Profile>();

        /// <summary>
        /// True when the dialog is about to close.
        /// </summary>
        private bool _isClosing;

        /// <summary>
        /// Min. possible X in the optical profile [mm]
        /// </summary>
        private double _xMin;

        /// <summary>
        /// Max. possible X in the optical profile [mm]
        /// </summary>
        private double _xMax;

	    /// <summary>
	    /// Calibrated profile width (x-axis) in pixels.
	    /// </summary>
	    private int _xWidth;

        /// <summary>
        /// The min. value for the target intensity of the profile.
        /// </summary>
        private float _targetIntensityMin;

        /// <summary>
        /// The max. value for the target intensity of the profile.
        /// </summary>
        private float _targetIntensityMax;

        /// <summary>
        /// Flag set to true if a user is about to modify LED pulse width.
        /// </summary>
        private bool _ledPulseWidthHasFocus;

        private bool _formLoaded;
        private bool _isBatchVisualizerVisible;
        private bool _isRawImageVisible;
        private bool _isThicknessVisible;
        private bool _isAgcSupported;
        private bool _firstProfile = true;
        private int _panel1OriginalWidth;
        private ViewMode _currentViewMode = ViewMode.RealTime;

        private double _profileCursorXPosition, _profileCursorYPosition, _thicknessCursorXPosition, _thicknessCursorYPosition;
        private Point _profileCursorPixelPoint, _thicknessCursorPixelPoint;
        private bool _resetProfileCursor, _resetThicknessCursor;

        private int _seriesCount;
        private ExportLayer _selectedLayer;
        private int _selectedLayerIndex;

        private readonly object _chartLock = new object();
        private readonly object _profileLock = new object();
        private readonly Dispatcher _uiDispatcher;
        private readonly object _thicknessLock = new object();

        private readonly System.Timers.Timer _resizeTimer = new System.Timers.Timer { Interval = 1000 };
        private readonly System.Timers.Timer _pollTimer = new System.Timers.Timer { Interval = 100 };
        private readonly System.Timers.Timer _layerSelectionTimer = new System.Timers.Timer { Interval = 5 };
        private FsApi.Header _lastHeader;
        private string _selectedRecipe;
        private FormWindowState _lastWindowState;

        private readonly AdvancedView _advancedView;
        private SensorSettingsView _sensorSettingsView;
        private FilterView _filterView;
        private readonly RefractionView _refractionView;
        private readonly PeakDetectionView _peakDetectionView;
        private readonly RecipeSelectView _loadRecipeView;

        public bool IsHsCamera { get; set; }
        public bool IsXFilterSupported { get; set; }
        public bool IsHdrSupported { get; set; }
        public bool IsSignalDetectionFilterSupported { get; set; }
        public bool LayerIntensityTypeSupported { get; set; }

        private RapidFunctions rapidFunctions;

        /// <summary>
        /// Prepares the view for displaying measurements.
        /// </summary>
        public MainView()
        {
            _sensorSettingsView = new SensorSettingsView(this);
            var _ = _sensorSettingsView.Handle;

            InitializeComponent();

            rapidFunctions = new RapidFunctions(this);

			_uiDispatcher = Dispatcher.CurrentDispatcher;
            _parameters = SensorParameterStore.GetInstance();

            _sensorSettingsView.radioButtonExportAll.Tag = ExportLayer.All;
            _sensorSettingsView.radioButtonExportTop.Tag = ExportLayer.Top;
            _sensorSettingsView.radioButtonExportBottom.Tag = ExportLayer.Bottom;
            _sensorSettingsView.radioButtonExportBrightest.Tag = ExportLayer.BrightestAndTop;

            _sensorSettingsView.comboboxLedPulseWidth.SelectedIndex = 31;
            _sensorSettingsView.comboboxFrequency.SelectedIndex = 4;
            _sensorSettingsView.comboBoxTop.SelectedIndex = _sensorSettingsView.comboBoxBottom.SelectedIndex = _sensorSettingsView.comboBoxBrightest.SelectedIndex = 0;

            WindowState = FormWindowState.Maximized;
            _lastWindowState = WindowState;

            _profileChart.Titles.Add("Layer Profile");
            _profileChart.Titles[0].Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _profileChart.Series["profile0"].YAxisType = AxisType.Primary;
            _profileChart.Series["intensity0"].YAxisType = AxisType.Secondary;
            _profileChart.Series["intensity0"].Color = Color.DarkRed;
            _seriesCount = 1;

            _profileChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            _profileChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            _profileChart.ChartAreas[0].CursorX.Interval = 0;
            _profileChart.ChartAreas[0].CursorX.SetCursorPosition(double.NaN);
            _profileChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            _profileChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            _profileChart.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            _profileChart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            _profileChart.ChartAreas[0].AxisX.IsMarksNextToAxis = false;
            _profileChart.ChartAreas[0].AxisX.IsStartedFromZero = true;

            _profileChart.ChartAreas[0].CursorY.IsUserEnabled = true;
            _profileChart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            _profileChart.ChartAreas[0].CursorY.Interval = 0;
            _profileChart.ChartAreas[0].CursorY.SetCursorPosition(double.NaN);
            _profileChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            _profileChart.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            _profileChart.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            _profileChart.ChartAreas[0].AxisY.ScrollBar.Enabled = true;

            // Set nm accuracy for the axis labels.
            _profileChart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.000000}";
            _profileChart.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.000000}";

            // Right axis values for intensity 
            _profileChart.ChartAreas[0].AxisY2.Minimum = 0;
            _profileChart.ChartAreas[0].AxisY2.Maximum = 255;
            _profileChart.ChartAreas[0].AxisY2.Title = "Intensity";
            _profileChart.ChartAreas[0].AxisY2.ScaleView.Zoomable = true;
            _profileChart.ChartAreas[0].AxisY2.ScrollBar.IsPositionedInside = true;
            _profileChart.ChartAreas[0].AxisY2.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            _profileChart.ChartAreas[0].AxisY2.IsInterlaced = false;
            _profileChart.ChartAreas[0].AxisY2.MajorGrid.Enabled = false;
            _profileChart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;

            _thicknessChart.Titles.Add("Thickness");
            _thicknessChart.Titles[0].Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            _thicknessChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            _thicknessChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            _thicknessChart.ChartAreas[0].CursorX.Interval = 0;
            _thicknessChart.ChartAreas[0].CursorX.SetCursorPosition(double.NaN);
            _thicknessChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            _thicknessChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            _thicknessChart.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            _thicknessChart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;
            _thicknessChart.ChartAreas[0].CursorY.IsUserEnabled = true;
            _thicknessChart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            _thicknessChart.ChartAreas[0].CursorY.Interval = 0;
            _thicknessChart.ChartAreas[0].CursorY.SetCursorPosition(double.NaN);
            _thicknessChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            _thicknessChart.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;
            _thicknessChart.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = ScrollBarButtonStyles.ResetZoom;
            _thicknessChart.ChartAreas[0].AxisY.ScrollBar.Enabled = true;
            _thicknessChart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;
            _thicknessChart.Legends.Add(new Legend("ThicknessLegend"));
            _thicknessChart.Legends[0].IsDockedInsideChartArea = false;
            _thicknessChart.Series[0].Legend = "ThicknessLegend";
            _thicknessChart.Series[0].IsVisibleInLegend = true;
            _thicknessChart.Series[0].YAxisType = AxisType.Primary;
            _thicknessChart.Series[0].Color = GetLayerColor(1);
            _thicknessChart.Visible = false;

            panelSettings.HorizontalScroll.Maximum = 0;
            panelSettings.AutoScroll = false;
            panelSettings.VerticalScroll.Visible = false;
            panelSettings.AutoScroll = true;
            _panel1OriginalWidth = 0;

            _sensorSettingsView.comboBoxMaterialType.SelectedIndex = 1;
            _sensorSettingsView.comboBoxSensitivity.SelectedIndex = 1;

            _resizeTimer.AutoReset = true;
            _resizeTimer.SynchronizingObject = this;
            _resizeTimer.Elapsed += ResizeTimer_Elapsed;

            _advancedView = new AdvancedView();
            _refractionView = new RefractionView();
            _peakDetectionView = new PeakDetectionView();
            _loadRecipeView = new RecipeSelectView();
            _filterView = new FilterView();

            EnableSurfaceSelections();
            IsThicknessEnabled = false;

            HandleCreated += MainView_HandleCreated;

            Load += OnLoad;

            _pollTimer.Enabled = true;
            _pollTimer.SynchronizingObject = this;
            _pollTimer.Elapsed += OnPollProfiles;

            _layerSelectionTimer.SynchronizingObject = this;
            _layerSelectionTimer.Elapsed += OnLayerSelection;
        }

        private void OnLoad(object sender, EventArgs eventArgs)
        {
            _formLoaded = true;

            _sensorSettingsView.checkBoxRawImage.Checked = false;
            _filterView.PeakXFilterEnabled = IsXFilterSupported;

            _peakDetectionView.IsPeakDetection = IsSignalDetectionFilterSupported;

            FitScrollbar();

            _sensorSettingsView.buttonAdvanced.Enabled = IsHdrSupported || LayerIntensityTypeSupported;

            _sensorSettingsView.radioButtonExportTop.Checked = true;

            // Top is selected by default.
            _selectedLayer = ExportLayer.Top;
            _selectedLayerIndex = 2;
            OnProfileLayerSelected?.Invoke(_selectedLayer, _selectedLayerIndex);

            Cursor.Current = Cursors.Default;
        }

        private void MainView_HandleCreated(object sender, EventArgs e)
        {
            IsBatchVisualizerVisible = false;
        }
        public BatchModePresenter getBatchMode()
        {
            return _sensorSettingsView._batchMode;
        }
        public ExportLayer getSelectedLayer()
        {
            return _selectedLayer;
        }
        public void setSelectedLayer(ExportLayer newExportLayer)
        {
            _selectedLayer = newExportLayer;
        }

        public event ApplySensorSettingsHandler OnApplySensorSettings;

        public event ApplyFilterSettingsHandler OnApplyFilterSettings;

        public event SetLayerSpecificSettingsHandler OnSetLayerSpecificSettings;

        public event ApplyUiSettingsHandler OnApplyUiSettings;

        public event ExportDataRequestedHandler OnExportProfileCsv;

        public event ProfileLayerSelectedHandler OnProfileLayerSelected;

        public event WindowClosingHandler OnWindowClosing;

        public event WindowShownHandler OnWindowShown;

        public event ShowRawImageHandler OnShowRawImage;

        public event ExportRawImageHandler OnExportRawImage;

        public event RequestCalibrationFilesHandler OnRequestCalibrationFiles;

        public event FilterHandler OnFilter;

        public event RefractiveIndexHandler OnSetRefractiveIndexes;

        public event ThicknessModeHandler OnSetThicknessMode;

        public event LoadRecipeHandler OnLoadRecipe;

        public event SaveRecipeHandler OnSaveRecipe;

        public void SelectRecipe(string lastRecipe = null)
        {
            if (Session.ViewMode != ViewMode.RealTime) return;

            _loadRecipeView.SelectedRecipe = lastRecipe ?? _selectedRecipe;
            _loadRecipeView.SensorType = _parameters.SensorType;
            if (_loadRecipeView.ShowDialog(this) == DialogResult.Cancel) return;
            _selectedRecipe = _loadRecipeView.SelectedRecipe;

            _sensorSettingsView.comboBoxMaterialType.SelectedIndex = 1;
            _sensorSettingsView.comboBoxSensitivity.SelectedIndex = 1;

            _refractionView.Reset();
            
            OnLoadRecipe?.Invoke(_selectedRecipe);
        }

        public void SetTargetIntensityLimits(float min, float max)
        {
            _targetIntensityMin = min;
            _targetIntensityMax = max;
        }

        public void SetProfile(List<ProfileInfo> profiles)
        {
            if (!ControlIsAvailable()) return;

            lock (_profileLock)
            {
                _profiles.Clear();
                if (_selectedLayer == ExportLayer.All)
                {
                    _profiles.Add(profiles[0].Profile);
                }
                else
                {
                    foreach (var profile in profiles)
                    {
                        if (profile.Profile == null) break;
                        if (profile.Layer >= 0)
                            _profiles.Add(profile.Profile);
                    }
                }
            }
        }

        private void OnPollProfiles(Object myObject, EventArgs myEventArgs)
        {
            if (!ControlIsAvailable()) return;

            _pollTimer.Enabled = false;
            if (IsRawImageVisible)
            {
                FsApi.Header header;
                lock (_profileLock)
                {
                    header = _lastHeader;
                }
                UpdateLabelFrameIndex(header);
            }
            else
            {
                var profiles = new List<Profile>();
                lock (_profileLock)
                {
                    foreach (var profile in _profiles)
                        profiles.Add(profile);
                }
                if (profiles.Count > 0)
                {
                    ShowProfile(profiles);
                }
            }

            // CPU load get heavy on multiple layers if thickness is visible
            _pollTimer.Interval = _selectedLayerIndex > 0 && _sensorSettingsView.checkBoxThickness.Checked ? (_selectedLayerIndex + 1) * 100 : 100;
            _pollTimer.Enabled = true;
        }

        private void OnLayerSelection(Object myObject, EventArgs myEventArgs)
        {
            _layerSelectionTimer.Stop();
            if (!ControlIsAvailable()) return;

            Cursor.Current = Cursors.WaitCursor;
            if (_currentViewMode == ViewMode.RealTime)
            {
                IsThicknessEnabled = _selectedLayerIndex > 0;
                IsThicknessVisible = _sensorSettingsView.checkBoxThickness.Checked && _selectedLayerIndex > 0;
                SetThicknessTitle();
            }

            _sensorSettingsView.groupboxSurface.Invalidate();
            OnProfileLayerSelected?.Invoke(_selectedLayer, _selectedLayerIndex);
            Cursor.Current = Cursors.Default;
        }

        private void ShowProfile(List<Profile> profiles)
        {
            ResetSeries();

            if (!ControlIsAvailable()) return;

            _profileChart.BeginInit();
            if (profiles.Count > _seriesCount)
            {
                for (int i = _seriesCount; i < profiles.Count; i++)
                {
                    var profileSeries = new Series
                    {
                        ChartArea = "ChartArea1",
                        ChartType = SeriesChartType.FastPoint,
                        MarkerSize = 2,
                        Name = "profile" + i,
                        Color = GetLayerColor(i)
                    };
                    var intensitySeries = new Series
                    {
                        ChartArea = "ChartArea1",
                        ChartType = SeriesChartType.FastPoint,
                        MarkerSize = 2,
                        Name = "intensity" + i
                    };

                    lock (_chartLock)
                    {
                        _profileChart.Series.Add(profileSeries);
                        _profileChart.Series["profile" + i].YAxisType = AxisType.Primary;
                        _profileChart.Series.Add(intensitySeries);
                        _profileChart.Series["intensity" + i].YAxisType = AxisType.Secondary;
                    }
                }

                _seriesCount = profiles.Count;
            }

            double profileScale = 1;

            if (_sensorSettingsView.radioButtonGraphUnitUm.Checked)
            {
                _profileChart.ChartAreas[0].AxisX.Minimum = _xMin * 1000;
                _profileChart.ChartAreas[0].AxisX.Maximum = _xMax * 1000;
            }
            else
            {
                profileScale = Defines.ProfileScale;
                _profileChart.ChartAreas[0].AxisX.Minimum = _xMin;
                _profileChart.ChartAreas[0].AxisX.Maximum = _xMax;
            }

            int maxLayer = _sensorSettingsView.checkBoxThickness.Checked ? 0 : _selectedLayerIndex >= 0 ? _selectedLayerIndex : _parameters.Layers.Length;
            double newMin = 0.0;
            lock (_chartLock)
            {
                if (!ControlIsAvailable()) return;
                for (int layerIndex = 0; layerIndex <= maxLayer && layerIndex < profiles.Count; layerIndex++)
                {
                    var profile = profiles[layerIndex];
                    if (profile == null) continue;
                    if (profile.LayerId < 0)
                    {
                        foreach (var point in profile.Points)
                        {
                            _profileChart.Series["profile" + layerIndex].Points.AddXY(point.X * profileScale, point.Y * profileScale);
                            newMin = Math.Min(newMin, point.X);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < profile.LineLength; ++i)
                        {
                            var x = (float) (i * profile.XStep);
                            _profileChart.Series["profile" + layerIndex].Points.AddXY(x * profileScale, profile.ZValues[i] * profileScale);
                            newMin = Math.Min(newMin, x);
                        }
                    }

                    if (!_sensorSettingsView.checkBoxIntensity.Checked) continue;
                    if (profile.LayerId < 0)
                    {
                        foreach (var point in profile.Points)
                        {
                            _profileChart.Series["intensity" + layerIndex].Points.AddXY(point.X * profileScale, point.Intensity);
                        }
                    }
                    else if (Session.ViewMode == ViewMode.RealTime)
                    {
                        for (int i = 0; i < profile.LineLength; ++i)
                        {
                            _profileChart.Series["intensity" + layerIndex].Points.AddXY((float)(i * profile.XStep) * profileScale, profile.IntensityValues[i]);
                        }
                    }
                }
            }
            _profileChart.EndInit();

            if (_sensorSettingsView.checkBoxThickness.Checked)
                ShowThickness(profiles, profileScale);

            // scaling of x-axis minimum done based on received profile data
            _xMin = Math.Round(newMin / 1000 - 0.05, 1);

            bool signal = true;
            float averageIntensity = 0;
            int count = 0;
            for (int layerIndex = 0; layerIndex <= maxLayer && layerIndex < profiles.Count; layerIndex++)
            {
                var profile = profiles[layerIndex];
                if (profile == null) continue;
                if (profile.LayerId < 0)
                {
                    signal &= profile.Points.Any();
                    averageIntensity = profile.Points.Count * profile.AverageIntensity; // Weighted average
                    count += profile.Points.Count;
                }
                else
                {
                    signal &= profile.LineLength > 0;
                    double sumIntensity = 0;
                    int validValues = 0;
                    foreach (var intensity in profile.IntensityValues)
                    {
                        if (intensity >= 0 && intensity < 256)
                        {
                            sumIntensity += intensity;
                            ++validValues;
                        }
                    }
                    averageIntensity += (float)sumIntensity;
                    count += validValues;
                }
            }

            if (count > 0)
            {
                averageIntensity /= count;
            }
            else
            {
                signal = false;
            }
          
            if (!signal)
            {
                _sensorSettingsView.textBoxAverageIntensity.BackColor = Color.LightCoral;
                // ReSharper disable once LocalizableElement
                _sensorSettingsView.textBoxAverageIntensity.Text = "No signal";
            }
            else
            {
                if (averageIntensity < _targetIntensityMin)
                {
                    _sensorSettingsView.textBoxAverageIntensity.BackColor = Color.LightCoral;
                    _sensorSettingsView.textBoxAverageIntensity.Text = string.Format("{0:0} (too low)", averageIntensity);
                }
                else if (averageIntensity > _targetIntensityMax)
                {
                    _sensorSettingsView.textBoxAverageIntensity.BackColor = Color.LightCoral;
                    _sensorSettingsView.textBoxAverageIntensity.Text = string.Format("{0:0} (too high)", averageIntensity);
                }
                else
                {
                    _sensorSettingsView.textBoxAverageIntensity.BackColor = Color.LightGreen;
                    _sensorSettingsView.textBoxAverageIntensity.Text = string.Format("{0:0} (OK)", averageIntensity);
                }
            }

            int maxIndex = -1, maxFrame = -1;
            for (int layerIndex = 0; layerIndex <= maxLayer && layerIndex < profiles.Count; layerIndex++)
            {
                if (profiles[layerIndex] == null) continue;
                if (profiles[layerIndex].Header.Index > maxFrame)
                {
                    maxIndex = layerIndex;
                    maxFrame = (int)profiles[layerIndex].Header.Index;
                }
            }
            if (maxIndex > -1)
            {
                UpdateLabelFrameIndex(profiles[maxIndex].Header);
            }
            else
                labelFrameIndex.Text = "";

            if (_resetProfileCursor)
                ResetProfileCursorLines();

            if (_firstProfile)
            {
                UpdateGraphScales();
                _firstProfile = false;
            }
        }

        private void ShowThickness(List<Profile> profiles, double profileScale)
        {
            _thicknessChart.BeginInit();
            for (int i = _thicknessChart.Series.Count; i < profiles.Count - 1; i++)
            {
                var thicknessSeries = new Series
                {
                    ChartArea = "ChartArea2",
                    ChartType = SeriesChartType.FastPoint,
                    MarkerSize = 2,
                    Name = "Layer " + (i + 1).ToString(),
                    Color = GetLayerColor(i + 1)
                };
                lock (_thicknessLock)
                {
                    _thicknessChart.Series.Add(thicknessSeries);
                    _thicknessChart.Series[i].YAxisType = AxisType.Primary;
                    _thicknessChart.Series[i].Legend = "ThicknessLegend";
                    _thicknessChart.Series[i].IsVisibleInLegend = true;
                }
            }
            for (int i = 0; i < _thicknessChart.Series.Count; ++i)
                _thicknessChart.Series[i].IsVisibleInLegend = i < _selectedLayerIndex;

            if (_sensorSettingsView.radioButtonGraphUnitUm.Checked)
            {
                _thicknessChart.ChartAreas[0].AxisX.Minimum = _xMin * 1000;
                _thicknessChart.ChartAreas[0].AxisX.Maximum = _xMax * 1000;
            }
            else
            {
                _thicknessChart.ChartAreas[0].AxisX.Minimum = _xMin;
                _thicknessChart.ChartAreas[0].AxisX.Maximum = _xMax;
            }

            lock (_thicknessLock)
            {
                if (!ControlIsAvailable()) return;
                for (int layerIndex = 1; layerIndex <= _selectedLayerIndex && layerIndex < profiles.Count; layerIndex++)
                {
                    var profile = profiles[layerIndex];
                    if (profile == null) continue;
                    if (profile.LayerId < 0)
                    {
                        foreach (var point in profile.Points)
                        {
                            _thicknessChart.Series[layerIndex - 1].Points.AddXY(point.X * profileScale, Math.Abs(point.Y) * profileScale);
                        }
                    }
                    else if (Session.ViewMode == ViewMode.RealTime)
                    {
                        for (int i = 0; i < profile.LineLength; ++i)
                        {
                            _thicknessChart.Series[layerIndex - 1].Points.AddXY((float)(i * profile.XStep) * profileScale, Math.Abs(profile.ZValues[i]) * profileScale);
                        }
                    }
                }
            }

            _thicknessChart.EndInit();

            if (_resetThicknessCursor)
                ResetThicknessCursorLines();
        }

        public void SetRawImage(RawImage rawImage)
        {
            if (!ControlIsAvailable() || !IsRawImageVisible || rawImage == null)
                return;

            lock (_profileLock)
            {
                _lastHeader = rawImage.Header;
            }

            _rawImageViewer.SetImage(rawImage);
        }

        public void EnableAveragingFir(bool enable)
        {
            if (!ControlIsAvailable() || _isRawImageVisible) return;
            _peakDetectionView.EnableAveragingFir(enable);
        }

        public void ViewModeChanged(ViewMode mode)
        {
            Session.ViewMode = mode;
            if (_currentViewMode == mode)
                return;

            if (!ControlIsAvailable())
                return;

            switch (mode)
            {
                case ViewMode.RealTime:
                case ViewMode.RawImage:

                    IsRawImageEnabled = true;
					IsIntensityEnabled = !_isRawImageVisible;
                    IsThicknessEnabled = _selectedLayerIndex > 0 && !_isRawImageVisible && !_sensorSettingsView.radioButtonExportAll.Checked;

                    _sensorSettingsView.comboboxFrequency.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboboxFrequency.Enabled = !_isRawImageVisible; });       
		            _sensorSettingsView.checkBoxExternalPulsing.Invoke((MethodInvoker)delegate{ _sensorSettingsView.checkBoxExternalPulsing.Enabled = !_isRawImageVisible; });
					_sensorSettingsView.radioButtonExportAll.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonExportAll.Enabled = !_isRawImageVisible; });

		            _sensorSettingsView.radioButtonExportTop.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonExportTop.Enabled = !_isRawImageVisible; });
                    _sensorSettingsView.comboBoxTop.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboBoxTop.Enabled = (!_isRawImageVisible && _sensorSettingsView.radioButtonExportTop.Checked); });
					_sensorSettingsView.radioButtonExportBottom.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonExportBottom.Enabled = !_isRawImageVisible; });
                    _sensorSettingsView.comboBoxBottom.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboBoxBottom.Enabled = (!_isRawImageVisible && _sensorSettingsView.radioButtonExportBottom.Checked); });
		            _sensorSettingsView.radioButtonExportBrightest.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonExportBrightest.Enabled = !_isRawImageVisible; });
                    _sensorSettingsView.comboBoxBrightest.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboBoxBrightest.Enabled = (!_isRawImageVisible && _sensorSettingsView.radioButtonExportBrightest.Checked); });
		           
	                _sensorSettingsView.buttonFilter.Invoke((MethodInvoker)delegate{ _sensorSettingsView.buttonFilter.Enabled = !_isRawImageVisible; });			            
		            _sensorSettingsView.checkBoxHeightZeroAdjust.Invoke((MethodInvoker)delegate{ _sensorSettingsView.checkBoxHeightZeroAdjust.Enabled = !_isRawImageVisible; });

                    _sensorSettingsView.comboBoxMaterialType.Invoke((MethodInvoker) delegate{ _sensorSettingsView.comboBoxMaterialType.Enabled = !_isRawImageVisible; });
                    _sensorSettingsView.comboBoxSensitivity.Invoke((MethodInvoker) delegate{ _sensorSettingsView.comboBoxSensitivity.Enabled = !_isRawImageVisible && _sensorSettingsView.comboBoxMaterialType.SelectedIndex != 0; });
                    _sensorSettingsView.textBoxMinThickness.Invoke((MethodInvoker)delegate{ _sensorSettingsView.textBoxMinThickness.Enabled = !_isRawImageVisible; });
                    _sensorSettingsView.buttonPeakDetection.Invoke((MethodInvoker) delegate{ _sensorSettingsView.buttonPeakDetection.Enabled = !_isRawImageVisible; });

                    _sensorSettingsView.radioButtonGraphUnitUm.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonGraphUnitUm.Enabled = !_isRawImageVisible; });
		            _sensorSettingsView.radioButtonGraphUnitMm.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonGraphUnitMm.Enabled = !_isRawImageVisible; });

		            _sensorSettingsView.numericUpDownWindowSize.Invoke((MethodInvoker)delegate{ _sensorSettingsView.numericUpDownWindowSize.Enabled  = !_isRawImageVisible; });
                    
		            _sensorSettingsView.buttonAdvanced.Invoke((MethodInvoker) delegate{ _sensorSettingsView.buttonAdvanced.Enabled = IsHdrSupported || LayerIntensityTypeSupported; });
 
	                _sensorSettingsView.textBoxAverageIntensity.Invoke((MethodInvoker)delegate{ _sensorSettingsView.textBoxAverageIntensity.Enabled = !_isRawImageVisible; });
                    if (_isAgcSupported)
                    {
                        _sensorSettingsView.checkBoxAgcEnabled.Invoke((MethodInvoker)delegate{ _sensorSettingsView.checkBoxAgcEnabled.Enabled = !_isRawImageVisible; });
                        _sensorSettingsView.textBoxMaxPulseWidth.Invoke((MethodInvoker)delegate{ _sensorSettingsView.textBoxMaxPulseWidth.Enabled = (!_isRawImageVisible && _sensorSettingsView.checkBoxAgcEnabled.Checked); });
                        _sensorSettingsView.textBoxAgcTargetIntensity.Invoke((MethodInvoker)delegate{ _sensorSettingsView.textBoxAgcTargetIntensity.Enabled = (!_isRawImageVisible && _sensorSettingsView.checkBoxAgcEnabled.Checked); });
                    }

                    loadRecipeToolStripMenuItem.Enabled = !_isRawImageVisible;
                    saveRecipeToolStripMenuItem.Enabled = !_isRawImageVisible;
                    saveRecipeAsToolStripMenuItem.Enabled = !_isRawImageVisible;

                    break;

                case ViewMode.Batch:
                case ViewMode.Recording:
	                var isRecording = (mode == ViewMode.Recording);

                    if (!(_currentViewMode == ViewMode.Batch || _currentViewMode == ViewMode.Recording))
                    {
                        IsRawImageEnabled = false;
                        IsIntensityEnabled = false;
                        IsThicknessEnabled = false;
                        IsThicknessVisible = false;
                        _sensorSettingsView.checkBoxThickness.Invoke((MethodInvoker)delegate{ _sensorSettingsView.checkBoxThickness.Checked = false; });

                        _sensorSettingsView.checkBoxExternalPulsing.Invoke((MethodInvoker)delegate{ _sensorSettingsView.checkBoxExternalPulsing.Enabled = false; });
                        _sensorSettingsView.comboboxFrequency.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboboxFrequency.Enabled = false; });
	                    _sensorSettingsView.numericUpDownWindowSize.Invoke((MethodInvoker)delegate{ _sensorSettingsView.numericUpDownWindowSize.Enabled  = false; });

	                    _sensorSettingsView.radioButtonGraphUnitUm.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonGraphUnitUm.Enabled = false; });
	                    _sensorSettingsView.radioButtonGraphUnitMm.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonGraphUnitMm.Enabled = false; });
	                }

	                _sensorSettingsView.radioButtonExportAll.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonExportAll.Enabled = !isRecording; });			
	                _sensorSettingsView.radioButtonExportTop.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonExportTop.Enabled = !isRecording; });
	                _sensorSettingsView.radioButtonExportBottom.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonExportBottom.Enabled = !isRecording; });
	                _sensorSettingsView.radioButtonExportBrightest.Invoke((MethodInvoker)delegate{ _sensorSettingsView.radioButtonExportBrightest.Enabled = !isRecording; });
                    _sensorSettingsView.comboBoxTop.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboBoxTop.Enabled = (!isRecording && _sensorSettingsView.radioButtonExportTop.Checked); });
                    _sensorSettingsView.comboBoxBottom.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboBoxBottom.Enabled = (!isRecording && _sensorSettingsView.radioButtonExportBottom.Checked); });
                    _sensorSettingsView.comboBoxBrightest.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboBoxBrightest.Enabled = (!isRecording && _sensorSettingsView.radioButtonExportBrightest.Checked); });
		           
	                _sensorSettingsView.buttonApply.Invoke((MethodInvoker)delegate{ _sensorSettingsView.buttonApply.Enabled = !isRecording; });
	                _sensorSettingsView.buttonExportPeakData.Invoke((MethodInvoker)delegate{ _sensorSettingsView.buttonExportPeakData.Enabled = !isRecording; });
	                _sensorSettingsView.buttonFilter.Invoke((MethodInvoker)delegate{ _sensorSettingsView.buttonFilter.Enabled = !isRecording; });
	                _sensorSettingsView.comboboxLedPulseWidth.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboboxLedPulseWidth.Enabled = !isRecording; });
					
		            _sensorSettingsView.checkBoxHeightZeroAdjust.Invoke((MethodInvoker)delegate{ _sensorSettingsView.checkBoxHeightZeroAdjust.Enabled = !isRecording; });

                    _sensorSettingsView.comboBoxMaterialType.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboBoxMaterialType.Enabled = !isRecording; });
                    _sensorSettingsView.comboBoxSensitivity.Invoke((MethodInvoker)delegate{ _sensorSettingsView.comboBoxSensitivity.Enabled = !isRecording && _sensorSettingsView.comboBoxMaterialType.SelectedIndex != 0; });
                    _sensorSettingsView.textBoxMinThickness.Invoke((MethodInvoker)delegate{ _sensorSettingsView.textBoxMinThickness.Enabled = !_isRawImageVisible; });
                    _sensorSettingsView.buttonPeakDetection.Invoke((MethodInvoker)delegate{ _sensorSettingsView.buttonPeakDetection.Enabled = !isRecording; });

                    _sensorSettingsView.textBoxAverageIntensity.Invoke((MethodInvoker)delegate{ _sensorSettingsView.textBoxAverageIntensity.Enabled = !isRecording; });

                    if (_isAgcSupported)
                    {
                        _sensorSettingsView.checkBoxAgcEnabled.Invoke((MethodInvoker)delegate{ _sensorSettingsView.checkBoxAgcEnabled.Enabled = !isRecording; });
	                    _sensorSettingsView.textBoxMaxPulseWidth.Invoke((MethodInvoker)delegate{ _sensorSettingsView.textBoxMaxPulseWidth.Enabled = (!isRecording && _sensorSettingsView.checkBoxAgcEnabled.Checked); });
	                    _sensorSettingsView.textBoxAgcTargetIntensity.Invoke((MethodInvoker)delegate{ _sensorSettingsView.textBoxAgcTargetIntensity.Enabled = (!isRecording && _sensorSettingsView.checkBoxAgcEnabled.Checked); });
                    }

                    _sensorSettingsView.buttonAdvanced.Invoke((MethodInvoker) delegate{ _sensorSettingsView.buttonAdvanced.Enabled = !isRecording && (IsHdrSupported || LayerIntensityTypeSupported); });

                    loadRecipeToolStripMenuItem.Enabled = false;
                    saveRecipeToolStripMenuItem.Enabled = false;
                    saveRecipeAsToolStripMenuItem.Enabled = false;

                    BatchView.EnableConfigure = !isRecording;

                    break;
            }
            _currentViewMode = mode;

            labelViewMode.Invoke((MethodInvoker)delegate{labelViewMode.Text = mode.ToString();});
        }

        public void SetTitle(string title)
        {
            if (!ControlIsAvailable())
            {
                return;
            }

            Invoke((MethodInvoker)delegate{ Text = title; });
        }

        public void ShowMessage(string message, string caption)
        {
            if (_isClosing) return;

            _uiDispatcher.BeginInvoke(new Action(() =>
            {
                MessageBox.Show(this, message, caption);
            }));
        }

        public bool Ask(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        public void ShowRecording(RecordContainer recordContainer, BatchConfiguration batchConf, double averagePixelWidth)
        {
            if (elementHostBatchVisualizer.Child is BatchVisualizer2DUc ucBatchVisualizer2D)
            {
                ucBatchVisualizer2D.Dispatcher?.BeginInvoke(new Action(() =>
                {
                    ucBatchVisualizer2D.ShowScan(recordContainer, batchConf, averagePixelWidth, _xWidth, MinLayerId, (int)(_sensorSettingsView.numericUpDownWindowSize.Value));
                }));
            }
        }

        public void ClearBatchVisualizer()
        {
            if (elementHostBatchVisualizer.Child is BatchVisualizer2DUc ucBatchVisualizer2D)
            {
                ucBatchVisualizer2D.ClearImage();
            }
        }

        /// <summary>
        /// Event triggers when user presses apply button for sensor settings.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event information.</param>
        public void buttonApply_Click(object sender, EventArgs e)
        {
            try
            {
                var ledPulseWidth = Convert.ToSingle(_sensorSettingsView.comboboxLedPulseWidth.Text, CultureInfo.InvariantCulture);
                int? maxLedPulseWidth = null;
                float? agcTargetIntensity = null;
                var freq = Convert.ToInt32(_sensorSettingsView.comboboxFrequency.Text);
                if (freq <= 0)
                {   // Do not allow set external pulsing by setting frequency to '0'. Checkbox if for that purpose.
                    freq = 1;
                    _sensorSettingsView.comboboxFrequency.Text = freq.ToString();
                }

                for (int i = 0; i < _parameters.Layers.Length; i++)
                {
                    var setting = _advancedView.LayerSettings[i];
                    _parameters.Layers[i].IntensityType = _advancedView.LayerIntensityTypeEnabled ? setting.IntensityType : 0;
                    _parameters.Layers[i].MaxThickness = _advancedView.LayerIntensityTypeEnabled ? (float)setting.Thickness : 0;
                }
                OnSetLayerSpecificSettings?.Invoke();

                if (OnApplySensorSettings == null) return;
                if (_isAgcSupported)
                {
                    maxLedPulseWidth = Convert.ToInt32(_sensorSettingsView.textBoxMaxPulseWidth.Text);
                    agcTargetIntensity = Convert.ToSingle(_sensorSettingsView.textBoxAgcTargetIntensity.Text, CultureInfo.InvariantCulture);
                }

                float minThickness = CheckMinThickness();
                _parameters.LayerMinThickness = minThickness;
                foreach (var layer in _parameters.Layers)
                    layer.MinThickness = minThickness;

                var fir = _peakDetectionView.FirLength;
                var averFirLength = _peakDetectionView.AverFirLength;
                var detectionFilter = _peakDetectionView.DetectionFilter;
                var averageIntensityFilter = _peakDetectionView.AverageIntensityFilter;
                var threshold = _peakDetectionView.Threshold;
                OnApplySensorSettings(ledPulseWidth, maxLedPulseWidth, freq, _sensorSettingsView.checkBoxExternalPulsing.Checked, _sensorSettingsView.checkBoxAgcEnabled.Checked, 
                    agcTargetIntensity, ref fir, ref averFirLength, ref detectionFilter, 
                    ref averageIntensityFilter, ref threshold, _advancedView.Hdr, 
                    _advancedView.VLow2, _advancedView.VLow3, _advancedView.Kp1Pos, _advancedView.Kp2Pos, minThickness, 
                    _sensorSettingsView.comboBoxMaterialType.SelectedIndex - 1, _sensorSettingsView.comboBoxSensitivity.SelectedIndex - 1);

                _peakDetectionView.FirLength = fir;
                _peakDetectionView.AverFirLength = averFirLength;
                _peakDetectionView.DetectionFilter = detectionFilter;
                _peakDetectionView.AverageIntensityFilter = averageIntensityFilter;
                _peakDetectionView.Threshold = threshold;

                _ledPulseWidthHasFocus = false;
            }
            catch
            {
                // ignored
            }
        }

        void ApplyFilterChanges()
        {
            if (OnApplyFilterSettings == null) return;

            float minThickness = CheckMinThickness();
            _parameters.LayerMinThickness = minThickness;
            foreach (var layer in _parameters.Layers)
                layer.MinThickness = minThickness;

            var fir = _peakDetectionView.FirLength;
            var averFirLength = _peakDetectionView.AverFirLength;
            var detectionFilter = _peakDetectionView.DetectionFilter;
            var averageIntensityFilter = _peakDetectionView.AverageIntensityFilter;
            var threshold = _peakDetectionView.Threshold;
            OnApplyFilterSettings(ref fir, ref averFirLength, ref detectionFilter,
                ref averageIntensityFilter, ref threshold,minThickness, _sensorSettingsView.comboBoxMaterialType.SelectedIndex - 1, _sensorSettingsView.comboBoxSensitivity.SelectedIndex - 1);

            _peakDetectionView.FirLength = fir;
            _peakDetectionView.AverFirLength = averFirLength;
            _peakDetectionView.DetectionFilter = detectionFilter;
            _peakDetectionView.AverageIntensityFilter = averageIntensityFilter;
            _peakDetectionView.Threshold = threshold;
        }

        /// <summary>
        /// Event triggers when user has requested to export a point cloud as CSV data.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Event information.</param>
        public void _exportPeakData_Click(object sender, EventArgs e)
        {
            if (IsRawImageVisible && _rawImageViewer != null)
            {
                var saveFileDialog = new SaveFileDialog
                {
                    // ReSharper disable LocalizableElement
                    DefaultExt = "png",
                    Filter = "PNG files (*.png)|*.png",
                    Title = "Save raw image in PNG format"
                    // ReSharper restore LocalizableElement
                };
                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                Cursor.Current = Cursors.WaitCursor;

                OnExportRawImage?.Invoke(saveFileDialog.FileName, _rawImageViewer.GetLatestImage());

                Cursor.Current = Cursors.Default;
            }
            else
            {
                int layer = 0;
                if (_selectedLayerIndex > 0)
                {
                    var dialog = new LayerSelectionView(_selectedLayerIndex);
                    if (dialog.ShowDialog(this) != DialogResult.OK) return;
                    layer = dialog.Layer;
                }

                var saveFileDialog = new SaveFileDialog
                {
                    // ReSharper disable LocalizableElement
                    DefaultExt = "csv",
                    Filter = "CSV files (*.csv)|*.csv",
                    Title = "Save in CSV cloud format"
                    // ReSharper restore LocalizableElement
                };

                if (saveFileDialog.ShowDialog() != DialogResult.OK) return;

                Cursor.Current = Cursors.WaitCursor;

                Properties.Settings.Default.Save();

                var selectedExport = _sensorSettingsView.groupboxSurface.Controls.OfType<RadioButton>().FirstOrDefault(n => n.Checked);

                if (selectedExport != null)
                {
                    OnExportProfileCsv?.Invoke(saveFileDialog.FileName, _selectedLayerIndex == -1 ? -1 : layer);
                }

                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// Determines if a control is available.
        /// </summary>
        /// <returns>True for yes.</returns>
        private bool ControlIsAvailable()
        {
            return IsHandleCreated && !IsDisposed && !_isClosing;
        }

        /// <summary>
        /// Event triggers when user is closing form.
        /// </summary>
        /// <param name="sender">Source of the event.</param>
        /// <param name="e">Form closing event information.</param>
        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            _isClosing = true;
            _pollTimer.Enabled = false;

            ResetSeries();

            if (Session.ViewMode == ViewMode.RealTime)
            {
                if (MessageBox.Show($@"Do you want to save {_selectedRecipe} recipe?", @"GuiExample", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (string.IsNullOrEmpty(_selectedRecipe))
                        SaveRecipeAs();
                    else
                        SaveRecipe();
                }
            }

            OnWindowClosing?.Invoke();
        }

        /// <summary>Event triggers when window is shown for the first time.</summary>
        /// <param name="sender">Source of the event. </param>
        /// <param name="e">     Event information. </param>
        private void MainView_Shown(object sender, EventArgs e)
        {
            if (OnWindowShown != null)
            {
                OnWindowShown();

                buttonApply_Click(this, null);

                var mouseTimer = new System.Timers.Timer { Interval = 1000 };
                mouseTimer.Start();
                mouseTimer.AutoReset = false;
                mouseTimer.SynchronizingObject = this;
                mouseTimer.Elapsed += MouseTimer_Elapsed;
            }
        }

        private void MouseTimer_Elapsed(object sender, EventArgs e)
        {
            _profileChart.MouseWheel += _chart_MouseWheel;
            _profileChart.MouseClick += _profileChart_MouseClick;
            _thicknessChart.MouseWheel += _chart_MouseWheel;
            _thicknessChart.MouseClick += _thicknessChart_MouseClick;
        }

        public IBatchModeView BatchView => _sensorSettingsView._batchMode;

        public bool IsBatchVisualizerVisible
        {
            get => _isBatchVisualizerVisible;
            set
            {
                _isBatchVisualizerVisible = value;

                if (!(elementHostBatchVisualizer?.Child is BatchVisualizer2DUc)) return;

                elementHostBatchVisualizer.BeginInvoke((MethodInvoker)delegate
                {
                    elementHostBatchVisualizer.Visible = value;
                });

                tableLayoutPanelDataVisualizers?.BeginInvoke((MethodInvoker)delegate
                {
                    if (_isBatchVisualizerVisible)
                    {
                        tableLayoutPanelDataVisualizers.ColumnStyles[0].Width = 50F;
                        tableLayoutPanelDataVisualizers.ColumnStyles[2].Width = 50F;
                    }
                    else
                    {
                        tableLayoutPanelDataVisualizers.ColumnStyles[0].Width = 100F;
                        tableLayoutPanelDataVisualizers.ColumnStyles[2].Width = 0F;
                    }
                });
            }
        }

        public bool IsRawImageVisible
        {
            get => _isRawImageVisible;
            set
            {
                _isRawImageVisible = value;

                if (_elementHostRawImage == null)
                {
                    const string imageName = "elementHostRawImage";
                    _elementHostRawImage = new ElementHost
                    {
                        Dock = DockStyle.Fill,
                        Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                        Location = new Point(295, 3),
                        Name = imageName,
                        Size = new Size(281, 571),
                        Text = imageName
                    };

                    _rawImageViewer = new RawImageViewer();
                    _elementHostRawImage.Child = _rawImageViewer;
                    if (_elementHostRawImage == null || _rawImageViewer == null) return;
                }

                if (tableLayoutPanelDataVisualizers == null) return;

                if (_isRawImageVisible && IsThicknessVisible)
                {
                    _sensorSettingsView.checkBoxThickness.Checked = false;
                    IsThicknessVisible = false;
                }

                // Dynamically swap between profile chart and raw image
                tableLayoutPanelDataVisualizers.Invoke((MethodInvoker)delegate
                {
                    if (_isRawImageVisible)
                    {
                        tableLayoutPanelCharts.Controls.Remove(_profileChart);
                        tableLayoutPanelCharts.Controls.Remove(_thicknessChart);
                        tableLayoutPanelDataVisualizers.Controls.Add(_elementHostRawImage, 0, 0);
	                    ViewModeChanged(ViewMode.RawImage);
                    }
                    else
                    {
                        tableLayoutPanelDataVisualizers.Controls.Remove(_elementHostRawImage);
                        tableLayoutPanelCharts.Controls.Add(_profileChart, 0, 0);
                        tableLayoutPanelCharts.Controls.Add(_thicknessChart, 0, 1);
	                    ViewModeChanged(ViewMode.RealTime);
                    }

                });

                _sensorSettingsView.checkBoxIntensity.Enabled = !_isRawImageVisible;
                if (_isRawImageVisible)
                    _sensorSettingsView.checkBoxIntensity.Checked = false;

                BatchView.EnableBatch = !_isRawImageVisible;
                OnShowRawImage?.Invoke(_isRawImageVisible);

                Thread.Sleep(200);

            }
        }

        public bool IsThicknessVisible
        {
            get => _isThicknessVisible;
            set
            {
                _isThicknessVisible = value;

                // Dynamically swap between profile chart and raw image
                tableLayoutPanelCharts.Invoke((MethodInvoker)delegate
                {
                    if (_isThicknessVisible)
                    {
                        tableLayoutPanelCharts.RowStyles[0].Height = 64F;
                        tableLayoutPanelCharts.RowStyles[1].Height = 36F;
                    }
                    else
                    {
                        tableLayoutPanelCharts.RowStyles[0].Height = 100F;
                        tableLayoutPanelCharts.RowStyles[1].Height = 0F;
                    }
                });
            }
        }

        public bool IsRawImageEnabled
        {
            get => _sensorSettingsView.checkBoxRawImage.Enabled;
            set => _sensorSettingsView.checkBoxRawImage.Enabled = value;
        }

        public bool IsIntensityEnabled
        {
            get => _sensorSettingsView.checkBoxIntensity.Enabled;
            set
            {
                _sensorSettingsView.checkBoxIntensity.Enabled = value; 
                SetProfileTitle();
            }
        }

        public bool IsThicknessEnabled
        {
            get => _sensorSettingsView.checkBoxThickness.Enabled && _selectedLayerIndex > 0;
            set => _sensorSettingsView.checkBoxThickness.Enabled = value;
        }

        public float CurrentLedPulseWidth
        {
            set
            {
                if (_ledPulseWidthHasFocus || !ControlIsAvailable())
                {
                    return;
                }

                _sensorSettingsView.comboboxLedPulseWidth.Invoke((MethodInvoker)delegate
                {
                    _sensorSettingsView.comboboxLedPulseWidth.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.0}", value);
                });
            }
        }

        public int MinLayerId => _selectedLayer == ExportLayer.All ? -1 : 0;

        public void LoadSettings(ApplicationSettings settings)
        {
            if (settings.UiWindowHeight < 1 || settings.UiWindowHeight > 20)
                settings.UiWindowHeight = 20;
            _sensorSettingsView.numericUpDownWindowSize.Value = 20;

	        _xMin = Math.Round(settings.OpticalProfileMinX - 0.05, 1);
	        _xMax = Math.Round(settings.OpticalProfileMaxX + 0.05, 1);
        }

        public void LoadSensorParameters(bool isAgcSupported)
        {
            _sensorSettingsView.checkBoxAgcEnabled.Enabled = isAgcSupported;
            _sensorSettingsView.checkBoxAgcEnabled.Checked = isAgcSupported && _parameters.IsAgcEnabled;

            _sensorSettingsView.textBoxAgcTargetIntensity.Enabled = isAgcSupported && _parameters.IsAgcEnabled;
            _sensorSettingsView.textBoxAgcTargetIntensity.Text = isAgcSupported ? string.Format(CultureInfo.InvariantCulture, "{0:0.0}", _parameters.AgcTargetIntensity) : "N/A";
            _sensorSettingsView.comboboxLedPulseWidth.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.0}", _parameters.LedPulseWidth);

            _sensorSettingsView.textBoxMaxPulseWidth.Enabled = isAgcSupported && _parameters.IsAgcEnabled;
            _sensorSettingsView.textBoxMaxPulseWidth.Text = isAgcSupported ? _parameters.MaxLedPulseWidth.ToString() : "N/A";

            _sensorSettingsView.checkBoxExternalPulsing.Checked = false;
            _sensorSettingsView.checkBoxHeightZeroAdjust.Checked = true;
            _sensorSettingsView.checkBoxThickness.Checked = _parameters.IsThicknessMode;

            if (_formLoaded)
                _sensorSettingsView.checkBoxRawImage.Checked = !_parameters.IsPeakEnabled;

            // Do not allow set external pulsing by setting frequency to '0'. Checkbox should be used. 
            if (_parameters.Freq <= 0)
                _parameters.Freq = 1;

            var frequencyString = _parameters.Freq.ToString();
            if (!_sensorSettingsView.comboboxFrequency.Items.Contains(frequencyString))
                _sensorSettingsView.comboboxFrequency.Items.Add(frequencyString);

            _xWidth = _parameters.SensorWidth;

            _isAgcSupported = isAgcSupported;

            var thickness = _parameters.LayerMinThickness > 5 ? _parameters.LayerMinThickness :
                            _parameters.Layers.Length > 0 ? _parameters.Layers[0].MinThickness : 50;
            if (thickness < 5)
                thickness = 50;
            _sensorSettingsView.textBoxMinThickness.Text = thickness.ToString(CultureInfo.InvariantCulture);

            _peakDetectionView.FirLength = _parameters.FirLength;
            _peakDetectionView.AverFirLength = _parameters.AverFirLength;
            _peakDetectionView.DetectionFilter = _parameters.DetectionFilter;
            _peakDetectionView.AverageIntensityFilter = _parameters.AverageIntensityFilter;
            _peakDetectionView.Threshold = _parameters.Threshold;

            _advancedView.Hdr = (_parameters.HdrEnabled && IsHdrSupported);
            _advancedView.VLow2 = _parameters.HdrVLow2;
            _advancedView.VLow3 = _parameters.HdrVLow3;
            _advancedView.Kp1Pos = _parameters.HdrKp1Pos;
            _advancedView.Kp2Pos = _parameters.HdrKp2Pos;

            _filterView.IsNoiseRemoval = _parameters.NoiseRemoval;
            _filterView.AverageZ = _parameters.AverageZFilterSize;
            _filterView.AverageIntensity = _parameters.AverageIntensityFilterSize;
            _filterView.MedianZ = _parameters.MedianZFilterSize;
            _filterView.MedianIntensity = _parameters.MedianIntensityFilterSize;
            _filterView.Resample = _parameters.ResampleLineXResolution;
            _filterView.PeakXFilter = _parameters.PeakXFilter;
            _filterView.FillGapMax = _parameters.FillGapMax;
            _filterView.IsTrimEdges = _parameters.IsTrimEdges;

            _loadRecipeView.SensorType = _parameters.SensorType;

            var layerSettings = new List<AdvancedView.LayerSetting>();
            for (int layer = 0; layer < _parameters.Layers.Length; layer++)
            {
                _refractionView.Indexes[layer] = (float)_parameters.Layers[layer].RefractiveIndex;
                layerSettings.Add(new AdvancedView.LayerSetting(layer, _parameters.Layers[layer].MaxThickness, _parameters.Layers[layer].IntensityType));
            }
            _advancedView.LayerSettings = layerSettings;
        }


        /// <summary>
        /// Signals code behind not to allow updating LED pulse width value.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        public void ComboboxLedPulseWidth_Enter(object sender, EventArgs e)
        {
            _ledPulseWidthHasFocus = true;
        }

        private void UpdateLabelFrameIndex(FsApi.Header header)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("Frame Index = {0}", header.Index);
            sb.AppendFormat(" | Pulse Width = {0:0.0 us}", header.PulseWidth);
            sb.AppendFormat(" | Reception Frequency = {0:0.0 Hz}", header.ReceptionFrequency);
            sb.AppendFormat(" | Reception Queue Size = {0} profiles", header.ReceptionQueueSize);
            if (FsApi.IsBitSet(FsApi.Bit.Location, header.EnabledFields))
            {
                sb.AppendFormat(" | Encoder Location = {0}", header.Location);
            }
            labelFrameIndex.Text = sb.ToString();
        }

        private void SetProfileTitle()
        {
            _profileChart.Titles[0].Text = _sensorSettingsView.checkBoxIntensity.Checked ? "Layer Profile / Intensity" : "Layer Profile";
        }

        private void SetThicknessTitle()
        {
            _thicknessChart.Titles[0].Text = "Layer Thickness";
        }

        /// <summary>
        /// Opens an about dialog showing general information.
        /// </summary>
        /// <param name="sender">Not used.</param>
        /// <param name="e">Not used.</param>
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var view = new AboutView { StartPosition = FormStartPosition.CenterParent };
            view.ShowDialog(this);
        }

        public void RadioButtonProfileLayer_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton radioButton))
                return;

            if (ReferenceEquals(radioButton, _sensorSettingsView.radioButtonExportAll) && radioButton.Checked)
            {
                _selectedLayer = ExportLayer.All;
                _selectedLayerIndex = -1;
                _sensorSettingsView.checkBoxThickness.Checked = false;
                IsThicknessEnabled = false;
                IsThicknessVisible = false;
            }
            else
            {
                if (ReferenceEquals(radioButton, _sensorSettingsView.radioButtonExportTop) && radioButton.Checked)
                {
                    _selectedLayer = ExportLayer.Top;
                    _selectedLayerIndex = _sensorSettingsView.comboBoxTop.SelectedIndex;
                }
                else if (ReferenceEquals(radioButton, _sensorSettingsView.radioButtonExportBottom) && radioButton.Checked)
                {
                    _selectedLayer = ExportLayer.Bottom;
                    _selectedLayerIndex = _sensorSettingsView.comboBoxBottom.SelectedIndex;
                }
                else if (ReferenceEquals(radioButton, _sensorSettingsView.radioButtonExportBrightest) && radioButton.Checked)
                {
                    _selectedLayer = ExportLayer.BrightestAndTop;
                    _selectedLayerIndex = _sensorSettingsView.comboBoxBrightest.SelectedIndex;
                }

                if (_currentViewMode == ViewMode.RealTime)
                {
                    IsThicknessEnabled = _selectedLayerIndex > 0;
                    IsThicknessVisible = _selectedLayerIndex > 0 && _sensorSettingsView.checkBoxThickness.Checked;
                    SetThicknessTitle();
                }
            }

            OnProfileLayerSelected?.Invoke(_selectedLayer, _selectedLayerIndex);

            EnableSurfaceSelections();

            ResetSeries(true);
        }

        public void CheckBoxAgcEnabled_CheckedChanged(object sender, EventArgs e)
        {
            _sensorSettingsView.textBoxAgcTargetIntensity.Enabled = _sensorSettingsView.checkBoxAgcEnabled.Checked;
            _sensorSettingsView.textBoxMaxPulseWidth.Enabled = _sensorSettingsView.checkBoxAgcEnabled.Checked;
            _sensorSettingsView.textBoxAgcTargetIntensity.Update();
            _sensorSettingsView.textBoxMaxPulseWidth.Update();
        }

        public void graphUnitMm_CheckedChanged(object sender, EventArgs e)
        {
            _profileChart.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            _profileChart.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
            _thicknessChart.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            _thicknessChart.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
            _resetProfileCursor = _resetThicknessCursor = true;
            UpdateGraphScales();
        }

        public void graphUnitUm_CheckedChanged(object sender, EventArgs e)
        {
            _profileChart.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            _profileChart.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
            _thicknessChart.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            _thicknessChart.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
            _resetProfileCursor = _resetThicknessCursor = true;
            UpdateGraphScales();
        }

        private void _batchMode_Load(object sender, EventArgs e)
        {
        }

        public void checkBoxHeightZeroAdjust_CheckedChanged(object sender, EventArgs e)
        {
            _parameters.OffsetY = _sensorSettingsView.checkBoxHeightZeroAdjust.Checked ? -1 : 1;
        }

        private void EnableSurfaceSelections()
        {
            _sensorSettingsView.comboBoxTop.Enabled = _sensorSettingsView.radioButtonExportTop.Checked;
            _sensorSettingsView.comboBoxBottom.Enabled = _sensorSettingsView.radioButtonExportBottom.Checked;
            _sensorSettingsView.comboBoxBrightest.Enabled = _sensorSettingsView.radioButtonExportBrightest.Checked;
        }

        public void ComboBoxTop_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedLayer = ExportLayer.Top;
            _selectedLayerIndex = _sensorSettingsView.comboBoxTop.SelectedIndex;
            _layerSelectionTimer.Start();
        }

        public void ComboBoxBottom_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedLayer = ExportLayer.Bottom;
            _selectedLayerIndex = _sensorSettingsView.comboBoxBottom.SelectedIndex;
            _layerSelectionTimer.Start();
        }

        public void ComboBoxBrightest_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedLayer = ExportLayer.BrightestAndTop;
            _selectedLayerIndex = _sensorSettingsView.comboBoxBrightest.SelectedIndex;
            _layerSelectionTimer.Start();
        }

        public void checkBoxRawImage_CheckedChanged(object sender, EventArgs e)
        {
            IsRawImageVisible = _sensorSettingsView.checkBoxRawImage.Checked;
        }

        private void MainView_Resize(object sender, EventArgs e)
        {
            if (_panel1OriginalWidth == 0)
            {
                _panel1OriginalWidth = (int)tableLayoutPanelMain.ColumnStyles[0].Width;
                return;
            }

            if (IsRawImageVisible)
            {
                _resizeTimer.Start();
            }
            else
                FitScrollbar();

            if (WindowState == _lastWindowState) return;
            Update();
            _lastWindowState = WindowState;
        }

        private void ResizeTimer_Elapsed(object sender, EventArgs e)
        {
            FitScrollbar();
            _resizeTimer.Stop();
        }

        private void FitScrollbar()
        {
            if (panelSettings.VerticalScroll.Visible)
            {
                if ((int)tableLayoutPanelMain.ColumnStyles[0].Width == _panel1OriginalWidth + SystemInformation.VerticalScrollBarWidth) return;
                tableLayoutPanelMain.BeginInvoke((MethodInvoker) delegate
                {
                    tableLayoutPanelMain.ColumnStyles[0].Width =
                        _panel1OriginalWidth + SystemInformation.VerticalScrollBarWidth;
                });
            }
            else
            {
                if ((int)tableLayoutPanelMain.ColumnStyles[0].Width == _panel1OriginalWidth) return;
                tableLayoutPanelMain.BeginInvoke((MethodInvoker) delegate
                {
                    tableLayoutPanelMain.ColumnStyles[0].Width = _panel1OriginalWidth;
                });
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OnRequestCalibrationFiles?.Invoke();
        }

        private void loadRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectRecipe();
        }

        private void saveRecipeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedRecipe))
                SaveRecipeAs();
            else
                SaveRecipe();
        }

        private void saveRecipeAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveRecipeAs();
        }

        public void checkBoxIntensity_CheckedChanged(object sender, EventArgs e)
        {
            _profileChart.Invoke((MethodInvoker)delegate
            {
                _profileChart.ChartAreas[0].AxisY2.Enabled = _sensorSettingsView.checkBoxIntensity.Checked ? AxisEnabled.True : AxisEnabled.False;
            });
            SetProfileTitle();
        }

        private void _profileChart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (_sensorSettingsView.checkBoxIntensity.Checked)
            {
                _profileChart.Invoke((MethodInvoker) delegate
                {
                    if (Math.Abs(_profileChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum - _profileChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum) < GetZoomLimit()) return;
                    double scale = 255 / (_profileChart.ChartAreas[0].AxisY.Maximum -
                                          _profileChart.ChartAreas[0].AxisY.Minimum);
                    var minimum = (_profileChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum -
                                   _profileChart.ChartAreas[0].AxisY.Minimum) * scale;
                    var maximum = (_profileChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum -
                                   _profileChart.ChartAreas[0].AxisY.Minimum) * scale;
                    _profileChart.ChartAreas[0].AxisY2.Minimum = Math.Round(minimum, maximum - minimum > 0.1 ? 2 : 6);
                    _profileChart.ChartAreas[0].AxisY2.Maximum = Math.Round(maximum, maximum - minimum > 0.1 ? 2 : 6);
                    _profileChart.ChartAreas[0].AxisY2.Interval = _profileChart.ChartAreas[0].AxisY.Interval * scale;
                });
            }

            if (_currentViewMode == ViewMode.RealTime)
            {
                _resetProfileCursor = true;
            }
            else
            {
                ResetProfileCursorLines();
                _profileChart.Invoke((MethodInvoker) delegate{ _profileChart.Invalidate(); });
            }
        }

        private void _thicknessChart_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if (_currentViewMode == ViewMode.RealTime)
            {
                _resetThicknessCursor = true;
            }
            else
            {
                ResetThicknessCursorLines();
                _thicknessChart.Invoke((MethodInvoker)delegate{ _thicknessChart.Invalidate(); });
            }
        }

        private void _profileChart_MouseClick(object sender, MouseEventArgs e)
        {
            double left = _profileChart.ChartAreas[0].AxisX.ValueToPixelPosition(_profileChart.ChartAreas[0].AxisX.Minimum);
            double right = _profileChart.ChartAreas[0].AxisX.ValueToPixelPosition(_profileChart.ChartAreas[0].AxisX.Maximum);
            double top = _profileChart.ChartAreas[0].AxisY.ValueToPixelPosition(_profileChart.ChartAreas[0].AxisY.Maximum);
            double bottom = _profileChart.ChartAreas[0].AxisY.ValueToPixelPosition(_profileChart.ChartAreas[0].AxisY.Minimum);
            if (!_profileChart.ChartAreas[0].AxisX.ScaleView.Position.IsNaN())
            {
                left = _profileChart.ChartAreas[0].AxisX.ValueToPixelPosition(_profileChart.ChartAreas[0].AxisX.ScaleView.ViewMinimum);
                right = _profileChart.ChartAreas[0].AxisX.ValueToPixelPosition(_profileChart.ChartAreas[0].AxisX.ScaleView.ViewMaximum);
            }
            if (!_profileChart.ChartAreas[0].AxisY.ScaleView.Position.IsNaN())
            {
                top = _profileChart.ChartAreas[0].AxisY.ValueToPixelPosition(_profileChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum);
                bottom = _profileChart.ChartAreas[0].AxisY.ValueToPixelPosition(_profileChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum);
            }

            if (e.X < left || e.X > right || e.Y < top || e.Y > bottom) // cursor not on chart area
            {
                ResetProfileCursorLines();
                return;
            }

            _profileCursorPixelPoint = new Point(e.X, e.Y);
            _profileChart.ChartAreas[0].CursorX.SetCursorPixelPosition(_profileCursorPixelPoint, true);
            _profileChart.ChartAreas[0].CursorY.SetCursorPixelPosition(_profileCursorPixelPoint, true);

            _profileCursorXPosition = _profileChart.ChartAreas[0].CursorX.Position;
            _profileCursorYPosition = _profileChart.ChartAreas[0].CursorY.Position;

            _profileChart.Invalidate();
        }

        private void _thicknessChart_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                double left = _thicknessChart.ChartAreas[0].AxisX.ValueToPixelPosition(_thicknessChart.ChartAreas[0].AxisX.Minimum);
                double right = _thicknessChart.ChartAreas[0].AxisX.ValueToPixelPosition(_thicknessChart.ChartAreas[0].AxisX.Maximum);
                double top = _thicknessChart.ChartAreas[0].AxisY.ValueToPixelPosition(_thicknessChart.ChartAreas[0].AxisY.Maximum);
                double bottom = _thicknessChart.ChartAreas[0].AxisY.ValueToPixelPosition(_thicknessChart.ChartAreas[0].AxisY.Minimum);
                if (!_thicknessChart.ChartAreas[0].AxisX.ScaleView.Position.IsNaN())
                {
                    left = _thicknessChart.ChartAreas[0].AxisX.ValueToPixelPosition(_thicknessChart.ChartAreas[0].AxisX.ScaleView.ViewMinimum);
                    right = _thicknessChart.ChartAreas[0].AxisX.ValueToPixelPosition(_thicknessChart.ChartAreas[0].AxisX.ScaleView.ViewMaximum);
                }
                if (!_thicknessChart.ChartAreas[0].AxisY.ScaleView.Position.IsNaN())
                {
                    top = _thicknessChart.ChartAreas[0].AxisY.ValueToPixelPosition(_thicknessChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum);
                    bottom = _thicknessChart.ChartAreas[0].AxisY.ValueToPixelPosition(_thicknessChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum);
                }

                if (e.X < left || e.X > right || e.Y < top || e.Y > bottom) // cursor not on chart area
                {
                    ResetThicknessCursorLines();
                    return;
                }

                _thicknessCursorPixelPoint = new Point(e.X, e.Y);
                _thicknessChart.ChartAreas[0].CursorX.SetCursorPixelPosition(_thicknessCursorPixelPoint, true);
                _thicknessChart.ChartAreas[0].CursorY.SetCursorPixelPosition(_thicknessCursorPixelPoint, true);

                _thicknessCursorXPosition = _thicknessChart.ChartAreas[0].CursorX.Position;
                _thicknessCursorYPosition = _thicknessChart.ChartAreas[0].CursorY.Position;

                _thicknessChart.Invalidate();
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"thicknessChart_MouseClick {exception.Message}");
            }
        }

        private void _profileChart_Paint(object sender, PaintEventArgs e)
        {
            if (_profileCursorPixelPoint == Point.Empty || Math.Abs(_profileCursorXPosition) < double.Epsilon || Math.Abs(_profileCursorYPosition) < double.Epsilon || _profileCursorXPosition.IsNaN() || _profileCursorYPosition.IsNaN()) return;
            if (Math.Abs(_profileChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum - _profileChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum) < GetZoomLimit()) return;
            if (_profileCursorXPosition < _profileChart.ChartAreas[0].AxisX.ScaleView.ViewMinimum || _profileCursorXPosition > _profileChart.ChartAreas[0].AxisX.ScaleView.ViewMaximum) return;
            if (_profileCursorYPosition < _profileChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum || _profileCursorYPosition > _profileChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum) return;

            string cursorX = _sensorSettingsView.radioButtonGraphUnitUm.Checked ? _profileCursorXPosition.ToString("0.##") : _profileCursorXPosition.ToString("0.###");
            string cursorY = _sensorSettingsView.radioButtonGraphUnitUm.Checked ? _profileCursorYPosition.ToString("0.##") : _profileCursorYPosition.ToString("0.###");
            string cursorY2 = "";

            if (_sensorSettingsView.checkBoxIntensity.Checked)
            {
                double intensity = 255 * (_profileCursorYPosition - _profileChart.ChartAreas[0].AxisY.Minimum) /
                                   (_profileChart.ChartAreas[0].AxisY.Maximum - _profileChart.ChartAreas[0].AxisY.Minimum);
                intensity = Math.Round(intensity, (_profileChart.ChartAreas[0].AxisY2.Maximum - _profileChart.ChartAreas[0].AxisY2.Minimum) > 254 ? 0: 3);
                cursorY2 = ", " + intensity;
            }

            string positionText = "(" + cursorX + ", " + cursorY + cursorY2 + ")";

            e.Graphics.DrawString(positionText, new Font("Microsoft Sans Serif", 9), new SolidBrush(Color.Red), _profileCursorPixelPoint.X + 5, _profileCursorPixelPoint.Y + 5);
        }

        private void _thicknessChart_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (_thicknessCursorPixelPoint == Point.Empty || Math.Abs(_thicknessCursorXPosition) < double.Epsilon || Math.Abs(_thicknessCursorYPosition) < double.Epsilon || _thicknessCursorXPosition.IsNaN() || _thicknessCursorYPosition.IsNaN()) return;
                if (Math.Abs(_thicknessChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum - _thicknessChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum) < GetZoomLimit()) return;
                if (_thicknessCursorXPosition < _thicknessChart.ChartAreas[0].AxisX.ScaleView.ViewMinimum || _thicknessCursorXPosition > _thicknessChart.ChartAreas[0].AxisX.ScaleView.ViewMaximum) return;
                if (_thicknessCursorYPosition < _thicknessChart.ChartAreas[0].AxisY.ScaleView.ViewMinimum || _thicknessCursorYPosition > _thicknessChart.ChartAreas[0].AxisY.ScaleView.ViewMaximum) return;

                string cursorX = _sensorSettingsView.radioButtonGraphUnitUm.Checked ? _thicknessCursorXPosition.ToString("0.##") : _thicknessCursorXPosition.ToString("0.###");
                string cursorY = _sensorSettingsView.radioButtonGraphUnitUm.Checked ? _thicknessCursorYPosition.ToString("0.##") : _thicknessCursorYPosition.ToString("0.###");

                string positionText = "(" + cursorX + ", " + cursorY + ")";

                e.Graphics.DrawString(positionText, new Font("Microsoft Sans Serif", 9), new SolidBrush(Color.Red), _thicknessCursorPixelPoint.X + 5, _thicknessCursorPixelPoint.Y + 5);
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"_thicknessChart_Paint {exception.Message}");
            }
        }

        private void _chart_MouseWheel(object sender, MouseEventArgs e)
        {
            var chart = (Chart)sender;
            var xAxis = chart.ChartAreas[0].AxisX;
            var yAxis = chart.ChartAreas[0].AxisY;
            var y2Axis = chart.ChartAreas[0].AxisY2;
            double posXStart = 0, posXFinish = 0, posYStart = 0, posYFinish = 0, posY2Start = 0, posY2Finish = 0;

            try
            {
                var xMin = xAxis.ScaleView.ViewMinimum;
                var xMax = xAxis.ScaleView.ViewMaximum;
                var yMin = yAxis.ScaleView.ViewMinimum;
                var yMax = yAxis.ScaleView.ViewMaximum;
                var y2Min = y2Axis.ScaleView.ViewMinimum;
                var y2Max = y2Axis.ScaleView.ViewMaximum;

                if (e.Delta > 0) // Scroll up
                {
                    posXStart = xAxis.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 4;
                    posXFinish = xAxis.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 4;
                    posYStart = yAxis.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 4;
                    posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 4;
                    posY2Start = y2Axis.PixelPositionToValue(e.Location.Y) - (y2Max - y2Min) / 4;
                    posY2Finish = y2Axis.PixelPositionToValue(e.Location.Y) + (y2Max - y2Min) / 4;
                }
                else if (e.Delta < 0)
                {
                    if (y2Max - y2Min > 125)
                    {
                        xAxis.ScaleView.ZoomReset();
                        yAxis.ScaleView.ZoomReset();
                        y2Axis.ScaleView.ZoomReset();
                        y2Axis.Minimum = 0;
                        y2Axis.Maximum = 255;
                        return;
                    }
                    posXStart = xAxis.PixelPositionToValue(e.Location.X) - 4 * (xMax - xMin);
                    posXFinish = xAxis.PixelPositionToValue(e.Location.X) + 4 * (xMax - xMin);
                    posYStart = yAxis.PixelPositionToValue(e.Location.Y) - 4 * (yMax - yMin);
                    posYFinish = yAxis.PixelPositionToValue(e.Location.Y) + 4 * (yMax - yMin);
                    posY2Start = y2Axis.PixelPositionToValue(e.Location.Y) - 4 * (y2Max - y2Min);
                    posY2Finish = y2Axis.PixelPositionToValue(e.Location.Y) + 4 * (y2Max - y2Min);
                }

                var limit = GetZoomLimit();
                if (Math.Abs(posXStart - posXFinish) < limit || Math.Abs(posYStart - posYFinish) < limit || Math.Abs(posY2Start - posY2Finish) < limit) return;

                xAxis.ScaleView.Zoom(posXStart, posXFinish);
                yAxis.ScaleView.Zoom(posYStart, posYFinish);
                if (y2Axis.Enabled == AxisEnabled.False) return;
                posY2Start = posY2Start > 255 ? 255 : posY2Start < 0 ? 0 : posY2Start;
                posY2Finish = posY2Finish > 255 ? 255 : posY2Finish < 0 ? 0 : posY2Finish;
                y2Axis.Minimum = Math.Round(posY2Start, posY2Finish - posY2Start > 0.1 ? 2 : 6);
                y2Axis.Maximum = Math.Round(posY2Finish, posY2Finish - posY2Start > 0.1 ? 2 : 6);
            }
            catch (Exception exception)
            {
                Trace.WriteLine($"_chart_MouseWheel {exception.Message}");
            }
        }

        public void numericUpDownWindowSize_ValueChanged(object sender, EventArgs e)
        {
            if (!IsHandleCreated) return;

            _sensorSettingsView.numericUpDownWindowSize.Invoke((MethodInvoker)delegate
            {
                OnApplyUiSettings?.Invoke((int)_sensorSettingsView.numericUpDownWindowSize.Value);
            });
            UpdateGraphScales();

            _resetProfileCursor = _resetThicknessCursor = true;
        }

        private void ResetProfileCursorLines()
        {
            _profileCursorXPosition = _profileCursorYPosition = 0;
            _profileChart.Invoke((MethodInvoker) delegate
            {
                _profileCursorPixelPoint = new Point(int.MinValue, int.MinValue);
                _profileChart.ChartAreas[0].CursorX.Position = double.NaN;
                _profileChart.ChartAreas[0].CursorY.Position = double.NaN;
                _profileChart.Invalidate();
            });
            _resetProfileCursor = false;
        }

        private void ResetThicknessCursorLines()
        {
            _thicknessCursorXPosition = _thicknessCursorYPosition = 0;
            _thicknessChart.Invoke((MethodInvoker)delegate
            {
                _thicknessCursorPixelPoint = new Point(int.MinValue, int.MinValue);
                _thicknessChart.ChartAreas[0].CursorX.Position = double.NaN;
                _thicknessChart.ChartAreas[0].CursorY.Position = double.NaN;
                _thicknessChart.Invalidate();
            });
            _resetThicknessCursor = false;
        }

        public void _buttonAdvanced_Click(object sender, EventArgs e)
        {
            _advancedView.Location = new Point(_sensorSettingsView.buttonAdvanced.Right + 50, _sensorSettingsView.buttonAdvanced.Top);

            _advancedView.IsHs = IsHsCamera;
            _advancedView.IsHdrEnabled = IsHdrSupported;
            _advancedView.LayerIntensityTypeEnabled = LayerIntensityTypeSupported;
            _advancedView.ShowDialog(this);
        }

        public void _buttonFilter_Click(object sender, EventArgs e)
        {
            _filterView.Location = new Point(_sensorSettingsView.buttonFilter.Right + 50, _sensorSettingsView.buttonFilter.Top + _sensorSettingsView.groupboxSurface.Top);
            _filterView.ShowDialog(this);
	        if (_filterView.ApplyFilters)
	        {
		        OnFilter?.Invoke(_filterView.IsNoiseRemoval, _filterView.AverageZ, _filterView.AverageIntensity, _filterView.MedianZ, 
			                     _filterView.MedianIntensity, _filterView.Resample, _filterView.PeakXFilter, _filterView.FillGapMax, _filterView.IsTrimEdges);
	        }
        }

        public void ButtonPeakDetection_Click(object sender, EventArgs e)
        {
            ApplyFilterChanges();
            _peakDetectionView.Location = new Point(_sensorSettingsView.buttonPeakDetection.Right+50, _sensorSettingsView.buttonPeakDetection.Top + _sensorSettingsView.groupboxSensorSettings.Top);
            _peakDetectionView.ShowDialog(this);
            if (_peakDetectionView.ValuesChanged)
            {
                _sensorSettingsView.comboBoxMaterialType.SelectedIndex = 0;
                _sensorSettingsView.comboBoxSensitivity.SelectedIndex = 0;
                ApplyFilterChanges();
            }
        }

        private void _thicknessChart_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
        {
            foreach (LegendItem item in e.LegendItems)
            {
                item.MarkerStyle = MarkerStyle.Circle;
                item.MarkerSize = 100;
            }
        }

        public void ComboBoxMaterialType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_sensorSettingsView.comboBoxMaterialType.SelectedIndex == 0)
            {
                _sensorSettingsView.comboBoxSensitivity.SelectedIndex = 0;
                _sensorSettingsView.comboBoxSensitivity.Enabled = false;
            }
            else
            {
                _sensorSettingsView.comboBoxSensitivity.Enabled = true;
                if (_sensorSettingsView.comboBoxSensitivity.SelectedIndex == 0)
                    _sensorSettingsView.comboBoxSensitivity.SelectedIndex = 1;
                ApplyFilterChanges();
            }
            _sensorSettingsView.comboBoxSensitivity.Update();
        }

        public void ComboBoxSensitivity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_sensorSettingsView.comboBoxSensitivity.SelectedIndex == 0)
            {
                _sensorSettingsView.comboBoxMaterialType.SelectedIndex = 0;
                _sensorSettingsView.comboBoxMaterialType.Update();
            }
            else
            {
                ApplyFilterChanges();
            }
        }

        private double GetZoomLimit()
        {
            return _sensorSettingsView.radioButtonGraphUnitUm.Checked ? 0.001 : 0.000001;
        }

        private void ResetSeries(bool invalidate = false)
        {
            lock (_chartLock)
            {
                if (!ControlIsAvailable()) return;
                for (int i = 0; i < _seriesCount; i++)
                {
                    _profileChart.Series["profile" + i].Points.Clear();
                    _profileChart.Series["intensity" + i].Points.Clear();
                    if (_thicknessChart.Series.Count > i)
                        _thicknessChart.Series[i].Points.Clear();
                }
                if (invalidate)
                    _profileChart.Invalidate();
            }
        }

        public void checkBoxThickness_CheckedChanged(object sender, EventArgs e)
        {
            _thicknessChart.Visible = _sensorSettingsView.checkBoxThickness.Checked;
            IsThicknessVisible = _sensorSettingsView.checkBoxThickness.Checked;
            OnSetThicknessMode?.Invoke(IsThicknessVisible);
        }

        public void textBoxMinThickness_TextChanged(object sender, EventArgs e)
        {
            float thickness = CheckMinThickness();
            _sensorSettingsView.textBoxMinThickness.Text = thickness.ToString(CultureInfo.InvariantCulture);
        }

        public void _buttonRefraction_Click(object sender, EventArgs e)
        {
            _refractionView.Location = new Point(_sensorSettingsView.buttonRefraction.Right + 50, _sensorSettingsView.buttonRefraction.Top + _sensorSettingsView.groupboxViewSettings.Top);
            _refractionView.ShowDialog(this);
            for (int layer = 0; layer < _parameters.Layers.Length; layer++)
            {
                _parameters.Layers[layer].RefractiveIndex = _refractionView.Indexes[layer];
            }
            OnSetRefractiveIndexes?.Invoke();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    // Opens the Robot Interface. Used to adjust config settings
        //    if (_photogrammetryView.IsDisposed)
        //    {
        //        _photogrammetryView = new PhotogrammetryView(this);  // recreate it if it was closed before
        //        _photogrammetryView.Show();
        //    }
        //    else if (!_photogrammetryView.Visible)
        //    {
        //        _photogrammetryView.Show();
        //    }
        //}

        private void btn_ScanCTRLS_Click(object sender, EventArgs e)
        {
            ControllerInfoCollection ControllerList = rapidFunctions.ScanControllers();
            ListViewItem item = null;
            this.listView_Controllers.Items.Clear();
            foreach (ControllerInfo controllerInfo in ControllerList)
            {
                item = new ListViewItem(controllerInfo.IPAddress.ToString());
                item.SubItems.Add(controllerInfo.ControllerName);
                item.Tag = controllerInfo;
                this.listView_Controllers.Items.Add(item);
            }
        }

        private void btn_ConnectCTRL_Click(object sender, EventArgs e)
        {
            if (btn_ConnectCTRL.Text == "Connect")
            {
                rapidFunctions.ConnectController(listView_Controllers.SelectedItems[0]);
                rapidFunctions.controller.Logon(UserInfo.DefaultUser);
                if (rapidFunctions.controller.Connected == true)
                {
                    btn_ConnectCTRL.Text = "Disconnect";
                    if (rapidFunctions.controller.OperatingMode == ControllerOperatingMode.Auto)
                    {
                        rapidFunctions.tasks = rapidFunctions.controller.Rapid.GetTasks();
                        //  rapidFunctions.tasks[0].Stop();
                    }
                    else
                        MessageBox.Show("Automatic mode is required to start execution from a remote client.");
                }
                else
                    LogMessage("Connect Failed");
            }
            else
                btn_ConnectCTRL.Text = "Connect";
        }

        private void SaveRecipe()
        {
            if (Session.ViewMode != ViewMode.RealTime) return;
            OnSaveRecipe?.Invoke(_selectedRecipe);
        }

        private void btn_StopRap_Click(object sender, EventArgs e)
        {
            rapidFunctions.Stop();
        }

        private void btn_StartRAP_Click(object sender, EventArgs e)
        {
            rapidFunctions.Start();
            rapidFunctions.PhotoSequence();
        }

        private void btn_RapContinue_Click(object sender, EventArgs e)
        {
            rapidFunctions.Resume();
        }

        private void btn_SaveLog_Click(object sender, EventArgs e)
        {
            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "log files (*.log)|*.txt|All files (*.*)|*.*";
            file.DefaultExt = ".log";

            if (file.ShowDialog() == DialogResult.OK)
                this.richTextBox1.SaveFile(file.FileName, RichTextBoxStreamType.PlainText);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Using Show since ShowDialog seems to reset the handle
            _sensorSettingsView.Show(this);
        }

        private void SaveRecipeAs()
        {
            if (Session.ViewMode != ViewMode.RealTime) return;
            var saveRecipeView = new RecipeSave(_selectedRecipe);
            saveRecipeView.ShowDialog(this);
            _selectedRecipe = saveRecipeView.Recipe;
            if (!string.IsNullOrEmpty(_selectedRecipe))
                SaveRecipe();
        }

        private void travelSpeedUpDown_ValueChanged(object sender, EventArgs e)
        {
            rapidFunctions.SetTravelSpeed((int) travelSpeedUpDown.Value);
        }

        private void estop_button_Click(object sender, EventArgs e)
        {
            rapidFunctions.Stop(true);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
           _sensorSettingsView._batchMode.UpdateInternalTriggeringFreq(numericUpDown1.Value);
        }

        private void UpdateGraphScales()
        {
            if (_sensorSettingsView.radioButtonGraphUnitMm.Checked)
            {
                double yScale = (double)_sensorSettingsView.numericUpDownWindowSize.Value / 2.0;
                _profileChart.ChartAreas[0].AxisY.Minimum = -yScale;
                _profileChart.ChartAreas[0].AxisY.Maximum = yScale;
                _profileChart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.000}";
                _profileChart.ChartAreas[0].AxisX.Title = "X [mm]";
                _profileChart.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.000}";
                _profileChart.ChartAreas[0].AxisY.Title = "Z [mm]";
                _thicknessChart.ChartAreas[0].AxisY.Maximum = yScale;
                _thicknessChart.ChartAreas[0].AxisY.Minimum = 0;
                _thicknessChart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.000}";
                _thicknessChart.ChartAreas[0].AxisX.Title = "X [mm]";
                _thicknessChart.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.000}";
                _thicknessChart.ChartAreas[0].AxisY.Title = "Z [mm]";
            }
            else
            {
                double yScale = (double)_sensorSettingsView.numericUpDownWindowSize.Value / 2.0 * 1000;
                _profileChart.ChartAreas[0].AxisY.Minimum = -yScale;
                _profileChart.ChartAreas[0].AxisY.Maximum = yScale;
                _profileChart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
                _profileChart.ChartAreas[0].AxisX.Title = "X [\u00B5m]";
                _profileChart.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}";
                _profileChart.ChartAreas[0].AxisY.Title = "Z [\u00B5m]";
                _thicknessChart.ChartAreas[0].AxisY.Maximum = yScale;
                _thicknessChart.ChartAreas[0].AxisY.Minimum = 0;
                _thicknessChart.ChartAreas[0].AxisX.LabelStyle.Format = "{0:0.00}";
                _thicknessChart.ChartAreas[0].AxisX.Title = "X [\u00B5m]";
                _thicknessChart.ChartAreas[0].AxisY.LabelStyle.Format = "{0:0.00}";
                _thicknessChart.ChartAreas[0].AxisY.Title = "Z [\u00B5m]";
            }
        }

        private static Color GetLayerColor(int index)
        {
            switch (index)
            {
                case 0: return Color.Blue;
                case 1: return Color.Red;
                case 2: return Color.Green;
                case 3: return Color.Chocolate;
                case 4: return Color.Magenta;
                case 5: return Color.Gold;
                case 6: return Color.Black;
                case 7: return Color.DeepSkyBlue;
                case 8: return Color.Orange;
                case 9: return Color.LawnGreen;
                default: return Color.White;
            }
        }

        private float CheckMinThickness()
        {
            float minThickness = 5;
            try
            {
                minThickness = Convert.ToSingle(_sensorSettingsView.textBoxMinThickness.Text);
            }
            catch (Exception)
            {
                // ignored
            }
            if (minThickness < 5)
                minThickness = 5;
            return minThickness;
        }

        private void btn_HideTerminal_Click(object sender, EventArgs e)
        {
            if (showHideTerminal.Text == "Hide Terminal")
            {
                terminaloutputGroup.Visible = false;
                btn_SaveLog.Visible = false;

                showHideTerminal.Text = "Show Terminal";
            }
            else
            {
                terminaloutputGroup.Visible = true;
                btn_SaveLog.Visible = true;

                showHideTerminal.Text = "Hide Terminal";
            }
        }

                // Log a string to the Log Buffer
                public void LogMessage(string MSG)
                {
                    Control.CheckForIllegalCrossThreadCalls = false;
                    this.richTextBox1.AppendText(DateTime.Now.ToString() + ":   ");
                    this.richTextBox1.AppendText(MSG);
                    this.richTextBox1.AppendText("\n\r");
                    this.richTextBox1.ScrollToCaret();
                }
    }
}