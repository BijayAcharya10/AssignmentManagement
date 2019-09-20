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
    public partial class Staff : Form
    {
        public Staff()
        {
            InitializeComponent();
        }
        AMSDBEntities db = new AMSDBEntities();

        private void Staff_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        public void LoadGrid()
        {
            dataGridView1.DataSource = db.tblStaffs.ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tblStaff tb = new tblStaff();
            tb.StaffName = txtStaffName.Text;
            tb.UserName = txtUsername.Text;
            tb.Password = txtPassword.Text;
            tb.Email = txtEmail.Text;
            tb.ContactNo = txtContactNumber.Text;
            db.tblStaffs.Add(tb);
            if (db.SaveChanges() > 0)
            {
                LoadGrid();
                MessageBox.Show("Staff Details Added");
                ClearData();
            };

        }
        int staffid;
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            staffid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            tblStaff tb =db.tblStaffs.Where(a=>a.StaffId==staffid).FirstOrDefault();
            txtStaffName.Text = tb.StaffName;
            txtUsername.Text = tb.UserName;
            txtPassword.Text = tb.Password;
            txtEmail.Text = tb.Email;
            txtContactNumber.Text = tb.ContactNo;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            tblStaff tb = db.tblStaffs.Where(a => a.StaffId == staffid).FirstOrDefault();
            tb.StaffName = txtStaffName.Text;
            tb.UserName = txtUsername.Text;
            tb.Password = txtPassword.Text;
            tb.Email = txtEmail.Text;
            tb.ContactNo = txtContactNumber.Text;
            if (db.SaveChanges() > 0)
            {
                LoadGrid();
                MessageBox.Show("Staff Details Updated");
                ClearData();
            };
        }
        
        private void btnDelete_Click(object sender, EventArgs e)
        {
            tblStaff tb = db.tblStaffs.Where(a => a.StaffId == staffid).FirstOrDefault();
            db.tblStaffs.Remove(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Staff ID Removed");
                LoadGrid();
            }

        }
        private void ClearData()
        {
            txtStaffName.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtEmail.Text = "";
            txtContactNumber.Text = "";
            
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.Show();
            this.Hide();
        }

       
    }
}
