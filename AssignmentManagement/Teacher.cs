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
    public partial class Teacher : Form
    {
        AMSDBEntities db = new AMSDBEntities();
        
        public Teacher()
        {
            InitializeComponent();
        }

        private void Teacher_Load(object sender, EventArgs e)
        {
            LoadGrid();
           
        }
        public void LoadGrid()
        {
            dataGridView1.DataSource = db.tblTeachers.ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tblTeacher tb = new tblTeacher();
            tb.TeacherName = txtTeacherName.Text;
            tb.Email = txtEmail.Text;
            tb.Remarks = txtRemarks.Text;
            db.tblTeachers.Add(tb);
            db.SaveChanges();
            MessageBox.Show("Teacher Details Saved");
            LoadGrid();
            ClearData();

        }
        int teacherid;
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            teacherid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            tblTeacher tb = db.tblTeachers.Where(u => u.TeacherId == teacherid).FirstOrDefault();
            txtTeacherName.Text = tb.TeacherName;
            txtEmail.Text = tb.Email;
            txtRemarks.Text = tb.Remarks;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            tblTeacher tb = db.tblTeachers.Where(u => u.TeacherId == teacherid).FirstOrDefault();
            tb.TeacherName = txtTeacherName.Text;
            tb.Email = txtEmail.Text;
            tb.Remarks = txtRemarks.Text;
            db.SaveChanges();
            MessageBox.Show("Teacher Details Updated");
            LoadGrid();
            ClearData();

        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.Show();
            this.Hide();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            tblTeacher tb = db.tblTeachers.Where(u => u.TeacherId == teacherid).FirstOrDefault();
            db.tblTeachers.Remove(tb);
            
            if (db.SaveChanges()>0)
            {
                MessageBox.Show("Teacher Detail Deleted");
            }
            
        }
        public bool ValidateControl()
        {
            bool isvalid = true;
            if (txtTeacherName.Text.Trim() == "")
            {
                MessageBox.Show("Teacher Name Required");
                return false;
            }
            if (txtEmail.Text.Trim() == "")
            {
                MessageBox.Show("Email Required");
                return false;
            }
            if (txtRemarks.Text.Trim() == "")
            {
                MessageBox.Show("Remarks Required");
                return false;
            }
            return isvalid;
        }
        private void ClearData()
        {
            txtTeacherName.Text = "";
            txtEmail.Text = "";
            txtRemarks.Text = "";
        }
    }
}
