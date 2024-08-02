using BTVN.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BTVN
{
    public partial class Form1 : Form
    {
        StudentsDb context = new StudentsDb();
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            try
            {
                List<Faculty> listFalcultys = context.Faculties.ToList(); //lấy các khoa
                List<Student> listStudent = context.Students.ToList(); //lấy sinh viên
                FillFalcultyCombobox(listFalcultys);
                BindGrid(listStudent);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //Hàm binding list có tên hiện thị là tên khoa, giá trị là Mã khoa
        private void FillFalcultyCombobox(List<Faculty> listFalcultys)
        {
            this.cmbKhoa.DataSource = listFalcultys;
            this.cmbKhoa.DisplayMember = "FacultyName";
            this.cmbKhoa.ValueMember = "FacultyID";
        }
        //Hàm binding gridView từ list sinh viên
        private void BindGrid(List<Student> listStudent)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in listStudent)
            {
                int index = dataGridView1.Rows.Add();
                dataGridView1.Rows[index].Cells[0].Value = item.StudentID;
                dataGridView1.Rows[index].Cells[1].Value = item.FullName;
                dataGridView1.Rows[index].Cells[2].Value = item.Faculty.FacultyName;
                dataGridView1.Rows[index].Cells[3].Value = item.AverageScore;
            }
        }

        int checkIdKhoa(String s)
        {
            if(String.IsNullOrEmpty(s))
                return 0;
            if(s == "Công nghệ thông tin")
                return 1;
            if(s == "Ngôn ngữ Anh")
                return 2;
            if(s == "Quản trị kinh doanh")
                return 3;
            return 0;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        private bool checkdata()
        {
            if (string.IsNullOrEmpty(txtMssv.Text.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else

            if (string.IsNullOrEmpty(txtHoTen.Text.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else

            if (string.IsNullOrEmpty(txtDiemTb.Text.ToString()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else

            if ((txtMssv.Text.ToString().Length != 10) || !int.TryParse(txtMssv.Text, out int result))
            {
                MessageBox.Show("Mã số sinh viên phải có 10 kí tự số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else

            if (!float.TryParse(txtDiemTb.Text, out float temple))
            {
                MessageBox.Show("Điểm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

           
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (checkdata())
            {
                if (checkIdExiting(txtMssv.Text.ToString()))
                {
                    Student dbUpdate = context.Students.FirstOrDefault(p => p.StudentID == txtMssv.Text.ToString());
                    if (dbUpdate != null)
                    {
                        dbUpdate.FullName = txtHoTen.Text.ToString();
                        dbUpdate.StudentID = txtMssv.Text.ToString();
                        dbUpdate.AverageScore = Convert.ToDouble(txtDiemTb.Text.ToString());
                        //  dbUpdate.Faculty = (Faculty) cmbKhoa.Text.ToString();
                        if (cmbKhoa.SelectedItem is Faculty selectedFaculty)
                        {
                            // Gán đối tượng Faculty được chọn cho biến dbUpdate.Faculty
                            dbUpdate.Faculty = selectedFaculty;
                        }
                        context.SaveChanges(); //lưu thay đổi
                    }
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    int id = checkIdKhoa(cmbKhoa.Text.ToString());
                    Student s = new Student()
                    { StudentID = txtMssv.Text.ToString(), FullName = txtHoTen.Text.ToString(), AverageScore = Convert.ToDouble(txtDiemTb.Text.ToString()),
                        FacultyID = id
                    };
                    context.Students.Add(s);
                    context.SaveChanges();
                    MessageBox.Show("Thêm mới dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                List<Student> listStudent = context.Students.ToList(); //lấy sinh viên
                BindGrid(listStudent);
            }

            //add data to database

        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (checkdata())
            {
                if (checkIdExiting(txtMssv.Text.ToString()))
                {
                    Student dbUpdate = context.Students.FirstOrDefault(p => p.StudentID == txtMssv.Text.ToString());
                    if (dbUpdate != null)
                    {
                        dbUpdate.FullName = txtHoTen.Text.ToString();
                        dbUpdate.StudentID = txtMssv.Text.ToString();
                        dbUpdate.AverageScore = Convert.ToDouble(txtDiemTb.Text.ToString());
                        //  dbUpdate.Faculty = (Faculty) cmbKhoa.Text.ToString();
                        if (cmbKhoa.SelectedItem is Faculty selectedFaculty)
                        {
                            // Gán đối tượng Faculty được chọn cho biến dbUpdate.Faculty
                            dbUpdate.Faculty = selectedFaculty;
                        }
                        context.SaveChanges(); //lưu thay đổi
                    }
                    MessageBox.Show("Cập nhật dữ liệu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<Student> listStudent = context.Students.ToList(); //lấy sinh viên
                    BindGrid(listStudent);
                }
                else
                {
                    MessageBox.Show("MSSV không tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
               
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
            txtHoTen.Text = row.Cells["clHoVaTen"].Value.ToString();
            txtMssv.Text = row.Cells["clMssv"].Value.ToString();
            txtDiemTb.Text = row.Cells["clDiem"].Value.ToString();
            cmbKhoa.Text = row.Cells["clKhoa"].Value.ToString();


        }

        private bool checkIdExiting (String studentId)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if(row.Cells["clMssv"].Value.ToString() == studentId)
                {
                    return true;
                }
            }
            return false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (checkdata())
            {
                if (checkIdExiting(txtMssv.Text.ToString()))
                {
                   
                    if (MessageBox.Show("Xác nhận xóa", "thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        Student dbDelete = context.Students.FirstOrDefault(p => p.StudentID == txtMssv.Text.ToString());
                        if (dbDelete != null)
                        {
                            context.Students.Remove(dbDelete);
                            context.SaveChanges(); // lưu thay dổi
                        }
                    }
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List<Student> listStudent = context.Students.ToList(); //lấy sinh viên
                    BindGrid(listStudent);

                                     
                }
                else
                {
                   
                    MessageBox.Show("Không tìm thấy mssv!", "lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error   );
                }
                
            }
        }
    }
}
