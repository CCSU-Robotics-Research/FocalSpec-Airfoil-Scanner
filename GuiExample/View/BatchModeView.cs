// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchModeView.cs" company="FocalSpec Oy">
//   FocalSpec Oy 2016-
// </copyright>
// <summary>
//   WinForms implementation of the BatchMode view.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace FocalSpec.GuiExample.View
{
    using System.Windows.Forms;

    /// <summary>WinForms implementation of the BatchMode view.</summary>
    public partial class BatchModeView : UserControl, IBatchModeView
    {
        /// <summary>Initializes a new instance of the <see cref="BatchModeView"/> class.</summary>
        public BatchModeView()
        {
            InitializeComponent();
        }

        /// <summary>Event fires when user wants to configure the batch recorder.</summary>
        public event ConfigureHandler OnConfigure;

        /// <summary>Event fires when user requests to start recording.</summary>
        public event StartHandler OnStart;

        /// <summary>Event fires when user requests to stop recording.</summary>
        public event StopHandler OnStop;

        /// <summary>Event fires when user requests to stop recording.</summary>
        public event SaveHandler OnSave;

        public event ClearHandler OnClear;

        /// <summary>Event fires when user browses recorded profiles.</summary>
        public event PositionBrowsedHandler OnPositionBrowsed;

        /// <summary>Event fires when user selects to show or hide batch visualizer.</summary>
        public event EventHandler OnShowHideBatchVisualizer;

        public bool IsConfigured { get; set; }

        /// <summary>Sets a value indicating whether the recording related buttons are enabled.</summary>
        public bool EnableRecord
        {
            set
            {
                Invoke((MethodInvoker)delegate
                { 
                    _start.Enabled = value;
                });
            }
        }

        /// <summary>Sets a value indicating whether the record saving is enabled.</summary>
        public bool EnableSave
        {
            set
            {
                Invoke((MethodInvoker)delegate
                {
                    _save.Enabled = value;
                });
            }
        }

	    public bool EnableConfigure
	    {
		    set
		    {
			    Invoke((MethodInvoker)delegate
				{
					_configure.Enabled = value;
				});
		    }
	    }

        /// <summary>Sets a value indicating whether the clear recording is enabled.</summary>
        public bool EnableClear
        {
            set
            {
                Invoke((MethodInvoker)delegate
                {
                    _clear.Enabled = value;
                });
            }
        }

        /// <summary>Sets a value indicating whether the stop recording is enabled.</summary>
        public bool EnableStop
        {
            set
            {
                Invoke((MethodInvoker)delegate
                {
                    _stop.Enabled = value;
                });
            }
        }

        /// <summary>Sets a value indicating whether the position tracking is enabled.</summary>
        /// <value>true if enable position, false if not.</value>
        public bool EnablePosition
        {
            set
            {
                Invoke((MethodInvoker)delegate
                {
                    _position.Enabled = value;
                    if (_position.Enabled)
                        _position.Minimum = 0;
                });
            }
        }

        /// <inheritdoc />
        public bool EnableBatch
        {
            set
            {
                if (!IsHandleCreated) return;

                BeginInvoke((MethodInvoker)delegate
                {
                    checkBoxShowHideBatchVisualizer.Enabled = value;
                    _configure.Enabled = value;
                    _start.Enabled = (value && IsConfigured);
                });

                if (value == false)
                    IsBatchVisualizerVisible = false;
            }
        }

        public bool IsBatchVisualizerVisible
        {
            get => checkBoxShowHideBatchVisualizer.Checked;
            set
            {
                if (!IsHandleCreated) return;

                BeginInvoke((MethodInvoker) delegate
                {
                    checkBoxShowHideBatchVisualizer.Checked = value;
                });
            }
        }

        /// <summary>Sets the zero-based index of the frame.</summary>
        public int Position
        {
            set
            {
                Invoke((MethodInvoker)delegate
                {
                    if (value < _position.Minimum || value > _position.Maximum) return;
                    _positionText.Text = value.ToString();
                    _position.Value = value;
                });
            }
        }

        /// <summary>Sets the maximum position in the track bar.</summary>
        public int MaxPosition
        {
            set
            {
                Invoke((MethodInvoker)delegate
                {
                    _position.Maximum = value;
                });
            }
        }

        /// <summary>Event handler, user has clicked configure button.</summary>
        /// <param name="sender">Source of the event. </param>
        /// <param name="e">     Event information. </param>
        private void _configure_Click(object sender, EventArgs e)
        {
            OnConfigure?.Invoke();
        }

        /// <summary>Event handler, user has clicked start button.</summary>
        /// <param name="sender">Source of the event. </param>
        /// <param name="e">     Event information. </param>
        private void _start_Click(object sender, EventArgs e)
        {
            if (OnStart == null) return;
            Cursor.Current = Cursors.WaitCursor;
				
            OnStart();
				
            Cursor.Current = Cursors.Default;
        }

        /// <summary>Event handler, user has clicked save button.</summary>
        /// <param name="sender">Source of the event. </param>
        /// <param name="e">     Event information. </param>
        private void _save_Click(object sender, EventArgs e)
        {
            if (OnSave == null) return;
            var save = new SaveFileDialog
            {
                FileName = "pointcloud.asc",
                Filter = @"Point cloud in ASC format (*.asc)|*.asc|Point cloud in PCD format (*.pcd)|*.pcd|2D grayscale bitmap (*.bmp)|*.bmp|All files (*.*)|*.*"
            };

            if (save.ShowDialog() != DialogResult.OK) return;

            if (string.IsNullOrWhiteSpace(save.FileName)) return;

            Cursor.Current = Cursors.WaitCursor;

            OnSave(save.FileName);

            Cursor.Current = Cursors.Default;
        }

        /// <summary>Event handler, user has pressed stop button.</summary>
        /// <param name="sender">Source of the event. </param>
        /// <param name="e">     Event information. </param>
        private void _stop_Click(object sender, EventArgs e)
        {
            OnStop?.Invoke(true);
        }

        /// <summary>Event handler, user has changed the browser track bar position.</summary>
        /// <param name="sender">Source of the event. </param>
        /// <param name="e">     Event information. </param>
        private void _position_Scroll(object sender, EventArgs e)
        {
            _positionText.Text = _position.Value.ToString();

            OnPositionBrowsed?.Invoke(_position.Value);
        }

        /// <summary>Event handler, user has pressed clear button.</summary>
        /// <param name="sender">Source of the event. </param>
        /// <param name="e">     Event information. </param>
        private void _clear_Click(object sender, EventArgs e)
        {
            if (OnClear == null) return;
            Cursor.Current = Cursors.WaitCursor;
				
            OnClear();
				
            Cursor.Current = Cursors.Default;
        }

        private void checkBoxShowHideBatchVisualizer_CheckedChanged(object sender, EventArgs e)
        {
            OnShowHideBatchVisualizer?.Invoke(this, e);
        }
    }
}