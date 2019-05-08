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

namespace Shamrock
{
    public partial class Form1 : Form
    {
        List<TeamInput> InputTeams = new List<TeamInput>();
        List<TeamInput> InputTeamsTemp = new List<TeamInput>();
        List<DrawRestriction> DrawRestrictions = new List<DrawRestriction>();
        Compet _c; 
        String _dataFolder;
        String _dataRoot;
        int ctDay = 0;
        public Form1()
        {
            InitializeComponent();
            _dataRoot = "data\\";
            _dataFolder = _dataRoot + textBoxDataFolder.Text;
            ctDay = getDayNrFromRadio();
            initialiseCompet();
        }
        private void initialiseCompet()
        {
            try
            {
                _dataFolder = _dataRoot + textBoxDataFolder.Text;
                ctDay = getDayNrFromRadio();
                _c = new Compet(textBoxDataFolder.Text);
                _c.Players = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(_dataFolder, "Players.json")));
                _c.configForYear = JsonConvert.DeserializeObject<ConfigsForYear>(File.ReadAllText(Path.Combine(_dataFolder, "configForYear.json")));
                InputTeams = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataFolder, "InputTeams.json")));
                for (int i = 1; i <= 5; ++i)
                {
                    _c.newDay(_c.configForYear.getPlayModeForRound(i));
                }
                for (int i = 1; i <= ctDay; ++i)
                {
                    try
                    {
                        _c.getDaybyNr(i).courseDefinition = JsonConvert.DeserializeObject<course>(File.ReadAllText(Path.Combine(_dataFolder, String.Format("Course{0}.json", i))));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Could not initialise courseDefinition for day {0}: {1}", ctDay, ex.Message));
                    }
                    try
                    {
                        _c.initialiseflightsFromInputTeam(i, InputTeams);
                        //_c.initialiseflightsForDossard(_c.Players, i);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not initialise Flights for Dossard, check dossard nr" + ex.Message);
                    }
                    try
                    {
                        _c.getDaybyNr(i).scores = JsonConvert.DeserializeObject<dailyScores>(File.ReadAllText(Path.Combine(_dataFolder, String.Format("PlayerScores{0}.json", i))));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Could not initialise dailyScores for day {0}: {1}", ctDay, ex.Message));
                    }
                    try
                    {
                        _c.getDaybyNr(i).matchScores = JsonConvert.DeserializeObject<MatchScores>(File.ReadAllText(Path.Combine(_dataFolder, String.Format("MatchScores{0}.json", i))));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(String.Format("Could not initialise MatchScores for day {0}: {1}", ctDay, ex.Message));
                    }
                }
                displayPlayers();
                displayDrawRestrictions();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in initialiseCompet: " + ex.Message);
            }
        }
        private void displayPlayers()
        {
            var bindingList = new BindingList<Player>(_c.Players);
            var source = new BindingSource(bindingList, null);
            dataGridViewPlayers.DataSource = source;
        }
        private void displayDrawRestrictions()
        {
            if (DrawRestrictions.Count == 0)
            {
                DrawRestriction dr = new DrawRestriction();
                dr.nrTry = 4000;
                dr.Max4B = 5;
                dr.Max2B = 3;
                dr.MaxWch = 5;
                dr.MaxFlt = 4;
                dr.MaxTm = 2;
                dr.MaxEmy = 2;
                dr.Flt0 = 0;
                dr.Emy0 = 10;
                DrawRestrictions.Add(dr);
            }
            dataGridView4.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            var bindingList = new BindingList<DrawRestriction>(DrawRestrictions);
            var source = new BindingSource(bindingList, null);
            dataGridView4.DataSource = source;
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

        private void buttonDrawNew_Click(object sender, EventArgs e)
        {
            List<Player> lPlayers2 = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(_dataFolder, "Players.json")));
            lPlayers2.Sort((x, y) => x.name.CompareTo(y.name));

            drawResults drawRes = new drawResults();

            int nbOfDay = getDayNrFromRadio();
            day ctDay;
            #region initialise from InputTeams (check for Abandon = 0, and forced place (p.ex. stay in 2b)
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
            labelDrawText.Text = "Starting draw...";
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
                        foreach (flight ctFlight in ctDay.flights.Values.OrderBy(x => x.CountPlayer())) //make sure to get the 2B first (probably easier to get available players
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

                                        string diagnostic = "";
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
                                                                diagnostic += " " + HowManyTimeInSameTeam;
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
                        drawResTry.calculateDrawStatXT(ctDay.PlayersForTheDay);
                        if (drawResTry.statXTFlight.ContainsKey(0) && drawResTry.statXTFlight[0] > maxFlight0
                                || drawResTry.statXTEnemy.ContainsKey(0) && drawResTry.statXTEnemy[0] > maxEnnemy0
                            )
                        {
                            doInterruptDraw = true; // not good
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
                    dataGridView1.ColumnCount = lPlayers2.Count + 1;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns[0].Name = "Flt|Emy|Tm";

                    for (int iP1 = 0; iP1 < lPlayers2.Count; iP1++)
                    {
                        dataGridView1.Columns[iP1 + 1].Name = lPlayers2[iP1].name.Left(4);
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[iP1].Cells[0].Value = lPlayers2[iP1].name.Left(4);
                        for (int iP2 = 0; iP2 < lPlayers2.Count; iP2++)
                        {
                            if (iP1 > iP2)
                            {
                                int ctHTFlight = drawResTry.GetHowManyTimeInSameFlight(lPlayers2[iP1], lPlayers2[iP2]);
                                int ctHTEnemy = drawResTry.GetHowManyTimeEnemy(lPlayers2[iP1], lPlayers2[iP2]);
                                dataGridView1.Rows[iP1].Cells[iP2 + 1].Value = String.Format("{0} | {1}", ctHTFlight, ctHTEnemy);
                                if (ctHTEnemy == 0)
                                    dataGridView1.Rows[iP1].Cells[iP2 + 1].Style.BackColor = Color.Azure;
                            }
                        }
                    }
                    #endregion
                    #region display stats
                    dataGridView3.ColumnCount = 8;
                    dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dataGridView3.Rows.Clear();
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows.Add();
                    for (int i = 0; i < dataGridView3.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            dataGridView3.Rows[0].Cells[i].Value = "Flight";
                            dataGridView3.Rows[1].Cells[i].Value = "Enemy";
                            dataGridView3.Rows[2].Cells[i].Value = "in2B";
                            dataGridView3.Rows[3].Cells[i].Value = "inWinch";
                            dataGridView3.Rows[4].Cells[i].Value = "in4B";
                        }
                        else
                        {
                            dataGridView3.Columns[i].Name = (i - 1).ToString();
                            dataGridView3.Rows[0].Cells[i].Value = drawResTry.statXTFlight[i - 1];
                            dataGridView3.Rows[1].Cells[i].Value = drawResTry.statXTEnemy[i - 1];
                            dataGridView3.Rows[2].Cells[i].Value = drawResTry.statXTIn2B[i - 1];
                            dataGridView3.Rows[3].Cells[i].Value = drawResTry.statXTInWinch[i - 1];
                            dataGridView3.Rows[4].Cells[i].Value = drawResTry.statXTIn4B[i - 1];
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
                labelDrawText.Text = String.Format("no solution found after {0} tries. Time {1:T}", maxTry, end - start);
            else
                labelDrawText.Text = String.Format("Solution found after {0} tries. Time {1:T}", ctTry, end - start);

            #region display teams Grid
            dataGridView2.ColumnCount = drawRes.days.Count * 2;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.Rows.Clear();

            dataGridView2.Rows.Add(lPlayers2.Count);
            foreach (day ctD in drawRes.days)
            {
                int ctRow = -1;
                dataGridView2.Columns[(int)((ctD.nr - 1)* 2)].Name = ctD.nr.ToString();
                dataGridView2.Columns[(int)(((ctD.nr - 1) * 2)+1)].Name = ctD.playMode.ToString();
                foreach (flight ctFlight in ctD.flights.Values)
                {
                    foreach (team ctTeam in ctFlight.teams.Values)
                    {
                        foreach(playingBall b in ctTeam.playingBalls .Values)
                        {
                            foreach (Player P in b.players.Values)
                            {
                                ctRow++;
                                string ballName = "";
                                if (ctFlight.matchType == flight.MatchType.Match4b5 && b.nbOfPlayerForBall > 1)
                                    ballName = b.name;
                                dataGridView2.Rows[ctRow].Cells[(int)((ctD.nr - 1) * 2)].Value = ctFlight.name + ctTeam.name1based + ballName;
                                dataGridView2.Rows[ctRow].Cells[(int)(((ctD.nr - 1) * 2) + 1)].Value = P.name;
                            }
                        }
                    }
                }
            }
            #endregion

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
                                }
                            }
                        }
                    }
                }
            }
            InputTeamsTemp = tis.Values.ToList();
            #endregion

            string json = JsonConvert.SerializeObject(drawRes);
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
                    foreach (flight ctFlight in ctDay.flights.Values.OrderBy(x => x.CountPlayer())) //make sure to get the 2B first (probably easier to get available players
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
                    dataGridView1.ColumnCount = lPlayers2.Count + 1;
                    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Columns[0].Name = "Flight|Enemy|Team";

                    for (int iP1 = 0; iP1 < lPlayers2.Count; iP1++)
                    {
                        dataGridView1.Columns[iP1 + 1].Name = lPlayers2[iP1].name.Left(4);
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[iP1].Cells[0].Value = lPlayers2[iP1].name.Left(4);
                        for (int iP2 = 0; iP2 < lPlayers2.Count; iP2++)
                        {
                            if (iP1 > iP2)
                            {
                                int ctHTFlight = drawRes.GetHowManyTimeInSameFlight(lPlayers2[iP1], lPlayers2[iP2]);
                                int ctHTEnemy = drawRes.GetHowManyTimeEnemy(lPlayers2[iP1], lPlayers2[iP2]);
                                dataGridView1.Rows[iP1].Cells[iP2 + 1].Value = String.Format("{0} | {1}", ctHTFlight, ctHTEnemy);
                                if (ctHTEnemy == 0)
                                    dataGridView1.Rows[iP1].Cells[iP2 + 1].Style.BackColor = Color.Azure;
                            }
                        }
                    }
                    #endregion
                    #region display stats
                    dataGridView3.ColumnCount = 8;
                    dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dataGridView3.Rows.Clear();
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows.Add();
                    dataGridView3.Rows.Add();
                    for (int i = 0; i < dataGridView3.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            dataGridView3.Rows[0].Cells[i].Value = "Flight";
                            dataGridView3.Rows[1].Cells[i].Value = "Enemy";
                            dataGridView3.Rows[2].Cells[i].Value = "in2B";
                        }
                        else
                        {
                            dataGridView3.Columns[i].Name = (i - 1).ToString();
                            dataGridView3.Rows[0].Cells[i].Value = drawRes.statXTFlight[i - 1];
                            dataGridView3.Rows[1].Cells[i].Value = drawRes.statXTEnemy[i - 1];
                            dataGridView3.Rows[2].Cells[i].Value = drawRes.statXTIn2B[i - 1];
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

            #region display teams Grid
            dataGridView2.ColumnCount = drawRes.days.Count + 1;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.Rows.Clear();

            dataGridView2.Rows.Add(lPlayers2.Count);
            foreach (day ctD in drawRes.days)
            {
                int ctRow = -1;
                dataGridView2.Columns[(int)ctD.nr].Name = ctD.nr.ToString();
                foreach (flight ctFlight in ctD.flights.Values)
                {
                    foreach (team ctTeam in ctFlight.teams.Values)
                    {
                        foreach (Player P in ctTeam.players.Values)
                        {
                            ctRow++;
                            dataGridView2.Rows[ctRow].Cells[0].Value = ctFlight.name + ctTeam.name;
                            dataGridView2.Rows[ctRow].Cells[(int)ctD.nr].Value = P.name;
                        }
                    }
                }
            }
            #endregion

            string json = JsonConvert.SerializeObject(drawRes);
        }

        private void buttonTeamInputInitialise_Click(object sender, EventArgs e)
        {
            initialiseCompet();
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
                        for (int i = 0; i < ctTeam.nbOfBall; i++)
                        {
                            ++cnt;
                            
                            Player candidate = tmpPlayers[dayOrder[cnt] - 1];
                            TeamInput ti = InputTeams.Find(x => x.Player == candidate.name);
                            if(ti != null)
                                InputTeams.Remove(ti);
                            else
                                ti = new TeamInput();
                            ti.Player = candidate.name;
                            switch (dayNr)
                            {
                                case 1: ti.R1 = ctFlight.name + ctTeam.name; break;
                                case 2: ti.R2 = ctFlight.name + ctTeam.name; break;
                                case 3: ti.R3 = ctFlight.name + ctTeam.name; break;
                                case 4: ti.R4 = ctFlight.name + ctTeam.name; break;
                                case 5: ti.R5 = ctFlight.name + ctTeam.name; break;
                            }
                            InputTeams.Add(ti);
                        }
                    }
                }
                
            }
            InputTeams.Sort((x, y) => x.R1.CompareTo(y.R1));
            var bindingListP = new BindingList<TeamInput>(InputTeams);
            var sourceP = new BindingSource(bindingListP, null);
            dataGridViewInput.DataSource = sourceP;

        }
        private void buttonTeamInputSave_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(_dataFolder, "InputTeams.json"), JsonConvert.SerializeObject(InputTeams));
        }
        private void buttonInputTeamRead_Click(object sender, EventArgs e)
        {
            dataGridViewInput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewInput.Rows.Clear();

            InputTeams = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataFolder, "InputTeams.json")));

            var bindingListP = new BindingList<TeamInput>(InputTeams);
            var sourceP = new BindingSource(bindingListP, null);
            dataGridViewInput.DataSource = sourceP;

        }
        private void buttonTeamInputSaveToTemp_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(_dataFolder, "InputTeamsTemp.json"), JsonConvert.SerializeObject(InputTeams));
        }
        private void buttonInputTeamReadfromTemp_Click(object sender, EventArgs e)
        {
            dataGridViewInput.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewInput.Rows.Clear();

            InputTeams = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(_dataFolder, "InputTeamsTemp.json")));

            var bindingListP = new BindingList<TeamInput>(InputTeams);
            var sourceP = new BindingSource(bindingListP, null);
            dataGridViewInput.DataSource = sourceP;

        }
        private void buttonSaveDrawToFile_Click(object sender, EventArgs e)
        {
            File.WriteAllText(Path.Combine(_dataFolder, "InputTeams.json"), JsonConvert.SerializeObject(InputTeamsTemp));
        }

        private void buttonDisplayStats_Click(object sender, EventArgs e)
        {
            initialiseCompet();
            _c.calculateDrawStatXT(_c.Players);
            #region display xFlight-xEnemy-xTeam Matrix
            dataGridView1.ColumnCount = _c.Players.Count + 1;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns[0].Name = "Flight|Enemy|Team";

            for (int iP1 = 0; iP1 < _c.Players.Count; iP1++)
            {
                dataGridView1.Columns[iP1 + 1].Name = _c.Players[iP1].name.Left(4);
                dataGridView1.Rows.Add();
                dataGridView1.Rows[iP1].Cells[0].Value = _c.Players[iP1].name.Left(4);
                for (int iP2 = 0; iP2 < _c.Players.Count; iP2++)
                {
                    if (iP1 > iP2)
                    {
                        int ctHTFlight = _c.GetHowManyTimeInSameFlight(_c.Players[iP1], _c.Players[iP2]);
                        int ctHTEnemy = _c.GetHowManyTimeEnemy(_c.Players[iP1], _c.Players[iP2]);
                        dataGridView1.Rows[iP1].Cells[iP2 + 1].Value = String.Format("{0} | {1}", ctHTFlight, ctHTEnemy);
                        if (ctHTEnemy == 0)
                            dataGridView1.Rows[iP1].Cells[iP2 + 1].Style.BackColor = Color.Azure;
                    }
                }
            }
            #endregion
            #region display stats
            dataGridView3.ColumnCount = 8;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView3.Rows.Clear();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            dataGridView3.Rows.Add();
            for (int i = 0; i < dataGridView3.Columns.Count; i++)
            {
                if (i == 0)
                {
                    dataGridView3.Rows[0].Cells[i].Value = "Flight";
                    dataGridView3.Rows[1].Cells[i].Value = "Enemy";
                    dataGridView3.Rows[2].Cells[i].Value = "in2B";
                    dataGridView3.Rows[3].Cells[i].Value = "inWinch";
                    dataGridView3.Rows[4].Cells[i].Value = "in4B";
                }
                else
                {
                    dataGridView3.Columns[i].Name = (i - 1).ToString();
                    dataGridView3.Rows[0].Cells[i].Value = _c.statXTFlight[i - 1];
                    dataGridView3.Rows[1].Cells[i].Value = _c.statXTEnemy[i - 1];
                    dataGridView3.Rows[2].Cells[i].Value = _c.statXTIn2B[i - 1];
                    dataGridView3.Rows[3].Cells[i].Value = _c.statXTInWinch[i - 1];
                    dataGridView3.Rows[4].Cells[i].Value = _c.statXTIn4B[i - 1];
                }
            }
            #endregion
            #region display teams Grid
            dataGridView2.ColumnCount = _c.days.Count * 2;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView2.Rows.Clear();

            dataGridView2.Rows.Add(_c.Players.Count);
            foreach (day ctD in _c.days)
            {
                int ctRow = -1;
                dataGridView2.Columns[(int)((ctD.nr - 1) * 2)].Name = ctD.nr.ToString();
                foreach (flight ctFlight in ctD.flights.Values)
                {
                    foreach (team ctTeam in ctFlight.teams.Values)
                    {
                        foreach (Player P in ctTeam.players.Values)
                        {
                            ctRow++;
                            dataGridView2.Rows[ctRow].Cells[(int)((ctD.nr - 1) * 2)].Value = ctFlight.name + ctTeam.name;
                            dataGridView2.Rows[ctRow].Cells[(int)(((ctD.nr - 1) * 2) + 1)].Value = P.name;
                        }
                    }
                }
            }
            #endregion

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> years = new List<string> { "2012", "2013", "2014", "2015", "2016", "2017" };
                //List<string> years = new List<string> { "2012", "2013", "2014", "2015", "2016" };
                //List<string> years = new List<string> { "2012" };
                Dictionary<string, Compet> hC = new Dictionary<string, Compet>();

                #region read everything for years and initialise
                foreach (string ctYear in years)
                {
                    string ctDF = _dataRoot + ctYear;
                    Compet ctC = new Compet(ctYear);
                    ctC.Players = JsonConvert.DeserializeObject<List<Player>>(File.ReadAllText(Path.Combine(ctDF, "Players.json")));
                    ctC.configForYear = JsonConvert.DeserializeObject<ConfigsForYear>(File.ReadAllText(Path.Combine(ctDF, "configForYear.json")));
                    List<TeamInput> ctTeams = new List<TeamInput>();
                    ctTeams = JsonConvert.DeserializeObject<List<TeamInput>>(File.ReadAllText(Path.Combine(ctDF, "InputTeams.json")));

                    for (int i = 1; i <= 5; ++i)
                    {
                        ctC.newDay(ctC.configForYear.getPlayModeForRound(i));
                        try
                        {
                            ctC.getDaybyNr(i).courseDefinition = JsonConvert.DeserializeObject<course>(File.ReadAllText(Path.Combine(ctDF, String.Format("Course{0}.json", i))));
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(String.Format("Could not initialise courseDefinition for day {0}: {1}", ctDay, ex.Message));
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
                #region loop and Calc Stats
                statsMatch statsM = new statsMatch();
                foreach (string ctYear in years)
                {
                    Compet ctC = hC[ctYear];
                    statsM.addC(ctC);
                }
                statsM.Calc();
                #endregion
                #region writeCSVFile
                System.IO.StreamWriter csvFile = new System.IO.StreamWriter(_dataRoot + "MatchHistory.csv");
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
                        csvFile.Write(ctPMInfo.flight.CountPlayer().ToString() + separator);
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
                            csvFile.Write(ctPMInfo.myTeam.name1based + separator);
                            csvFile.Write(ctPMInfo.myTeam.GetTeamMate(ctPMatchHist.name) + separator);
                        }
                        else
                        {
                            csvFile.Write(separator);
                            csvFile.Write(separator);
                        }
                        if (ctPMInfo.opTeam != null)
                        {
                            csvFile.Write(ctPMInfo.opTeam.name1based + separator);
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
                csvFile = new System.IO.StreamWriter(_dataRoot + "statsMatchTeamMate.csv");
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
                csvFile = new System.IO.StreamWriter(_dataRoot + "statsMatchBeteNoire.csv");
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
            catch (Exception ex)
            {
                MessageBox.Show("Error in CalcStats: " + ex.Message);
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
    }
    public class TeamInput
    {
        public String Player { get; set; }
        public String R1 { get; set; }
        public String R2 { get; set; }
        public String R3 { get; set; }
        public String R4 { get; set; }
        public String R5 { get; set; }
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
    }

}
