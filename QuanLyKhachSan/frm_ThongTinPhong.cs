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
    public partial class frm_ThongTinPhong : Form
    {
        string roomId;
        LOPDUNGCHUNG util;
        frm_RoomManager roomManager;
        public frm_ThongTinPhong(string roomId, frm_RoomManager roomManager)
        {
            InitializeComponent();
            util = new LOPDUNGCHUNG();
            this.roomId = roomId;
            this.roomManager = roomManager;
        }

        public frm_ThongTinPhong()
        {
            InitializeComponent();
        }

        private void btn_ThoatThanhToan_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_CheckOut_Click(object sender, EventArgs e)
        {
            string sql_phong = string.Format("update PHONG set trangthai = {0}, ngaydat = {1}, songaythue = {2}, vip = {3} where maphong = '{4}'",
                0, "NULL", "NULL", 0, roomId);
            string sql_hoadon = "insert into hoadon(ngaylap, tongthu, maphong) values(GETDATE(), " + int.Parse(lb_Price.Text.Split(' ')[0]) + ", '" + roomId + "')";
            string sql_khach = string.Format("delete from KHACHHANG where maphong = '{0}'", roomId);
            int kq2 = util.ThemXoaSua(sql_phong);
            int kq = util.ThemXoaSua(sql_hoadon);
            int kq3 = util.ThemXoaSua(sql_khach);
            if (kq > 0 && kq2 > 0 && kq3 > 0)
            {
                MessageBox.Show("Thanh toán thành công");
                roomManager.LoadRoom();
                Close();
            }
            else
            {
                MessageBox.Show("Thanh toán thất bại");
            }
        }

        private void frm_ThongTinPhong_Load_1(object sender, EventArgs e)
        {
            lb_RoomId.Text = "Phòng " + roomId;
            string sql_getRoom = "select * from PHONG where maphong = '" + roomId + "'";
            string sql_getCustomer = "select * from khachhang where maphong = '" + roomId + "'";
            DataTable dtRoom = util.LoadData(sql_getRoom);
            DataTable dataCustomer = util.LoadData(sql_getCustomer);
            int hireDay = (int)dtRoom.Rows[0]["songaythue"];
            bool vip = (bool)dtRoom.Rows[0]["vip"];
            lb_HireDay.Text = hireDay.ToString();
            lb_RoomType.Text = vip ? "VIP" : "Thường";
            lb_Price.Text = vip ? 500000*hireDay + " VND" : 300000*hireDay + " VND";
            lb_Name.Text = dataCustomer.Rows[0]["tenkhachhang"].ToString();
            lb_CCCD.Text = dataCustomer.Rows[0]["CCCD"].ToString();
        }
    }
}
