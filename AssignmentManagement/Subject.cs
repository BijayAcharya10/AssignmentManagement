using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;

namespace AssignmentManagement
{
    public partial class Subject : Form
    {
        public Subject()
        {
            InitializeComponent();
        }
        AMSDBEntities db = new AMSDBEntities();

        private void Subject_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            dataGridView1.DataSource = db.tblSubjects.Select(s=>new { SubjectId = s.SubjectId,SubjectName=s.SubjectName,Description=s.Description }).ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tblSubject tb = new tblSubject();
            tb.SubjectName = txtSubjectName.Text;
            tb.Description = txtDescription.Text;
            db.tblSubjects.Add(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Subject Added");
                LoadGrid();
                ClearData();
            }

        }
        int subjectid;
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            subjectid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            tblSubject tb = db.tblSubjects.Where(a => a.SubjectId == subjectid).FirstOrDefault();
            txtSubjectName.Text = tb.SubjectName;
            txtDescription.Text = tb.Description;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            tblSubject tb = db.tblSubjects.Where(a => a.SubjectId == subjectid).FirstOrDefault();
            tb.SubjectName = txtSubjectName.Text;
            tb.Description = txtDescription.Text;
            if (db.SaveChanges() > 0)
            {
                LoadGrid();
                MessageBox.Show("Subject Details Updated");
                ClearData();
               
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            tblSubject tb = db.tblSubjects.Where(a => a.SubjectId == subjectid).FirstOrDefault();
            db.tblSubjects.Remove(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Subject Deleted");
                LoadGrid();
            };

        }
        private void ClearData()
        {
            txtSubjectName.Text = "";
            txtDescription.Text = "";
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.Show();
            this.Hide();
        }
    }
}
