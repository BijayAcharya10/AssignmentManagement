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
    public partial class Assignment : Form
    {
        public Assignment()
        {
            InitializeComponent();
        }
        AMSDBEntities db = new AMSDBEntities();
        private void Assignment_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            var subject = db.tblSubjects.ToList();
            tblSubject tbl = new tblSubject();
            tbl.SubjectName = "Choose Subject";
            subject.Insert(0, tbl);

            cboSubjectId.DataSource = subject;
            cboSubjectId.DisplayMember = "SubjectName";
            cboSubjectId.ValueMember = "SubjectId";
            cboSubjectId.SelectedIndex = 0;


            //show table data in datagridview while foreign key enabled
            dataGridView1.DataSource = db.tblAssignments.Select(x=>new {AssignmentId=x.AssignmentId,SubjectId=x.tblSubject.SubjectName, ReleaseDate=x.ReleaseDate, SubmitionDate=x.SubmissionDate,Remarks=x.Remarks }).ToList();
            
        }
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            tblAssignment tb = new tblAssignment();            
            tb.SubjectId =Convert.ToInt32(cboSubjectId.SelectedValue);
            tb.ReleaseDate = dateTimePicker1.Value;
            tb.SubmissionDate = dateTimePicker2.Value;
            tb.Remarks = txtRemarks.Text;
            db.tblAssignments.Add(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Assignment Added");
                LoadGrid();
                ClearData();
            }

        }

        private void ClearData()
        {
            cboSubjectId.Text = "";
            txtRemarks.Text = "";
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.Show();
            this.Hide();
        }

       public int assignmentid;
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
          
                assignmentid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                tblAssignment tb = db.tblAssignments.Where(a => a.AssignmentId == assignmentid).FirstOrDefault();
                cboSubjectId.Text = tb.tblSubject.SubjectName;
                dateTimePicker1.Value = Convert.ToDateTime(tb.ReleaseDate);
                dateTimePicker2.Value = Convert.ToDateTime(tb.SubmissionDate);
                txtRemarks.Text = tb.Remarks;
           
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            tblAssignment tb = db.tblAssignments.Where(a => a.AssignmentId == assignmentid).FirstOrDefault();
            tb.SubjectId = Convert.ToInt32(cboSubjectId.SelectedValue);
            tb.ReleaseDate = dateTimePicker1.Value;
            tb.SubmissionDate = dateTimePicker2.Value;
            tb.Remarks = txtRemarks.Text;          
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Assignment Updated");
                LoadGrid();
                ClearData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            tblAssignment tb = db.tblAssignments.Where(a => a.AssignmentId == assignmentid).FirstOrDefault();
            db.tblAssignments.Remove(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Assignment Deleted");
                LoadGrid();
                ClearData();
            }
        }
    }
}
