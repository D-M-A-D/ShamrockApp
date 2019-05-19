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
    public partial class FormExtra : Form
    {
        int _dayNr;
        String _file;
        Compet _c;
        public List<playerExtraForDisplay> playerExtrasForDisplay = new List<playerExtraForDisplay>();
        public FormExtra()
        {
            InitializeComponent();
        }
        public void ShowDialog(int dayNr, String folderData, Compet Compet)
        {
            _dayNr = dayNr;
            _c = Compet;
            _file = Path.Combine(folderData, String.Format("ExtraShamrock{0}.json", _dayNr));

            if (File.Exists(_file))
                _c.getDaybyNr(_dayNr).extras = JsonConvert.DeserializeObject<dailyExtras>(File.ReadAllText(_file));
            else
                _c.getDaybyNr(_dayNr).initialiseEmptyExtra();

            display();
            this.Text = "Extra Shamrock input for round " + _dayNr.ToString();

            this.ShowDialog();

        }
        public void save()
        {
            _c.getDaybyNr(_dayNr).extras.extras.Clear();
            foreach (playerExtraForDisplay ctPS in playerExtrasForDisplay)
            {
                _c.getDaybyNr(_dayNr).extras.extras.Add(ctPS.playerName, ctPS.ptShamrock);
            }
            File.WriteAllText(_file, JsonConvert.SerializeObject(_c.getDaybyNr(_dayNr).extras));
        }
        public void display()
        {
            initialiseExtrasForDisplay(_c.getDaybyNr(_dayNr).extras);
            
            var bindingListP = new BindingList<playerExtraForDisplay>(playerExtrasForDisplay);
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

            //dataGridView1.Columns["_4"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            //dataGridView1.Columns["_5"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            //dataGridView1.Columns["_6"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            //dataGridView1.Columns["_10"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            //dataGridView1.Columns["_11"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            //dataGridView1.Columns["_12"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            //dataGridView1.Columns["_16"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            //dataGridView1.Columns["_17"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);
            //dataGridView1.Columns["_18"].DefaultCellStyle.BackColor = Color.FromArgb(228, 237,240);

            //dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.Gray;
            //dataGridView1.Rows[1].DefaultCellStyle.BackColor = Color.LightGray;
            //dataGridView1.Rows[0].ReadOnly = true;
            //dataGridView1.Rows[1].ReadOnly = true;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            save();
            this.Close();
        }
        public void initialiseExtrasForDisplay(dailyExtras extras)
        {
            playerExtraForDisplay ps;
            foreach (String ctPlayer in extras.extras.Keys)
            {
                ps = new playerExtraForDisplay();
                ps.playerName = ctPlayer;
                ps.ptShamrock = extras.extras[ctPlayer];
                playerExtrasForDisplay.Add(ps);
            }
        }
    }
    public class playerExtraForDisplay
    {
        public String playerName { get; set; }
        public double ptShamrock { get; set; }
    }
}
