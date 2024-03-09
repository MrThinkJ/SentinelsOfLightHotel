using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class frm_MainNhanVien : Form
    {
        public frm_MainNhanVien()
        {
            InitializeComponent();
        }

        private Form currentFormChild;
        private void OpenChildForm(Form childForm)
        {
            if (currentFormChild != null) 
            {
                currentFormChild.Close();
            }

            currentFormChild = childForm;
            childForm.TopLevel = false;
            p_Main.Controls.Add(childForm);
            p_Main.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btn_TrangChu_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frm_TrangChu());
        }

        private void btn_DatPhong_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frm_DatPhong());
        }

        private void btn_KhachHang_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frm_KhachHang());
        }

        private void btn_HoaDon_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frm_ThongKe());
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có thật sự muốn thoát không", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dl == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
