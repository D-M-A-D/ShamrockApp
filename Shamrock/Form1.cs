using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Force.DeepCloner;
using Combinatorics.Collections;
using System.Configuration;

namespace Shamrock
{
    public partial class Form1 : Form
    {
        List<TeamInput> InputTeamsProd = new List<TeamInput>();
        List<TeamInput> InputTeamsStartOfDraw = new List<TeamInput>();
        List<TeamInput> InputTeamsResultOfDraw = new List<TeamInput>();
        List<DrawRestriction> DrawRestrictions = new List<DrawRestriction>();
        Compet _c; 
        String _dataFolder;
        String _dataInputTeamsSubFolder;
        String _dataRoot;
        int ctDay = 0;
        public Form1()
        {
            InitializeComponent();
            _dataRoot = ConfigurationManager.AppSettings["dataFolderSecret"];
            _dataFolder = Path.Combine(_dataRoot, textBoxDataFolder.Text);
            ctDay = getDayNrFromRadio();
            initialiseCompet();
        }
        private void initialiseFolders()
        {
            _dataFolder = Path.Combine(_dataRoot, textBoxDataFolder.Text);
            _dataInputTeamsSubFolder = Path.Combine(_dataFolder, "DrawTemp");
        }
        private void initialiseCompet(bool doLoadresults = true, int untilDay = 0)
        {
            labelDrawText.Text = "";
            initialiseFolders();
            ctDay = untilDay > 0 ? untilDay:getDayNrFromRadio();
            _c = new Compet(textBoxDataFolder.Text);
            _c.Players = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(_dataFolder, "Players.json")));
            _c.configForYear = JsonConvert.DeserializeObject<ConfigsForYear>(File.ReadAllText(Path.Combine(_dataFolder, "configForYear.json")));
            InputTeamsProd = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataFolder, "InputTeams.json")));
            int nbRounds = _c.configForYear.nbRounds > 0 ? _c.configForYear.nbRounds : 5;

            for (int i = 1; i <= nbRounds; ++i)
            {
                _c.newDay(_c.configForYear.getPlayModeForRound(i));
            }
            for (int i = 1; i <= ctDay; ++i)
            {
                #region Load Course Definition and initialise Flights
                try
                {
                    string path = Path.Combine(_dataFolder, $"Course{i}.json");
                    if(File.Exists(path))
                        _c.getDaybyNr(i).courseDefinition = JsonConvert.DeserializeObject<course>(File.ReadAllText(path));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(String.Format("Could not initialise courseDefinition for day {0}: {1}",ctDay, ex.Message));
                }
                try
                {
                    _c.initialiseflightsFromInputTeam(i, InputTeamsProd);
                    //_c.initialiseflightsForDossard(_c.Players, i);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not initialise Flights for Dossard, check dossard nr" + ex.Message);
                }
                #endregion
                if (doLoadresults)
                {
                    #region Load PlayerScores and MatchScores
                    try
                    {
                        string path = Path.Combine(_dataFolder, $"PlayerScores{i}.json");
                        if (File.Exists(path))
                            _c.getDaybyNr(i).scores = JsonConvert.DeserializeObject<dailyScores>(File.ReadAllText(path));
                    }
                    catch (Exception ex)
                    {
                       MessageBox.Show(String.Format("Could not initialise dailyScores for day {0}: {1}", ctDay, ex.Message));
                    }
                    try
                    {
                        string path = Path.Combine(_dataFolder, $"MatchScores{i}.json");
                        if (File.Exists(path))
                            _c.getDaybyNr(i).matchScores = JsonConvert.DeserializeObject<MatchScores>(File.ReadAllText(path));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Could not initialise MatchScores for day {0}: {1}", ctDay, ex.Message));
                    }
                    try
                    {
                        string path = Path.Combine(_dataFolder, $"ExtraShamrock{i}.json");
                        if (_c.configForYear.useExtra && File.Exists(path))
                            _c.getDaybyNr(i).extras = JsonConvert.DeserializeObject<dailyExtras>(File.ReadAllText(path));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Could not initialise ExtraShamrocks for day {0}: {1}", ctDay, ex.Message));
                    }
                    #endregion
                }
            }
            displayPlayers();
            displayDrawRestrictions(_c.Players.Count);
        }
        private void displayPlayers()
        {
            var bindingList = new BindingList<Player>(_c.Players);
            var source = new BindingSource(bindingList, null);
            dataGridViewPlayers.DataSource = source;
        }
        private void displayDrawRestrictions(int nbPlayer)
        {
            if (DrawRestrictions.Count == 0)
            {
                DrawRestriction drBest8 = new DrawRestriction();
                drBest8.nrTry = 4000;
                drBest8.MaxFlt = 3;
                drBest8.MaxEmy = 2;
                drBest8.MaxTm = 1;

                drBest8.Flt0 = 0;
                drBest8.Emy0 = 0;

                drBest8.Max2B = 4;
                drBest8.MaxWch = 4;
                drBest8.Max4B = 4;
                drBest8.Description = "best for 8 (4 rounds)";

                DrawRestriction drBest8r7 = new DrawRestriction();
                drBest8r7.nrTry = 4000;
                drBest8r7.MaxFlt = 3;
                drBest8r7.MaxEmy = 3;
                drBest8r7.MaxTm = 1;

                drBest8r7.Flt0 = 0;
                drBest8r7.Emy0 = 0;

                drBest8r7.Max2B = 0;
                drBest8r7.MaxWch = 0;
                drBest8r7.Max4B = 7;
                drBest8r7.Description = "best for 8 (7 rounds)";

                DrawRestriction drBest10 = new DrawRestriction();
                drBest10.nrTry = 4000;
                drBest10.MaxFlt = 2;
                drBest10.MaxEmy = 2;
                drBest10.MaxTm = 1;

                drBest10.Flt0 = 0;
                drBest10.Emy0 = 4;

                drBest10.Max2B = 1;
                drBest10.MaxWch = 0;
                drBest10.Max4B = 4;
                drBest10.Description = "probably best for 10";

                DrawRestriction drBest10abandon = new DrawRestriction();
                drBest10abandon.nrTry = 4000;
                drBest10abandon.MaxFlt = 2;
                drBest10abandon.MaxEmy = 2;
                drBest10abandon.MaxTm = 1;

                drBest10abandon.Flt0 = 0;
                drBest10abandon.Emy0 = 3;

                drBest10abandon.Max2B = 2;
                drBest10abandon.MaxWch = 1;
                drBest10abandon.Max4B = 4;
                drBest10abandon.Description = "prob.best 10 (1xAbandon (=0))";

                DrawRestriction dr = new DrawRestriction();
                if (nbPlayer == 10)
                {
                    dr = drBest10.CloneJson();
                    dr.Description = "active";
                    DrawRestrictions.Add(dr);
                    DrawRestrictions.Add(drBest10);
                    DrawRestrictions.Add(drBest10abandon);
                }
                else
                {
                    dr.nrTry = 10000;
                    dr.Max4B = 7;
                    dr.Max2B = 0;
                    dr.MaxWch = 0;
                    dr.MaxFlt = 3;
                    dr.MaxTm = 1;
                    dr.MaxEmy = 3;
                    dr.Flt0 = 0;
                    dr.Emy0 = 0;
                    dr.Description = "active";
                    DrawRestrictions.Add(dr);
                    DrawRestrictions.Add(drBest8);
                    DrawRestrictions.Add(drBest8r7);
                }
            }
            dataGridDrawRestrictionInput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            var bindingList = new BindingList<DrawRestriction>(DrawRestrictions);
            var source = new BindingSource(bindingList, null);
            dataGridDrawRestrictionInput.DataSource = source;
        }

        private void buttonSavePlayers_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(_dataFolder, "Players.json"), JsonConvert.SerializeObject(_c.Players));
        }
        public int getDayNrFromRadio()
        {
            if (radioButton1.Checked)
                return 1;
            else if (radioButton2.Checked)
                return 2;
            else if (radioButton3.Checked)
                return 3;
            else if (radioButton4.Checked)
                return 4;
            else if (radioButton5.Checked)
                return 5;
            else if (radioButton6.Checked)
                return 6;
            else if (radioButton7.Checked)
                return 7;
            else
                return 0;
        }
        private void buttonCourse_Click(object sender, EventArgs e)
        {
            using (Form2 f = new Form2())
            {
                f.ShowDialog(getDayNrFromRadio(), _dataFolder);
            }
            initialiseCompet();
        }
        private void buttonScores_Click(object sender, EventArgs e)
        {
            initialiseCompet();
            using (Form3 f = new Form3())
            {
                f.ShowDialog(getDayNrFromRadio(), _dataFolder, _c);
            }
            initialiseCompet();
        }
        private void buttonMatchResults_Click(object sender, EventArgs e)
        {
            initialiseCompet();
            using (Form4 f = new Form4())
            {
                f.ShowDialog(getDayNrFromRadio(), _dataFolder, _c);
            }
            initialiseCompet();
        }
        private void buttonExtraPts_Click(object sender, EventArgs e)
        {
            initialiseCompet();
            using (FormExtra f = new FormExtra())
            {
                f.ShowDialog(getDayNrFromRadio(), _dataFolder, _c);
            }
            initialiseCompet();
        }
        private void buttonReporting_Click(object sender, EventArgs e)
        {
            initialiseCompet();
            _c.calculateDrawStatXT(_c.Players);
            _c.calculate(getDayNrFromRadio());
            Stream Output = new FileStream(Path.Combine(_dataFolder, "Shamrock.pdf"), FileMode.Create);
            Reporting r = new Reporting();
            r.CreatePDF(Output, _c);
            this.notifyIcon1.BalloonTipText = "PDF produced";
            this.notifyIcon1.BalloonTipTitle = "Done";
            //this.notifyIcon1.Icon = new Icon("3000.ico");
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(3);
            //MessageBox.Show("pdf produced");
        }

        private void buttonShPoints_Click(object sender, EventArgs e)
        {
            initialiseCompet();
            using (Form5 f = new Form5())
            {
                f.ShowDialog(_dataFolder, _c);
            }
            initialiseCompet();
        }

        private void buttonEvaluateSavedDraws_Click(object sender, EventArgs e)
        {
            List<Player> lPlayers2 = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(_dataFolder, "Players.json")));
            lPlayers2.Sort((x, y) => x.name.CompareTo(y.name));

            List<string> ListOfDrawsBeforeCheck = new List<string>();
            List<string> ListOfGoodDraws = new List<string>();
            if (File.Exists(Path.Combine(_dataInputTeamsSubFolder, "ListOfDraws1stGrade.json")))
                ListOfDrawsBeforeCheck = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(Path.Combine(_dataInputTeamsSubFolder, "ListOfDraws1stGrade.json")));

            foreach (string ctDraw in ListOfDrawsBeforeCheck)
            {
                List<TeamInput> ti = JsonConvert.DeserializeObject<List<TeamInput>>(ctDraw);
                drawResults drawRes = new drawResults();
                InitialiseForDraw_ReadSetPlayers(5, drawRes, lPlayers2, ti);
                drawRes.calculateDrawStatXT(lPlayers2);
                if (drawRes.statXTIn2B[1] != 10)
                    continue;
                else if (drawRes.statXTIn4B[4] != 10)
                    continue;
                else if (drawRes.statXTEnemy[0] > 4)
                    continue;
                else if(drawRes.statXTFlight[0] > 0)
                    continue;
                else
                {
                    ListOfGoodDraws.Add(ctDraw);
                }
            }

            int cnt = 0;
            foreach (string ctDraw in ListOfGoodDraws)
            {
                cnt++;
                File.WriteAllText(Path.Combine(_dataInputTeamsSubFolder, string.Format("GoodDraw10_{0}_{1}.json", DateTime.Now.ToString("yymmddhhmmss"), cnt)), ctDraw);
            }
        }
        private void buttonDrawNew_Click(object sender, EventArgs e)
        {
            List<Player> lPlayers2 = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(_dataFolder, "Players.json")));
            lPlayers2.Sort((x, y) => x.name.CompareTo(y.name));

            drawResults drawRes = new drawResults();
            drawResults drawResLastTry = new drawResults();
            
            int nbOfDay = _c.configForYear.nbRounds > 0 ? _c.configForYear.nbRounds: getDayNrFromRadio();
            day ctDay;
            InitialiseForDraw_ReadSetPlayers(nbOfDay, drawRes, lPlayers2);

            Random rnd = new Random();
            int maxTry = 8000;
            int ctTry = 0;
            int maxTimeInSameFlight = 2;
            int maxTimeEnnemy = 2;
            int maxFlight0 = 0;
            int maxEnnemy0 = 4;
            int maxTimeIn2B = 1;
            int maxTimeInWinch = 1;
            int maxTimeIn4B = 1;
            int maxTimeInSameTeam = 1;
            maxTry = DrawRestrictions[0].nrTry;
            maxTimeInSameFlight = DrawRestrictions[0].MaxFlt;
            maxTimeEnnemy = DrawRestrictions[0].MaxEmy;
            maxTimeInSameTeam = DrawRestrictions[0].MaxTm;
            maxFlight0 = DrawRestrictions[0].Flt0;
            maxEnnemy0 = DrawRestrictions[0].Emy0;
            maxTimeIn2B = DrawRestrictions[0].Max2B;
            maxTimeInWinch = DrawRestrictions[0].MaxWch;
            maxTimeIn4B = DrawRestrictions[0].Max4B;
            Boolean found = false;
            DateTime start = DateTime.Now;
            labelDrawText.Text = String.Format("Starting draw... {0} tries", maxTry);
            labelDrawText.Refresh();
            string diagnostic = "";
            List<string> ListOfDrawsBeforeCheck = new List<string>();
            if(File.Exists(Path.Combine(_dataInputTeamsSubFolder, "ListOfDraws1stGrade.json")))
                ListOfDrawsBeforeCheck = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText(Path.Combine(_dataInputTeamsSubFolder, "ListOfDraws1stGrade.json")));
            for (int tryNr = 0; tryNr < maxTry; tryNr++)
            {
                ctTry = tryNr;
                Boolean doInterruptDraw = false; //interrupt means not good, make an other try
                //drawResults drawResTry = drawRes.CloneJson(); //take a copy at the beginning of each try
                drawResults drawResTry = drawRes.quickCloneForDraw(); //take a copy at the beginning of each try
                #region next days
                for (int dayNr = 1; dayNr <= nbOfDay; ++dayNr)
                {
                    ctDay = drawResTry.getDaybyNr(dayNr);

                    if (ctDay.PlayersLeftToDraw.Count > 0)
                    {
                        List<Player> PlayersToDraw = ctDay.PlayersLeftToDraw;
                        PlayersToDraw.Shuffle();
                        List<int> drawPool = new List<int>();
                        for (int d = 0; d < PlayersToDraw.Count; d++)
                            drawPool.Add(d);
                        #region Loop for day
                        #region loop for flight, teams and try to fill the empty places
                        foreach (flight ctFlight in ctDay.flights.Values.OrderBy(x => x.GetTheoreticalNbOfPlayer())) //make sure to get the 2B first (probably easier to get available players
                        {
                            foreach (team ctTeam in ctFlight.teams.Values)
                            {
                                for (int i = 0; i < ctTeam.nbOfMensh; i++)
                                {
                                    String TeamString = ctFlight.name + ctTeam.name1based;
                                    //Check if not full already (allready set)
                                    if (!ctTeam.IsFull())
                                    {
                                        Player candidate = null;
                                        String sHowManyTime = "";

                                        diagnostic = "";
                                        int validDraw = -1;
                                        foreach (int draw in drawPool)
                                        {
                                            candidate = PlayersToDraw[draw];
                                            diagnostic = dayNr + " " + TeamString + " " + candidate.name;
                                            int HowManyTimeInSameFlight = 0;
                                            int HowManyTimeEnemy = 0;
                                            foreach (Player ctFlightMate in ctFlight.GetFligtMates(candidate))
                                            {
                                                int ctHT = drawResTry.GetHowManyTimeInSameFlight(candidate, ctFlightMate);
                                                if (ctHT > HowManyTimeInSameFlight)
                                                    HowManyTimeInSameFlight = ctHT;
                                                int ctHTE = drawResTry.GetHowManyTimeEnemy(candidate, ctFlightMate);
                                                if (ctHTE > HowManyTimeEnemy)
                                                    HowManyTimeEnemy = ctHTE;
                                            }
                                            if (HowManyTimeInSameFlight <= (maxTimeInSameFlight - 1) && HowManyTimeEnemy <= (maxTimeEnnemy - 1)) // good go ahead with this candidate
                                            {
                                                if (ctFlight.matchType == flight.MatchType.Match2b || ctFlight.matchType == flight.MatchType.Winch) //2B check how many time in 2B
                                                {
                                                    #region 2B + Winch
                                                    Boolean maxTimeOk = false;
                                                    if (ctFlight.matchType == flight.MatchType.Winch)
                                                    {
                                                        int HowManyTime = drawResTry.GetHowManyTimeInWinch(candidate);
                                                        diagnostic += " " + ctFlight.matchType + " " + HowManyTime;
                                                        if (HowManyTime <= maxTimeInWinch - 1)
                                                            maxTimeOk = true;
                                                    }
                                                    else if (ctFlight.matchType == flight.MatchType.Match2b)
                                                    {
                                                        int HowManyTime = drawResTry.GetHowManyTimeIn2B(candidate);
                                                        diagnostic += " " + ctFlight.matchType + " " + HowManyTime;
                                                        if (HowManyTime <= maxTimeIn2B - 1)
                                                            maxTimeOk = true;
                                                    }
                                                    if (maxTimeOk)
                                                    {
                                                        validDraw = draw;
                                                        break; //goood
                                                    }
                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region 4B and foursome
                                                    int HowManyTimeInSameTeam = 0;
                                                    Boolean maxTimeOk = false;
                                                    if (ctFlight.matchType == flight.MatchType.Match4b || ctFlight.matchType == flight.MatchType.Foursome )
                                                    {
                                                        int HowManyTime = drawResTry.GetHowManyTimeIn4B(candidate);
                                                        diagnostic += " " + ctFlight.matchType + " " + HowManyTime;
                                                        if (HowManyTime <= maxTimeIn4B - 1)
                                                            maxTimeOk = true;
                                                    }
                                                    else
                                                        maxTimeOk = true;

                                                    if (maxTimeOk)
                                                    {
                                                        List<Player> ctTeamMates = ctTeam.GetTeamMates(candidate);
                                                        if (ctTeamMates.Count > 0)
                                                        {
                                                            //toCheck9
                                                            bool teamMateOk = true;
                                                            foreach (Player ctTeamMate in ctTeam.GetTeamMates(candidate))
                                                            {
                                                                HowManyTimeInSameTeam = drawResTry.GetHowManyTimeInSameTeam(candidate, ctTeamMate);
                                                                sHowManyTime = String.Format("{0}x SameT, {1} & {2}", HowManyTimeInSameTeam, candidate.name, ctTeamMate.name);
                                                                diagnostic += " TeamMate " + ctTeamMate.name + " " + HowManyTimeInSameTeam;
                                                                if (HowManyTimeInSameTeam >= maxTimeInSameTeam) //not good take an other candidate
                                                                {
                                                                    teamMateOk = false;
                                                                    break; // not goood
                                                                }
                                                            }
                                                            if (teamMateOk)
                                                            {
                                                                validDraw = draw;
                                                                break; //goood
                                                            }
                                                        }
                                                        else //nobody else in team up to know, go ahead (add)
                                                        {
                                                            validDraw = draw;
                                                            break;// good
                                                        }
                                                    }
                                                    else
                                                        maxTimeOk = true;
                                                    #endregion
                                                }
                                            }
                                            else
                                                diagnostic += " sameFlight: " + HowManyTimeInSameFlight + " enemy:" + HowManyTimeEnemy;
                                        }
                                        if (validDraw < 0)
                                        {
                                            doInterruptDraw = true;
                                            break;
                                        }
                                        ctTeam.SetPlayer(new dayInputTeamForPlayer { PlayerName = candidate.name, FlightName = ctFlight.name, TeamName = ctTeam.name1based  }, candidate);
                                        drawPool.Remove(validDraw);
                                    }
                                }
                                if (doInterruptDraw)
                                    break;
                            }
                            if (doInterruptDraw)
                                break;
                        }
                        #endregion
                        if (doInterruptDraw)
                            break;
                    }

                    if (nbOfDay == ctDay.nr) // can only be checked at the end
                    {
                        List<TeamInput> ctInputTeam = GetInputTeamFromDraw(drawResTry);
                        string ctsInputTeam = JsonConvert.SerializeObject(ctInputTeam);
                        if (ListOfDrawsBeforeCheck.Contains(ctsInputTeam))
                            diagnostic = "";
                        else
                            ListOfDrawsBeforeCheck.Add(ctsInputTeam);

                        diagnostic = "End of day check: ";
                        drawResTry.calculateDrawStatXT(ctDay.PlayersForTheDay);
                        int statMaxFlight0 = drawResTry.statXTFlight.ContainsKey(0) ? drawResTry.statXTFlight[0] : 0;
                        int statMaxEnnemy0 = drawResTry.statXTEnemy.ContainsKey(0) ? drawResTry.statXTEnemy[0] : 0;
                        if (statMaxFlight0 > maxFlight0 || statMaxEnnemy0 > maxEnnemy0
                            )
                        {
                            diagnostic += string.Format("failed statMaxFlight0: {0}, statMaxEnnemy0: {1} ", statMaxFlight0, statMaxEnnemy0);
                            doInterruptDraw = true; // not good
                            break;
                        }
                        else
                        {
                            diagnostic += string.Format("good statMaxFlight0: {0}, statMaxEnnemy0: {1} ", statMaxFlight0, statMaxEnnemy0);
                        }
                    }
                }
                    #endregion
                #endregion
                #region if succesfull (doInterruptDraw=false) end + display stats
                if (!doInterruptDraw)
                {
                    #region display xFlight-xEnemy-xTeam Matrix
                    dataGridPlayerMatrix.ColumnCount = lPlayers2.Count + 1;
                    dataGridPlayerMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dataGridPlayerMatrix.Rows.Clear();
                    dataGridPlayerMatrix.Columns[0].Name = "Flt|Emy|Tm";

                    for (int iP1 = 0; iP1 < lPlayers2.Count; iP1++)
                    {
                        dataGridPlayerMatrix.Columns[iP1 + 1].Name = lPlayers2[iP1].name.Left(4);
                        dataGridPlayerMatrix.Rows.Add();
                        dataGridPlayerMatrix.Rows[iP1].Cells[0].Value = lPlayers2[iP1].name.Left(4);
                        for (int iP2 = 0; iP2 < lPlayers2.Count; iP2++)
                        {
                            if (iP1 > iP2)
                            {
                                int ctHTFlight = drawResTry.GetHowManyTimeInSameFlight(lPlayers2[iP1], lPlayers2[iP2]);
                                int ctHTEnemy = drawResTry.GetHowManyTimeEnemy(lPlayers2[iP1], lPlayers2[iP2]);
                                int ctHTteam = drawResTry.GetHowManyTimeInSameTeam(lPlayers2[iP1], lPlayers2[iP2]);
                                dataGridPlayerMatrix.Rows[iP1].Cells[iP2 + 1].Value = String.Format("{0} | {1} | {2}", ctHTFlight, ctHTEnemy, ctHTteam);
                                if (ctHTEnemy == 0)
                                    dataGridPlayerMatrix.Rows[iP1].Cells[iP2 + 1].Style.BackColor = Color.Azure;
                            }
                        }
                    }
                    #endregion
                    #region display stats
                    dataGridPlayerMatrixStats.ColumnCount = 8;
                    dataGridPlayerMatrixStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dataGridPlayerMatrixStats.Rows.Clear();
                    dataGridPlayerMatrixStats.Rows.Add();
                    dataGridPlayerMatrixStats.Rows.Add();
                    dataGridPlayerMatrixStats.Rows.Add();
                    dataGridPlayerMatrixStats.Rows.Add();
                    dataGridPlayerMatrixStats.Rows.Add();
                    for (int i = 0; i < dataGridPlayerMatrixStats.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            dataGridPlayerMatrixStats.Rows[0].Cells[i].Value = "Flight";
                            dataGridPlayerMatrixStats.Rows[1].Cells[i].Value = "Enemy";
                            dataGridPlayerMatrixStats.Rows[2].Cells[i].Value = "in2B";
                            dataGridPlayerMatrixStats.Rows[3].Cells[i].Value = "inWinch";
                            dataGridPlayerMatrixStats.Rows[4].Cells[i].Value = "in4B";
                        }
                        else
                        {
                            dataGridPlayerMatrixStats.Columns[i].Name = (i - 1).ToString();
                            dataGridPlayerMatrixStats.Rows[0].Cells[i].Value = drawResTry.statXTFlight[i - 1];
                            dataGridPlayerMatrixStats.Rows[1].Cells[i].Value = drawResTry.statXTEnemy[i - 1];
                            dataGridPlayerMatrixStats.Rows[2].Cells[i].Value = drawResTry.statXTIn2B[i - 1];
                            dataGridPlayerMatrixStats.Rows[3].Cells[i].Value = drawResTry.statXTInWinch[i - 1];
                            dataGridPlayerMatrixStats.Rows[4].Cells[i].Value = drawResTry.statXTIn4B[i - 1];
                        }
                    }
                    #endregion
                    //no exception, than it's good
                    found = true;
                    drawRes = drawResTry;
                    break;
                }
                #endregion
            }
            DateTime end = DateTime.Now;
            if (!found)
            {
                labelDrawText.Text = String.Format("no solution found after {0} tries. Time {1:T} ({2})", maxTry, end - start, diagnostic);
                drawResLastTry.calculateDrawStatXT(lPlayers2);
                DisplayDrawPlayer(drawResLastTry, lPlayers2);
                DisplayTeamsGrid(drawResLastTry);
            }
            else
            {
                labelDrawText.Text = String.Format("Solution found after {0} tries. Time {1:T}", ctTry, end - start);
                DisplayDrawPlayer(drawRes, lPlayers2);
                DisplayTeamsGrid(drawRes);
            }
            File.WriteAllText(Path.Combine(_dataInputTeamsSubFolder, "ListOfDraws1stGrade.json"), JsonConvert.SerializeObject(ListOfDrawsBeforeCheck));

            #region add results to InputTeamsTemp
            Dictionary<string, TeamInput> tis = new Dictionary<string, TeamInput>();
            foreach (day ctD in drawRes.days)
            {
                foreach (flight ctFlight in ctD.flights.Values)
                {
                    foreach (team ctTeam in ctFlight.teams.Values)
                    {
                        foreach (playingBall b in ctTeam.playingBalls.Values)
                        {
                            foreach (Player P in b.players.Values)
                            {
                                if (!tis.ContainsKey(P.name))
                                    tis.Add(P.name, new TeamInput { Player = P.name });

                                string ballName = "";
                                if (ctFlight.matchType == flight.MatchType.Match4b5 && b.nbOfPlayerForBall > 1)
                                    ballName = b.name;
                                switch (ctD.nr)
                                {
                                    case 1: tis[P.name].R1 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 2: tis[P.name].R2 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 3: tis[P.name].R3 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 4: tis[P.name].R4 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 5: tis[P.name].R5 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 6: tis[P.name].R6 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 7: tis[P.name].R7 = ctFlight.name + ctTeam.name1based + ballName; break;
                                }
                            }
                        }
                    }
                }
            }
            InputTeamsResultOfDraw = tis.Values.ToList();
            #endregion

            //string json = JsonConvert.SerializeObject(drawRes);
        }
        private void buttonDrawPerm_Click(object sender, EventArgs e)
        {
            List<Player> lPlayers = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(_dataFolder, "Players.json")));
            lPlayers.Sort((x, y) => x.startNumber.CompareTo(y.startNumber));

            //read day1 from init
            //pool pairs 2b
            //pool teams 4b

            //get 2b team day1
            //get 4b flight day1
            //get 4b teams day1
            //combination 2balls pairs --> loop
            //  set 2b pair

            drawResults drawRes = new drawResults();
            drawResults drawResLastTry = new drawResults();
            List<drawResults> drawResTrys = new List<drawResults>(30000);

            InitialiseForDraw_ReadSetPlayers(5, drawRes, lPlayers);

            int[] Players = new int[]{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            int[] Positions = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            List<int> PlayersLeftFor2b = new List<int>(8);
            foreach (int ctPl in Players)
            {
                string ctPlName = lPlayers[ctPl].name;
                if (drawRes.days[0].flights["C"].teams["0"].ContainsPlayer(ctPlName))
                    continue;
                else if (drawRes.days[0].flights["C"].teams["1"].ContainsPlayer(ctPlName))
                    continue;
                else
                    PlayersLeftFor2b.Add(ctPl);
            }

            Dictionary<int, List<int>> dictionaryPairs_2b;
            List<int[]> keyCombination_2b;
            GetCombination_2balls_8player_4days(PlayersLeftFor2b, out dictionaryPairs_2b, out keyCombination_2b);

            int cnt_2b = 0;
            int cnt_tot = 0;
            foreach (int[] pairKeys_2b in keyCombination_2b)
            {
                cnt_2b++;
                int ctDay = 0;
                drawResults drawResTry_2b = drawRes.quickCloneForDraw(); //take a copy at the beginning of each try
                List<List<int>> PlayersLeftFor4bs = new List<List<int>>();
                List<List<int[]>> keyCombination_4bs = new List<List<int[]>>();
                List<Dictionary<int, List<int>>> dictionaryPairs_4bs = new List<Dictionary<int, List<int>>>();
                foreach (int pairKey_2b in pairKeys_2b) // one pair per day, --> do not need to loop for days
                {
                    //set 2b
                    ctDay++;
                    drawResTry_2b.days[ctDay].flights["C"].teams["0"].SetPlayer(new dayInputTeamForPlayer { PlayerName = lPlayers[dictionaryPairs_2b[pairKey_2b][0]].name, FlightName = "C", TeamName = "1" }, lPlayers[dictionaryPairs_2b[pairKey_2b][0]]);
                    drawResTry_2b.days[ctDay].flights["C"].teams["1"].SetPlayer(new dayInputTeamForPlayer { PlayerName = lPlayers[dictionaryPairs_2b[pairKey_2b][1]].name, FlightName = "C", TeamName = "2" }, lPlayers[dictionaryPairs_2b[pairKey_2b][1]]);

                    //remainingplayer for 2x4b --> combination for day
                    List<int> PlayersLeftFor4b = new List<int>(8);
                    foreach (int ctPl in Players)
                    {
                        string ctPlName = lPlayers[ctPl].name;
                        if (drawResTry_2b.days[ctDay].flights["C"].teams["0"].ContainsPlayer(ctPlName))
                            continue;
                        else if (drawResTry_2b.days[ctDay].flights["C"].teams["1"].ContainsPlayer(ctPlName))
                            continue;
                        else
                            PlayersLeftFor4b.Add(ctPl);
                    }
                    PlayersLeftFor4bs.Add(PlayersLeftFor4b);

                    List<int[]> keyCombination_4b;
                    Dictionary<int, List<int>> dictionaryPairs_4b;
                    GetCombination_2_4balls_8player_1day(PlayersLeftFor4b, out dictionaryPairs_4b, out keyCombination_4b);
                    keyCombination_4bs.Add(keyCombination_4b);
                    dictionaryPairs_4bs.Add(dictionaryPairs_4b);
                }

                List<List<int[]>> keyWeekcombination_4bs = new List<List<int[]>>(300000);
                foreach (List<int[]> keyCombination_4b in keyCombination_4bs)
                {
                    List<int[]> keyWeekcombination_4b = new List<int[]>();
                    foreach (int[] pairKeys_4b in keyCombination_4b)
                    {
                        keyWeekcombination_4b.Add(pairKeys_4b);
                    }
                    keyWeekcombination_4bs.Add(keyWeekcombination_4b);
                }


                ctDay = 0;
                drawResults drawResTry_4b = null;
                foreach (int[] pairKeys_4b_0 in keyCombination_4bs[0]) //day 1
                {
                    drawResTry_4b = drawResTry_2b.quickCloneForDraw(); //take a copy at the beginning of each try
                    SetPlayersForCombination_4b(lPlayers, drawResTry_4b, pairKeys_4b_0, dictionaryPairs_4bs[0]);
                    foreach (int[] pairKeys_4b_1 in keyCombination_4bs[1]) //day 2
                    {
                        SetPlayersForCombination_4b(lPlayers, drawResTry_4b, pairKeys_4b_1, dictionaryPairs_4bs[1]);
                        foreach (int[] pairKeys_4b_2 in keyCombination_4bs[2]) //day 3
                        {
                            SetPlayersForCombination_4b(lPlayers, drawResTry_4b, pairKeys_4b_2, dictionaryPairs_4bs[2]);
                            foreach (int[] pairKeys_4b_3 in keyCombination_4bs[3]) //day 4
                            {
                                SetPlayersForCombination_4b(lPlayers, drawResTry_4b, pairKeys_4b_3, dictionaryPairs_4bs[3]);
                                cnt_tot++;
                            }
                            cnt_tot++;
                        }
                        cnt_tot++;
                    }
                    cnt_tot++;
                }
                if (ctDay == 4)
                {
                    drawResTry_4b.calculateDrawStatXT(lPlayers);
                    if (drawResTry_4b.statXTIn2B[1] != 2)
                        continue;
                    if (drawResTry_4b.statXTIn4B[4] != 2)
                        continue;
                    drawResTrys.Add(drawResTry_4b);
                }
            }

            //SavePermutations();

            List<int[]> DailyPerms = new List<int[]>(30000);
            DailyPerms = JsonConvert.DeserializeObject<List<int[]>>(File.ReadAllText(Path.Combine(_dataFolder, "DailyPerms10.json")));

            int cntCombination = 0;
            var DailyPermCombinations = DailyPerms.Combinations<int[]>(4);
            foreach (var PermCombination4 in DailyPermCombinations)
            {
                cntCombination++;
                int PermDayNr = 2;
                drawResults drawResTry = drawRes.quickCloneForDraw(); //take a copy at the beginning of each try
                foreach (var dailyPerm in PermCombination4)
                {
                    addDayToDrawRes(drawResTry, lPlayers, (int[])dailyPerm, PermDayNr++);
                }
                drawResTry.calculateDrawStatXT(lPlayers);
                if(drawResTry.statXTIn2B[1] != 10)
                    continue;
                if (drawResTry.statXTIn4B[4] != 10)
                    continue;
                drawResTrys.Add(drawResTry);
            }

            //testCombination();
        }
        private void buttonDrawPerm_old_Click(object sender, EventArgs e)
        {
            List<Player> lPlayers2 = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(_dataFolder, "Players.json")));
            lPlayers2.Sort((x, y) => x.name.CompareTo(y.name));

            drawResults drawRes = new drawResults();
            drawResults drawResLastTry = new drawResults();
            List<drawResults> drawResTrys = new List<drawResults>(30000);

            int nbOfDay = getDayNrFromRadio();
            InitialiseForDraw_ReadSetPlayers(nbOfDay, drawRes, lPlayers2);

            //SavePermutations();

            List<int[]> DailyPerms = new List<int[]>(30000);
            DailyPerms = JsonConvert.DeserializeObject<List<int[]>>(File.ReadAllText(Path.Combine(_dataFolder, "DailyPerms10.json")));

            int cntCombination = 0;
            var DailyPermCombinations = DailyPerms.Combinations<int[]>(4);
            foreach (var PermCombination4 in DailyPermCombinations)
            {
                cntCombination++;
                int PermDayNr = 2;
                drawResults drawResTry = drawRes.quickCloneForDraw(); //take a copy at the beginning of each try
                foreach (var dailyPerm in PermCombination4)
                {
                    addDayToDrawRes(drawResTry, lPlayers2, (int[])dailyPerm, PermDayNr++);
                }
                drawResTry.calculateDrawStatXT(lPlayers2);
                if (drawResTry.statXTIn2B[1] != 10)
                    continue;
                if (drawResTry.statXTIn4B[4] != 10)
                    continue;
                drawResTrys.Add(drawResTry);
            }

            //testCombination();
        }
        private void buttonDrawTeams_Click_1(object sender, EventArgs e)
        {
            List<Player> lPlayers2 = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(_dataFolder, "Players.json")));

            //string json = JsonConvert.SerializeObject(lPlayers);
            lPlayers2.Sort((x, y) => x.name.CompareTo(y.name));
            //lPlayers2.Sort((x, y) => x.initialHcp.CompareTo(y.initialHcp));
            //lPlayers2.Shuffle();

            drawResults drawRes = new drawResults();

            day ctDay;
            #region day1
            ctDay = drawRes.newDay(_c.configForYear.getPlayModeForRound(1));
            //lPlayers2.Shuffle();

            int cnt = -1;
            foreach (flight ctFlight in ctDay.flights.Values)
            {
                foreach (team ctTeam in ctFlight.teams.Values)
                {
                    for (int i = 0; i < ctTeam.nbOfBall; i++)
                    {
                        ++cnt;
                        Player candidate = lPlayers2[cnt];
                        ctTeam.players.Add(candidate.name, candidate);
                        //listViewDraw.Items.Add(new ListViewItem(new[] { "1", ctFlight.name, ctTeam.name, lPlayers2[cnt].name, "" }));
                    }
                }
            }
            #endregion

            Random rnd = new Random();
            int maxTry = 2000000;
            int maxTimeInSameFlight = 2;
            int maxTimeEnnemy = 2;
            int maxFlight0 = 0;
            int maxEnnemy0 = 4;
            int maxTimeIn2B = 1;
            int maxTimeInSameTeam = 1;
            int nbOfDay = 5;
            Boolean found = false;
            for (int tryNr = 0; tryNr < maxTry; tryNr++)
            {
                Boolean doInterruptDraw = false;
                day Day1 = drawRes.days[0];
                drawRes.days.Clear();
                drawRes.days.Add(Day1);
                #region next days
                for (int dayNr = 2; dayNr <= nbOfDay; ++dayNr)
                {
                    ctDay = drawRes.newDay(_c.configForYear.getPlayModeForRound(dayNr));
                    lPlayers2.Shuffle();
                    List<int> drawPool = new List<int>();
                    for (int d = 0; d < lPlayers2.Count; d++)
                        drawPool.Add(d);
                    #region Loop for day
                    foreach (flight ctFlight in ctDay.flights.Values.OrderBy(x => x.GetTheoreticalNbOfPlayer())) //make sure to get the 2B first (probably easier to get available players
                    {
                        foreach (team ctTeam in ctFlight.teams.Values)
                        {
                            for (int i = 0; i < ctTeam.nbOfBall; i++)
                            {
                                Player candidate = null;
                                String sHowManyTime = "";
                                int validDraw = -1;
                                foreach (int draw in drawPool)
                                {
                                    candidate = lPlayers2[draw];
                                    int HowManyTimeInSameFlight = 0;
                                    int HowManyTimeEnemy = 0;
                                    foreach (Player ctFlightMate in ctFlight.GetFligtMates(candidate))
                                    {
                                        int ctHT = drawRes.GetHowManyTimeInSameFlight(candidate, ctFlightMate);
                                        if (ctHT > HowManyTimeInSameFlight)
                                            HowManyTimeInSameFlight = ctHT;
                                        int ctHTE = drawRes.GetHowManyTimeEnemy(candidate, ctFlightMate);
                                        if (ctHTE > HowManyTimeEnemy)
                                            HowManyTimeEnemy = ctHTE;
                                    }
                                    if (HowManyTimeInSameFlight <= (maxTimeInSameFlight - 1) && HowManyTimeEnemy <= (maxTimeEnnemy - 1)) // good go ahead with this candidate
                                    {
                                        int HowManyTimeIn2B = 0;
                                        if (ctFlight.matchType == flight.MatchType.Match2b) //2B check how many time in 2B
                                        {
                                            #region 2B
                                            HowManyTimeIn2B = drawRes.GetHowManyTimeIn2B(candidate);
                                            sHowManyTime = String.Format("{0}x 2B, {1}", HowManyTimeIn2B, candidate.name);
                                            if (HowManyTimeIn2B <= maxTimeIn2B - 1)
                                            {
                                                validDraw = draw;
                                                break; //goood
                                            }
                                            #endregion
                                        }
                                        else
                                        {
                                            #region 4B and other
                                            Player TeamMate = null;
                                            int HowManyTimeInSameTeam = 0;

                                            if (ctTeam.GetTeamMate(candidate, ref TeamMate))
                                            {
                                                HowManyTimeInSameTeam = drawRes.GetHowManyTimeInSameTeam(candidate, TeamMate);
                                                sHowManyTime = String.Format("{0}x SameT, {1} & {2}", HowManyTimeInSameTeam, candidate.name, TeamMate.name);
                                                if (HowManyTimeInSameTeam < maxTimeInSameTeam) //not good take an other candidate
                                                {
                                                    validDraw = draw;
                                                    break; //goood
                                                }
                                            }
                                            else //nobody else in team up to know, go ahead (add)
                                            {
                                                validDraw = draw;
                                                break;// good
                                            }
                                            #endregion
                                        }
                                    }
                                }
                                if (validDraw < 0)
                                {
                                    doInterruptDraw = true;
                                    break;
                                }
                                ctTeam.players.Add(candidate.name, candidate);
                                drawPool.Remove(validDraw);
                            }
                            if (doInterruptDraw)
                                break;
                        }
                        if (doInterruptDraw)
                            break;
                    }
                    if (doInterruptDraw)
                        break;

                    if (nbOfDay == ctDay.nr) // can only be checked at the end
                    {
                        drawRes.calculateDrawStatXT(lPlayers2);
                        if (drawRes.statXTFlight.ContainsKey(0) && drawRes.statXTFlight[0] > maxFlight0
                                || drawRes.statXTEnemy.ContainsKey(0) && drawRes.statXTEnemy[0] > maxEnnemy0
                            )
                        {
                            doInterruptDraw = true;
                            break;
                        }
                    }
                }
                    #endregion
                #endregion
                #region if succesfull (doInterruptDraw=false) end + display stats
                if (!doInterruptDraw)
                {
                    #region display xFlight-xEnemy-xTeam Matrix
                    dataGridPlayerMatrix.ColumnCount = lPlayers2.Count + 1;
                    dataGridPlayerMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dataGridPlayerMatrix.Rows.Clear();
                    dataGridPlayerMatrix.Columns[0].Name = "Flight|Enemy|Team";

                    for (int iP1 = 0; iP1 < lPlayers2.Count; iP1++)
                    {
                        dataGridPlayerMatrix.Columns[iP1 + 1].Name = lPlayers2[iP1].name.Left(4);
                        dataGridPlayerMatrix.Rows.Add();
                        dataGridPlayerMatrix.Rows[iP1].Cells[0].Value = lPlayers2[iP1].name.Left(4);
                        for (int iP2 = 0; iP2 < lPlayers2.Count; iP2++)
                        {
                            if (iP1 > iP2)
                            {
                                int ctHTFlight = drawRes.GetHowManyTimeInSameFlight(lPlayers2[iP1], lPlayers2[iP2]);
                                int ctHTEnemy = drawRes.GetHowManyTimeEnemy(lPlayers2[iP1], lPlayers2[iP2]);
                                dataGridPlayerMatrix.Rows[iP1].Cells[iP2 + 1].Value = String.Format("{0} | {1}", ctHTFlight, ctHTEnemy);
                                if (ctHTEnemy == 0)
                                    dataGridPlayerMatrix.Rows[iP1].Cells[iP2 + 1].Style.BackColor = Color.Azure;
                            }
                        }
                    }
                    #endregion
                    #region display stats
                    dataGridPlayerMatrixStats.ColumnCount = 8;
                    dataGridPlayerMatrixStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dataGridPlayerMatrixStats.Rows.Clear();
                    dataGridPlayerMatrixStats.Rows.Add();
                    dataGridPlayerMatrixStats.Rows.Add();
                    dataGridPlayerMatrixStats.Rows.Add();
                    for (int i = 0; i < dataGridPlayerMatrixStats.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            dataGridPlayerMatrixStats.Rows[0].Cells[i].Value = "Flight";
                            dataGridPlayerMatrixStats.Rows[1].Cells[i].Value = "Enemy";
                            dataGridPlayerMatrixStats.Rows[2].Cells[i].Value = "in2B";
                        }
                        else
                        {
                            dataGridPlayerMatrixStats.Columns[i].Name = (i - 1).ToString();
                            dataGridPlayerMatrixStats.Rows[0].Cells[i].Value = drawRes.statXTFlight[i - 1];
                            dataGridPlayerMatrixStats.Rows[1].Cells[i].Value = drawRes.statXTEnemy[i - 1];
                            dataGridPlayerMatrixStats.Rows[2].Cells[i].Value = drawRes.statXTIn2B[i - 1];
                        }
                    }
                    #endregion
                    //no exception, than it's good
                    found = true;
                    break;
                }
                #endregion
            }
            if (!found)
                MessageBox.Show(String.Format("no solution found after {0} tries ", maxTry));

            DisplayTeamsGrid(drawRes);
            //#region display teams Grid
            //dataGridTeamsGrid.ColumnCount = drawRes.days.Count + 1;
            //dataGridTeamsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            //dataGridTeamsGrid.Rows.Clear();

            //dataGridTeamsGrid.Rows.Add(lPlayers2.Count);
            //foreach (day ctD in drawRes.days)
            //{
            //    int ctRow = -1;
            //    dataGridTeamsGrid.Columns[(int)ctD.nr].Name = ctD.nr.ToString();
            //    foreach (flight ctFlight in ctD.flights.Values)
            //    {
            //        foreach (team ctTeam in ctFlight.teams.Values)
            //        {
            //            foreach (Player P in ctTeam.players.Values)
            //            {
            //                ctRow++;
            //                dataGridTeamsGrid.Rows[ctRow].Cells[0].Value = ctFlight.name + ctTeam.name;
            //                dataGridTeamsGrid.Rows[ctRow].Cells[(int)ctD.nr].Value = P.name;
            //            }
            //        }
            //    }
            //}
            //#endregion

            string json = JsonConvert.SerializeObject(drawRes);
        }

        private void EvaluateDrawRes(drawResults drawRes, List<Player> lPlayers2)
        {
            

        }
        private void addDayToDrawRes(drawResults drawRes, List<Player> lPlayers2, int[] perm, int dayNr)
        {
            day ctDay = drawRes.getDaybyNr(dayNr);

            for (int permI = 0; permI < 10; permI++)
            {
                Player candidate = lPlayers2[permI];
                int ctPosition = perm[permI];
                switch(ctPosition)
                {
                    case 0: ctDay.flights["C"].teams["0"].players.Add(candidate.name, candidate); break;
                    case 1: ctDay.flights["C"].teams["1"].players.Add(candidate.name, candidate); break;
                    case 2: ctDay.flights["A"].teams["0"].players.Add(candidate.name, candidate); break;
                    case 3: ctDay.flights["A"].teams["0"].players.Add(candidate.name, candidate); break;
                    case 4: ctDay.flights["A"].teams["1"].players.Add(candidate.name, candidate); break;
                    case 5: ctDay.flights["A"].teams["1"].players.Add(candidate.name, candidate); break;
                    case 6: ctDay.flights["B"].teams["0"].players.Add(candidate.name, candidate); break;
                    case 7: ctDay.flights["B"].teams["0"].players.Add(candidate.name, candidate); break;
                    case 8: ctDay.flights["B"].teams["1"].players.Add(candidate.name, candidate); break;
                    case 9: ctDay.flights["B"].teams["1"].players.Add(candidate.name, candidate); break;
                }
            }
        }
        private List<TeamInput> GetInputTeamFromDraw (drawResults drawRes)
        {
            Dictionary<string, TeamInput> tis = new Dictionary<string, TeamInput>();
            foreach (day ctD in drawRes.days)
            {
                foreach (flight ctFlight in ctD.flights.Values)
                {
                    foreach (team ctTeam in ctFlight.teams.Values)
                    {
                        foreach (playingBall b in ctTeam.playingBalls.Values)
                        {
                            foreach (Player P in b.players.Values)
                            {
                                if (!tis.ContainsKey(P.name))
                                    tis.Add(P.name, new TeamInput { Player = P.name });

                                string ballName = "";
                                if (ctFlight.matchType == flight.MatchType.Match4b5 && b.nbOfPlayerForBall > 1)
                                    ballName = b.name;
                                switch (ctD.nr)
                                {
                                    case 1: tis[P.name].R1 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 2: tis[P.name].R2 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 3: tis[P.name].R3 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 4: tis[P.name].R4 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 5: tis[P.name].R5 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 6: tis[P.name].R6 = ctFlight.name + ctTeam.name1based + ballName; break;
                                    case 7: tis[P.name].R7 = ctFlight.name + ctTeam.name1based + ballName; break;
                                }
                            }
                        }
                    }
                }
            }
            return tis.Values.ToList();
        }

        static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
        private void SavePermutations()
        {

            IEnumerable<IEnumerable<int>> AllPermutations = GetPermutations(Enumerable.Range(0, 10), 10); //Permutation Pairs of Players (10 personnes --> lot of permutations, (too much as 1+2 = 2+1, filter out later)
            int cntCombination = 0;
            List<int[]> Perms = new List<int[]>(30000);
            foreach (var PPlayers in AllPermutations)
            {
                int[] ArrayPlayer = new int[10];
                int cnt = 0;
                int prevPPair = 0;
                bool PairInGoodOrder = true;
                foreach (int PPlayer in PPlayers)
                {
                    switch (cnt)
                    {
                        case 1: // first pair (playing 2 balls)
                            if (prevPPair >= PPlayer)
                                PairInGoodOrder = false; // 1-2 is equivalent to 2-1 in team, so ignore this combination
                            break;
                        case 3: // Second pair (playing 4 balls)
                            if (prevPPair >= PPlayer)
                                PairInGoodOrder = false;
                            break;
                        case 5: // third pair (playing 4 balls)
                            if (prevPPair >= PPlayer)
                            {
                                PairInGoodOrder = false;
                                break;
                            }
                            int firstPair = ArrayPlayer[2] * 100 + ArrayPlayer[3];
                            int secondPair = ArrayPlayer[4] * 100 + PPlayer;
                            if ((Math.Pow((double)ArrayPlayer[2], 2) + Math.Pow((double)ArrayPlayer[3], 2)) >= Math.Pow((double)ArrayPlayer[4], 2) + Math.Pow((double)PPlayer, 2))
                                PairInGoodOrder = false; // 1-2 vs 3-4 is equivalent to 3-4 vs 1-2 in 4 balls, so ignore this combination
                            break;
                        case 7: // fourth pair (playing 4 balls)
                            if (prevPPair >= PPlayer)
                                PairInGoodOrder = false;
                            break;
                        case 9: // fifth pair (playing 4 balls)
                            if (prevPPair >= PPlayer)
                            {
                                PairInGoodOrder = false;
                                break;
                            }
                            if ((Math.Pow((double)ArrayPlayer[6], 2) + Math.Pow((double)ArrayPlayer[7], 2)) >= Math.Pow((double)ArrayPlayer[8], 2) + Math.Pow((double)PPlayer, 2))
                                PairInGoodOrder = false;
                            break;
                    }
                    if (!PairInGoodOrder)
                        break;
                    ArrayPlayer[cnt++] = PPlayer;
                    prevPPair = PPlayer;
                }
                if (!PairInGoodOrder)
                    continue;
                cntCombination++;
                Console.WriteLine(cntCombination.ToString() + ": " + string.Join(",", ArrayPlayer));
                Perms.Add(ArrayPlayer);
            }
            File.WriteAllText(Path.Combine(_dataFolder, "DailyPerms10.json"), JsonConvert.SerializeObject(Perms)); 
        }

        private void SetPlayersForCombination_4b(List<Player> lPlayers, drawResults drawRes, int[] pairKeys_4b, Dictionary<int, List<int>> dictionaryPairs_4b)
        {
            Player ctPlayer; string ctFlight; string ctTeam;
            ctFlight = "A";
            ctTeam = "0";
            ctPlayer = lPlayers[dictionaryPairs_4b[pairKeys_4b[0]][0]];
            drawRes.days[ctDay].flights[ctFlight].teams[ctTeam].SetPlayer(new dayInputTeamForPlayer { PlayerName = ctPlayer.name, FlightName = ctFlight, TeamName = ctTeam }, ctPlayer);
            ctPlayer = lPlayers[dictionaryPairs_4b[pairKeys_4b[0]][1]];
            drawRes.days[ctDay].flights[ctFlight].teams[ctTeam].SetPlayer(new dayInputTeamForPlayer { PlayerName = ctPlayer.name, FlightName = ctFlight, TeamName = ctTeam }, ctPlayer);
            ctTeam = "1";
            ctPlayer = lPlayers[dictionaryPairs_4b[pairKeys_4b[0]][0]];
            drawRes.days[ctDay].flights[ctFlight].teams[ctTeam].SetPlayer(new dayInputTeamForPlayer { PlayerName = ctPlayer.name, FlightName = ctFlight, TeamName = ctTeam }, ctPlayer);
            ctPlayer = lPlayers[dictionaryPairs_4b[pairKeys_4b[0]][1]];
            drawRes.days[ctDay].flights[ctFlight].teams[ctTeam].SetPlayer(new dayInputTeamForPlayer { PlayerName = ctPlayer.name, FlightName = ctFlight, TeamName = ctTeam }, ctPlayer);
            ctFlight = "B";
            ctTeam = "0";
            ctPlayer = lPlayers[dictionaryPairs_4b[pairKeys_4b[0]][0]];
            drawRes.days[ctDay].flights[ctFlight].teams[ctTeam].SetPlayer(new dayInputTeamForPlayer { PlayerName = ctPlayer.name, FlightName = ctFlight, TeamName = ctTeam }, ctPlayer);
            ctPlayer = lPlayers[dictionaryPairs_4b[pairKeys_4b[0]][1]];
            drawRes.days[ctDay].flights[ctFlight].teams[ctTeam].SetPlayer(new dayInputTeamForPlayer { PlayerName = ctPlayer.name, FlightName = ctFlight, TeamName = ctTeam }, ctPlayer);
            ctTeam = "1";
            ctPlayer = lPlayers[dictionaryPairs_4b[pairKeys_4b[0]][0]];
            drawRes.days[ctDay].flights[ctFlight].teams[ctTeam].SetPlayer(new dayInputTeamForPlayer { PlayerName = ctPlayer.name, FlightName = ctFlight, TeamName = ctTeam }, ctPlayer);
            ctPlayer = lPlayers[dictionaryPairs_4b[pairKeys_4b[0]][1]];
            drawRes.days[ctDay].flights[ctFlight].teams[ctTeam].SetPlayer(new dayInputTeamForPlayer { PlayerName = ctPlayer.name, FlightName = ctFlight, TeamName = ctTeam }, ctPlayer);
        }
        private void GetCombination_2_4balls_8player_1day(List<int> PlayersLeft, out Dictionary<int, List<int>> dictionaryPairs, out List<int[]> CombinationOfKeys)
        {
            // 1-2 equivaux 2-1
            // 1-2 vs 3-4 equivaux 3-4 vs 1-2
            // A et B equivaux B et A
            dictionaryPairs = new Dictionary<int, List<int>>();
            IEnumerable<IEnumerable<int>> combs = GetPermutations(PlayersLeft, PlayersLeft.Count); //Permutation 8 people
            List<int[]> combKeys = new List<int[]>();
            foreach (var comb in combs)
            {
                List<List<int>> ctPairs = new List<List<int>>();
                List<int> ctPair = new List<int>();
                int cnt = 0;
                int prevPlayer = 0;

                bool goodpair = true;
                foreach (var p in comb)
                {
                    if (cnt % 2 != 0)
                    {
                        if (prevPlayer < p)
                        {
                            ctPair.Add(prevPlayer);
                            ctPair.Add(p);
                            ctPairs.Add(ctPair.CloneJson<List<int>>());
                            ctPair.Clear();
                        }
                        else
                        {
                            goodpair = false;
                            break;
                        }
                    }
                    prevPlayer = p;
                    cnt++;
                }
                if (goodpair)
                {
                    List<int> keys = new List<int>();
                    foreach (var item in ctPairs)
                    {
                        int key = (int)(Math.Pow((double)item[0] + 1, 2) + Math.Pow((double)item[1] + 1, 2));
                        keys.Add(key);
                        if (!dictionaryPairs.ContainsKey(key))
                            dictionaryPairs.Add(key, item);
                    }
                    combKeys.Add(keys.ToArray());
                }
                else
                    continue;
            }
            //int cntD = 0;
            CombinationOfKeys = new List<int[]>();
            foreach (int[] item in combKeys)
            {
                if (item[0] > item[1])
                    continue;
                if (item[2] > item[3])
                    continue;
                if (string.Join(", ", new int[] { item[0], item[1] }).GetHashCode() > string.Join(", ", new int[] { item[2], item[3] }).GetHashCode())
                    continue;
                CombinationOfKeys.Add(item);
                //Console.WriteLine(string.Join(", ", item));
                //cntD++;
            }
        }
        private void GetCombination_2balls_8player_4days (List<int> PlayersLeft, out Dictionary<int, List<int>> dictionaryPairs, out List<int[]> CombinationOfKeys)
        {
            dictionaryPairs = new Dictionary<int, List<int>>();
            IEnumerable<IEnumerable<int>> combs = GetPermutations(PlayersLeft, PlayersLeft.Count); //Permutation 8 people
            List<int[]> combKeys = new List<int[]>();
            foreach (var comb in combs)
            {
                List<List<int>> ctPairs = new List<List<int>>();
                List<int> ctPair = new List<int>();
                int cnt = 0;
                int prevPlayer = 0;

                bool goodpair = true;
                foreach (var p in comb)
                {
                    if (cnt % 2 != 0)
                    {
                        if (prevPlayer < p)
                        {
                            ctPair.Add(prevPlayer);
                            ctPair.Add(p);
                            ctPairs.Add(ctPair.CloneJson<List<int>>());
                            ctPair.Clear();
                        }
                        else
                        {
                            goodpair = false;
                            break;
                        }
                    }
                    prevPlayer = p;
                    cnt++;
                }
                if (goodpair)
                {
                    List<int> keys = new List<int>();
                    foreach (var item in ctPairs)
                    {
                        int key = (int)(Math.Pow((double)item[0] + 1, 2) + Math.Pow((double)item[1] + 1, 2));
                        keys.Add(key);
                        if (!dictionaryPairs.ContainsKey(key))
                            dictionaryPairs.Add(key, item);
                    }
                    keys.Sort();
                    combKeys.Add(keys.ToArray());
                }
                else
                    continue;
            }
            //int cntD = 0;
            CombinationOfKeys = new List<int[]>();
            var myComp = new EqualityArrayInt();
            foreach (int[] item in combKeys.Distinct<int[]>(myComp))
            {
                CombinationOfKeys.Add(item);
                //Console.WriteLine(string.Join(", ", item));
                //cntD++;
            }
        }
        private void testCombination()
        {
            int[] myList = new int[] { 1, 2, 3, 4, 5 };
            var result = myList.Combinations<int>(3);
            foreach(var res in result)
            {
                foreach (var i in res)
                    Console.Write(i + " ");
                Console.WriteLine("");
            }
        }

        private void InitialiseForDraw_ReadSetPlayers(int nbOfDay, drawResults drawRes, List<Player> lPlayers2)
        {
            day ctDay;
            #region initialise from InputTeamsStartOfDraw (check for Abandon = 0, and forced place (p.ex. stay in 2b)
            for (int dayNr = 1; dayNr <= nbOfDay; ++dayNr)
            {
                //initialise day
                ctDay = drawRes.newDay(_c.configForYear.getPlayModeForRound(dayNr));

                //List<Player> ctPlayers = lPlayers2.CloneJson();
                List<Player> ctPlayers = lPlayers2.ConvertAll<Player>(x => new Player { name = x.name, startNumber = x.startNumber });
                //remove Players if abandon 0
                foreach (TeamInput ti in InputTeamsStartOfDraw)
                {
                    String ctI = ((String)ti.GetType().GetProperty(String.Format("R{0}", dayNr)).GetValue(ti));
                    if (!string.IsNullOrEmpty(ctI))
                        ctI.Trim();
                    if (ctI == "0")
                        ctPlayers.RemoveAt(ctPlayers.FindIndex(x => x.name == ti.Player));
                    else if (!string.IsNullOrEmpty(ctI)) //players with info
                    {
                        ctDay.SetPlayer(new dayInputTeamForPlayer
                        {
                            PlayerName = ti.Player,
                            FlightName = ctI.Substring(0, 1),
                            TeamName = ctI.Substring(1, 1),
                            BallName = ctI.Remove(0, 2)
                        }, ctPlayers.Find(x => x.name == ti.Player).CloneJson());
                    }
                    else
                        ctDay.PlayersLeftToDraw.Add(ctPlayers.Find(x => x.name == ti.Player).CloneJson()); //Players without inputTeam Info
                }
                ctDay.PlayersForTheDay = ctPlayers.CloneJson(); //all players without Abandon
            }
            #endregion
        }
        private void InitialiseForDraw_ReadSetPlayers(int nbOfDay, drawResults drawRes, List<Player> lPlayers2, List<TeamInput> InputTeams)
        {
            day ctDay;
            #region initialise from InputTeamsStartOfDraw (check for Abandon = 0, and forced place (p.ex. stay in 2b)
            for (int dayNr = 1; dayNr <= nbOfDay; ++dayNr)
            {
                //initialise day
                ctDay = drawRes.newDay(_c.configForYear.getPlayModeForRound(dayNr));

                //List<Player> ctPlayers = lPlayers2.CloneJson();
                List<Player> ctPlayers = lPlayers2.ConvertAll<Player>(x => new Player { name = x.name, startNumber = x.startNumber });
                //remove Players if abandon 0
                foreach (TeamInput ti in InputTeams)
                {
                    String ctI = ((String)ti.GetType().GetProperty(String.Format("R{0}", dayNr)).GetValue(ti));
                    if (!string.IsNullOrEmpty(ctI))
                        ctI.Trim();
                    if (ctI == "0")
                        ctPlayers.RemoveAt(ctPlayers.FindIndex(x => x.name == ti.Player));
                    else if (!string.IsNullOrEmpty(ctI)) //players with info
                    {
                        ctDay.SetPlayer(new dayInputTeamForPlayer
                        {
                            PlayerName = ti.Player,
                            FlightName = ctI.Substring(0, 1),
                            TeamName = ctI.Substring(1, 1),
                            BallName = ctI.Remove(0, 2)
                        }, ctPlayers.Find(x => x.name == ti.Player).CloneJson());
                    }
                    else
                        ctDay.PlayersLeftToDraw.Add(ctPlayers.Find(x => x.name == ti.Player).CloneJson()); //Players without inputTeam Info
                }
                ctDay.PlayersForTheDay = ctPlayers.CloneJson(); //all players without Abandon
            }
            #endregion
        }
        private void DisplayTeamsGrid(drawResults drawRes)
        {
            dataGridTeamsGrid.ColumnCount = drawRes.days.Count * 2;
            dataGridTeamsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridTeamsGrid.Rows.Clear();

            if (drawRes.days.Count > 0)
                dataGridTeamsGrid.Rows.Add(drawRes.days[0].CountPlayer());
            foreach (day ctD in drawRes.days)
            {
                int ctRow = -1;
                dataGridTeamsGrid.Columns[(int)((ctD.nr - 1) * 2)].Name = ctD.nr.ToString();
                dataGridTeamsGrid.Columns[(int)(((ctD.nr - 1) * 2) + 1)].Name = ctD.playMode.ToString();
                foreach (flight ctFlight in ctD.flights.Values)
                {
                    foreach (team ctTeam in ctFlight.teams.Values)
                    {
                        foreach (playingBall b in ctTeam.playingBalls.Values)
                        {
                            foreach (Player P in b.players.Values)
                            {
                                ctRow++;
                                string ballName = "";
                                if (ctFlight.matchType == flight.MatchType.Match4b5 && b.nbOfPlayerForBall > 1)
                                    ballName = b.name;
                                dataGridTeamsGrid.Rows[ctRow].Cells[(int)((ctD.nr - 1) * 2)].Value = ctFlight.name + ctTeam.name1based + ballName;
                                dataGridTeamsGrid.Rows[ctRow].Cells[(int)(((ctD.nr - 1) * 2) + 1)].Value = P.name;
                            }
                        }
                    }
                }
            }
        }
        private void DisplayDrawPlayer(drawResults drawRes, List<Player> Players)
        {
            #region display xFlight-xEnemy-xTeam Matrix
            dataGridPlayerMatrix.ColumnCount = Players.Count + 1;
            dataGridPlayerMatrix.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridPlayerMatrix.Rows.Clear();
            dataGridPlayerMatrix.Columns[0].Name = "Flt|Emy";

            for (int iP1 = 0; iP1 < Players.Count; iP1++)
            {
                dataGridPlayerMatrix.Columns[iP1 + 1].Name = Players[iP1].name.Left(4);
                dataGridPlayerMatrix.Rows.Add();
                dataGridPlayerMatrix.Rows[iP1].Cells[0].Value = Players[iP1].name.Left(4);
                for (int iP2 = 0; iP2 < Players.Count; iP2++)
                {
                    if (iP1 > iP2)
                    {
                        int ctHTFlight = drawRes.GetHowManyTimeInSameFlight(Players[iP1], Players[iP2]);
                        int ctHTEnemy = drawRes.GetHowManyTimeEnemy(Players[iP1], Players[iP2]);
                        int ctHTteam = drawRes.GetHowManyTimeInSameTeam(Players[iP1], Players[iP2]);
                        dataGridPlayerMatrix.Rows[iP1].Cells[iP2 + 1].Value = String.Format("{0} | {1}", ctHTFlight, ctHTEnemy, ctHTteam);
                        if (ctHTEnemy == 0)
                            dataGridPlayerMatrix.Rows[iP1].Cells[iP2 + 1].Style.BackColor = Color.Azure;
                    }
                }
            }
            #endregion
            #region display stats
            dataGridPlayerMatrixStats.ColumnCount = 8;
            dataGridPlayerMatrixStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridPlayerMatrixStats.Rows.Clear();
            dataGridPlayerMatrixStats.Rows.Add();
            dataGridPlayerMatrixStats.Rows.Add();
            dataGridPlayerMatrixStats.Rows.Add();
            dataGridPlayerMatrixStats.Rows.Add();
            dataGridPlayerMatrixStats.Rows.Add();
            for (int i = 0; i < dataGridPlayerMatrixStats.Columns.Count; i++)
            {
                if (i == 0)
                {
                    dataGridPlayerMatrixStats.Rows[0].Cells[i].Value = "Flight";
                    dataGridPlayerMatrixStats.Rows[1].Cells[i].Value = "Enemy";
                    dataGridPlayerMatrixStats.Rows[2].Cells[i].Value = "in2B";
                    dataGridPlayerMatrixStats.Rows[3].Cells[i].Value = "inWinch";
                    dataGridPlayerMatrixStats.Rows[4].Cells[i].Value = "in4B";
                }
                else
                {
                    dataGridPlayerMatrixStats.Columns[i].Name = (i - 1).ToString();
                    dataGridPlayerMatrixStats.Rows[0].Cells[i].Value = drawRes.statXTFlight[i - 1];
                    dataGridPlayerMatrixStats.Rows[1].Cells[i].Value = drawRes.statXTEnemy[i - 1];
                    dataGridPlayerMatrixStats.Rows[2].Cells[i].Value = drawRes.statXTIn2B[i - 1];
                    dataGridPlayerMatrixStats.Rows[3].Cells[i].Value = drawRes.statXTInWinch[i - 1];
                    dataGridPlayerMatrixStats.Rows[4].Cells[i].Value = drawRes.statXTIn4B[i - 1];
                }
            }
            #endregion
        }

        private void buttonStartDraw_TeamInput_InitialiseHardCoded_10_Click(object sender, EventArgs e)
        {
            initialiseFolders();
            initialiseCompet(true);
            dataGridViewInput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewInput.Rows.Clear();
            List<Player> tmpPlayers = _c.Players.CloneJson();
            tmpPlayers.Sort((x, y) => x.startNumber.CompareTo(y.startNumber));
            int[] dayOrder = new int[] { };
            for (int dayNr = 1; dayNr <= 5; ++dayNr)
            {
                switch (dayNr)
                {
                    case 1: dayOrder = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }; break;
                    case 2: dayOrder = new int[] { 9, 2, 5, 3, 7, 10, 4, 8, 6, 1 }; break;
                    case 3: dayOrder = new int[] { 10, 2, 8, 1, 9, 3, 7, 6, 5, 4 }; break;
                    case 4: dayOrder = new int[] { 9, 1, 5, 8, 3, 6, 4, 10, 7, 2 }; break;
                    case 5: dayOrder = new int[] { 9, 7, 1, 4, 2, 5, 10, 6, 3, 8 }; break;
                }


                int cnt = -1;
                foreach (flight ctFlight in _c.getDaybyNr(dayNr).flights.Values)
                {
                    foreach (team ctTeam in ctFlight.teams.Values)
                    {
                        for (int i = 0; i < ctTeam.nbOfMensh; i++)
                        {
                            ++cnt;
                            
                            Player candidate = tmpPlayers[dayOrder[cnt] - 1];
                            TeamInput ti = InputTeamsStartOfDraw.Find(x => x.Player == candidate.name);
                            if(ti != null)
                                InputTeamsStartOfDraw.Remove(ti);
                            else
                                ti = new TeamInput();
                            ti.Player = candidate.name;
                            switch (dayNr)
                            {
                                case 1: ti.R1 = ctFlight.name + ctTeam.name1based; break;
                                case 2: ti.R2 = ctFlight.name + ctTeam.name1based; break;
                                case 3: ti.R3 = ctFlight.name + ctTeam.name1based; break;
                                case 4: ti.R4 = ctFlight.name + ctTeam.name1based; break;
                                case 5: ti.R5 = ctFlight.name + ctTeam.name1based; break;
                                case 6: ti.R6 = ctFlight.name + ctTeam.name1based; break;
                                case 7: ti.R7 = ctFlight.name + ctTeam.name1based; break;
                            }
                            InputTeamsStartOfDraw.Add(ti);
                        }
                    }
                }
                
            }
            InputTeamsStartOfDraw.Sort((x, y) => x.R1.CompareTo(y.R1));
            var bindingListP = new BindingList<TeamInput>(InputTeamsStartOfDraw);
            var sourceP = new BindingSource(bindingListP, null);
            dataGridViewInput.DataSource = sourceP;

        }
        private void buttonStartDraw_TeamInput_SaveProd_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(_dataFolder, "InputTeams.json"), JsonConvert.SerializeObject(InputTeamsStartOfDraw));
        }
        private void buttonStartDraw_TeamInput_LoadProd_Click(object sender, EventArgs e)
        {
            InputTeamsStartOfDraw = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataFolder, "InputTeams.json")));
            BindStartOfDraw(InputTeamsStartOfDraw);
        }
        private void buttonStartDraw_TeamInput_SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.InitialDirectory = _dataInputTeamsSubFolder;
            saveFileDialog1.Filter = "*.json|";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(InputTeamsStartOfDraw));
            }
        }
        private void buttonStartDraw_TeamInput_LoadFrom_Click(object sender, EventArgs e)
        {
            initialiseFolders();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = _dataInputTeamsSubFolder;
            openFileDialog1.Filter = "*.json|";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                InputTeamsStartOfDraw = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(openFileDialog1.FileName));
                BindStartOfDraw(InputTeamsStartOfDraw);
            }
        }
        private void buttonResultDraw_TeamInput_SaveProd_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(_dataFolder, "InputTeams.json"), JsonConvert.SerializeObject(InputTeamsResultOfDraw));
        }
        private void buttonResultDraw_TeamInput_SaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.InitialDirectory = _dataInputTeamsSubFolder;
            saveFileDialog1.Filter = "json files (*.json)";
            saveFileDialog1.FilterIndex = 2;
            saveFileDialog1.RestoreDirectory = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, JsonConvert.SerializeObject(InputTeamsResultOfDraw));
            }
        }

        private void buttonDisplayStats_Click(object sender, EventArgs e)
        {
            List<Player> lPlayers = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(_dataFolder, "Players.json")));
            lPlayers.Sort((x, y) => x.startNumber.CompareTo(y.startNumber));

            //initialiseCompet();
            drawResults drawRes = new drawResults();
            
            InitialiseForDraw_ReadSetPlayers(_c.days.Count, drawRes, lPlayers, InputTeamsStartOfDraw);
            drawRes.calculateDrawStatXT(lPlayers);

            DisplayDrawPlayer(drawRes, lPlayers);
            DisplayTeamsGrid(drawRes);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //List<string> years = new List<string> { "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017" };
            //List<string> years = new List<string> { "2012", "2013", "2014" };
            List<string> years = new List<string>();
            foreach (var item in listBoxYearForStats.SelectedItems)
            {
                years.Add(item.ToString());

            }
            Dictionary<string, Compet> hC = new Dictionary<string, Compet>();

            try
            {
                #region read everything for years
                foreach (string ctYear in years)
                {
                    string ctDF = Path.Combine(_dataRoot, ctYear);
                    Compet ctC = new Compet(ctYear);
                    ctC.Players = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(ctDF, "Players.json")));
                    ctC.configForYear = JsonConvert.DeserializeObject<ConfigsForYear>(File.ReadAllText(Path.Combine(ctDF, "configForYear.json")));
                    List<TeamInput> ctTeams = new List<TeamInput>();
                    ctTeams = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(ctDF, "InputTeams.json")));
                    int nbRounds = _c.configForYear.nbRounds > 0 ? _c.configForYear.nbRounds : 5;

                    for (int i = 1; i <= nbRounds; ++i)
                    {
                        day.PlayMode ctPlayMode = ctC.configForYear.getPlayModeForRound(i);
                        if (ctPlayMode == day.PlayMode.NoGame)
                            continue;
                        ctC.newDay(ctPlayMode);
                        try
                        {
                            ctC.getDaybyNr(i).courseDefinition = JsonConvert.DeserializeObject<course>(File.ReadAllText(Path.Combine(ctDF, String.Format("Course{0}.json", i))));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(String.Format("Could not initialise courseDefinition for day {0}: {1}", i, ex.Message));
                        }
                        try
                        {
                            ctC.initialiseflightsFromInputTeam(i, ctTeams);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Could not initialise Flights for Dossard, check dossard nr" + ex.Message);
                        }
                        try
                        {
                            ctC.getDaybyNr(i).scores = JsonConvert.DeserializeObject<dailyScores>(File.ReadAllText(Path.Combine(ctDF, String.Format("PlayerScores{0}.json", i))));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(String.Format("Could not initialise dailyScores for day {0}: {1}", ctDay, ex.Message));
                        }
                        try
                        {
                            ctC.getDaybyNr(i).matchScores = JsonConvert.DeserializeObject<MatchScores>(File.ReadAllText(Path.Combine(ctDF, String.Format("MatchScores{0}.json", i))));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(String.Format("Could not initialise MatchScores for day {0}: {1}", ctDay, ex.Message));
                        }
                    }
                    hC.Add(ctYear, ctC);
                }
                #endregion
                #region loop and create PlayerMatchHistory
                statsMatch statsM = new statsMatch();
                foreach (string ctYear in years)
                {
                    Compet ctC = hC[ctYear];
                    statsM.addC(ctC);
                }
                statsM.Calc();
                #endregion

                #region writeCSVFile
                System.IO.StreamWriter csvFile = new System.IO.StreamWriter(Path.Combine(_dataRoot, "MatchHistory.csv"));
                string separator = ",";
                csvFile.Write("Player" + separator);
                csvFile.Write("Year" + separator);
                csvFile.Write("day" + separator);
                csvFile.Write("matchName" + separator);
                csvFile.Write("matchType" + separator);
                csvFile.Write("cntPlayer" + separator);
                csvFile.Write("WLD" + separator);
                csvFile.Write("Win" + separator);
                csvFile.Write("Played" + separator);
                csvFile.Write("myTeamName" + separator);
                csvFile.Write("TeamMate" + separator);
                csvFile.Write("opTeamName" + separator);
                csvFile.Write("opPlayer1" + separator);
                csvFile.Write("opPlayer2" + separator);
                csvFile.WriteLine();
                foreach (MatchHistory ctPMatchHist in statsM.histMatchsForPlayer.Values)
                {
                    double gagné = 0;
                    foreach (MatchInfo ctPMInfo in ctPMatchHist.matchs)
                    {
                        csvFile.Write(ctPMatchHist.name + separator);
                        csvFile.Write(ctPMInfo.year + separator);
                        csvFile.Write(ctPMInfo.dayNr.ToString() + separator);
                        csvFile.Write(ctPMInfo.matchName + separator);
                        csvFile.Write(ctPMInfo.flight.matchType + separator);
                        csvFile.Write(ctPMInfo.flight.GetRealNbOfPlayer().ToString() + separator);
                        match ctMatch = ctPMInfo.flight.matchs[ctPMInfo.matchName];
                        if (ctMatch.myMatchResult(ctPMatchHist.name, ctPMInfo.matchScore.WinnerteamName) == 0)
                        {
                            gagné += 0.5;
                            csvFile.Write("D" + separator);
                            csvFile.Write("0.5" + separator);
                        }
                        else if (ctMatch.myMatchResult(ctPMatchHist.name, ctPMInfo.matchScore.WinnerteamName) == 1)
                        {
                            ++gagné;
                            csvFile.Write("W" + separator);
                            csvFile.Write("1" + separator);
                        }
                        else
                        {
                            csvFile.Write("L" + separator);
                            csvFile.Write("0" + separator);

                        }
                        csvFile.Write("1" + separator);

                        if (ctPMInfo.myTeam != null)
                        {
                            csvFile.Write(ctPMInfo.myTeam.name + separator);
                            csvFile.Write(ctPMInfo.myTeam.GetTeamMate(ctPMatchHist.name) + separator);
                        }
                        else
                        {
                            csvFile.Write(separator);
                            csvFile.Write(separator);
                        }
                        if (ctPMInfo.opTeam != null)
                        {
                            csvFile.Write(ctPMInfo.opTeam.name + separator);
                            if (ctPMInfo.opTeam.players.Count == 2)
                            {
                                foreach (string opPName in ctPMInfo.opTeam.players.Keys)
                                    csvFile.Write(opPName + separator);
                            }
                            else if (ctPMInfo.opTeam.players.Count == 1)
                            {
                                foreach (string opPName in ctPMInfo.opTeam.players.Keys)
                                    csvFile.Write(opPName + separator);
                                csvFile.Write(separator);
                            }
                        }
                        else
                        {
                            csvFile.Write(separator);
                            csvFile.Write(separator);
                            csvFile.Write(separator);
                        }
                        csvFile.WriteLine();
                    }
                    double perc = (double)gagné / (double)ctPMatchHist.matchs.Count;
                    string res = string.Format("{0} matchs sur {1} ({2:P})", gagné, ctPMatchHist.matchs.Count, perc);
                }
                csvFile.Close();
                #endregion
                #region writeCSVFileTeamMate
                csvFile = new System.IO.StreamWriter(Path.Combine(_dataRoot, "statsMatchTeamMate.csv"));
                csvFile.Write("PairKey" + separator);
                csvFile.Write("Player1" + separator);
                csvFile.Write("Player2" + separator);
                csvFile.Write("Won" + separator);
                csvFile.Write("Played" + separator);
                csvFile.Write("Perc" + separator);
                csvFile.WriteLine();
                foreach (string ctPairKey in statsM.statMatchsAsTeam.Keys)
                {
                    statMatchsForKey ctSM = statsM.statMatchsAsTeam[ctPairKey];
                    csvFile.Write(ctPairKey + separator);
                    csvFile.Write(ctSM.playerName1 + separator);
                    csvFile.Write(ctSM.playerName2 + separator);
                    csvFile.Write(ctSM.won + separator);
                    csvFile.Write(ctSM.played + separator);
                    csvFile.Write(ctSM.percWon + separator);
                    csvFile.WriteLine();
                }
                csvFile.Close();
                #endregion
                #region writeCSVFileTeamMate
                csvFile = new System.IO.StreamWriter(Path.Combine(_dataRoot, "statsMatchBeteNoire.csv"));
                csvFile.Write("Player" + separator);
                csvFile.Write("Opponent" + separator);
                csvFile.Write("Won" + separator);
                csvFile.Write("Played" + separator);
                csvFile.Write("Perc" + separator);
                csvFile.WriteLine();
                foreach (string ctPlayer in statsM.statMatchsBeteNoire.Keys)
                {
                    Dictionary<string, wonPlayedStat> ctDictWP = statsM.statMatchsBeteNoire[ctPlayer];
                    foreach (string ctOpponent in ctDictWP.Keys)
                    {
                        csvFile.Write(ctPlayer + separator);
                        csvFile.Write(ctOpponent + separator);
                        csvFile.Write(ctDictWP[ctOpponent].won + separator);
                        csvFile.Write(ctDictWP[ctOpponent].played + separator);
                        csvFile.Write(ctDictWP[ctOpponent].percWon + separator);
                        csvFile.WriteLine();
                    }
                }
                csvFile.Close();
                #endregion
            }
            catch(Exception ex)
            {
                MessageBox.Show(String.Format("unknown in Calc History : {0}", ex.Message));
            }
        }
        private void buttonSimulatePlayerScores_Click(object sender, EventArgs e)
        {
            initialiseCompet();
            foreach (day d in _c.days)
            {
                d.simulateScores();
                File.WriteAllText(Path.Combine(_dataFolder, String.Format("PlayerScores{0}.json", d.nr)), JsonConvert.SerializeObject(d.scores));
            }
        }
        private void buttonCalcHoleStats_Click(object sender, EventArgs e)
        {
            initialiseCompet();
            _c.calculate(getDayNrFromRadio());
            List<HoleForStat> statHoles = _c.GetHoleStats(getDayNrFromRadio());
            #region writeCSVFile HoleStats
            System.IO.StreamWriter csvFile = new System.IO.StreamWriter(Path.Combine(_dataRoot, "statsHolesOverall.csv"));
            string separator = ",";
            csvFile.Write("Year" + separator);
            csvFile.Write("Player" + separator);
            csvFile.Write("Birdies or Better" + separator);
            csvFile.Write("Par" + separator);
            csvFile.Write("Bog" + separator);
            csvFile.Write("Dbl" + separator);
            csvFile.Write("Trp" + separator);
            csvFile.Write("X" + separator);
            csvFile.WriteLine();
            foreach (Player p in _c.Players)
            {
                csvFile.Write(_c.year + separator);
                csvFile.Write(p.name + separator);
                List<HoleForStat> plH_all = statHoles.FindAll(x => x.playerName == p.name);
                List<HoleForStat> plH_birdie = plH_all.FindAll(x => (x.getResType() == HoleForStat.resType.Birdie || x.getResType() == HoleForStat.resType.Eagle || x.getResType() == HoleForStat.resType.Albatros));
                csvFile.Write(((double)plH_birdie.Count / (double)plH_all.Count).ToString() + separator);
                List<HoleForStat> plH_par = plH_all.FindAll(x => x.getResType() == HoleForStat.resType.Par);
                csvFile.Write(((double)plH_par.Count / (double)plH_all.Count).ToString() + separator);
                List<HoleForStat> plH_bog = plH_all.FindAll(x => x.getResType() == HoleForStat.resType.Boguey);
                csvFile.Write(((double)plH_bog.Count / (double)plH_all.Count).ToString() + separator);
                List<HoleForStat> plH_bog2 = plH_all.FindAll(x => x.getResType() == HoleForStat.resType.Bog2);
                csvFile.Write(((double)plH_bog2.Count / (double)plH_all.Count).ToString() + separator);
                List<HoleForStat> plH_bog3 = plH_all.FindAll(x => x.getResType() == HoleForStat.resType.Bog3);
                csvFile.Write(((double)plH_bog3.Count / (double)plH_all.Count).ToString() + separator);
                List<HoleForStat> plH_X = plH_all.FindAll(x => x.getResType() == HoleForStat.resType.X);
                csvFile.Write(((double)plH_X.Count / (double)plH_all.Count).ToString() + separator);
                csvFile.WriteLine();
            }
            csvFile.Close();
            #endregion

        }

        private void BindStartOfDraw(List<TeamInput> InputTeamsStartOfDraw)
        {
            dataGridViewInput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewInput.Rows.Clear();

            var bindingListP = new BindingList<TeamInput>(InputTeamsStartOfDraw);
            var sourceP = new BindingSource(bindingListP, null);
            dataGridViewInput.DataSource = sourceP;
        }
        private void button10_helper(List<TeamInput> InputTeamsStartOfDraw)
        {
            initialiseFolders();
            List<Player> lPlayers = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(_dataFolder, "Players.json")));
            lPlayers.Sort((x, y) => x.startNumber.CompareTo(y.startNumber));

            //change Player accroding to startNumber
            for (int i = 0; i < 10; i++)
            {
                InputTeamsStartOfDraw[i].Player = lPlayers[i].name;
            }
            InputTeamsStartOfDraw.Sort((x, y) => x.R1.CompareTo(y.R1));

            lPlayers.Sort((x, y) => x.initialHcp.CompareTo(y.initialHcp));
            InputTeamsStartOfDraw.Sort((x, y) => lPlayers.FindIndex(z => z.name == x.Player).CompareTo(lPlayers.FindIndex(z => z.name == y.Player)));
            BindStartOfDraw(InputTeamsStartOfDraw);

            drawResults drawRes = new drawResults();
            InitialiseForDraw_ReadSetPlayers(5, drawRes, lPlayers, InputTeamsStartOfDraw);
            drawRes.calculateDrawStatXT(lPlayers);

            DisplayDrawPlayer(drawRes, lPlayers);
            DisplayTeamsGrid(drawRes);

        }
        private void button10_1_Click(object sender, EventArgs e)
        {
            InputTeamsStartOfDraw = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataInputTeamsSubFolder, "GoodDraw10_182126122123_1.json")));
            button10_helper(InputTeamsStartOfDraw);
        }
        private void button10_2_Click(object sender, EventArgs e)
        {
            InputTeamsStartOfDraw = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataInputTeamsSubFolder, "GoodDraw10_182126122124_2.json")));
            button10_helper(InputTeamsStartOfDraw);

        }
        private void button10_3_Click(object sender, EventArgs e)
        {
            InputTeamsStartOfDraw = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataInputTeamsSubFolder, "GoodDraw10_182126122124_3.json")));
            button10_helper(InputTeamsStartOfDraw);

        }
        private void button10_4_Click(object sender, EventArgs e)
        {
            InputTeamsStartOfDraw = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataInputTeamsSubFolder, "GoodDraw10_182126122125_4.json")));
            button10_helper(InputTeamsStartOfDraw);

        }
        private void button10_5_Click(object sender, EventArgs e)
        {
            InputTeamsStartOfDraw = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataInputTeamsSubFolder, "GoodDraw10_182126122125_5.json")));
            button10_helper(InputTeamsStartOfDraw);

        }
        private void button10_6_Click(object sender, EventArgs e)
        {
            InputTeamsStartOfDraw = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataInputTeamsSubFolder, "GoodDraw10_182126122125_6.json")));
            button10_helper(InputTeamsStartOfDraw);

        }
        private void button10_7_Click(object sender, EventArgs e)
        {
            InputTeamsStartOfDraw = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataInputTeamsSubFolder, "GoodDraw10_orig10.json")));
            button10_helper(InputTeamsStartOfDraw);

        }
        private void button10_8_Click(object sender, EventArgs e)
        {
            InputTeamsStartOfDraw = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataInputTeamsSubFolder, "GoodDraw10_origHardCoded10.json")));
            button10_helper(InputTeamsStartOfDraw);

        }

        private void buttonInitialiseForDay_Click(object sender, EventArgs e)
        {
            int nbRounds = _c.configForYear.nbRounds > 0 ? _c.configForYear.nbRounds : 5;
            string iniType = "";
            int i = getDayNrFromRadio();

            iniType = "course";
            bool iniCourse = MessageBox.Show($"Do you want to initialise the {iniType} files for day {i}? {Environment.NewLine}(overwrite existing ones if any!)", $"Initialse files for day {i}: {iniType}", MessageBoxButtons.YesNo) == DialogResult.Yes;
            iniType = "score";
            bool iniScore = MessageBox.Show($"Do you want to initialise the {iniType} files for day {i}? {Environment.NewLine}(overwrite existing ones if any!)", $"Initialse files for day {i}: {iniType}", MessageBoxButtons.YesNo) == DialogResult.Yes;
            iniType = "match";
            bool iniMatch = MessageBox.Show($"Do you want to initialise the {iniType} files for day {i}? {Environment.NewLine}(overwrite existing ones if any!)", $"Initialse files for day {i}: {iniType}", MessageBoxButtons.YesNo) == DialogResult.Yes;

            iniType = "extra";
            bool iniExtra = false;
            if (_c.configForYear.useExtra)
                iniExtra = MessageBox.Show($"Do you want to initialise the {iniType} files for day {i}? {Environment.NewLine}(overwrite existing ones if any!)", $"Initialse files for day {i}: {iniType}", MessageBoxButtons.YesNo) == DialogResult.Yes;

            if (iniCourse)
            {
                using (Form2 f = new Form2())
                {
                    f.initialiseFileWithoutDialog(i, _dataFolder);
                }
            }
            initialiseCompet(false, i);

            if (iniCourse)
            {
                using (Form3 f = new Form3())
                {
                    f.initialiseFileWithoutDialog(i, _dataFolder, _c);
                }
            }
            initialiseCompet(false, i);

            //matchScores
            if (iniMatch)
            {
                using (Form4 f = new Form4())
                {
                    f.initialiseFileWithoutDialog(i, _dataFolder, _c);
                }
            }
            initialiseCompet(false, i);

            //Extras
            if (_c.configForYear.useExtra && iniExtra)
            {
                using (FormExtra f = new FormExtra())
                {
                    f.initialiseFileWithoutDialog(i, _dataFolder, _c);
                }
                initialiseCompet(false, i);
            }

        }

        private void buttonInitialiseforYear_Click(object sender, EventArgs e)
        {

            int nbRounds = _c.configForYear.nbRounds > 0 ? _c.configForYear.nbRounds : 5;
            string iniType = "";

            iniType = "course";
            bool iniCourse = MessageBox.Show($"Do you want to initialise the {iniType} files for year? {Environment.NewLine}(overwrite existing ones if any!)", $"Initialse files for year: {iniType}", MessageBoxButtons.YesNo) == DialogResult.Yes;
            iniType = "score";
            bool iniScore = MessageBox.Show($"Do you want to initialise the {iniType} files for year? {Environment.NewLine}(overwrite existing ones if any!)", $"Initialse files for year: {iniType}", MessageBoxButtons.YesNo) == DialogResult.Yes;
            iniType = "match";
            bool iniMatch = MessageBox.Show($"Do you want to initialise the {iniType} files for year? {Environment.NewLine}(overwrite existing ones if any!)", $"Initialse files for year: {iniType}", MessageBoxButtons.YesNo) == DialogResult.Yes;

            iniType = "extra";
            bool iniExtra = false;
            if (_c.configForYear.useExtra)
                iniExtra = MessageBox.Show($"Do you want to initialise the {iniType} files for year? {Environment.NewLine}(overwrite existing ones if any!)", $"Initialse files for year: {iniType}", MessageBoxButtons.YesNo) == DialogResult.Yes;

            for (int i = 1; i <= nbRounds; ++i)
            {
                if (iniCourse)
                {
                    using (Form2 f = new Form2())
                    {
                        f.initialiseFileWithoutDialog(i, _dataFolder);
                    }
                }
                initialiseCompet(false, i);

                if (iniCourse)
                {
                    using (Form3 f = new Form3())
                    {
                        f.initialiseFileWithoutDialog(i, _dataFolder, _c);
                    }
                }
                initialiseCompet(false, i);

                //matchScores
                if (iniMatch)
                {
                    using (Form4 f = new Form4())
                    {
                        f.initialiseFileWithoutDialog(i, _dataFolder, _c);
                    }
                }
                    initialiseCompet(false, i);
                
                //Extras
                if (_c.configForYear.useExtra && iniExtra)
                {
                    using (FormExtra f = new FormExtra())
                    {
                        f.initialiseFileWithoutDialog(i, _dataFolder, _c);
                    }
                    initialiseCompet(false, i);
                }
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
    public class TeamInput
    {
        public String Player { get; set; }
        public String R1 { get; set; }
        public String R2 { get; set; }
        public String R3 { get; set; }
        public String R4 { get; set; }
        public String R5 { get; set; }
        public String R6 { get; set; }
        public String R7 { get; set; }
    }
    public class DrawRestriction
    {
        public int nrTry { get; set; }
        public int MaxFlt { get; set; }
        public int MaxEmy  { get; set; }
        public int MaxTm  { get; set; }
        public int Flt0  { get; set; }
        public int Emy0 { get; set; }
        public int Max2B  { get; set; }
        public int MaxWch  { get; set; }
        public int Max4B { get; set; }
        public string Description { get; set; }
    }

}
