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
    public partial class Assignment_Submission : Form
    {
        public Assignment_Submission()
        {
            InitializeComponent();
        }
        AMSDBEntities db = new AMSDBEntities();

        private void Assignment_Submission_Load(object sender, EventArgs e)
        {
            LoadGrid();
            GridLoad();
        }
        private void LoadGrid()
        {
            var statu = db.tblStatus.ToList();
           tblStatu tbl = new tblStatu();
            tbl.StatusDescription = "Choose Status";
            statu.Insert(0, tbl);

            cboStatusId.DataSource = statu;
            cboStatusId.DisplayMember = "StatusDescription";
            cboStatusId.ValueMember = "StatusId";
            cboStatusId.SelectedIndex = 0;


            //Select query to display column in datagrid view
            dataGridView1.DataSource = db.tblAssignmentSubmissions.Select(x => new { AssignmentSubmissionId = x.AssignmentSubmissionId, AssignmentId =x.tblAssignment.tblSubject.SubjectName,Status=x.tblStatu.StatusDescription,SubmittedDate=x.SubmittedDate,IsOnTime=x.IsOnTime }).ToList();
        }
        private void GridLoad()
        {
         var subject = db.tblAssignments.Select(x=>new {AssignmentId=x.AssignmentId, SubjectName=x.tblSubject.SubjectName }).ToList();
            cboAssignmentId.DataSource = subject;
            cboAssignmentId.DisplayMember = "SubjectName";
            cboAssignmentId.ValueMember = "AssignmentId";
        }
        int studentid;
        private void txtStudentName_Click(object sender, EventArgs e)
        {
            StudentList frm = new StudentList();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                studentid = frm.studentid;
                txtStudentName.Text = frm.studentname;

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            tblAssignmentSubmission tb = new tblAssignmentSubmission();
            tblStudent tbl = db.tblStudents.Where(a => a.StudentId == studentid).FirstOrDefault();          
            tb.StudentId = tbl.StudentId;
            tb.AssignmentId = Convert.ToInt32(cboAssignmentId.SelectedValue);
            tb.StatusId = Convert.ToInt32(cboStatusId.SelectedValue);
            tb.SubmittedDate = dateTimePicker1.Value;
            tblAssignment tba = db.tblAssignments.Where(a => a.AssignmentId == tb.AssignmentId).FirstOrDefault();
            DateTime subdate = Convert.ToDateTime(tb.SubmittedDate);
            DateTime submissiondate = Convert.ToDateTime(tba.SubmissionDate);
           //DateTime submissiondate = Convert.ToDateTime(tba.ReleaseDate);
            TimeSpan dt = subdate.Subtract(submissiondate);

            if (dt.TotalDays>=1)
            {
                txtIsOnTime.Text = "No";
                int d = Convert.ToInt16(dt.TotalDays);
                MessageBox.Show("Assignment Submission Late by " + d+" days");
            }
            else
            {
                txtIsOnTime.Text = "Yes";
            }

            tb.IsOnTime = Convert.ToString(txtIsOnTime.Text);
          
            db.tblAssignmentSubmissions.Add(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Assignment Submitted");
                LoadGrid();
               // ClearData();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.Show();
            this.Hide();
        }

        public void ClearData()
        {
            txtStudentName.Text = "";
            
        }
        
    }
}
