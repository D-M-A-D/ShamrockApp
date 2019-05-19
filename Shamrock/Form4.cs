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

namespace Shamrock
{
    public partial class Form4 : Form
    {
        int _dayNr;
        String _file;
        Compet _c;
        public List<matchScoreForDisplay> matchScoresForDisplay = new List<matchScoreForDisplay>();
        public Form4()
        {
            InitializeComponent();
        }
        public void ShowDialog(int dayNr, String folderData, Compet Compet)
        {
            _dayNr = dayNr;
            _c = Compet;
            _file = Path.Combine(folderData, String.Format("MatchScores{0}.json", _dayNr));

            if (File.Exists(_file))
                _c.getDaybyNr(_dayNr).matchScores = JsonConvert.DeserializeObject<MatchScores>(File.ReadAllText(_file));
            else
                _c.getDaybyNr(_dayNr).matchScores.initialise(_c.getDaybyNr(_dayNr).flights);

            display();
            this.Text = "Match Results input for round " + _dayNr.ToString();

            this.ShowDialog();

        }
        public void display()
        {
            initialiseForDisplay(_c.getDaybyNr(_dayNr).flights, _c.getDaybyNr(_dayNr).matchScores);

            var bindingListP = new BindingList<matchScoreForDisplay>(matchScoresForDisplay);
            var sourceP = new BindingSource(bindingListP, null);
            dataGridView1.DataSource = sourceP;
            dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            //dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            dataGridView1.RowPostPaint += OnRowPostPaint;
            //dataGridView1.Rows[1].DefaultCellStyle.BackColor = Color.Red;

        }
        void OnRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Columns["matchName"].ReadOnly = true;
            dataGridView1.Columns["matchType"].ReadOnly = true;
            dataGridView1.Columns["Team1"].ReadOnly = true;
            dataGridView1.Columns["Team2"].ReadOnly = true;
            dataGridView1.Columns["Team1"].DefaultCellStyle.BackColor = Color.LightGreen;
            dataGridView1.Columns["Team2"].DefaultCellStyle.BackColor = Color.LightBlue;
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText;

            //// Abort validation if cell is not in the CompanyName column.
            //if (headerText.Equals("par"))
            //{
            //    label1.Text = ctCourse.holes.Sum(x => x.par).ToString();
            //    return;
            //}


            // Confirm that the cell is not empty.
            if (headerText.Equals("WinnerTeam"))
            {
                if(e.FormattedValue.ToString() != "1" && e.FormattedValue.ToString() != "2" && e.FormattedValue.ToString() != "0" && e.FormattedValue.ToString() != "-1")
                {
                    dataGridView1.Rows[e.RowIndex].ErrorText = "WinnerTeam, plz enter 1 or 2 (0 for tie, -1 not played yet)";
                    e.Cancel = true;
                }
            }
        }
        public void initialiseForDisplay(Dictionary<String, flight> flights, MatchScores matchScores)
        {
            matchScoreForDisplay mr;

            foreach (flight ctFlight in flights.Values)
            {
                if(ctFlight.matchType == flight.MatchType.Winch || ctFlight.matchType == flight.MatchType.FoursomeWinch5)
                {
                    foreach (string ctc in ctFlight.WinchCombin)
                    {
                        string ctt1 = (Convert.ToInt32(ctc.Left(1))).ToString();
                        string ctt2 = (Convert.ToInt32(ctc.Substring(1))).ToString();

                        mr = new matchScoreForDisplay();
                        mr.Team1 = ctFlight.teams[ctt1].GetPlayersString();
                        mr.Team2 = ctFlight.teams[ctt2].GetPlayersString();
                        mr.matchName = ctFlight.name + ctFlight.teams[ctt1].name1based + ctFlight.name + ctFlight.teams[ctt2].name1based;
                        mr.matchType = ctFlight.matchType.ToString();

                        if (matchScores.matchResults.ContainsKey(mr.matchName))
                        {
                            mr.WinnerTeam = matchScores.matchResults[mr.matchName].WinnerteamName;
                            mr.nrHolesLeft = matchScores.matchResults[mr.matchName].nrHolesLeft;
                            mr.nrPoints = matchScores.matchResults[mr.matchName].nrPoints;
                        }

                        matchScoresForDisplay.Add(mr);
                    }
                }
                else
                {
                    mr = new matchScoreForDisplay();
                    mr.matchName = ctFlight.name;
                    mr.matchType = ctFlight.matchType.ToString();

                    int cntTeam = 0;
                    foreach (team ctTeam in ctFlight.teams.Values)
                    {
                        cntTeam++;
                        mr.GetType().GetProperty(String.Format("Team{0}", cntTeam)).SetValue(mr, ctTeam.GetPlayersString());
                    }
                    if (matchScores.matchResults.ContainsKey(mr.matchName))
                    {
                        mr.WinnerTeam = matchScores.matchResults[mr.matchName].WinnerteamName;
                        mr.nrHolesLeft = matchScores.matchResults[mr.matchName].nrHolesLeft;
                        mr.nrPoints = matchScores.matchResults[mr.matchName].nrPoints;
                    }
                    matchScoresForDisplay.Add(mr);
                }
            }
        }
        public void save()
        {
            _c.getDaybyNr(_dayNr).matchScores.matchResults.Clear();
            foreach (matchScoreForDisplay ctPS in matchScoresForDisplay)
            {
                MatchScore ctMS = new MatchScore();
                ctMS.WinnerteamName = ctPS.WinnerTeam;
                ctMS.nrHolesLeft = ctPS.nrHolesLeft;
                ctMS.nrPoints = ctPS.nrPoints;
                _c.getDaybyNr(_dayNr).matchScores.matchResults.Add(ctPS.matchName, ctMS);
            }
            File.WriteAllText(_file, JsonConvert.SerializeObject(_c.getDaybyNr(_dayNr).matchScores));
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            save();
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
    public class matchScoreForDisplay
    {
        public String matchName { get; set; }
        public String matchType { get; set; }
        public String Team1 { get; set; }
        public String Team2 { get; set; }
        public int WinnerTeam { get; set; }
        public int nrPoints { get; set; }
        public int nrHolesLeft { get; set; }
    }

}
