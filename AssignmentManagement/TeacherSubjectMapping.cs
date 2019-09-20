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
    public partial class TeacherSubjectMapping : Form
    {
        public TeacherSubjectMapping()
        {
            InitializeComponent();
        }
        AMSDBEntities db = new AMSDBEntities();
        private void TeacherSubjectMapping_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }
        private void LoadGrid()
        {
            //Cbo Load Subjects
            var subject = db.tblSubjects.ToList();
            tblSubject tbl = new tblSubject();
            tbl.SubjectName = "Choose Subject";
            subject.Insert(0, tbl);
            cboSubject.DataSource = subject;
            cboSubject.DisplayMember = "SubjectName";
            cboSubject.ValueMember = "SubjectId";
            cboSubject.SelectedIndex = 0;


            //Cbo Load Teacher
            var teacher = db.tblTeachers.ToList();
            tblTeacher tb = new tblTeacher();
            tb.TeacherName = "Choose Teacher Name";
            teacher.Insert(0, tb);
            cboTeacher.DataSource = teacher;
            cboTeacher.DisplayMember = "TeacherName";
            cboTeacher.ValueMember = "TeacherId";
            cboTeacher.SelectedIndex = 0;

            dataGridView1.DataSource = db.tblTeacherSubjectMappings.Select(x => new { MappingId = x.TeacherSubjectMappingId, TeacherName = x.tblTeacher.TeacherName, SubjectName = x.tblSubject.SubjectName }).ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            tblTeacherSubjectMapping tb = new tblTeacherSubjectMapping();
            tb.TeacherId = Convert.ToInt32(cboTeacher.SelectedValue);
            tb.SubjectId = Convert.ToInt32(cboSubject.SelectedValue);
            db.tblTeacherSubjectMappings.Add(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Mapping Added");
                LoadGrid();
               ClearData();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            tblTeacherSubjectMapping tb = db.tblTeacherSubjectMappings.Where(a => a.TeacherSubjectMappingId == mappingid).FirstOrDefault();
            tb.TeacherId = Convert.ToInt32(cboTeacher.SelectedValue);
            tb.SubjectId = Convert.ToInt32(cboSubject.SelectedValue);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Mapping Updated");
                LoadGrid();
                ClearData();
            }

        }
        int mappingid;
        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            mappingid = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            tblTeacherSubjectMapping tb = db.tblTeacherSubjectMappings.Where(a => a.TeacherSubjectMappingId == mappingid).FirstOrDefault();
            cboSubject.Text = tb.tblSubject.SubjectName;
            cboTeacher.Text = tb.tblTeacher.TeacherName;


        }

        private void ClearData()
        {        
            cboTeacher.SelectedIndex = 0;
            cboSubject.SelectedIndex = 0;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            tblTeacherSubjectMapping tb = db.tblTeacherSubjectMappings.Where(a => a.TeacherSubjectMappingId == mappingid).FirstOrDefault();
            db.tblTeacherSubjectMappings.Remove(tb);
            if (db.SaveChanges() > 0)
            {
                MessageBox.Show("Mapping Deleted");
                LoadGrid();
                ClearData();
            }
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard frm = new Dashboard();
            frm.Show();
            this.Hide();
        }
    }
}
