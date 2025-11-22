// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchModePresenter.cs" company="FocalSpec Oy">
//   FocalSpec Oy 2016-
// </copyright>
// <summary>
//   Batch mode presenter is used for controlling batch recording.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics;

namespace FocalSpec.GuiExample.Presenter
{

    using Model;
    using Model.BatchMode;
    using Model.Camera;
    using Model.Export;
    using View;

    /// <summary>
    /// A batch mode presenter.
    /// </summary>
    public class BatchModePresenter
    {
        // TODO/FOCALSPEC Do we need this?
        /// <summary>
        /// The synchronization lock.
        /// </summary>
        private static readonly object SyncLock = new object();

        /// <summary>
        /// Object used for controlling the camera.
        /// </summary>
        private readonly CameraManager _cameraManager;

        /// <summary>
        /// The record container. This is null, if container is not available.
        /// </summary>
        private RecordContainer _recordContainer;

        /// <summary>
        /// The batch recording configuration.
        /// </summary>
        private BatchConfiguration _batchConfiguration = new BatchConfiguration();

        /// <summary>
        /// Gets or sets a value indicating whether recording for this container is stopped.
        /// </summary>
        private bool _isStopped;

        /// <summary>
        /// Flag indicating whether frequency parameter update is required before batch recording is started.
        /// </summary>
        private bool _freqUpdatedRequired;

        /// <summary>
        /// Main view, which is hosting batch view.
        /// </summary>
        private readonly IMainView _mainView;

        /// <summary>
        /// Runtime settings.
        /// </summary>
        private readonly ApplicationSettings _applicationSettings;

        /// <summary>
        /// Registers to UI actions.
        /// </summary>
        /// <param name="cameraManager">Controls the camera.</param>
        /// <param name="mainView">Main view hosting the batch view.</param>
        /// <param name="applicationSettings"></param>
        public BatchModePresenter(CameraManager cameraManager, IMainView mainView, ApplicationSettings applicationSettings)
        {
            _cameraManager = cameraManager;
            _mainView = mainView;
            _applicationSettings = applicationSettings;

            _mainView.BatchView.OnConfigure += ConfigureBatchMode;
            _mainView.BatchView.OnStart += StartRecording;
            _mainView.BatchView.OnStop += StopRecording;
            _mainView.BatchView.OnSave += SavePoints;
            _mainView.BatchView.OnClear += ClearRecording;
            _mainView.BatchView.OnPositionBrowsed += BrowseToPosition;
            _mainView.BatchView.OnShowHideBatchVisualizer += BatchView_OnShowHideBatchVisualizer;

            _mainView.OnWindowShown += OnWindowShown;

            cameraManager.OnPointCloudReceivedEvent += OnPointCloudReceived;
        }

        /// <summary> 
        /// Clears the recording.
        /// </summary>
        private void ClearRecording()
        {
            lock (SyncLock)
            {
                StopRecording(false);
                _recordContainer = null;
            }

            _mainView.BatchView.EnableStop = false;
            _mainView.BatchView.EnableSave = false;
            _mainView.BatchView.EnableClear = false;
            _mainView.BatchView.EnablePosition = false;
            _mainView.BatchView.MaxPosition = _batchConfiguration.BatchLength - 1;

            _mainView.ViewModeChanged(ViewMode.RealTime);
            StopBatch();
            _mainView.BatchView.IsBatchVisualizerVisible = false;
            _mainView.ClearBatchVisualizer();
            _mainView.BatchView.Position = 0;
            _freqUpdatedRequired = true;
            _mainView.BatchView.EnableRecord = true;
	        _cameraManager.StartGrabbing(10);
        }

        /// <summary>
        /// Show the requested profile on the UI.
        /// </summary>
        /// <param name="index">Zero-based index of the profile in the container. </param>
        private void BrowseToPosition(int index)
        {
            if (!_isStopped)
            {
                return;
            }

            lock (SyncLock)
            {
                var profiles = new List<ProfileInfo>
                {
                    new ProfileInfo(_mainView.MinLayerId + 1, _recordContainer.GetProfile(_mainView.MinLayerId, index))
                };
                _mainView.SetProfile(profiles);
            }
        }

        private void BatchView_OnShowHideBatchVisualizer(object sender, System.EventArgs e)
        {
            _mainView.IsBatchVisualizerVisible = _mainView.BatchView.IsBatchVisualizerVisible;
        }

        /// <summary>
        /// Saves the points to a file.
        /// </summary>
        /// <param name="path">Full pathname of the file. </param>
        private void SavePoints(string path)
        {
            lock (SyncLock)
            {
                if (_recordContainer != null)
                {
	                ExportBatch.SaveToPointCloudFile(path, _recordContainer.GetProfiles(), _batchConfiguration.ScanStepLength, _applicationSettings);
                }
            }
        }

        /// <summary>
        /// Starts a recording. Initializes a container for saving, and updates the buttons on the view.
        /// </summary>
        public void StartRecording()
        {
            /*
            if (!Environment.Is64BitProcess)
            {
                Utils.GetPhysicallyInstalledSystemMemory(out var memKb);

                if ((_cameraManager.SelectedLayerIndex + 1) * _batchConfiguration.BatchLength >
                    (int) memKb / (1024 * 1024) * 1000)
                {
                    _mainView.ShowMessage(
                        "Potential risk of running out of memory with configured batch. Reduce batch length or number of layers",
                        "High Memory Usage Warning");
                    return;
                }
            }
            */
            _mainView.ViewModeChanged(ViewMode.Recording);
            StartBatch(_batchConfiguration);

            _isStopped = false;
            _mainView.BatchView.EnableRecord = false;
            _mainView.BatchView.EnableStop = true;
            _mainView.BatchView.EnablePosition = false;
            _mainView.BatchView.EnableClear = false;
            _mainView.BatchView.EnableSave = false;
            _mainView.BatchView.MaxPosition = _batchConfiguration.BatchLength - 1;

            lock (SyncLock)
            {
				_recordContainer = null;
	            // ReSharper disable once RedundantArgumentDefaultValue
                int length = _applicationSettings.SaveRate > 1
                    ? _batchConfiguration.BatchLength / _applicationSettings.SaveRate + 1
                    : _batchConfiguration.BatchLength;
                _recordContainer = new RecordContainer(length);
            }
            _freqUpdatedRequired = false;
	        _cameraManager.StartGrabbing(20);
        }

