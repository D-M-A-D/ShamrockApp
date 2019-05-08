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
            this.buttonSaveDrawToFile = new System.Windows.Forms.Button();
            this.labelDrawText = new System.Windows.Forms.Label();
            this.buttonInputTeamReadfromTemp = new System.Windows.Forms.Button();
            this.buttonTeamInputSaveToTemp = new System.Windows.Forms.Button();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.buttonDisplayStats = new System.Windows.Forms.Button();
            this.buttonInputTeamRead = new System.Windows.Forms.Button();
            this.buttonTeamInputSave = new System.Windows.Forms.Button();
            this.buttonTeamInputInitialise = new System.Windows.Forms.Button();
            this.dataGridViewInput = new System.Windows.Forms.DataGridView();
            this.buttonDrawNew = new System.Windows.Forms.Button();
            this.buttonDrawTeams = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPlayers)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
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
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1768, 1045);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.buttonShPoints);
            this.tabPage1.Controls.Add(this.textBoxDataFolder);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(1760, 1012);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Input";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonShPoints
            // 
            this.buttonShPoints.Location = new System.Drawing.Point(723, 109);
            this.buttonShPoints.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonShPoints.Name = "buttonShPoints";
            this.buttonShPoints.Size = new System.Drawing.Size(176, 38);
            this.buttonShPoints.TabIndex = 11;
            this.buttonShPoints.Text = "Configs for Year";
            this.buttonShPoints.UseVisualStyleBackColor = true;
            this.buttonShPoints.Click += new System.EventHandler(this.buttonShPoints_Click);
            // 
            // textBoxDataFolder
            // 
            this.textBoxDataFolder.Location = new System.Drawing.Point(723, 38);
            this.textBoxDataFolder.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxDataFolder.Name = "textBoxDataFolder";
            this.textBoxDataFolder.Size = new System.Drawing.Size(174, 26);
            this.textBoxDataFolder.TabIndex = 10;
            this.textBoxDataFolder.Text = "2017";
            // 
            // groupBox2
            // 
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
            this.groupBox2.Location = new System.Drawing.Point(908, 9);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(722, 592);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Courses";
            // 
            // buttonSimulatePlayerScores
            // 
            this.buttonSimulatePlayerScores.Enabled = false;
            this.buttonSimulatePlayerScores.Location = new System.Drawing.Point(572, 515);
            this.buttonSimulatePlayerScores.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSimulatePlayerScores.Name = "buttonSimulatePlayerScores";
            this.buttonSimulatePlayerScores.Size = new System.Drawing.Size(124, 54);
            this.buttonSimulatePlayerScores.TabIndex = 10;
            this.buttonSimulatePlayerScores.Text = "Simulate Scores";
            this.buttonSimulatePlayerScores.UseVisualStyleBackColor = true;
            this.buttonSimulatePlayerScores.Click += new System.EventHandler(this.buttonSimulatePlayerScores_Click);
            // 
            // buttonMatchResults
            // 
            this.buttonMatchResults.Location = new System.Drawing.Point(406, 42);
            this.buttonMatchResults.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonMatchResults.Name = "buttonMatchResults";
            this.buttonMatchResults.Size = new System.Drawing.Size(124, 38);
            this.buttonMatchResults.TabIndex = 9;
            this.buttonMatchResults.Text = "MatchResults";
            this.buttonMatchResults.UseVisualStyleBackColor = true;
            this.buttonMatchResults.Click += new System.EventHandler(this.buttonMatchResults_Click);
            // 
            // buttonReporting
            // 
            this.buttonReporting.Location = new System.Drawing.Point(140, 274);
            this.buttonReporting.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonReporting.Name = "buttonReporting";
            this.buttonReporting.Size = new System.Drawing.Size(124, 38);
            this.buttonReporting.TabIndex = 8;
            this.buttonReporting.Text = "Reporting";
            this.buttonReporting.UseVisualStyleBackColor = true;
            this.buttonReporting.Click += new System.EventHandler(this.buttonReporting_Click);
            // 
            // buttonScores
            // 
            this.buttonScores.Location = new System.Drawing.Point(273, 42);
            this.buttonScores.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonScores.Name = "buttonScores";
            this.buttonScores.Size = new System.Drawing.Size(124, 38);
            this.buttonScores.TabIndex = 7;
            this.buttonScores.Text = "Scores";
            this.buttonScores.UseVisualStyleBackColor = true;
            this.buttonScores.Click += new System.EventHandler(this.buttonScores_Click);
            // 
            // buttonCourse
            // 
            this.buttonCourse.Location = new System.Drawing.Point(140, 42);
            this.buttonCourse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonCourse.Name = "buttonCourse";
            this.buttonCourse.Size = new System.Drawing.Size(124, 38);
            this.buttonCourse.TabIndex = 6;
            this.buttonCourse.Text = "Course";
            this.buttonCourse.UseVisualStyleBackColor = true;
            this.buttonCourse.Click += new System.EventHandler(this.buttonCourse_Click);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(26, 183);
            this.radioButton5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(70, 24);
            this.radioButton5.TabIndex = 5;
            this.radioButton5.Text = "Rnd 5";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(26, 148);
            this.radioButton4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(70, 24);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.Text = "Rnd 4";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(26, 112);
            this.radioButton3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(70, 24);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.Text = "Rnd 3";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(26, 77);
            this.radioButton2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(70, 24);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "Rnd 2";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(26, 42);
            this.radioButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(70, 24);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Rnd 1";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonSavePlayers);
            this.groupBox1.Controls.Add(this.dataGridViewPlayers);
            this.groupBox1.Location = new System.Drawing.Point(12, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(684, 540);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Players";
            // 
            // buttonSavePlayers
            // 
            this.buttonSavePlayers.Location = new System.Drawing.Point(562, 489);
            this.buttonSavePlayers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSavePlayers.Name = "buttonSavePlayers";
            this.buttonSavePlayers.Size = new System.Drawing.Size(112, 35);
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
            this.dataGridViewPlayers.Location = new System.Drawing.Point(9, 29);
            this.dataGridViewPlayers.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewPlayers.Name = "dataGridViewPlayers";
            this.dataGridViewPlayers.Size = new System.Drawing.Size(666, 451);
            this.dataGridViewPlayers.TabIndex = 5;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.buttonSaveDrawToFile);
            this.tabPage3.Controls.Add(this.labelDrawText);
            this.tabPage3.Controls.Add(this.buttonInputTeamReadfromTemp);
            this.tabPage3.Controls.Add(this.buttonTeamInputSaveToTemp);
            this.tabPage3.Controls.Add(this.dataGridView4);
            this.tabPage3.Controls.Add(this.buttonDisplayStats);
            this.tabPage3.Controls.Add(this.buttonInputTeamRead);
            this.tabPage3.Controls.Add(this.buttonTeamInputSave);
            this.tabPage3.Controls.Add(this.buttonTeamInputInitialise);
            this.tabPage3.Controls.Add(this.dataGridViewInput);
            this.tabPage3.Controls.Add(this.buttonDrawNew);
            this.tabPage3.Controls.Add(this.buttonDrawTeams);
            this.tabPage3.Controls.Add(this.dataGridView2);
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage3.Size = new System.Drawing.Size(1760, 1012);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Draw";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // buttonSaveDrawToFile
            // 
            this.buttonSaveDrawToFile.ForeColor = System.Drawing.Color.Red;
            this.buttonSaveDrawToFile.Location = new System.Drawing.Point(21, 484);
            this.buttonSaveDrawToFile.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonSaveDrawToFile.Name = "buttonSaveDrawToFile";
            this.buttonSaveDrawToFile.Size = new System.Drawing.Size(173, 40);
            this.buttonSaveDrawToFile.TabIndex = 19;
            this.buttonSaveDrawToFile.Text = "Save Draw to prod file";
            this.buttonSaveDrawToFile.UseVisualStyleBackColor = true;
            this.buttonSaveDrawToFile.Click += new System.EventHandler(this.buttonSaveDrawToFile_Click);
            // 
            // labelDrawText
            // 
            this.labelDrawText.AutoSize = true;
            this.labelDrawText.Location = new System.Drawing.Point(4, 540);
            this.labelDrawText.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelDrawText.Name = "labelDrawText";
            this.labelDrawText.Size = new System.Drawing.Size(51, 20);
            this.labelDrawText.TabIndex = 18;
            this.labelDrawText.Text = "label1";
            // 
            // buttonInputTeamReadfromTemp
            // 
            this.buttonInputTeamReadfromTemp.Location = new System.Drawing.Point(568, 271);
            this.buttonInputTeamReadfromTemp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonInputTeamReadfromTemp.Name = "buttonInputTeamReadfromTemp";
            this.buttonInputTeamReadfromTemp.Size = new System.Drawing.Size(195, 35);
            this.buttonInputTeamReadfromTemp.TabIndex = 17;
            this.buttonInputTeamReadfromTemp.Text = "Read from temp File";
            this.buttonInputTeamReadfromTemp.UseVisualStyleBackColor = true;
            this.buttonInputTeamReadfromTemp.Click += new System.EventHandler(this.buttonInputTeamReadfromTemp_Click);
            // 
            // buttonTeamInputSaveToTemp
            // 
            this.buttonTeamInputSaveToTemp.Location = new System.Drawing.Point(568, 222);
            this.buttonTeamInputSaveToTemp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonTeamInputSaveToTemp.Name = "buttonTeamInputSaveToTemp";
            this.buttonTeamInputSaveToTemp.Size = new System.Drawing.Size(195, 40);
            this.buttonTeamInputSaveToTemp.TabIndex = 16;
            this.buttonTeamInputSaveToTemp.Text = "Save to temp file";
            this.buttonTeamInputSaveToTemp.UseVisualStyleBackColor = true;
            this.buttonTeamInputSaveToTemp.Click += new System.EventHandler(this.buttonTeamInputSaveToTemp_Click);
            // 
            // dataGridView4
            // 
            this.dataGridView4.AllowUserToAddRows = false;
            this.dataGridView4.AllowUserToDeleteRows = false;
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(772, 18);
            this.dataGridView4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.Size = new System.Drawing.Size(972, 92);
            this.dataGridView4.TabIndex = 15;
            // 
            // buttonDisplayStats
            // 
            this.buttonDisplayStats.Location = new System.Drawing.Point(568, 446);
            this.buttonDisplayStats.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDisplayStats.Name = "buttonDisplayStats";
            this.buttonDisplayStats.Size = new System.Drawing.Size(195, 35);
            this.buttonDisplayStats.TabIndex = 14;
            this.buttonDisplayStats.Text = "Display Stats";
            this.buttonDisplayStats.UseVisualStyleBackColor = true;
            this.buttonDisplayStats.Click += new System.EventHandler(this.buttonDisplayStats_Click);
            // 
            // buttonInputTeamRead
            // 
            this.buttonInputTeamRead.Location = new System.Drawing.Point(568, 18);
            this.buttonInputTeamRead.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonInputTeamRead.Name = "buttonInputTeamRead";
            this.buttonInputTeamRead.Size = new System.Drawing.Size(195, 35);
            this.buttonInputTeamRead.TabIndex = 13;
            this.buttonInputTeamRead.Text = "Read from File";
            this.buttonInputTeamRead.UseVisualStyleBackColor = true;
            this.buttonInputTeamRead.Click += new System.EventHandler(this.buttonInputTeamRead_Click);
            // 
            // buttonTeamInputSave
            // 
            this.buttonTeamInputSave.Location = new System.Drawing.Point(568, 63);
            this.buttonTeamInputSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonTeamInputSave.Name = "buttonTeamInputSave";
            this.buttonTeamInputSave.Size = new System.Drawing.Size(195, 35);
            this.buttonTeamInputSave.TabIndex = 12;
            this.buttonTeamInputSave.Text = "Save to file";
            this.buttonTeamInputSave.UseVisualStyleBackColor = true;
            this.buttonTeamInputSave.Click += new System.EventHandler(this.buttonTeamInputSave_Click);
            // 
            // buttonTeamInputInitialise
            // 
            this.buttonTeamInputInitialise.Location = new System.Drawing.Point(568, 152);
            this.buttonTeamInputInitialise.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonTeamInputInitialise.Name = "buttonTeamInputInitialise";
            this.buttonTeamInputInitialise.Size = new System.Drawing.Size(195, 60);
            this.buttonTeamInputInitialise.TabIndex = 11;
            this.buttonTeamInputInitialise.Text = "Initialise from Dossard (hardcode for 10)";
            this.buttonTeamInputInitialise.UseVisualStyleBackColor = true;
            this.buttonTeamInputInitialise.Click += new System.EventHandler(this.buttonTeamInputInitialise_Click);
            // 
            // dataGridViewInput
            // 
            this.dataGridViewInput.AllowUserToAddRows = false;
            this.dataGridViewInput.AllowUserToDeleteRows = false;
            this.dataGridViewInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewInput.Location = new System.Drawing.Point(21, 9);
            this.dataGridViewInput.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridViewInput.Name = "dataGridViewInput";
            this.dataGridViewInput.Size = new System.Drawing.Size(538, 437);
            this.dataGridViewInput.TabIndex = 10;
            // 
            // buttonDrawNew
            // 
            this.buttonDrawNew.Location = new System.Drawing.Point(568, 357);
            this.buttonDrawNew.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDrawNew.Name = "buttonDrawNew";
            this.buttonDrawNew.Size = new System.Drawing.Size(195, 35);
            this.buttonDrawNew.TabIndex = 9;
            this.buttonDrawNew.Text = "Draw Teams (new)";
            this.buttonDrawNew.UseVisualStyleBackColor = true;
            this.buttonDrawNew.Click += new System.EventHandler(this.buttonDrawNew_Click);
            // 
            // buttonDrawTeams
            // 
            this.buttonDrawTeams.Location = new System.Drawing.Point(568, 402);
            this.buttonDrawTeams.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonDrawTeams.Name = "buttonDrawTeams";
            this.buttonDrawTeams.Size = new System.Drawing.Size(195, 35);
            this.buttonDrawTeams.TabIndex = 5;
            this.buttonDrawTeams.Text = "Draw Teams orig(10)";
            this.buttonDrawTeams.UseVisualStyleBackColor = true;
            this.buttonDrawTeams.Click += new System.EventHandler(this.buttonDrawTeams_Click_1);
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(568, 569);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(1176, 423);
            this.dataGridView2.TabIndex = 7;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(9, 569);
            this.dataGridView3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(550, 423);
            this.dataGridView3.TabIndex = 8;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(772, 120);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(972, 440);
            this.dataGridView1.TabIndex = 6;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1760, 1012);
            this.tabPage2.TabIndex = 3;
            this.tabPage2.Text = "History";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 26);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 35);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1768, 1045);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
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
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridView dataGridView1;
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
        private System.Windows.Forms.Button buttonTeamInputInitialise;
        private System.Windows.Forms.Button buttonTeamInputSave;
        private System.Windows.Forms.Button buttonInputTeamRead;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button buttonDisplayStats;
        private System.Windows.Forms.DataGridView dataGridView4;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonTeamInputSaveToTemp;
        private System.Windows.Forms.Button buttonInputTeamReadfromTemp;
        private System.Windows.Forms.Label labelDrawText;
        private System.Windows.Forms.Button buttonSaveDrawToFile;
        private System.Windows.Forms.Button buttonSimulatePlayerScores;
    }
}

