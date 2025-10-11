namespace FocalSpec.GuiExample.View
{
    partial class MainView
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainView));
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelDataVisualizers = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanelCharts = new System.Windows.Forms.TableLayoutPanel();
            this._profileChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this._thicknessChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanelInfo = new System.Windows.Forms.TableLayoutPanel();
            this.labelViewMode = new System.Windows.Forms.Label();
            this.labelFrameIndex = new System.Windows.Forms.Label();
            this.elementHostBatchVisualizer = new System.Windows.Forms.Integration.ElementHost();
            this.batchVisualizer2DUc3 = new FocalSpec.GuiExample.View.BatchVisualizer2DUc();
            this.panelSettings = new System.Windows.Forms.Panel();
            this._batchMode = new FocalSpec.GuiExample.View.BatchModePresenter();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_SaveLog = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn_ConnectCTRL = new System.Windows.Forms.Button();
            this.btn_ScanCTRLS = new System.Windows.Forms.Button();
            this.btn_RapContinue = new System.Windows.Forms.Button();
            this.btn_StopRap = new System.Windows.Forms.Button();
            this.btn_StartRAP = new System.Windows.Forms.Button();
            this.listView_Controllers = new System.Windows.Forms.ListView();
            this.IPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ControllerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRecipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRecipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRecipeAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelDataVisualizers.SuspendLayout();
            this.tableLayoutPanelCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._profileChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._thicknessChart)).BeginInit();
            this.tableLayoutPanelInfo.SuspendLayout();
            this.panelSettings.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 2;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 265F));
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.tableLayoutPanelDataVisualizers, 1, 2);
            this.tableLayoutPanelMain.Controls.Add(this.panelSettings, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.panel5, 1, 1);
            this.tableLayoutPanelMain.Controls.Add(this.menuStrip1, 0, 0);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 123F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(1354, 860);
            this.tableLayoutPanelMain.TabIndex = 0;
            // 
            // tableLayoutPanelDataVisualizers
            // 
            this.tableLayoutPanelDataVisualizers.ColumnCount = 3;
            this.tableLayoutPanelDataVisualizers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelDataVisualizers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tableLayoutPanelDataVisualizers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelDataVisualizers.Controls.Add(this.tableLayoutPanelCharts, 0, 0);
            this.tableLayoutPanelDataVisualizers.Controls.Add(this.tableLayoutPanelInfo, 0, 1);
            this.tableLayoutPanelDataVisualizers.Controls.Add(this.elementHostBatchVisualizer, 2, 0);
            this.tableLayoutPanelDataVisualizers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelDataVisualizers.Location = new System.Drawing.Point(268, 148);
            this.tableLayoutPanelDataVisualizers.Name = "tableLayoutPanelDataVisualizers";
            this.tableLayoutPanelDataVisualizers.RowCount = 2;
            this.tableLayoutPanelDataVisualizers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelDataVisualizers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanelDataVisualizers.Size = new System.Drawing.Size(1083, 709);
            this.tableLayoutPanelDataVisualizers.TabIndex = 0;
            // 
            // tableLayoutPanelCharts
            // 
            this.tableLayoutPanelCharts.ColumnCount = 1;
            this.tableLayoutPanelCharts.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelCharts.Controls.Add(this._profileChart, 0, 0);
            this.tableLayoutPanelCharts.Controls.Add(this._thicknessChart, 0, 1);
            this.tableLayoutPanelCharts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelCharts.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanelCharts.Name = "tableLayoutPanelCharts";
            this.tableLayoutPanelCharts.RowCount = 2;
            this.tableLayoutPanelCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelCharts.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 0F));
            this.tableLayoutPanelCharts.Size = new System.Drawing.Size(533, 683);
            this.tableLayoutPanelCharts.TabIndex = 0;
            // 
            // _profileChart
            // 
            chartArea5.AxisX.Title = "X [mm]";
            chartArea5.AxisY.Title = "Z [mm]";
            chartArea5.Name = "ChartArea1";
            this._profileChart.ChartAreas.Add(chartArea5);
            this._profileChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._profileChart.Location = new System.Drawing.Point(3, 3);
            this._profileChart.Name = "_profileChart";
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series7.MarkerSize = 2;
            series7.Name = "profile0";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series8.MarkerSize = 2;
            series8.Name = "intensity0";
            this._profileChart.Series.Add(series7);
            this._profileChart.Series.Add(series8);
            this._profileChart.Size = new System.Drawing.Size(527, 677);
            this._profileChart.TabIndex = 5;
            this._profileChart.Text = "_profileChart";
            this._profileChart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this._profileChart_AxisViewChanged);
            this._profileChart.Paint += new System.Windows.Forms.PaintEventHandler(this._profileChart_Paint);
            // 
            // _thicknessChart
            // 
            chartArea6.AxisX.Title = "X [mm]";
            chartArea6.AxisY.Title = "Z [mm]";
            chartArea6.Name = "ChartArea2";
            this._thicknessChart.ChartAreas.Add(chartArea6);
            this._thicknessChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._thicknessChart.Location = new System.Drawing.Point(3, 686);
            this._thicknessChart.Name = "_thicknessChart";
            series9.ChartArea = "ChartArea2";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series9.MarkerSize = 2;
            series9.Name = "Layer 1";
            this._thicknessChart.Series.Add(series9);
            this._thicknessChart.Size = new System.Drawing.Size(527, 1);
            this._thicknessChart.TabIndex = 5;
            this._thicknessChart.Text = "_thicknessChart";
            this._thicknessChart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this._thicknessChart_AxisViewChanged);
            this._thicknessChart.CustomizeLegend += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.CustomizeLegendEventArgs>(this._thicknessChart_CustomizeLegend);
            this._thicknessChart.Paint += new System.Windows.Forms.PaintEventHandler(this._thicknessChart_Paint);
            // 
            // tableLayoutPanelInfo
            // 
            this.tableLayoutPanelInfo.ColumnCount = 2;
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanelInfo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelInfo.Controls.Add(this.labelViewMode, 0, 0);
            this.tableLayoutPanelInfo.Controls.Add(this.labelFrameIndex, 1, 0);
            this.tableLayoutPanelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelInfo.Location = new System.Drawing.Point(0, 689);
            this.tableLayoutPanelInfo.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelInfo.Name = "tableLayoutPanelInfo";
            this.tableLayoutPanelInfo.RowCount = 1;
            this.tableLayoutPanelInfo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelInfo.Size = new System.Drawing.Size(539, 20);
            this.tableLayoutPanelInfo.TabIndex = 0;
            // 
            // labelViewMode
            // 
            this.labelViewMode.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelViewMode.AutoSize = true;
            this.labelViewMode.Location = new System.Drawing.Point(4, 4);
            this.labelViewMode.Margin = new System.Windows.Forms.Padding(4);
            this.labelViewMode.Name = "labelViewMode";
            this.labelViewMode.Size = new System.Drawing.Size(48, 12);
            this.labelViewMode.TabIndex = 7;
            this.labelViewMode.Text = "Realtime";
            // 
            // labelFrameIndex
            // 
            this.labelFrameIndex.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.labelFrameIndex.AutoSize = true;
            this.labelFrameIndex.Location = new System.Drawing.Point(470, 4);
            this.labelFrameIndex.Margin = new System.Windows.Forms.Padding(4);
            this.labelFrameIndex.Name = "labelFrameIndex";
            this.labelFrameIndex.Size = new System.Drawing.Size(65, 12);
            this.labelFrameIndex.TabIndex = 6;
            this.labelFrameIndex.Text = "Frame Index";
            // 
            // elementHostBatchVisualizer
            // 
            this.elementHostBatchVisualizer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHostBatchVisualizer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.elementHostBatchVisualizer.Location = new System.Drawing.Point(547, 3);
            this.elementHostBatchVisualizer.Name = "elementHostBatchVisualizer";
            this.elementHostBatchVisualizer.Size = new System.Drawing.Size(533, 683);
            this.elementHostBatchVisualizer.TabIndex = 8;
            this.elementHostBatchVisualizer.Text = "elementHostBatchVisualizer";
            this.elementHostBatchVisualizer.Child = this.batchVisualizer2DUc3;
            // 
            // panelSettings
            // 
            this.panelSettings.Controls.Add(this._batchMode);
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSettings.Location = new System.Drawing.Point(3, 148);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(259, 709);
            this.panelSettings.TabIndex = 1;
            // 
            // _batchMode
            // 
            this._batchMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._batchMode.IsBatchVisualizerVisible = false;
            this._batchMode.IsConfigured = false;
            this._batchMode.Location = new System.Drawing.Point(6, 656);
            this._batchMode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._batchMode.Name = "_batchMode";
            this._batchMode.Size = new System.Drawing.Size(249, 242);
            this._batchMode.TabIndex = 5;
            this._batchMode.Load += new System.EventHandler(this._batchMode_Load);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 22);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(265, 123);
            this.panel4.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(265, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.White;
            this.panel5.Controls.Add(this.btn_SaveLog);
            this.panel5.Controls.Add(this.richTextBox1);
            this.panel5.Controls.Add(this.btn_ConnectCTRL);
            this.panel5.Controls.Add(this.btn_ScanCTRLS);
            this.panel5.Controls.Add(this.btn_RapContinue);
            this.panel5.Controls.Add(this.btn_StopRap);
            this.panel5.Controls.Add(this.btn_StartRAP);
            this.panel5.Controls.Add(this.listView_Controllers);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(265, 22);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1089, 123);
            this.panel5.TabIndex = 3;
            // 
            // btn_SaveLog
            // 
            this.btn_SaveLog.Location = new System.Drawing.Point(1003, 48);
            this.btn_SaveLog.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SaveLog.Name = "btn_SaveLog";
            this.btn_SaveLog.Size = new System.Drawing.Size(79, 24);
            this.btn_SaveLog.TabIndex = 59;
            this.btn_SaveLog.Text = "Save Log";
            this.btn_SaveLog.UseVisualStyleBackColor = true;
            this.btn_SaveLog.Click += new System.EventHandler(this.btn_SaveLog_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(440, 11);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(559, 103);
            this.richTextBox1.TabIndex = 58;
            this.richTextBox1.Text = "";
            // 
            // btn_ConnectCTRL
            // 
            this.btn_ConnectCTRL.Location = new System.Drawing.Point(255, 72);
            this.btn_ConnectCTRL.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ConnectCTRL.Name = "btn_ConnectCTRL";
            this.btn_ConnectCTRL.Size = new System.Drawing.Size(104, 19);
            this.btn_ConnectCTRL.TabIndex = 57;
            this.btn_ConnectCTRL.Text = "Connect";
            this.btn_ConnectCTRL.UseVisualStyleBackColor = true;
            this.btn_ConnectCTRL.Click += new System.EventHandler(this.btn_ConnectCTRL_Click);
            // 
            // btn_ScanCTRLS
            // 
            this.btn_ScanCTRLS.Location = new System.Drawing.Point(255, 11);
            this.btn_ScanCTRLS.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ScanCTRLS.Name = "btn_ScanCTRLS";
            this.btn_ScanCTRLS.Size = new System.Drawing.Size(104, 19);
            this.btn_ScanCTRLS.TabIndex = 56;
            this.btn_ScanCTRLS.Text = "Scan Controllers";
            this.btn_ScanCTRLS.UseVisualStyleBackColor = true;
            this.btn_ScanCTRLS.Click += new System.EventHandler(this.btn_ScanCTRLS_Click);
            // 
            // btn_RapContinue
            // 
            this.btn_RapContinue.Location = new System.Drawing.Point(193, 95);
            this.btn_RapContinue.Margin = new System.Windows.Forms.Padding(2);
            this.btn_RapContinue.Name = "btn_RapContinue";
            this.btn_RapContinue.Size = new System.Drawing.Size(58, 19);
            this.btn_RapContinue.TabIndex = 55;
            this.btn_RapContinue.Text = "Continue";
            this.btn_RapContinue.UseVisualStyleBackColor = true;
            this.btn_RapContinue.Click += new System.EventHandler(this.btn_RapContinue_Click);
            // 
            // btn_StopRap
            // 
            this.btn_StopRap.Location = new System.Drawing.Point(105, 95);
            this.btn_StopRap.Margin = new System.Windows.Forms.Padding(2);
            this.btn_StopRap.Name = "btn_StopRap";
            this.btn_StopRap.Size = new System.Drawing.Size(80, 19);
            this.btn_StopRap.TabIndex = 54;
            this.btn_StopRap.Text = "Stop RAPID";
            this.btn_StopRap.UseVisualStyleBackColor = true;
            this.btn_StopRap.Click += new System.EventHandler(this.btn_StopRap_Click);
            // 
            // btn_StartRAP
            // 
            this.btn_StartRAP.Location = new System.Drawing.Point(11, 95);
            this.btn_StartRAP.Margin = new System.Windows.Forms.Padding(2);
            this.btn_StartRAP.Name = "btn_StartRAP";
            this.btn_StartRAP.Size = new System.Drawing.Size(90, 19);
            this.btn_StartRAP.TabIndex = 53;
            this.btn_StartRAP.Text = "Start RAPID";
            this.btn_StartRAP.UseVisualStyleBackColor = true;
            this.btn_StartRAP.Click += new System.EventHandler(this.btn_StartRAP_Click);
            // 
            // listView_Controllers
            // 
            this.listView_Controllers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IPAddress,
            this.ControllerName});
            this.listView_Controllers.HideSelection = false;
            this.listView_Controllers.Location = new System.Drawing.Point(11, 11);
            this.listView_Controllers.Margin = new System.Windows.Forms.Padding(2);
            this.listView_Controllers.Name = "listView_Controllers";
            this.listView_Controllers.Size = new System.Drawing.Size(240, 80);
            this.listView_Controllers.TabIndex = 50;
            this.listView_Controllers.UseCompatibleStateImageBehavior = false;
            this.listView_Controllers.View = System.Windows.Forms.View.Details;
            // 
            // IPAddress
            // 
            this.IPAddress.Text = "IP Address";
            this.IPAddress.Width = 107;
            // 
            // ControllerName
            // 
            this.ControllerName.Text = "Controller Name";
            this.ControllerName.Width = 205;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(265, 22);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.loadRecipeToolStripMenuItem,
            this.saveRecipeToolStripMenuItem,
            this.saveRecipeAsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 18);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.openToolStripMenuItem.Text = "Calibration Setup";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // loadRecipeToolStripMenuItem
            // 
            this.loadRecipeToolStripMenuItem.Name = "loadRecipeToolStripMenuItem";
            this.loadRecipeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.loadRecipeToolStripMenuItem.Text = "Load Recipe";
            this.loadRecipeToolStripMenuItem.Click += new System.EventHandler(this.loadRecipeToolStripMenuItem_Click);
            // 
            // saveRecipeToolStripMenuItem
            // 
            this.saveRecipeToolStripMenuItem.Name = "saveRecipeToolStripMenuItem";
            this.saveRecipeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.saveRecipeToolStripMenuItem.Text = "Save Recipe";
            this.saveRecipeToolStripMenuItem.Click += new System.EventHandler(this.saveRecipeToolStripMenuItem_Click);
            // 
            // saveRecipeAsToolStripMenuItem
            // 
            this.saveRecipeAsToolStripMenuItem.Name = "saveRecipeAsToolStripMenuItem";
            this.saveRecipeAsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.saveRecipeAsToolStripMenuItem.Text = "Save Recipe As";
            this.saveRecipeAsToolStripMenuItem.Click += new System.EventHandler(this.saveRecipeAsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(52, 18);
            this.helpToolStripMenuItem.Text = "About";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // MainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1354, 860);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(864, 714);
            this.Name = "MainView";
            this.Text = "MainView";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainView_FormClosing);
            this.Shown += new System.EventHandler(this.MainView_Shown);
            this.Resize += new System.EventHandler(this.MainView_Resize);
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.tableLayoutPanelDataVisualizers.ResumeLayout(false);
            this.tableLayoutPanelCharts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._profileChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._thicknessChart)).EndInit();
            this.tableLayoutPanelInfo.ResumeLayout(false);
            this.tableLayoutPanelInfo.PerformLayout();
            this.panelSettings.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelDataVisualizers;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCharts;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelInfo;
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private BatchModePresenter _batchMode;
        private System.Windows.Forms.DataVisualization.Charting.Chart _profileChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart _thicknessChart;
        private System.Windows.Forms.Label labelFrameIndex;
        private System.Windows.Forms.Label labelViewMode;
        private System.Windows.Forms.Integration.ElementHost elementHostBatchVisualizer;
        private BatchVisualizer2DUc batchVisualizer2DUc3;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadRecipeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRecipeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveRecipeAsToolStripMenuItem;
        private System.Windows.Forms.ListView listView_Controllers;
        private System.Windows.Forms.ColumnHeader IPAddress;
        private System.Windows.Forms.ColumnHeader ControllerName;
        private System.Windows.Forms.Button btn_StartRAP;
        private System.Windows.Forms.Button btn_StopRap;
        private System.Windows.Forms.Button btn_RapContinue;
        private System.Windows.Forms.Button btn_ScanCTRLS;
        private System.Windows.Forms.Button btn_ConnectCTRL;
        public System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_SaveLog;
    }
}