        /// <summary>
        /// Starts a new batch.
        /// </summary>
        /// <param name="configuration">Batch configuration.</param>
        private void StartBatch(BatchConfiguration configuration)
        {
            Flush();
            _mainView.BatchView.EnableClear = false;
            // Pass the ***intended*** frequency set up in the live mode.
            if (_freqUpdatedRequired)
	        {
		        _cameraManager.SetFreq(configuration.TriggerFrequency, configuration.TriggerMode == TriggerMode.External);
	        }
            // _cameraManager.StartGrabbing(10);

            // Make sure we get all the profiles.
            _cameraManager.IsInfiniteQueueSizeEnabled = true;
        }

        /// <summary>
        /// Stops the batch.
        /// </summary>
        private void StopBatch()
        {
            Flush();
            _cameraManager.SetFreq(Session.RealTimeCameraSetting.Freq, Session.RealTimeCameraSetting.IsExternalPulsingEnabled);
            // Make sure UI does not get stuck.
            _cameraManager.IsInfiniteQueueSizeEnabled = false;
        }

        /// <summary>
        /// Ensures camera buffer is empty.
        /// </summary>
        private void Flush()
        {
            const double timeout = 3000;
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (!_cameraManager.IsCameraBufferEmpty)
            {
                if (stopwatch.Elapsed.TotalMilliseconds > timeout)
                    break;
            }
	        _cameraManager.FlushQueue(100);
        }

        /// <summary>
        /// Main view is shown for the first time. The UI states may be set.
        /// </summary>
        private void OnWindowShown()
        {
            _mainView.BatchView.EnableRecord = false;
            _mainView.BatchView.EnablePosition = false;
            _mainView.BatchView.EnableStop = false;
            _mainView.BatchView.EnableSave = false;
            _mainView.BatchView.EnableClear = false;
            _mainView.BatchView.MaxPosition = _batchConfiguration.BatchLength - 1;
        }

        /// <summary>
        /// Point cloud has been received from the camera. If a recording is active, add the profile to record container.
        /// </summary>
        /// <param name="profile">The profile.</param>
        private void OnPointCloudReceived(Profile profile)
        {
            lock (SyncLock)
            {
                if (_recordContainer == null || _isStopped)
                {
                    return;
                }

                if (_recordContainer.IsCollected)
                {
                    return;
                }

                _recordContainer.AddProfile(profile);

                if (_recordContainer.IsCollected)
                {
                    StopRecording(true);
                    _mainView.BatchView.EnableRecord = true;
	                _cameraManager.FlushQueue(100);
                }
            }

            int position = _recordContainer.Count * _applicationSettings.SaveRate - 1;
            if (position > _batchConfiguration.BatchLength)
                position = _batchConfiguration.BatchLength - 1;
            _mainView.BatchView.Position = position;
        }

        /// <summary>
        /// Finish the active recording. Update the view accordingly.
        /// </summary>
        public void StopRecording(bool show=true)
        {
	        _cameraManager.StopGrabbing(20);
            _isStopped = true;
            _mainView.BatchView.EnableRecord = false;
            _mainView.BatchView.EnableStop = false;
            _mainView.BatchView.EnableSave = true;
            _mainView.BatchView.EnableClear = true;
            _mainView.BatchView.EnablePosition = true;
            _mainView.ViewModeChanged(ViewMode.Batch);

            lock (SyncLock)
            {
                int max;
                if (_recordContainer == null)
                {
                    max = 0;
                }
                else if (_applicationSettings.SaveRate > 1)
                {
                    if (_recordContainer.Count * _applicationSettings.SaveRate > _batchConfiguration.BatchLength)
                    {
                        max = _batchConfiguration.BatchLength - 1;
                    }
                    else
                        max = _recordContainer.Count * _applicationSettings.SaveRate;
                }
                else
                    max = _recordContainer.Count - 1;

                _mainView.BatchView.MaxPosition = max;
            }

            if (show)
            {
                _mainView.BatchView.IsBatchVisualizerVisible = true;
                _mainView.ShowRecording(_recordContainer, _batchConfiguration, SensorParameterStore.GetInstance().AveragePixelWidth);
            }

            if (_recordContainer != null && _recordContainer.Count > 0)
            {
                var profiles = new List<ProfileInfo>
                {
                    new ProfileInfo(_mainView.MinLayerId + 1, _recordContainer.GetProfile(_mainView.MinLayerId, _recordContainer.Count - 1))
                };
                _mainView.SetProfile(profiles);
            }
        }

        /// <summary>
        /// Configure the batch mode.
        /// </summary>
        private void ConfigureBatchMode()
        {
            IBatchConfigurationView batchConfiguration = new BatchConfigurationView();

           // BatchConfiguration newBatchConfiguration;
            if (batchConfiguration.Display(_batchConfiguration, out BatchConfiguration newBatchConfiguration))
            {
                _batchConfiguration = newBatchConfiguration;
                _mainView.BatchView.EnableRecord = true;
                _freqUpdatedRequired = true;
                _mainView.BatchView.IsConfigured = true;
            }
        }
    }
}