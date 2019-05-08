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
            this.buttonShPoints = new System.Windows.Forms.Button();
            this.textBoxDataFolder = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
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
            this.button10_8 = new System.Windows.Forms.Button();
            this.button10_7 = new System.Windows.Forms.Button();
            this.button10_6 = new System.Windows.Forms.Button();
            this.button10_5 = new System.Windows.Forms.Button();
            this.button10_4 = new System.Windows.Forms.Button();
            this.button10_3 = new System.Windows.Forms.Button();
            this.button10_2 = new System.Windows.Forms.Button();
            this.button10_1 = new System.Windows.Forms.Button();
            this.buttonEvaluateSavedDraws = new System.Windows.Forms.Button();
            this.buttonResultDraw_TeamInput_SaveAs = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonDrawPerm = new System.Windows.Forms.Button();
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
            this.buttonDrawTeams = new System.Windows.Forms.Button();
            this.dataGridTeamsGrid = new System.Windows.Forms.DataGridView();
            this.dataGridPlayerMatrixStats = new System.Windows.Forms.DataGridView();
            this.dataGridPlayerMatrix = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
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
            this.tabControl1.Size = new System.Drawing.Size(1380, 757);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonShPoints);
            this.tabPage1.Controls.Add(this.textBoxDataFolder);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage1.Size = new System.Drawing.Size(1372, 731);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Input";
            this.tabPage1.UseVisualStyleBackColor = true;
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
            this.textBoxDataFolder.Text = "2018";
            // 
            // groupBox2
            // 
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
            this.buttonMatchResults.Location = new System.Drawing.Point(271, 27);
            this.buttonMatchResults.Name = "buttonMatchResults";
            this.buttonMatchResults.Size = new System.Drawing.Size(83, 25);
            this.buttonMatchResults.TabIndex = 9;
            this.buttonMatchResults.Text = "MatchResults";
            this.buttonMatchResults.UseVisualStyleBackColor = true;
            this.buttonMatchResults.Click += new System.EventHandler(this.buttonMatchResults_Click);
            // 
            // buttonReporting
            // 
            this.buttonReporting.Location = new System.Drawing.Point(93, 119);
            this.buttonReporting.Name = "buttonReporting";
            this.buttonReporting.Size = new System.Drawing.Size(83, 25);
            this.buttonReporting.TabIndex = 8;
            this.buttonReporting.Text = "Reporting";
            this.buttonReporting.UseVisualStyleBackColor = true;
            this.buttonReporting.Click += new System.EventHandler(this.buttonReporting_Click);
            // 
            // buttonScores
            // 
            this.buttonScores.Location = new System.Drawing.Point(182, 27);
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
            this.radioButton5.Location = new System.Drawing.Point(17, 119);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(61, 20);
            this.radioButton5.TabIndex = 5;
            this.radioButton5.Text = "Rnd 5";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(17, 96);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(61, 20);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.Text = "Rnd 4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(17, 73);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(61, 20);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.Text = "Rnd 3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(17, 50);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(61, 20);
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
            this.radioButton1.Size = new System.Drawing.Size(61, 20);
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
            this.dataGridViewPlayers.Size = new System.Drawing.Size(444, 293);
            this.dataGridViewPlayers.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button10_8);
            this.tabPage3.Controls.Add(this.button10_7);
            this.tabPage3.Controls.Add(this.button10_6);
            this.tabPage3.Controls.Add(this.button10_5);
            this.tabPage3.Controls.Add(this.button10_4);
            this.tabPage3.Controls.Add(this.button10_3);
            this.tabPage3.Controls.Add(this.button10_2);
            this.tabPage3.Controls.Add(this.button10_1);
            this.tabPage3.Controls.Add(this.buttonEvaluateSavedDraws);
            this.tabPage3.Controls.Add(this.buttonResultDraw_TeamInput_SaveAs);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.buttonDrawPerm);
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
            this.tabPage3.Controls.Add(this.buttonDrawTeams);
            this.tabPage3.Controls.Add(this.dataGridTeamsGrid);
            this.tabPage3.Controls.Add(this.dataGridPlayerMatrixStats);
            this.tabPage3.Controls.Add(this.dataGridPlayerMatrix);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 3, 3, 3);
            this.tabPage3.Size = new System.Drawing.Size(1372, 731);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Draw";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button10_8
            // 
            this.button10_8.Location = new System.Drawing.Point(165, 345);
            this.button10_8.Name = "button10_8";
            this.button10_8.Size = new System.Drawing.Size(40, 23);
            this.button10_8.TabIndex = 34;
            this.button10_8.Text = "10_8";
            this.button10_8.UseVisualStyleBackColor = true;
            this.button10_8.Click += new System.EventHandler(this.button10_8_Click);
            // 
            // button10_7
            // 
            this.button10_7.Location = new System.Drawing.Point(122, 345);
            this.button10_7.Name = "button10_7";
            this.button10_7.Size = new System.Drawing.Size(40, 23);
            this.button10_7.TabIndex = 33;
            this.button10_7.Text = "10_7";
            this.button10_7.UseVisualStyleBackColor = true;
            this.button10_7.Click += new System.EventHandler(this.button10_7_Click);
            // 
            // button10_6
            // 
            this.button10_6.Location = new System.Drawing.Point(207, 320);
            this.button10_6.Name = "button10_6";
            this.button10_6.Size = new System.Drawing.Size(40, 23);
            this.button10_6.TabIndex = 32;
            this.button10_6.Text = "10_6";
            this.button10_6.UseVisualStyleBackColor = true;
            this.button10_6.Click += new System.EventHandler(this.button10_6_Click);
            // 
            // button10_5
            // 
            this.button10_5.Location = new System.Drawing.Point(165, 320);
            this.button10_5.Name = "button10_5";
            this.button10_5.Size = new System.Drawing.Size(40, 23);
            this.button10_5.TabIndex = 31;
            this.button10_5.Text = "10_5";
            this.button10_5.UseVisualStyleBackColor = true;
            this.button10_5.Click += new System.EventHandler(this.button10_5_Click);
            // 
            // button10_4
            // 
            this.button10_4.Location = new System.Drawing.Point(122, 320);
            this.button10_4.Name = "button10_4";
            this.button10_4.Size = new System.Drawing.Size(40, 23);
            this.button10_4.TabIndex = 30;
            this.button10_4.Text = "10_4";
            this.button10_4.UseVisualStyleBackColor = true;
            this.button10_4.Click += new System.EventHandler(this.button10_4_Click);
            // 
            // button10_3
            // 
            this.button10_3.Location = new System.Drawing.Point(207, 294);
            this.button10_3.Name = "button10_3";
            this.button10_3.Size = new System.Drawing.Size(40, 23);
            this.button10_3.TabIndex = 29;
            this.button10_3.Text = "10_3";
            this.button10_3.UseVisualStyleBackColor = true;
            this.button10_3.Click += new System.EventHandler(this.button10_3_Click);
            // 
            // button10_2
            // 
            this.button10_2.Location = new System.Drawing.Point(165, 294);
            this.button10_2.Name = "button10_2";
            this.button10_2.Size = new System.Drawing.Size(40, 23);
            this.button10_2.TabIndex = 28;
            this.button10_2.Text = "10_2";
            this.button10_2.UseVisualStyleBackColor = true;
            this.button10_2.Click += new System.EventHandler(this.button10_2_Click);
            // 
            // button10_1
            // 
            this.button10_1.Location = new System.Drawing.Point(122, 294);
            this.button10_1.Name = "button10_1";
            this.button10_1.Size = new System.Drawing.Size(40, 23);
            this.button10_1.TabIndex = 27;
            this.button10_1.Text = "10_1";
            this.button10_1.UseVisualStyleBackColor = true;
            this.button10_1.Click += new System.EventHandler(this.button10_1_Click);
            // 
            // buttonEvaluateSavedDraws
            // 
            this.buttonEvaluateSavedDraws.Location = new System.Drawing.Point(378, 189);
            this.buttonEvaluateSavedDraws.Name = "buttonEvaluateSavedDraws";
            this.buttonEvaluateSavedDraws.Size = new System.Drawing.Size(130, 29);
            this.buttonEvaluateSavedDraws.TabIndex = 26;
            this.buttonEvaluateSavedDraws.Text = "Evaluate Saved Draws";
            this.buttonEvaluateSavedDraws.UseVisualStyleBackColor = true;
            this.buttonEvaluateSavedDraws.Click += new System.EventHandler(this.buttonEvaluateSavedDraws_Click);
            // 
            // buttonResultDraw_TeamInput_SaveAs
            // 
            this.buttonResultDraw_TeamInput_SaveAs.Location = new System.Drawing.Point(1171, 493);
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
            this.label4.Location = new System.Drawing.Point(1168, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 65);
            this.label4.TabIndex = 24;
            this.label4.Text = "Input Team:\r\n4B: A1, A1, A2, A2\r\nWinch: A1, A2, A3\r\n2B: A1, A2\r\nabandon: 0\r\n";
            // 
            // buttonDrawPerm
            // 
            this.buttonDrawPerm.Location = new System.Drawing.Point(378, 225);
            this.buttonDrawPerm.Name = "buttonDrawPerm";
            this.buttonDrawPerm.Size = new System.Drawing.Size(130, 21);
            this.buttonDrawPerm.TabIndex = 23;
            this.buttonDrawPerm.Text = "Draw Permutations";
            this.buttonDrawPerm.UseVisualStyleBackColor = true;
            this.buttonDrawPerm.Click += new System.EventHandler(this.buttonDrawPerm_Click);
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
            this.label2.Location = new System.Drawing.Point(512, 99);
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
            this.buttonResultDraw_TeamInput_SaveProd.Location = new System.Drawing.Point(1171, 409);
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
            this.buttonStartDraw_TeamInput_LoadFrom.Location = new System.Drawing.Point(273, 293);
            this.buttonStartDraw_TeamInput_LoadFrom.Name = "buttonStartDraw_TeamInput_LoadFrom";
            this.buttonStartDraw_TeamInput_LoadFrom.Size = new System.Drawing.Size(101, 23);
            this.buttonStartDraw_TeamInput_LoadFrom.TabIndex = 17;
            this.buttonStartDraw_TeamInput_LoadFrom.Text = "Load...";
            this.buttonStartDraw_TeamInput_LoadFrom.UseVisualStyleBackColor = true;
            this.buttonStartDraw_TeamInput_LoadFrom.Click += new System.EventHandler(this.buttonStartDraw_TeamInput_LoadFrom_Click);
            // 
            // buttonStartDraw_TeamInput_SaveAs
            // 
            this.buttonStartDraw_TeamInput_SaveAs.Location = new System.Drawing.Point(273, 318);
            this.buttonStartDraw_TeamInput_SaveAs.Name = "buttonStartDraw_TeamInput_SaveAs";
            this.buttonStartDraw_TeamInput_SaveAs.Size = new System.Drawing.Size(101, 26);
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
            this.dataGridDrawRestrictionInput.Location = new System.Drawing.Point(378, 6);
            this.dataGridDrawRestrictionInput.Name = "dataGridDrawRestrictionInput";
            this.dataGridDrawRestrictionInput.Size = new System.Drawing.Size(784, 89);
            this.dataGridDrawRestrictionInput.TabIndex = 15;
            // 
            // buttonDisplayStats
            // 
            this.buttonDisplayStats.Location = new System.Drawing.Point(379, 290);
            this.buttonDisplayStats.Name = "buttonDisplayStats";
            this.buttonDisplayStats.Size = new System.Drawing.Size(130, 23);
            this.buttonDisplayStats.TabIndex = 14;
            this.buttonDisplayStats.Text = "Display Stats";
            this.buttonDisplayStats.UseVisualStyleBackColor = true;
            this.buttonDisplayStats.Click += new System.EventHandler(this.buttonDisplayStats_Click);
            // 
            // buttonStartDraw_TeamInput_LoadProd
            // 
            this.buttonStartDraw_TeamInput_LoadProd.Location = new System.Drawing.Point(14, 294);
            this.buttonStartDraw_TeamInput_LoadProd.Name = "buttonStartDraw_TeamInput_LoadProd";
            this.buttonStartDraw_TeamInput_LoadProd.Size = new System.Drawing.Size(96, 23);
            this.buttonStartDraw_TeamInput_LoadProd.TabIndex = 13;
            this.buttonStartDraw_TeamInput_LoadProd.Text = "Load from Prod";
            this.buttonStartDraw_TeamInput_LoadProd.UseVisualStyleBackColor = true;
            this.buttonStartDraw_TeamInput_LoadProd.Click += new System.EventHandler(this.buttonStartDraw_TeamInput_LoadProd_Click);
            // 
            // buttonStartDraw_TeamInput_SaveProd
            // 
            this.buttonStartDraw_TeamInput_SaveProd.Location = new System.Drawing.Point(14, 318);
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
            this.dataGridViewInput.Location = new System.Drawing.Point(14, 6);
            this.dataGridViewInput.Name = "dataGridViewInput";
            this.dataGridViewInput.Size = new System.Drawing.Size(359, 284);
            this.dataGridViewInput.TabIndex = 10;
            // 
            // buttonDrawNew
            // 
            this.buttonDrawNew.Location = new System.Drawing.Point(378, 137);
            this.buttonDrawNew.Name = "buttonDrawNew";
            this.buttonDrawNew.Size = new System.Drawing.Size(130, 49);
            this.buttonDrawNew.TabIndex = 9;
            this.buttonDrawNew.Text = "Draw Teams (new)";
            this.buttonDrawNew.UseVisualStyleBackColor = true;
            this.buttonDrawNew.Click += new System.EventHandler(this.buttonDrawNew_Click);
            // 
            // buttonDrawTeams
            // 
            this.buttonDrawTeams.Location = new System.Drawing.Point(379, 261);
            this.buttonDrawTeams.Name = "buttonDrawTeams";
            this.buttonDrawTeams.Size = new System.Drawing.Size(130, 23);
            this.buttonDrawTeams.TabIndex = 5;
            this.buttonDrawTeams.Text = "Draw Teams orig(10)";
            this.buttonDrawTeams.UseVisualStyleBackColor = true;
            this.buttonDrawTeams.Click += new System.EventHandler(this.buttonDrawTeams_Click_1);
            // 
            // dataGridTeamsGrid
            // 
            this.dataGridTeamsGrid.AllowUserToAddRows = false;
            this.dataGridTeamsGrid.AllowUserToDeleteRows = false;
            this.dataGridTeamsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridTeamsGrid.Location = new System.Drawing.Point(379, 409);
            this.dataGridTeamsGrid.Name = "dataGridTeamsGrid";
            this.dataGridTeamsGrid.Size = new System.Drawing.Size(784, 275);
            this.dataGridTeamsGrid.TabIndex = 7;
            // 
            // dataGridPlayerMatrixStats
            // 
            this.dataGridPlayerMatrixStats.AllowUserToAddRows = false;
            this.dataGridPlayerMatrixStats.AllowUserToDeleteRows = false;
            this.dataGridPlayerMatrixStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPlayerMatrixStats.Location = new System.Drawing.Point(6, 409);
            this.dataGridPlayerMatrixStats.Name = "dataGridPlayerMatrixStats";
            this.dataGridPlayerMatrixStats.Size = new System.Drawing.Size(367, 275);
            this.dataGridPlayerMatrixStats.TabIndex = 8;
            // 
            // dataGridPlayerMatrix
            // 
            this.dataGridPlayerMatrix.AllowUserToAddRows = false;
            this.dataGridPlayerMatrix.AllowUserToDeleteRows = false;
            this.dataGridPlayerMatrix.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPlayerMatrix.Location = new System.Drawing.Point(514, 116);
            this.dataGridPlayerMatrix.Name = "dataGridPlayerMatrix";
            this.dataGridPlayerMatrix.Size = new System.Drawing.Size(648, 286);
            this.dataGridPlayerMatrix.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listBoxYearForStats);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1372, 731);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            "2018"});
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
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "CalcHistory";
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
            this.ClientSize = new System.Drawing.Size(1380, 757);
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
        private System.Windows.Forms.Button buttonDrawTeams;
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
        private System.Windows.Forms.Button buttonDrawPerm;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonResultDraw_TeamInput_SaveAs;
        private System.Windows.Forms.Button buttonEvaluateSavedDraws;
        private System.Windows.Forms.Button button10_1;
        private System.Windows.Forms.Button button10_6;
        private System.Windows.Forms.Button button10_5;
        private System.Windows.Forms.Button button10_4;
        private System.Windows.Forms.Button button10_3;
        private System.Windows.Forms.Button button10_2;
        private System.Windows.Forms.Button button10_8;
        private System.Windows.Forms.Button button10_7;
    }
}

