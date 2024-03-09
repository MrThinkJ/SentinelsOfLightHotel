using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoADO;

namespace QuanLyKhachSan
{
    public partial class frm_DangNhap : Form
    {
        LOPDUNGCHUNG util = new LOPDUNGCHUNG();
        public frm_DangNhap()
        {
            InitializeComponent();
        }

        private void pic_ThoatDangNhap_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn có thật sự muốn thoát không", "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dl == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            string sql = String.Format("Select COUNT(*) from taikhoan where username = '{0}' AND password = '{1}'", txt_TaiKhoan.Text, txt_MatKhau.Text);
            int kq = (int)util.getData(sql);

            if (kq >= 1)
            {
                MessageBox.Show("Đăng nhập thành công");
                frm_Main main = new frm_Main();
                main.Show();
            }
            else
            {
                MessageBox.Show("Tài khoản và mật khẩu không đúng");
            }
        }

        private void btn_NhapLai_Click(object sender, EventArgs e)
        {
            txt_TaiKhoan.Text = "";
            txt_MatKhau.Text = "";
        }

        private void chk_HienThiMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_HienThiMatKhau.Checked)
            {
                txt_MatKhau.PasswordChar = '\0';
            }
            else
            {
                txt_MatKhau.PasswordChar = '*';
            }
        }

        private void frm_DangNhap_Load(object sender, EventArgs e)
        {
            txt_MatKhau.PasswordChar = '*';
        }
    }
}
