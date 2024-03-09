using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DemoADO;

namespace QuanLyKhachSan
{
    public partial class frm_ChiTietDatPhong : Form
    {
        string roomId;
        LOPDUNGCHUNG util;
        frm_RoomManager roomManager;
        public frm_ChiTietDatPhong(string roomId, frm_RoomManager roomManager)
        {
            InitializeComponent();
            util = new LOPDUNGCHUNG();
            this.roomId = roomId;
            this.roomManager = roomManager;
        }
        public frm_ChiTietDatPhong()
        {
            InitializeComponent();
        }

        private void btn_NhapPhong_Click(object sender, EventArgs e)
        {
            string sql_phong = string.Format("update PHONG set trangthai = {0}, ngaydat = '{1}', songaythue = '{2}', vip = {3} where maphong = '{4}'",
                1, dtp_NgayDat.Value, txt_SoNgay.Text, (cb_LoaiPhong.Text == "VIP" ? 1 : 0), roomId);
            string sql_khach = string.Format("insert into KHACHHANG values ('{0}', N'{1}', '{2}')", txt_CCCD.Text, txt_Name.Text, roomId);
            int result = 0;
            int result2 = 0;
            try
            {
                result2 = util.ThemXoaSua(sql_khach);
                result = util.ThemXoaSua(sql_phong);
            } catch (Exception ex)
            {
                MessageBox.Show("Vui lòng nhập đúng thông tin");
                return;
            }
            if (result < 1 || result2 < 1)
                MessageBox.Show("Nhập phòng thất bại");
            else
                MessageBox.Show("Nhập phòng thành công");
            roomManager.LoadRoom();
            Close();
        }

        private void btn_ThoatPhong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
