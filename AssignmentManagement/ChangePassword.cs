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
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }
        AMSDBEntities db = new AMSDBEntities();

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            tblLogin tb = db.tblLogins.Where(u => u.Username == txtUsername.Text && u.Password == txtOldPassword.Text).FirstOrDefault();       
            if (tb != null)
            {
                if (txtNewPassword.Text == txtConfirmPassword.Text)
                {
                    tb.Password = txtNewPassword.Text;
                    if (db.SaveChanges() > 0)
                    {
                        MessageBox.Show("User Password Changed");
                        ClearData();
                    }
                    else
                    {
                        MessageBox.Show("Please Confirm New Password");
                    }

                }
            }
            else
            {
                MessageBox.Show("You have entered wrong password");
            }

        }
        public void ClearData()
        {
            txtNewPassword.Text = "";
            txtOldPassword.Text = "";
            txtConfirmPassword.Text = "";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtOldPassword.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            txtNewPassword.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            txtConfirmPassword.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.Show();
            this.Hide();
        }
    }
}
