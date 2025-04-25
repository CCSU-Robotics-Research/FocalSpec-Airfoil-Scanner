namespace FocalSpec.GuiExample.View
{
    partial class BatchModeView
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._groupBoxBatch = new System.Windows.Forms.GroupBox();
            this.checkBoxShowHideBatchVisualizer = new System.Windows.Forms.CheckBox();
            this._clear = new System.Windows.Forms.Button();
            this._positionText = new System.Windows.Forms.Label();
            this._position = new System.Windows.Forms.TrackBar();
            this._save = new System.Windows.Forms.Button();
            this._stop = new System.Windows.Forms.Button();
            this._start = new System.Windows.Forms.Button();
            this._configure = new System.Windows.Forms.Button();
            this._groupBoxBatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._position)).BeginInit();
            this.SuspendLayout();
            // 
            // _groupBoxBatch
            // 
            this._groupBoxBatch.Controls.Add(this.checkBoxShowHideBatchVisualizer);
            this._groupBoxBatch.Controls.Add(this._clear);
            this._groupBoxBatch.Controls.Add(this._positionText);
            this._groupBoxBatch.Controls.Add(this._position);
            this._groupBoxBatch.Controls.Add(this._save);
            this._groupBoxBatch.Controls.Add(this._stop);
            this._groupBoxBatch.Controls.Add(this._start);
            this._groupBoxBatch.Controls.Add(this._configure);
            this._groupBoxBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this._groupBoxBatch.Location = new System.Drawing.Point(0, 0);
            this._groupBoxBatch.Name = "_groupBoxBatch";
            this._groupBoxBatch.Size = new System.Drawing.Size(185, 256);
            this._groupBoxBatch.TabIndex = 0;
            this._groupBoxBatch.TabStop = false;
            this._groupBoxBatch.Text = "Batch";
            // 
            // checkBoxShowHideBatchVisualizer
            // 
            this.checkBoxShowHideBatchVisualizer.AutoSize = true;
            this.checkBoxShowHideBatchVisualizer.Location = new System.Drawing.Point(18, 176);
            this.checkBoxShowHideBatchVisualizer.Name = "checkBoxShowHideBatchVisualizer";
            this.checkBoxShowHideBatchVisualizer.Size = new System.Drawing.Size(131, 17);
            this.checkBoxShowHideBatchVisualizer.TabIndex = 7;
            this.checkBoxShowHideBatchVisualizer.Text = "Show Batch Visualizer";
            this.checkBoxShowHideBatchVisualizer.UseVisualStyleBackColor = true;
            this.checkBoxShowHideBatchVisualizer.CheckedChanged += new System.EventHandler(this.checkBoxShowHideBatchVisualizer_CheckedChanged);
            // 
            // _clear
            // 
            this._clear.Location = new System.Drawing.Point(18, 77);
            this._clear.Name = "_clear";
            this._clear.Size = new System.Drawing.Size(75, 23);
            this._clear.TabIndex = 6;
            this._clear.Text = "Clear";
            this._clear.UseVisualStyleBackColor = true;
            this._clear.Click += new System.EventHandler(this._clear_Click);
            // 
            // _positionText
            // 
            this._positionText.Location = new System.Drawing.Point(15, 140);
            this._positionText.Name = "_positionText";
            this._positionText.Size = new System.Drawing.Size(84, 23);
            this._positionText.TabIndex = 5;
            this._positionText.Text = "-";
            this._positionText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _position
            // 
            this._position.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._position.Location = new System.Drawing.Point(18, 106);
            this._position.Name = "_position";
            this._position.Size = new System.Drawing.Size(159, 45);
            this._position.TabIndex = 4;
            this._position.Scroll += new System.EventHandler(this._position_Scroll);
            // 
            // _save
            // 
            this._save.Location = new System.Drawing.Point(102, 77);
            this._save.Name = "_save";
            this._save.Size = new System.Drawing.Size(75, 23);
            this._save.TabIndex = 3;
            this._save.Text = "Save";
            this._save.UseVisualStyleBackColor = true;
            this._save.Click += new System.EventHandler(this._save_Click);
            // 
            // _stop
            // 
            this._stop.Location = new System.Drawing.Point(102, 48);
            this._stop.Name = "_stop";
            this._stop.Size = new System.Drawing.Size(75, 23);
            this._stop.TabIndex = 2;
            this._stop.Text = "Stop";
            this._stop.UseVisualStyleBackColor = true;
            this._stop.Click += new System.EventHandler(this._stop_Click);
            // 
            // _start
            // 
            this._start.Location = new System.Drawing.Point(18, 48);
            this._start.Name = "_start";
            this._start.Size = new System.Drawing.Size(75, 23);
            this._start.TabIndex = 1;
            this._start.Text = "Start";
            this._start.UseVisualStyleBackColor = true;
            this._start.Click += new System.EventHandler(this._start_Click);
            // 
            // _configure
            // 
            this._configure.Location = new System.Drawing.Point(18, 19);
            this._configure.Name = "_configure";
            this._configure.Size = new System.Drawing.Size(75, 23);
            this._configure.TabIndex = 0;
            this._configure.Text = "Configure";
            this._configure.UseVisualStyleBackColor = true;
            this._configure.Click += new System.EventHandler(this._configure_Click);
            // 
            // BatchModeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._groupBoxBatch);
            this.Name = "BatchModeView";
            this.Size = new System.Drawing.Size(185, 256);
            this._groupBoxBatch.ResumeLayout(false);
            this._groupBoxBatch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._position)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox _groupBoxBatch;
        private System.Windows.Forms.Button _save;
        private System.Windows.Forms.Button _stop;
        private System.Windows.Forms.Button _start;
        private System.Windows.Forms.Button _configure;
        private System.Windows.Forms.Label _positionText;
        private System.Windows.Forms.TrackBar _position;
        private System.Windows.Forms.Button _clear;
        private System.Windows.Forms.CheckBox checkBoxShowHideBatchVisualizer;
    }
}
