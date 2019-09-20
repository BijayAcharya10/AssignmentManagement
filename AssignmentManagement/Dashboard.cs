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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnStaff_Click(object sender, EventArgs e)
        {
            Staff frm = new Staff();
            frm.Show();
            this.Hide();
        }

        private void btnTeacher_Click(object sender, EventArgs e)
        {
            Teacher frm = new Teacher();
            frm.Show();
            this.Hide();
        }

        private void btnSession_Click(object sender, EventArgs e)
        {
            Session frm = new Session();
            frm.Show();
            this.Hide();
        }

        private void btnAssignmentSubmission_Click(object sender, EventArgs e)
        {
            Assignment_Submission frm = new Assignment_Submission();
            frm.Show();
            this.Hide();
        }

        private void btnAssignment_Click(object sender, EventArgs e)
        {
            Assignment frm = new Assignment();
            frm.Show();
            this.Hide();

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Login frm = new Login();
            frm.Show();
            this.Hide();

        }

        private void btnFaculty_Click(object sender, EventArgs e)
        {
            Faculty frm = new Faculty();
            frm.Show();
            this.Hide();

        }

        private void btnStudent_Click(object sender, EventArgs e)
        {
            Student frm = new Student();
            frm.Show();
            this.Hide();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            ChangePassword frm = new ChangePassword();
            frm.Show();
            this.Hide();
        }

        private void btnSubject_Click(object sender, EventArgs e)
        {
            Subject frm = new Subject();
            frm.Show();
            this.Hide();
        }

        private void btnMapping_Click(object sender, EventArgs e)
        {
            TeacherSubjectMapping frm = new TeacherSubjectMapping();
            frm.Show();
            this.Hide();
        }
    }
}
