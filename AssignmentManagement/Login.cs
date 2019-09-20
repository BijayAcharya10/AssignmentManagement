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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        AMSDBEntities db = new AMSDBEntities();
        private void btnLogin_Click(object sender, EventArgs e)
        {
            tblLogin tb = db.tblLogins.Where(u => u.Username == txtUsername.Text && u.Password == txtPassword.Text && u.Usertype==cboUsertype.Text).FirstOrDefault();
            if (tb!=null){
                Dashboard frm = new Dashboard();
                frm.Show();
                this.Hide();

            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }
    }
}
