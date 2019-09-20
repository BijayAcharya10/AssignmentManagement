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
    public partial class Faculty : Form
    {
        public Faculty()
        {
            InitializeComponent();
        }
        AMSDBEntities db = new AMSDBEntities();
        private void Faculty_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            dataGridView1.DataSource = db.tblFaculties.Select(x=>new {FacultyId=x.FacultyId,FacultyDescription=x.FacultyDescription}).ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tblFaculty tb = new tblFaculty();
            tb.FacultyDescription = txtFacultyDescription.Text;
            db.tblFaculties.Add(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Faculty Added");
                LoadGrid();
                ClearData();
            }
        }
        private void ClearData()
        {
            txtFacultyDescription.Text = "";
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.Show();
            this.Hide();
        }
    }
}
