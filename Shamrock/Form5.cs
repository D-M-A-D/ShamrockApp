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
    public partial class Form5 : Form
    {
        String _file;
        Compet _c;
        List<ConfigsForYear> PointsForDisplay = new List<ConfigsForYear>();
        public Form5()
        {
            InitializeComponent();
        }
        public void ShowDialog (String folderData, Compet Compet)
        {

            _c = Compet;
            _file = Path.Combine(folderData, String.Format("configForYear.json"));

            if (File.Exists(_file))
                _c.configForYear = JsonConvert.DeserializeObject<ConfigsForYear>(File.ReadAllText(_file));
            else
            {
                _c.configForYear.ptsMatch = "2,2"; //2 points for match, 2 points for foursome
                _c.configForYear.stlDay = "1.5,1,0.5";
                _c.configForYear.stlWeek = "4,3,2,1,0.5";
                _c.configForYear.useExtra = false;
                _c.configForYear.useScratch = false;
            }

            display();
            this.Text = "Input for Shamrock Points ";

            this.ShowDialog();

        }
        public void display()
        {
            PointsForDisplay.Add(_c.configForYear);
            var bindingListP = new BindingList<ConfigsForYear>(PointsForDisplay);
            var sourceP = new BindingSource(bindingListP, null);
            dataGridView1.DataSource = sourceP;
            //dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            //dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            //dataGridView1.RowPostPaint += OnRowPostPaint;
        }
        public void save()
        {
            foreach (ConfigsForYear ctPS in PointsForDisplay)
            {
                _c.configForYear.ptsMatch = ctPS.ptsMatch;
                _c.configForYear.stlDay = ctPS.stlDay;
                _c.configForYear.stlWeek = ctPS.stlWeek;
            }
            File.WriteAllText(_file, JsonConvert.SerializeObject(_c.configForYear));
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            save();
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
