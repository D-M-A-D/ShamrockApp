namespace Shamrock
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonInitialiseForDay = new System.Windows.Forms.Button();
            this.buttonInitialiseforYear = new System.Windows.Forms.Button();
            this.buttonShPoints = new System.Windows.Forms.Button();
            this.textBoxDataFolder = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.buttonExtraPts = new System.Windows.Forms.Button();
            this.buttonCalcHoleStats = new System.Windows.Forms.Button();
            this.buttonSimulatePlayerScores = new System.Windows.Forms.Button();
            this.buttonMatchResults = new System.Windows.Forms.Button();
            this.buttonReporting = new System.Windows.Forms.Button();
            this.buttonScores = new System.Windows.Forms.Button();
            this.buttonCourse = new System.Windows.Forms.Button();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonSavePlayers = new System.Windows.Forms.Button();
            this.dataGridViewPlayers = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.buttonResultDraw_TeamInput_SaveAs = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonResultDraw_TeamInput_SaveProd = new System.Windows.Forms.Button();
            this.labelDrawText = new System.Windows.Forms.Label();
            this.buttonStartDraw_TeamInput_LoadFrom = new System.Windows.Forms.Button();
            this.buttonStartDraw_TeamInput_SaveAs = new System.Windows.Forms.Button();
            this.dataGridDrawRestrictionInput = new System.Windows.Forms.DataGridView();
            this.buttonDisplayStats = new System.Windows.Forms.Button();
            this.buttonStartDraw_TeamInput_LoadProd = new System.Windows.Forms.Button();
            this.buttonStartDraw_TeamInput_SaveProd = new System.Windows.Forms.Button();
            this.dataGridViewInput = new System.Windows.Forms.DataGridView();
            this.buttonDrawNew = new System.Windows.Forms.Button();
            this.dataGridTeamsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridPlayerMatrixStats = new System.Windows.Forms.DataGridView();
            this.dataGridPlayerMatrix = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.listBoxYearForStats = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlayers)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDrawRestrictionInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTeamsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPlayerMatrixStats)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPlayerMatrix)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1283, 686);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonInitialiseForDay);
            this.tabPage1.Controls.Add(this.buttonInitialiseforYear);
            this.tabPage1.Controls.Add(this.buttonShPoints);
            this.tabPage1.Controls.Add(this.textBoxDataFolder);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1275, 660);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Input";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonInitialiseForDay
            // 
            this.buttonInitialiseForDay.Location = new System.Drawing.Point(481, 231);
            this.buttonInitialiseForDay.Name = "buttonInitialiseForDay";
            this.buttonInitialiseForDay.Size = new System.Drawing.Size(117, 25);
            this.buttonInitialiseForDay.TabIndex = 13;
            this.buttonInitialiseForDay.Text = "Initialise For Day";
            this.buttonInitialiseForDay.UseVisualStyleBackColor = true;
            this.buttonInitialiseForDay.Click += new System.EventHandler(this.buttonInitialiseForDay_Click);
            // 
            // buttonInitialiseforYear
            // 
            this.buttonInitialiseforYear.Location = new System.Drawing.Point(481, 262);
            this.buttonInitialiseforYear.Name = "buttonInitialiseforYear";
            this.buttonInitialiseforYear.Size = new System.Drawing.Size(117, 25);
            this.buttonInitialiseforYear.TabIndex = 12;
            this.buttonInitialiseforYear.Text = "Initialise for Year";
            this.buttonInitialiseforYear.UseVisualStyleBackColor = true;
            this.buttonInitialiseforYear.Click += new System.EventHandler(this.buttonInitialiseforYear_Click);
            // 
            // buttonShPoints
            // 
            this.buttonShPoints.Location = new System.Drawing.Point(482, 71);
            this.buttonShPoints.Name = "buttonShPoints";
            this.buttonShPoints.Size = new System.Drawing.Size(117, 25);
            this.buttonShPoints.TabIndex = 11;
            this.buttonShPoints.Text = "Configs for Year";
            this.buttonShPoints.UseVisualStyleBackColor = true;
            this.buttonShPoints.Click += new System.EventHandler(this.buttonShPoints_Click);
            // 
            // textBoxDataFolder
            // 
            this.textBoxDataFolder.Location = new System.Drawing.Point(482, 25);
            this.textBoxDataFolder.Name = "textBoxDataFolder";
            this.textBoxDataFolder.Size = new System.Drawing.Size(117, 20);
            this.textBoxDataFolder.TabIndex = 10;
            this.textBoxDataFolder.Text = "2025";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton7);
            this.groupBox2.Controls.Add(this.radioButton6);
            this.groupBox2.Controls.Add(this.buttonExtraPts);
            this.groupBox2.Controls.Add(this.buttonCalcHoleStats);
            this.groupBox2.Controls.Add(this.buttonSimulatePlayerScores);
            this.groupBox2.Controls.Add(this.buttonMatchResults);
            this.groupBox2.Controls.Add(this.buttonReporting);
            this.groupBox2.Controls.Add(this.buttonScores);
            this.groupBox2.Controls.Add(this.buttonCourse);
            this.groupBox2.Controls.Add(this.radioButton5);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Location = new System.Drawing.Point(605, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(481, 385);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Courses";
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Location = new System.Drawing.Point(17, 162);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(54, 17);
            this.radioButton7.TabIndex = 14;
            this.radioButton7.Text = "Rnd 7";
            this.radioButton7.UseVisualStyleBackColor = true;
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(17, 140);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(54, 17);
            this.radioButton6.TabIndex = 13;
            this.radioButton6.Text = "Rnd 6";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // buttonExtraPts
            // 
            this.buttonExtraPts.Location = new System.Drawing.Point(269, 58);
            this.buttonExtraPts.Name = "buttonExtraPts";
            this.buttonExtraPts.Size = new System.Drawing.Size(83, 25);
            this.buttonExtraPts.TabIndex = 12;
            this.buttonExtraPts.Text = "Extra. (chips)";
            this.buttonExtraPts.UseVisualStyleBackColor = true;
            this.buttonExtraPts.Click += new System.EventHandler(this.buttonExtraPts_Click);
            // 
            // buttonCalcHoleStats
            // 
            this.buttonCalcHoleStats.Location = new System.Drawing.Point(93, 252);
            this.buttonCalcHoleStats.Name = "buttonCalcHoleStats";
            this.buttonCalcHoleStats.Size = new System.Drawing.Size(83, 25);
            this.buttonCalcHoleStats.TabIndex = 11;
            this.buttonCalcHoleStats.Text = "HoleStats";
            this.buttonCalcHoleStats.UseVisualStyleBackColor = true;
            this.buttonCalcHoleStats.Click += new System.EventHandler(this.buttonCalcHoleStats_Click);
            // 
            // buttonSimulatePlayerScores
            // 
            this.buttonSimulatePlayerScores.Enabled = false;
            this.buttonSimulatePlayerScores.Location = new System.Drawing.Point(381, 335);
            this.buttonSimulatePlayerScores.Name = "buttonSimulatePlayerScores";
            this.buttonSimulatePlayerScores.Size = new System.Drawing.Size(83, 35);
            this.buttonSimulatePlayerScores.TabIndex = 10;
            this.buttonSimulatePlayerScores.Text = "Simulate Scores";
            this.buttonSimulatePlayerScores.UseVisualStyleBackColor = true;
            this.buttonSimulatePlayerScores.Click += new System.EventHandler(this.buttonSimulatePlayerScores_Click);
            // 
            // buttonMatchResults
            // 
            this.buttonMatchResults.Location = new System.Drawing.Point(181, 58);
            this.buttonMatchResults.Name = "buttonMatchResults";
            this.buttonMatchResults.Size = new System.Drawing.Size(83, 25);
            this.buttonMatchResults.TabIndex = 9;
            this.buttonMatchResults.Text = "MatchResults";
            this.buttonMatchResults.UseVisualStyleBackColor = true;
            this.buttonMatchResults.Click += new System.EventHandler(this.buttonMatchResults_Click);
            // 
            // buttonReporting
            // 
            this.buttonReporting.Location = new System.Drawing.Point(93, 118);
            this.buttonReporting.Name = "buttonReporting";
            this.buttonReporting.Size = new System.Drawing.Size(83, 25);
            this.buttonReporting.TabIndex = 8;
            this.buttonReporting.Text = "Reporting";
            this.buttonReporting.UseVisualStyleBackColor = true;
            this.buttonReporting.Click += new System.EventHandler(this.buttonReporting_Click);
            // 
            // buttonScores
            // 
            this.buttonScores.Location = new System.Drawing.Point(93, 58);
            this.buttonScores.Name = "buttonScores";
            this.buttonScores.Size = new System.Drawing.Size(83, 25);
            this.buttonScores.TabIndex = 7;
            this.buttonScores.Text = "Scores";
            this.buttonScores.UseVisualStyleBackColor = true;
            this.buttonScores.Click += new System.EventHandler(this.buttonScores_Click);
            // 
            // buttonCourse
            // 
            this.buttonCourse.Location = new System.Drawing.Point(93, 27);
            this.buttonCourse.Name = "buttonCourse";
            this.buttonCourse.Size = new System.Drawing.Size(83, 25);
            this.buttonCourse.TabIndex = 6;
            this.buttonCourse.Text = "Course";
            this.buttonCourse.UseVisualStyleBackColor = true;
            this.buttonCourse.Click += new System.EventHandler(this.buttonCourse_Click);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(17, 118);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(54, 17);
            this.radioButton5.TabIndex = 5;
            this.radioButton5.Text = "Rnd 5";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(17, 96);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(54, 17);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.Text = "Rnd 4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(17, 73);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(54, 17);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.Text = "Rnd 3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(17, 51);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(54, 17);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "Rnd 2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(17, 27);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(54, 17);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Rnd 1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSavePlayers);
            this.groupBox1.Controls.Add(this.dataGridViewPlayers);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 351);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Players";
            // 
            // buttonSavePlayers
            // 
            this.buttonSavePlayers.Location = new System.Drawing.Point(375, 318);
            this.buttonSavePlayers.Name = "buttonSavePlayers";
            this.buttonSavePlayers.Size = new System.Drawing.Size(75, 23);
            this.buttonSavePlayers.TabIndex = 7;
            this.buttonSavePlayers.Text = "Save";
            this.buttonSavePlayers.UseVisualStyleBackColor = true;
            this.buttonSavePlayers.Click += new System.EventHandler(this.buttonSavePlayers_Click);
            // 
            // dataGridViewPlayers
            // 
            this.dataGridViewPlayers.AllowUserToAddRows = false;
            this.dataGridViewPlayers.AllowUserToDeleteRows = false;
            this.dataGridViewPlayers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewPlayers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPlayers.Location = new System.Drawing.Point(6, 19);
            this.dataGridViewPlayers.Name = "dataGridViewPlayers";
            this.dataGridViewPlayers.RowHeadersWidth = 62;
            this.dataGridViewPlayers.Size = new System.Drawing.Size(444, 293);
            this.dataGridViewPlayers.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonResultDraw_TeamInput_SaveAs);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.label2);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.buttonResultDraw_TeamInput_SaveProd);
            this.tabPage3.Controls.Add(this.labelDrawText);
            this.tabPage3.Controls.Add(this.buttonStartDraw_TeamInput_LoadFrom);
            this.tabPage3.Controls.Add(this.buttonStartDraw_TeamInput_SaveAs);
            this.tabPage3.Controls.Add(this.dataGridDrawRestrictionInput);
            this.tabPage3.Controls.Add(this.buttonDisplayStats);
            this.tabPage3.Controls.Add(this.buttonStartDraw_TeamInput_LoadProd);
            this.tabPage3.Controls.Add(this.buttonStartDraw_TeamInput_SaveProd);
            this.tabPage3.Controls.Add(this.dataGridViewInput);
            this.tabPage3.Controls.Add(this.buttonDrawNew);
            this.tabPage3.Controls.Add(this.dataGridTeamsGrid);
            this.tabPage3.Controls.Add(this.dataGridPlayerMatrixStats);
            this.tabPage3.Controls.Add(this.dataGridPlayerMatrix);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1275, 660);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Draw";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonResultDraw_TeamInput_SaveAs
            // 
            this.buttonResultDraw_TeamInput_SaveAs.Location = new System.Drawing.Point(489, 318);
            this.buttonResultDraw_TeamInput_SaveAs.Name = "buttonResultDraw_TeamInput_SaveAs";
            this.buttonResultDraw_TeamInput_SaveAs.Size = new System.Drawing.Size(130, 26);
            this.buttonResultDraw_TeamInput_SaveAs.TabIndex = 25;
            this.buttonResultDraw_TeamInput_SaveAs.Text = "Save Draw (as)...";
            this.buttonResultDraw_TeamInput_SaveAs.UseVisualStyleBackColor = true;
            this.buttonResultDraw_TeamInput_SaveAs.Click += new System.EventHandler(this.buttonResultDraw_TeamInput_SaveAs_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(439, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 91);
            this.label4.TabIndex = 24;
            this.label4.Text = "Input Team:\r\n4B:\r\nA1, A1, A2, A2\r\nWinch:\r\nA1, A2, A3\r\n2B: A1, A2\r\nabandon: 0\r\n";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 393);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "DrawStats";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(553, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Player Matrix";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(383, 393);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Teams";
            // 
            // buttonResultDraw_TeamInput_SaveProd
            // 
            this.buttonResultDraw_TeamInput_SaveProd.Location = new System.Drawing.Point(489, 285);
            this.buttonResultDraw_TeamInput_SaveProd.Name = "buttonResultDraw_TeamInput_SaveProd";
            this.buttonResultDraw_TeamInput_SaveProd.Size = new System.Drawing.Size(130, 26);
            this.buttonResultDraw_TeamInput_SaveProd.TabIndex = 19;
            this.buttonResultDraw_TeamInput_SaveProd.Text = "Save Draw to Prod";
            this.buttonResultDraw_TeamInput_SaveProd.UseVisualStyleBackColor = true;
            this.buttonResultDraw_TeamInput_SaveProd.Click += new System.EventHandler(this.buttonResultDraw_TeamInput_SaveProd_Click);
            // 
            // labelDrawText
            // 
            this.labelDrawText.AutoSize = true;
            this.labelDrawText.Location = new System.Drawing.Point(3, 372);
            this.labelDrawText.Name = "labelDrawText";
            this.labelDrawText.Size = new System.Drawing.Size(35, 13);
            this.labelDrawText.TabIndex = 18;
            this.labelDrawText.Text = "label1";
            // 
            // buttonStartDraw_TeamInput_LoadFrom
            // 
            this.buttonStartDraw_TeamInput_LoadFrom.Location = new System.Drawing.Point(107, 293);
            this.buttonStartDraw_TeamInput_LoadFrom.Name = "buttonStartDraw_TeamInput_LoadFrom";
            this.buttonStartDraw_TeamInput_LoadFrom.Size = new System.Drawing.Size(101, 23);
            this.buttonStartDraw_TeamInput_LoadFrom.TabIndex = 17;
            this.buttonStartDraw_TeamInput_LoadFrom.Text = "Load...";
            this.buttonStartDraw_TeamInput_LoadFrom.UseVisualStyleBackColor = true;
            this.buttonStartDraw_TeamInput_LoadFrom.Click += new System.EventHandler(this.buttonStartDraw_TeamInput_LoadFrom_Click);
            // 
            // buttonStartDraw_TeamInput_SaveAs
            // 
            this.buttonStartDraw_TeamInput_SaveAs.Location = new System.Drawing.Point(107, 318);
            this.buttonStartDraw_TeamInput_SaveAs.Name = "buttonStartDraw_TeamInput_SaveAs";
            this.buttonStartDraw_TeamInput_SaveAs.Size = new System.Drawing.Size(101, 22);
            this.buttonStartDraw_TeamInput_SaveAs.TabIndex = 16;
            this.buttonStartDraw_TeamInput_SaveAs.Text = "Save (as)...";
            this.buttonStartDraw_TeamInput_SaveAs.UseVisualStyleBackColor = true;
            this.buttonStartDraw_TeamInput_SaveAs.Click += new System.EventHandler(this.buttonStartDraw_TeamInput_SaveAs_Click);
            // 
            // dataGridDrawRestrictionInput
            // 
            this.dataGridDrawRestrictionInput.AllowUserToAddRows = false;
            this.dataGridDrawRestrictionInput.AllowUserToDeleteRows = false;
            this.dataGridDrawRestrictionInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridDrawRestrictionInput.Location = new System.Drawing.Point(520, 6);
            this.dataGridDrawRestrictionInput.Name = "dataGridDrawRestrictionInput";
            this.dataGridDrawRestrictionInput.RowHeadersWidth = 62;
            this.dataGridDrawRestrictionInput.Size = new System.Drawing.Size(751, 103);
            this.dataGridDrawRestrictionInput.TabIndex = 15;
            // 
            // buttonDisplayStats
            // 
            this.buttonDisplayStats.Location = new System.Drawing.Point(304, 293);
            this.buttonDisplayStats.Name = "buttonDisplayStats";
            this.buttonDisplayStats.Size = new System.Drawing.Size(130, 23);
            this.buttonDisplayStats.TabIndex = 14;
            this.buttonDisplayStats.Text = "Display Stats For Input";
            this.buttonDisplayStats.UseVisualStyleBackColor = true;
            this.buttonDisplayStats.Click += new System.EventHandler(this.buttonDisplayStats_Click);
            // 
            // buttonStartDraw_TeamInput_LoadProd
            // 
            this.buttonStartDraw_TeamInput_LoadProd.Location = new System.Drawing.Point(6, 293);
            this.buttonStartDraw_TeamInput_LoadProd.Name = "buttonStartDraw_TeamInput_LoadProd";
            this.buttonStartDraw_TeamInput_LoadProd.Size = new System.Drawing.Size(96, 23);
            this.buttonStartDraw_TeamInput_LoadProd.TabIndex = 13;
            this.buttonStartDraw_TeamInput_LoadProd.Text = "Load from Prod";
            this.buttonStartDraw_TeamInput_LoadProd.UseVisualStyleBackColor = true;
            this.buttonStartDraw_TeamInput_LoadProd.Click += new System.EventHandler(this.buttonStartDraw_TeamInput_LoadProd_Click);
            // 
            // buttonStartDraw_TeamInput_SaveProd
            // 
            this.buttonStartDraw_TeamInput_SaveProd.Location = new System.Drawing.Point(6, 317);
            this.buttonStartDraw_TeamInput_SaveProd.Name = "buttonStartDraw_TeamInput_SaveProd";
            this.buttonStartDraw_TeamInput_SaveProd.Size = new System.Drawing.Size(96, 23);
            this.buttonStartDraw_TeamInput_SaveProd.TabIndex = 12;
            this.buttonStartDraw_TeamInput_SaveProd.Text = "Save to Prod";
            this.buttonStartDraw_TeamInput_SaveProd.UseVisualStyleBackColor = true;
            this.buttonStartDraw_TeamInput_SaveProd.Click += new System.EventHandler(this.buttonStartDraw_TeamInput_SaveProd_Click);
            // 
            // dataGridViewInput
            // 
            this.dataGridViewInput.AllowUserToAddRows = false;
            this.dataGridViewInput.AllowUserToDeleteRows = false;
            this.dataGridViewInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInput.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewInput.Name = "dataGridViewInput";
            this.dataGridViewInput.RowHeadersWidth = 62;
            this.dataGridViewInput.Size = new System.Drawing.Size(428, 285);
            this.dataGridViewInput.TabIndex = 10;
            // 
            // buttonDrawNew
            // 
            this.buttonDrawNew.Location = new System.Drawing.Point(439, 158);
            this.buttonDrawNew.Name = "buttonDrawNew";
            this.buttonDrawNew.Size = new System.Drawing.Size(130, 23);
            this.buttonDrawNew.TabIndex = 9;
            this.buttonDrawNew.Text = "Draw Teams";
            this.buttonDrawNew.UseVisualStyleBackColor = true;
            this.buttonDrawNew.Click += new System.EventHandler(this.buttonDrawNew_Click);
            // 
            // dataGridTeamsGrid
            // 
            this.dataGridTeamsGrid.AllowUserToAddRows = false;
            this.dataGridTeamsGrid.AllowUserToDeleteRows = false;
            this.dataGridTeamsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTeamsGrid.Location = new System.Drawing.Point(379, 409);
            this.dataGridTeamsGrid.Name = "dataGridTeamsGrid";
            this.dataGridTeamsGrid.RowHeadersWidth = 62;
            this.dataGridTeamsGrid.Size = new System.Drawing.Size(893, 274);
            this.dataGridTeamsGrid.TabIndex = 7;
            // 
            // dataGridPlayerMatrixStats
            // 
            this.dataGridPlayerMatrixStats.AllowUserToAddRows = false;
            this.dataGridPlayerMatrixStats.AllowUserToDeleteRows = false;
            this.dataGridPlayerMatrixStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPlayerMatrixStats.Location = new System.Drawing.Point(6, 409);
            this.dataGridPlayerMatrixStats.Name = "dataGridPlayerMatrixStats";
            this.dataGridPlayerMatrixStats.RowHeadersWidth = 62;
            this.dataGridPlayerMatrixStats.Size = new System.Drawing.Size(367, 274);
            this.dataGridPlayerMatrixStats.TabIndex = 8;
            // 
            // dataGridPlayerMatrix
            // 
            this.dataGridPlayerMatrix.AllowUserToAddRows = false;
            this.dataGridPlayerMatrix.AllowUserToDeleteRows = false;
            this.dataGridPlayerMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPlayerMatrix.Location = new System.Drawing.Point(624, 116);
            this.dataGridPlayerMatrix.Name = "dataGridPlayerMatrix";
            this.dataGridPlayerMatrix.RowHeadersWidth = 62;
            this.dataGridPlayerMatrix.Size = new System.Drawing.Size(648, 287);
            this.dataGridPlayerMatrix.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.listBoxYearForStats);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1275, 660);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(134, 51);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(142, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "CalcNewHcp2021";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // listBoxYearForStats
            // 
            this.listBoxYearForStats.FormattingEnabled = true;
            this.listBoxYearForStats.Items.AddRange(new object[] {
            "2004",
            "2005",
            "2006",
            "2007",
            "2008",
            "2009",
            "2010",
            "2011",
            "2012",
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021",
            "2022",
            "2023",
            "2024"});
            this.listBoxYearForStats.Location = new System.Drawing.Point(66, 22);
            this.listBoxYearForStats.Name = "listBoxYearForStats";
            this.listBoxYearForStats.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxYearForStats.Size = new System.Drawing.Size(39, 277);
            this.listBoxYearForStats.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(134, 22);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Calc Match History";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 686);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Shamrock";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlayers)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridDrawRestrictionInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridTeamsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPlayerMatrixStats)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPlayerMatrix)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView dataGridViewPlayers;
        private System.Windows.Forms.Button buttonSavePlayers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridTeamsGrid;
        private System.Windows.Forms.DataGridView dataGridPlayerMatrixStats;
        private System.Windows.Forms.DataGridView dataGridPlayerMatrix;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.Button buttonCourse;
        private System.Windows.Forms.TextBox textBoxDataFolder;
        private System.Windows.Forms.Button buttonScores;
        private System.Windows.Forms.Button buttonReporting;
        private System.Windows.Forms.Button buttonMatchResults;
        private System.Windows.Forms.Button buttonShPoints;
        private System.Windows.Forms.Button buttonDrawNew;
        private System.Windows.Forms.DataGridView dataGridViewInput;
        private System.Windows.Forms.Button buttonStartDraw_TeamInput_SaveProd;
        private System.Windows.Forms.Button buttonStartDraw_TeamInput_LoadProd;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button buttonDisplayStats;
        private System.Windows.Forms.DataGridView dataGridDrawRestrictionInput;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonStartDraw_TeamInput_SaveAs;
        private System.Windows.Forms.Button buttonStartDraw_TeamInput_LoadFrom;
        private System.Windows.Forms.Label labelDrawText;
        private System.Windows.Forms.Button buttonResultDraw_TeamInput_SaveProd;
        private System.Windows.Forms.Button buttonSimulatePlayerScores;
        private System.Windows.Forms.Button buttonCalcHoleStats;
        private System.Windows.Forms.ListBox listBoxYearForStats;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonResultDraw_TeamInput_SaveAs;
        private System.Windows.Forms.Button buttonExtraPts;
        private System.Windows.Forms.Button buttonInitialiseForDay;
        private System.Windows.Forms.Button buttonInitialiseforYear;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.Button button2;
    }
}

