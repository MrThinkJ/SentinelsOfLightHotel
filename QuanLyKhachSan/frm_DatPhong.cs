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
    public partial class frm_DatPhong : Form
    {

        bool conTrong = true;
        public frm_DatPhong()
        {
            InitializeComponent();
        }

        private void p_Phong_Click(object sender, EventArgs e)
        {
            frm_ChiTietDatPhong chiTietDatPhong = new frm_ChiTietDatPhong();
            chiTietDatPhong.Show();
        }

        private void bunifuPanel2_Click(object sender, EventArgs e)
        {
            frm_ThongTinPhong thongTinPhong = new frm_ThongTinPhong();
            thongTinPhong.Show();
        }
    }
}
