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
    public partial class Session : Form
    {
        public Session()
        {
            InitializeComponent();
        }
        AMSDBEntities db = new AMSDBEntities();
        private void Session_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            dataGridView1.DataSource = db.tblSessions.Select(x=>new {SessionId=x.SessionId,SessionName=x.SessionName,StartDate=x.StartDate}).ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tblSession tb = new tblSession();
            tb.SessionName = txtSessionName.Text;
            tb.StartDate = dateTimePicker1.Value;
            db.tblSessions.Add(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Session Added");
                LoadGrid();
                ClearData();
            }
        }


        private void ClearData()
        {
            txtSessionName.Text = "";
            
        }
        int sessionid;
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            sessionid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            tblSession tb = db.tblSessions.Where(u => u.SessionId == sessionid).FirstOrDefault();
            txtSessionName.Text = tb.SessionName;
            dateTimePicker1.Value = Convert.ToDateTime(tb.StartDate);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            tblSession tb = db.tblSessions.Where(u => u.SessionId == sessionid).FirstOrDefault();
            tb.SessionName = txtSessionName.Text;
            tb.StartDate = dateTimePicker1.Value;
            db.tblSessions.Add(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Session Updated");
                LoadGrid();
                ClearData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            tblSession tb = db.tblSessions.Where(u => u.SessionId == sessionid).FirstOrDefault();
            db.tblSessions.Remove(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Session Deleted");
                LoadGrid();
            };
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.Show();
            this.Hide();
        }
    }
}
