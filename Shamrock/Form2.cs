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
    public partial class Form2 : Form
    {
        course ctCourse;
        int _dayNr;
        String _file;
        public Form2()
        {
            InitializeComponent();
        }
        public void ShowDialog(int dayNr, String folderData)
        {
            _dayNr = dayNr;
            _file = Path.Combine(folderData, String.Format("Course{0}.json", _dayNr));

            if (File.Exists(_file))
                ctCourse = JsonConvert.DeserializeObject<course>(File.ReadAllText(_file));
            else
                initialise();

            display();
            this.Text = "Course definition round " + _dayNr.ToString(); 

            this.ShowDialog();

        }
        public void initialise()
        {
            ctCourse = new course();
            for (int i = 1; i <= 18; ++i)
            {
                ctCourse.addHole(4, i);
            }
        }
        public void save()
        {
            File.WriteAllText(_file, JsonConvert.SerializeObject(ctCourse));
        }
        public void display()
        {
            var bindingListP = new BindingList<hole>(ctCourse.holes);
            var sourceP = new BindingSource(bindingListP, null);
            dataGridView1.DataSource = sourceP;
            //dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            dataGridView1.RowPostPaint += OnRowPostPaint;

            refreshLabelSumPar();
            refreshLabelSumHcp();

            List<course> lc = new List<course>();
            lc.Add(ctCourse);
            var bindingListP2 = new BindingList<course>(lc);
            var sourceP2 = new BindingSource(bindingListP2, null);
            dataGridView2.DataSource = sourceP2;

        }
        void OnRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {

            dataGridView1.Rows[3].DefaultCellStyle.BackColor = Color.FromArgb(228, 237, 240);
            dataGridView1.Rows[4].DefaultCellStyle.BackColor = Color.FromArgb(228, 237, 240);
            dataGridView1.Rows[5].DefaultCellStyle.BackColor = Color.FromArgb(228, 237, 240);
            dataGridView1.Rows[9].DefaultCellStyle.BackColor = Color.FromArgb(228, 237, 240);
            dataGridView1.Rows[10].DefaultCellStyle.BackColor = Color.FromArgb(228, 237, 240);
            dataGridView1.Rows[11].DefaultCellStyle.BackColor = Color.FromArgb(228, 237, 240);
            dataGridView1.Rows[15].DefaultCellStyle.BackColor = Color.FromArgb(228, 237, 240);
            dataGridView1.Rows[16].DefaultCellStyle.BackColor = Color.FromArgb(228, 237, 240);
            dataGridView1.Rows[17].DefaultCellStyle.BackColor = Color.FromArgb(228, 237, 240);

            dataGridView1.Columns["nr"].ReadOnly = true;
            dataGridView1.Columns["nr"].DefaultCellStyle.BackColor = Color.Gray;
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText;

            //// Abort validation if cell is not in the CompanyName column.
            //if (headerText.Equals("par"))
            //{
            //    label1.Text = ctCourse.holes.Sum(x => x.par).ToString();
            //    return;
            //}
                

            //// Confirm that the cell is not empty.
            //if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
            //{
            //    dataGridView1.Rows[e.RowIndex].ErrorText =
            //        "Company Name must not be empty";
            //    e.Cancel = true;
            //}
        }
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string headerText = dataGridView1.Columns[e.ColumnIndex].HeaderText;

            // Abort validation if cell is not in the CompanyName column.
            if (headerText.Equals("par"))
            {
                refreshLabelSumPar();
                return;
            }
            if (headerText.Equals("hcp"))
            {
                refreshLabelSumHcp();
                return;
            }


        }
        private void refreshLabelSumPar()
        {
            label1.Text = String.Format("Sum Par: {0}", ctCourse.getSumPar());
        }
        private void refreshLabelSumHcp()
        {
            label2.Text = String.Format("Sum Hcps: {0}, (171 expected)", ctCourse.getSumHcp());
            if (ctCourse.getSumHcp() != 171)
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.Black;
        }
        private void refreshLabelNrHoles()
        {
            label2.Text = String.Format("Sum Hcps: {0}, (171 expected)", ctCourse.getSumHcp());
            if (ctCourse.getSumHcp() != 171)
                label2.ForeColor = Color.Red;
            else
                label2.ForeColor = Color.Black;
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            save();
            this.Close();
        }

    }
}
