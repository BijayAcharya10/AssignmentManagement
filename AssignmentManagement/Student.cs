using DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssignmentManagement
{
    public partial class Student : Form
    {
        public Student()
        {
            InitializeComponent();
        }
        AMSDBEntities db = new AMSDBEntities();

        private void Student_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        public void LoadGrid()
        {
            //Cbo Load Session
            var session = db.tblSessions.ToList();         
            tblSession tbl = new tblSession();
            tbl.SessionName = "Choose Session";
            session.Insert(0, tbl);

            cboSessionId.DataSource = session;
            cboSessionId.DisplayMember = "SessionName";
            cboSessionId.ValueMember = "SessionId";
            cboSessionId.SelectedIndex = 0;


            //Cbo Load Faculty
            var faculty = db.tblFaculties.ToList();
            tblFaculty tbf = new tblFaculty();
            tbf.FacultyDescription = "Choose Faculty";
            faculty.Insert(0, tbf);

            cboFacultyId.DataSource = faculty;
            cboFacultyId.DisplayMember = "FacultyDescription";
            cboFacultyId.ValueMember = "FacultyId";
            cboFacultyId.SelectedIndex = 0;

            //Selecting List of User & Displaying in Datagrid view
            dataGridView1.DataSource = db.tblStudents.Select(x=>new {StudentId=x.StudentId,StudentName=x.StudentName,Address=x.Address,ContactNumber=x.Contact,SessionName=x.tblSession.SessionName,Faculty=x.tblFaculty.FacultyDescription }).ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tblStudent tb = new tblStudent();
            tb.StudentName = txtStudentName.Text;
            tb.Address = txtStudentAddress.Text;
            tb.Email = txtEmail.Text;
            tb.Contact = txtContact.Text;
            tb.SessionId = Convert.ToInt32(cboSessionId.SelectedValue);
            tb.FacultyId = Convert.ToInt32(cboFacultyId.SelectedValue);
            db.tblStudents.Add(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Student Added");
                LoadGrid();
                ClearData();
            }
        }

        int studentid;
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            studentid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            tblStudent tb = db.tblStudents.Where(a => a.StudentId == studentid).FirstOrDefault();
            txtStudentName.Text = tb.StudentName;
            txtStudentAddress.Text = tb.Address;
            txtEmail.Text = tb.Email;
            txtContact.Text = tb.Contact;
            cboFacultyId.Text = tb.tblFaculty.FacultyDescription;
            cboSessionId.Text = tb.tblSession.SessionName;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            tblStudent tb = db.tblStudents.Where(a => a.StudentId == studentid).FirstOrDefault();
            tb.StudentName = txtStudentName.Text;
            tb.Address = txtStudentAddress.Text;
            tb.Email = txtEmail.Text;
            tb.Contact = txtContact.Text;
            tb.SessionId = Convert.ToInt32(cboSessionId.SelectedValue);
            tb.FacultyId = Convert.ToInt32(cboFacultyId.SelectedValue);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Student Details Updated");
                LoadGrid();
                ClearData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            tblStudent tb = db.tblStudents.Where(a => a.StudentId == studentid).FirstOrDefault();
            db.tblStudents.Remove(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Student Details Deleted");
                LoadGrid();
                ClearData();
            }

        }

        private void ClearData()
        {
            txtStudentName.Text = "";
            txtStudentAddress.Text = "";
            txtEmail.Text = "";
            txtContact.Text = "";
            cboFacultyId.SelectedIndex = 0;
            cboSessionId.SelectedIndex = 0;
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.Show();
            this.Hide();
        }
    }
}
