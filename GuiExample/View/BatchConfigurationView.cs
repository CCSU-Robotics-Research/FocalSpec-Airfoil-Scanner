// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BatchConfiguration.cs" company="FocalSpec Oy">
//   FocalSpec Oy 2016-
// </copyright>
// <summary>
//   Winforms implementation of the batch configuration dialog.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FocalSpec.GuiExample.View
{
    using System;
    using System.Drawing;
    using System.Globalization;
    using System.Windows.Forms;
    using Model;
    using Model.BatchMode;

    /// <summary>Winforms implementation of the batch configuration dialog.</summary>
    public partial class BatchConfigurationView : Form, IBatchConfigurationView
    {
        /// <summary>Flag is set to true, if window is closed by pressing Ok button.</summary>
        private bool _accepted;

        private bool _isExternalScanStepLengthValid = true;
        private bool _isInternalLineSpeedValid = true;
        private bool _isInternalTriggeringFreqValid = true;
        private bool _isBatchLengthValid = true;
        // Used to remember internal trigger freq. when switching between external and internal triggering.
        private int _internalTriggerFreq;

        private static int _maxBatchLength;

        /// <summary>The new batch configuration, which is updated, when user makes changes.</summary>
        private BatchConfiguration _newBatchConfiguration;

        public BatchConfigurationView()
        {
            Utils.GetPhysicallyInstalledSystemMemory(out var memKb);
            // This is not considering yet number of layers or possible profiles in queue.
            _maxBatchLength = (int)memKb/(1024*1024)*1000;
            InitializeComponent();
        }

        public bool Display(BatchConfiguration batchConfiguration, out BatchConfiguration newBatchConfiguration)
        {
            _newBatchConfiguration = batchConfiguration.Clone();
            newBatchConfiguration = _newBatchConfiguration;

            ComboBoxTrigger.SelectedIndex = _newBatchConfiguration.TriggerMode == TriggerMode.Internal ? 1 : 0;

            UpdateReadonlyFields(_newBatchConfiguration);

            _accepted = false;
            ShowDialog();
            return _accepted;
        }

        /// <summary>Event handler, user has pressed Ok button.</summary>
        /// <param name="sender">Source of the event. </param>
        /// <param name="e">     Event information. </param>
        private void _ok_Click(object sender, EventArgs e)
        {
            if (!_isBatchLengthValid)
                return;

            switch (_newBatchConfiguration.TriggerMode)
            {
                case TriggerMode.Internal:
                    if (!_isInternalLineSpeedValid || !_isInternalTriggeringFreqValid)
                    {
                        return;
                    }
                    break;
                case TriggerMode.External:
                    if (!_isExternalScanStepLengthValid)
                    {
                        return;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _accepted = true;
            Close();
        }

        /// <summary>Event handler, user has clicked cancel button.</summary>
        /// <param name="sender">Source of the event. </param>
        /// <param name="e">     Event information. </param>
        private void _cancel_Click(object sender, EventArgs e)
        {
            _accepted = false;
            Close();
        }

        /// <summary>
        /// Updates the readonly fields to UI.
        /// </summary>
        private void UpdateReadonlyFields(BatchConfiguration configuration)
        {
            double stepLength;

            switch (configuration.TriggerMode)
            {
                case TriggerMode.Internal:
                    stepLength = configuration.LineSpeedMmPerS / configuration.TriggerFrequency;
                    TextBoxScanStepLength.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.000}", stepLength);
                    break;
                case TriggerMode.External:
                    // Already validated.
                    double.TryParse(TextBoxScanStepLength.Text, out stepLength);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            double maxScanLength = stepLength * configuration.BatchLength;
            TextBoxMaxScanLength.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.000}", maxScanLength);
        }

        /// <summary>Event handler, user has changed the trigger selection. Show the group box related to
        /// internal or external trigger settings, according to the selection.
        /// </summary>
        /// <param name="sender">Source of the event. </param>
        /// <param name="e">     Cancel event information. </param>
        private void _sensorTrigger_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox selection = sender as ComboBox;

            if (selection == null)
            {
                return;
            }

            TextBoxBatchLength.Text = _newBatchConfiguration.BatchLength.ToString(CultureInfo.InvariantCulture);
         

            switch (selection.SelectedIndex)
            {
                case 0:
                    _newBatchConfiguration.TriggerMode = TriggerMode.External;

                    TextBoxLineSpeed.Enabled = false;
                    // ReSharper disable once LocalizableElement
                    TextBoxLineSpeed.Text = _newBatchConfiguration.LineSpeed.ToString(CultureInfo.InvariantCulture);

                    // Commented out, for external triggering this value is used for adjusting image height (z-range)
                    //  TextBoxTriggerFreq.Enabled = false;
                    // ReSharper disable once LocalizableElement
                    // TextBoxTriggerFreq.Text = "N/A";
                    // If user previously selected external trigger, we need to put back the original value.

                    TextBoxTriggerFreq.Enabled = true;
                    TextBoxTriggerFreq.Text = _newBatchConfiguration.TriggerFrequency.ToString(CultureInfo.InvariantCulture);

                    if (_newBatchConfiguration.TriggerFrequency == 0)
                    {
                        if (_internalTriggerFreq == 0)
                            // Internal trigger freq. not yet assigned by the user. Use default.
                            _newBatchConfiguration.TriggerFrequency = Defines.DefaultTriggerFrequency;
                        else
                            // Internal trigger freq. already assigned by the user.
                            _newBatchConfiguration.TriggerFrequency = _internalTriggerFreq;

                        TextBoxTriggerFreq.Text = _newBatchConfiguration.TriggerFrequency.ToString(CultureInfo.InvariantCulture);

                        // Together this and TextBoxTriggerFreq.Enter event handler (TextBoxTriggerFreq_Enter) ensure that the textbox gets validated.
                        TextBoxTriggerFreq.Focus();
                    }
                    
                    TextBoxScanStepLength.Enabled = true;
                    TextBoxScanStepLength.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.000}", _newBatchConfiguration.ScanStepLength);

                    break;
                case 1:
                    _newBatchConfiguration.TriggerMode = TriggerMode.Internal;

                    TextBoxLineSpeed.Enabled = true;
                    TextBoxLineSpeed.Text = _newBatchConfiguration.LineSpeed.ToString(CultureInfo.InvariantCulture);

                    TextBoxTriggerFreq.Enabled = true;
                    TextBoxTriggerFreq.Text = _newBatchConfiguration.TriggerFrequency.ToString(CultureInfo.InvariantCulture);

                    TextBoxScanStepLength.Enabled = false;
                    TextBoxScanStepLength.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.000}", _newBatchConfiguration.LineSpeed * _newBatchConfiguration.BatchLength);

                    //  If user previously selected external trigger, we need to put back the original value.
                   //   if (_newBatchConfiguration.TriggerFrequency == Defines.ExternalTriggering)
                   if (_newBatchConfiguration.TriggerFrequency == 0)
                   {
                        if (_internalTriggerFreq == 0)
                            // Internal trigger freq. not yet assigned by the user. Use default.
                            _newBatchConfiguration.TriggerFrequency = Defines.DefaultTriggerFrequency;
                        else
                            // Internal trigger freq. already assigned by the user.
                            _newBatchConfiguration.TriggerFrequency = _internalTriggerFreq;
                        
                        TextBoxTriggerFreq.Text = _newBatchConfiguration.TriggerFrequency.ToString(CultureInfo.InvariantCulture);

                        // Together this and TextBoxTriggerFreq.Enter event handler (TextBoxTriggerFreq_Enter) ensure that the textbox gets validated.
                        TextBoxTriggerFreq.Focus();
                    }
                    break;
            }

            UpdateReadonlyFields(_newBatchConfiguration);
        }

        /// <summary>Checks if the string is positive double (1.0, 2.0, 3.0, ...).</summary>
        /// <param name="text"> The text to be validated.</param>
        /// <param name="value">Extracted value.</param>
        /// <returns>Is text positive double.</returns>
        private bool IsPositiveDouble(string text, out double value)
        {
            if (!double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                return false;
            }

            if (value <= 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>Checks if the string is positive integer (1, 2, 3, ...).</summary>
        /// <param name="text"> The text to be validated. </param>
        /// <param name="value">Extracted value. </param>
        /// <returns>Is text positive integer.</returns>
        private bool IsPositiveInteger(string text, out int value)
        {
            if (!int.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                return false;
            }

            if (value < 1)
            {
                return false;
            }

            return true;
        }

        private void TextBoxScanStepLength_KeyUp(object sender, KeyEventArgs e)
        {
            string text = TextBoxScanStepLength.Text;
            double value;

            if (!IsPositiveDouble(text, out value))
            {
                TextBoxScanStepLength.BackColor = Color.LightCoral;
                _isExternalScanStepLengthValid = false;
            }
            else
            {
                TextBoxScanStepLength.BackColor = SystemColors.Window;
                _newBatchConfiguration.ScanStepLength = value;
                UpdateReadonlyFields(_newBatchConfiguration);
                _isExternalScanStepLengthValid = true;
            }
        }

        private void TextBoxLineSpeed_KeyUp(object sender, KeyEventArgs e)
        {
            string text = TextBoxLineSpeed.Text;
            double value;

            if (!IsPositiveDouble(text, out value))
            {
                TextBoxLineSpeed.BackColor = Color.LightCoral;
                _isInternalLineSpeedValid = false;
            }
            else
            {
                TextBoxLineSpeed.BackColor = SystemColors.Window;
                _newBatchConfiguration.LineSpeed = value;
                UpdateReadonlyFields(_newBatchConfiguration);
                _isInternalLineSpeedValid = true;
            }
        }

        private void TextBoxTriggerFreq_KeyUp(object sender, KeyEventArgs e)
        {
            string text = TextBoxTriggerFreq.Text;

            if (IsPositiveInteger(text, out int value))
            {
                TextBoxTriggerFreq.BackColor = SystemColors.Window;
                _newBatchConfiguration.TriggerFrequency = value;
                // if (_newBatchConfiguration.TriggerMode == TriggerMode.Internal)
				// Remember internal trigger freq. so that we can show it again if user switches between external and internal triggering.
                _internalTriggerFreq = value;

                UpdateReadonlyFields(_newBatchConfiguration);
                _isInternalTriggeringFreqValid = true;
            }
            else
            {
                TextBoxTriggerFreq.BackColor = Color.LightCoral;
                _isInternalTriggeringFreqValid = false;
            }
        }

        private void TextBoxTriggerFreq_Enter(object sender, EventArgs e)
        {
            TextBoxTriggerFreq_KeyUp(null, null);
        }

        private void TextBoxBatchLength_KeyUp(object sender, KeyEventArgs e)
        {
            string text = TextBoxBatchLength.Text;
            int value;

            if (!IsPositiveInteger(text, out value) || value > _maxBatchLength)
            {
                TextBoxBatchLength.BackColor = Color.LightCoral;
                _isBatchLengthValid = false;
            }
            else
            {
                TextBoxBatchLength.BackColor = SystemColors.Window;
                _newBatchConfiguration.BatchLength = value;
                UpdateReadonlyFields(_newBatchConfiguration);
                _isBatchLengthValid = true;
            }
        }
    }
}