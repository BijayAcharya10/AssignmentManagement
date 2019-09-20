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
    public partial class StudentList : Form
    {
        AMSDBEntities db = new AMSDBEntities();
        public StudentList()
        {
            InitializeComponent();
        }

        private void StudentList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.tblStudents.Select(x => new { StudentId = x.StudentId, StudentName = x.StudentName, Address = x.Address, ContactNumber = x.Contact, SessionName = x.tblSession.SessionName, Faculty = x.tblFaculty.FacultyDescription }).ToList();
        }
        public int studentid = 0;
        public string studentname = "";

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            studentid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            studentname = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            this.DialogResult = DialogResult.OK;
        }
    }
}
