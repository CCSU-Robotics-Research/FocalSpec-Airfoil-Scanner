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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
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
            this.terminalsettingsGroup = new System.Windows.Forms.GroupBox();
            this.btn_SaveLog = new System.Windows.Forms.Button();
            this.rapidControlsGroup = new System.Windows.Forms.GroupBox();
            this.btn_StartRAP = new System.Windows.Forms.Button();
            this.btn_StopRap = new System.Windows.Forms.Button();
            this.btn_RapContinue = new System.Windows.Forms.Button();
            this.controllerSettingsGroup = new System.Windows.Forms.GroupBox();
            this.listView_Controllers = new System.Windows.Forms.ListView();
            this.IPAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ControllerName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btn_ScanCTRLS = new System.Windows.Forms.Button();
            this.btn_ConnectCTRL = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.terminaloutputGroup = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRecipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRecipeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveRecipeAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanelMain.SuspendLayout();
            this.tableLayoutPanelDataVisualizers.SuspendLayout();
            this.tableLayoutPanelCharts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._profileChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._thicknessChart)).BeginInit();
            this.tableLayoutPanelInfo.SuspendLayout();
            this.panelSettings.SuspendLayout();
            this.terminalsettingsGroup.SuspendLayout();
            this.rapidControlsGroup.SuspendLayout();
            this.controllerSettingsGroup.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            this.terminaloutputGroup.SuspendLayout();
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
            chartArea3.AxisX.Title = "X [mm]";
            chartArea3.AxisY.Title = "Z [mm]";
            chartArea3.Name = "ChartArea1";
            this._profileChart.ChartAreas.Add(chartArea3);
            this._profileChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._profileChart.Location = new System.Drawing.Point(3, 3);
            this._profileChart.Name = "_profileChart";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series4.MarkerSize = 2;
            series4.Name = "profile0";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series5.MarkerSize = 2;
            series5.Name = "intensity0";
            this._profileChart.Series.Add(series4);
            this._profileChart.Series.Add(series5);
            this._profileChart.Size = new System.Drawing.Size(527, 677);
            this._profileChart.TabIndex = 5;
            this._profileChart.Text = "_profileChart";
            this._profileChart.AxisViewChanged += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ViewEventArgs>(this._profileChart_AxisViewChanged);
            this._profileChart.Paint += new System.Windows.Forms.PaintEventHandler(this._profileChart_Paint);
            // 
            // _thicknessChart
            // 
            chartArea4.AxisX.Title = "X [mm]";
            chartArea4.AxisY.Title = "Z [mm]";
            chartArea4.Name = "ChartArea2";
            this._thicknessChart.ChartAreas.Add(chartArea4);
            this._thicknessChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this._thicknessChart.Location = new System.Drawing.Point(3, 686);
            this._thicknessChart.Name = "_thicknessChart";
            series6.ChartArea = "ChartArea2";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastPoint;
            series6.MarkerSize = 2;
            series6.Name = "Layer 1";
            this._thicknessChart.Series.Add(series6);
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
            this.panelSettings.Controls.Add(this.terminalsettingsGroup);
            this.panelSettings.Controls.Add(this.rapidControlsGroup);
            this.panelSettings.Controls.Add(this.controllerSettingsGroup);
            this.panelSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSettings.Location = new System.Drawing.Point(3, 148);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(259, 709);
            this.panelSettings.TabIndex = 1;
            // 
            // terminalsettingsGroup
            // 
            this.terminalsettingsGroup.Controls.Add(this.btn_SaveLog);
            this.terminalsettingsGroup.Location = new System.Drawing.Point(9, 471);
            this.terminalsettingsGroup.Name = "terminalsettingsGroup";
            this.terminalsettingsGroup.Size = new System.Drawing.Size(236, 54);
            this.terminalsettingsGroup.TabIndex = 2;
            this.terminalsettingsGroup.TabStop = false;
            this.terminalsettingsGroup.Text = "Terminal Settings";
            // 
            // btn_SaveLog
            // 
            this.btn_SaveLog.Location = new System.Drawing.Point(140, 18);
            this.btn_SaveLog.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SaveLog.Name = "btn_SaveLog";
            this.btn_SaveLog.Size = new System.Drawing.Size(79, 24);
            this.btn_SaveLog.TabIndex = 59;
            this.btn_SaveLog.Text = "Save Log";
            this.btn_SaveLog.UseVisualStyleBackColor = true;
            this.btn_SaveLog.Click += new System.EventHandler(this.btn_SaveLog_Click);
            // 
            // rapidControlsGroup
            // 
            this.rapidControlsGroup.Controls.Add(this.btn_StartRAP);
            this.rapidControlsGroup.Controls.Add(this.btn_StopRap);
            this.rapidControlsGroup.Controls.Add(this.btn_RapContinue);
            this.rapidControlsGroup.Location = new System.Drawing.Point(9, 365);
            this.rapidControlsGroup.Name = "rapidControlsGroup";
            this.rapidControlsGroup.Size = new System.Drawing.Size(236, 100);
            this.rapidControlsGroup.TabIndex = 1;
            this.rapidControlsGroup.TabStop = false;
            this.rapidControlsGroup.Text = "RAPID Controls";
            // 
            // btn_StartRAP
            // 
            this.btn_StartRAP.Location = new System.Drawing.Point(5, 18);
            this.btn_StartRAP.Margin = new System.Windows.Forms.Padding(2);
            this.btn_StartRAP.Name = "btn_StartRAP";
            this.btn_StartRAP.Size = new System.Drawing.Size(87, 77);
            this.btn_StartRAP.TabIndex = 53;
            this.btn_StartRAP.Text = "Start RAPID";
            this.btn_StartRAP.UseVisualStyleBackColor = true;
            this.btn_StartRAP.Click += new System.EventHandler(this.btn_StartRAP_Click);
            // 
            // btn_StopRap
            // 
            this.btn_StopRap.Location = new System.Drawing.Point(96, 18);
            this.btn_StopRap.Margin = new System.Windows.Forms.Padding(2);
            this.btn_StopRap.Name = "btn_StopRap";
            this.btn_StopRap.Size = new System.Drawing.Size(73, 77);
            this.btn_StopRap.TabIndex = 54;
            this.btn_StopRap.Text = "Stop RAPID";
            this.btn_StopRap.UseVisualStyleBackColor = true;
            this.btn_StopRap.Click += new System.EventHandler(this.btn_StopRap_Click);
            // 
            // btn_RapContinue
            // 
            this.btn_RapContinue.Location = new System.Drawing.Point(173, 18);
            this.btn_RapContinue.Margin = new System.Windows.Forms.Padding(2);
            this.btn_RapContinue.Name = "btn_RapContinue";
            this.btn_RapContinue.Size = new System.Drawing.Size(58, 77);
            this.btn_RapContinue.TabIndex = 55;
            this.btn_RapContinue.Text = "Continue";
            this.btn_RapContinue.UseVisualStyleBackColor = true;
            this.btn_RapContinue.Click += new System.EventHandler(this.btn_RapContinue_Click);
            // 
            // controllerSettingsGroup
            // 
            this.controllerSettingsGroup.Controls.Add(this.listView_Controllers);
            this.controllerSettingsGroup.Controls.Add(this.btn_ScanCTRLS);
            this.controllerSettingsGroup.Controls.Add(this.btn_ConnectCTRL);
            this.controllerSettingsGroup.Location = new System.Drawing.Point(9, 20);
            this.controllerSettingsGroup.Name = "controllerSettingsGroup";
            this.controllerSettingsGroup.Size = new System.Drawing.Size(236, 339);
            this.controllerSettingsGroup.TabIndex = 0;
            this.controllerSettingsGroup.TabStop = false;
            this.controllerSettingsGroup.Text = "Controller Settings";
            // 
            // listView_Controllers
            // 
            this.listView_Controllers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.IPAddress,
            this.ControllerName});
            this.listView_Controllers.HideSelection = false;
            this.listView_Controllers.Location = new System.Drawing.Point(5, 18);
            this.listView_Controllers.Margin = new System.Windows.Forms.Padding(2);
            this.listView_Controllers.Name = "listView_Controllers";
            this.listView_Controllers.Size = new System.Drawing.Size(226, 287);
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
            // btn_ScanCTRLS
            // 
            this.btn_ScanCTRLS.Location = new System.Drawing.Point(5, 309);
            this.btn_ScanCTRLS.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ScanCTRLS.Name = "btn_ScanCTRLS";
            this.btn_ScanCTRLS.Size = new System.Drawing.Size(104, 19);
            this.btn_ScanCTRLS.TabIndex = 56;
            this.btn_ScanCTRLS.Text = "Scan Controllers";
            this.btn_ScanCTRLS.UseVisualStyleBackColor = true;
            this.btn_ScanCTRLS.Click += new System.EventHandler(this.btn_ScanCTRLS_Click);
            // 
            // btn_ConnectCTRL
            // 
            this.btn_ConnectCTRL.Location = new System.Drawing.Point(127, 309);
            this.btn_ConnectCTRL.Margin = new System.Windows.Forms.Padding(2);
            this.btn_ConnectCTRL.Name = "btn_ConnectCTRL";
            this.btn_ConnectCTRL.Size = new System.Drawing.Size(104, 19);
            this.btn_ConnectCTRL.TabIndex = 57;
            this.btn_ConnectCTRL.Text = "Connect";
            this.btn_ConnectCTRL.UseVisualStyleBackColor = true;
            this.btn_ConnectCTRL.Click += new System.EventHandler(this.btn_ConnectCTRL_Click);
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
            this.panel5.Controls.Add(this.terminaloutputGroup);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(265, 22);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1089, 123);
            this.panel5.TabIndex = 3;
            // 
            // terminaloutputGroup
            // 
            this.terminaloutputGroup.Controls.Add(this.richTextBox1);
            this.terminaloutputGroup.Location = new System.Drawing.Point(10, 3);
            this.terminaloutputGroup.Name = "terminaloutputGroup";
            this.terminaloutputGroup.Size = new System.Drawing.Size(839, 117);
            this.terminaloutputGroup.TabIndex = 59;
            this.terminaloutputGroup.TabStop = false;
            this.terminaloutputGroup.Text = "Terminal Output";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 18);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(802, 89);
            this.richTextBox1.TabIndex = 58;
            this.richTextBox1.Text = "";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.settingsToolStripMenuItem,
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
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(61, 18);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
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
            this.Text = "Airfoil Scanning Utility";
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
            this.terminalsettingsGroup.ResumeLayout(false);
            this.rapidControlsGroup.ResumeLayout(false);
            this.controllerSettingsGroup.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.terminaloutputGroup.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.GroupBox controllerSettingsGroup;
        private System.Windows.Forms.GroupBox terminalsettingsGroup;
        private System.Windows.Forms.GroupBox rapidControlsGroup;
        private System.Windows.Forms.GroupBox terminaloutputGroup;
    }
}