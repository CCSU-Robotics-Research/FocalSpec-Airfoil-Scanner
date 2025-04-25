namespace FocalSpec.GuiExample.View
{
    partial class RefractionView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxLayer = new System.Windows.Forms.ComboBox();
            this.textBoxRefractiveIndex = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Layer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Effective Refractive index";
            // 
            // comboBoxLayer
            // 
            this.comboBoxLayer.FormattingEnabled = true;
            this.comboBoxLayer.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.comboBoxLayer.Location = new System.Drawing.Point(151, 16);
            this.comboBoxLayer.Name = "comboBoxLayer";
            this.comboBoxLayer.Size = new System.Drawing.Size(77, 21);
            this.comboBoxLayer.TabIndex = 2;
            this.comboBoxLayer.SelectedIndexChanged += new System.EventHandler(this.ComboBoxLayer_SelectedIndexChanged);
            // 
            // textBoxRefractiveIndex
            // 
            this.textBoxRefractiveIndex.Location = new System.Drawing.Point(151, 44);
            this.textBoxRefractiveIndex.Name = "textBoxRefractiveIndex";
            this.textBoxRefractiveIndex.Size = new System.Drawing.Size(77, 20);
            this.textBoxRefractiveIndex.TabIndex = 3;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(153, 81);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 10;
            this.buttonOk.Text = "Close";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // RefractionView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 115);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxRefractiveIndex);
            this.Controls.Add(this.comboBoxLayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RefractionView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Effective Refraction Index";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RefractionView_FormClosing);
            this.Shown += new System.EventHandler(this.RefractionView_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxLayer;
        private System.Windows.Forms.TextBox textBoxRefractiveIndex;
        private System.Windows.Forms.Button buttonOk;
    }
}