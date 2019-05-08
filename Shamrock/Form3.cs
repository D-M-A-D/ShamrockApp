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
    public partial class Form3 : Form
    {
        int _dayNr;
        String _file;
        Compet _c;
        public List<playerScoreForDisplay> playerScoresForDisplay = new List<playerScoreForDisplay>();
        public Form3()
        {
            InitializeComponent();
        }
        public void ShowDialog(int dayNr, String folderData, Compet Compet)
        {
            _dayNr = dayNr;
            _c = Compet;
            _file = Path.Combine(folderData, String.Format("PlayerScores{0}.json", _dayNr));

            if (File.Exists(_file))
                _c.getDaybyNr(_dayNr).scores = JsonConvert.DeserializeObject<dailyScores>(File.ReadAllText(_file));
            else
                _c.getDaybyNr(_dayNr).initialiseEmptyScore();

            display();
            this.Text = "Scores input for round " + _dayNr.ToString();

            this.ShowDialog();

        }
        public void save()
        {
            _c.getDaybyNr(_dayNr).scores.holeResults.Clear();
            foreach(playerScoreForDisplay ctPS in playerScoresForDisplay)
            {
                if (ctPS.playerName != "par" && ctPS.playerName != "hcp")
                {
                    List<int> ctHoleResults = new List<int>();
                    foreach (hole ctHole in _c.getDaybyNr(_dayNr).courseDefinition.holes)
                    {
                        String s = ctPS.GetType().GetProperty(String.Format("_{0}", ctHole.nr)).GetValue(ctPS).ToString();
                        int hr = 0;
                        if (int.TryParse(s, out hr))
                            ctHoleResults.Add(hr);
                        else
                            ctHoleResults.Add(0);
                    }
                    _c.getDaybyNr(_dayNr).scores.holeResults.Add(ctPS.playerName, ctHoleResults);
                }
            }
            File.WriteAllText(_file, JsonConvert.SerializeObject(_c.getDaybyNr(_dayNr).scores));
        }
        public void display()
        {
            initialiseScoresForDisplay(_c.getDaybyNr(_dayNr).courseDefinition.holes, _c.getDaybyNr(_dayNr).scores);
            
            var bindingListP = new BindingList<playerScoreForDisplay>(playerScoresForDisplay);
            var sourceP = new BindingSource(bindingListP, null);
            dataGridView1.DataSource = sourceP;
            //dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            //dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            dataGridView1.RowPostPaint += OnRowPostPaint;
            //dataGridView1.Rows[1].DefaultCellStyle.BackColor = Color.Red;

        }

        void OnRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            dataGridView1.Columns["playerName"].ReadOnly = true;
            dataGridView1.Columns["playerName"].DefaultCellStyle.BackColor = Color.LightGreen;

            dataGridView1.Columns["_4"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            dataGridView1.Columns["_5"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            dataGridView1.Columns["_6"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            dataGridView1.Columns["_10"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            dataGridView1.Columns["_11"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            dataGridView1.Columns["_12"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            dataGridView1.Columns["_16"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            dataGridView1.Columns["_17"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            dataGridView1.Columns["_18"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);

            dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Gray;
            dataGridView1.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
            dataGridView1.Rows[0].ReadOnly = true;
            dataGridView1.Rows[1].ReadOnly = true;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            save();
            this.Close();
        }
        public void initialiseScoresForDisplay(List<hole> holes, dailyScores scores)
        {
            playerScoreForDisplay ps;

            ps = new playerScoreForDisplay();
            ps.playerName = "par";
            foreach (hole ctHole in holes)
                ps.GetType().GetProperty(String.Format("_{0}", ctHole.nr)).SetValue(ps, ctHole.par.ToString());
            playerScoresForDisplay.Add(ps);

            ps = new playerScoreForDisplay();
            ps.playerName = "hcp";
            foreach (hole ctHole in holes)
                ps.GetType().GetProperty(String.Format("_{0}", ctHole.nr)).SetValue(ps, ctHole.hcp.ToString());
            playerScoresForDisplay.Add(ps);

            foreach (String ctPlayer in scores.holeResults.Keys)
            {
                ps = new playerScoreForDisplay();
                ps.playerName = ctPlayer;
                foreach (hole ctHole in holes)
                    ps.GetType().GetProperty(String.Format("_{0}", ctHole.nr)).SetValue(ps, scores.getScore(ctPlayer,ctHole.nr).ToString().Replace("0","X"));
                playerScoresForDisplay.Add(ps);
            }
        }
    }
    public class playerScoreForDisplay
    {
        public String playerName { get; set; }
        public String _1 { get; set; }
        public String _2 { get; set; }
        public String _3 { get; set; }
        public String _4 { get; set; }
        public String _5 { get; set; }
        public String _6 { get; set; }
        public String _7 { get; set; }
        public String _8 { get; set; }
        public String _9 { get; set; }
        public String _10 { get; set; }
        public String _11 { get; set; }
        public String _12 { get; set; }
        public String _13 { get; set; }
        public String _14 { get; set; }
        public String _15 { get; set; }
        public String _16 { get; set; }
        public String _17 { get; set; }
        public String _18 { get; set; }
    }
}
