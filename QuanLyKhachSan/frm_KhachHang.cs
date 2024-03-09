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

    public partial class frm_KhachHang : Form
    {
        LOPDUNGCHUNG util = new LOPDUNGCHUNG();
        public frm_KhachHang()
        {
            InitializeComponent();
        }

        public void loadKH()
        {
            string sql = "select * from khachhang";
            dgv_KhachHang.DataSource = util.LoadData(sql);
        }

        private void frm_KhachHang_Load(object sender, EventArgs e)
        {
            loadKH();
            string sql_getRoom = "select maphong from PHONG";
            List<string> listRoomName = util.getListData(sql_getRoom).Select(s => s.ToString()).ToList();
            cb_RoomId.DataSource = listRoomName;
        }

        private void dt_KhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dt_KhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_Name.Text = dgv_KhachHang.CurrentRow.Cells[1].Value.ToString();
                txt_CCCD.Text = dgv_KhachHang.CurrentRow.Cells[0].Value.ToString();
                cb_RoomId.Text = dgv_KhachHang.CurrentRow.Cells[2].Value.ToString();
                string sqlload = string.Format("Select * from PHONG where maphong = '{0}'",
                    dgv_KhachHang.CurrentRow.Cells[2].Value.ToString());
                DataTable dt = util.LoadData(sqlload);
                cb_RoomType.Text = (bool)dt.Rows[0][4] ? "VIP" : "Thường";
                dtp_HireDay.Value = Convert.ToDateTime(dt.Rows[0][2].ToString());
                txt_NumberDayHire.Text = dt.Rows[0][3].ToString();
            } catch(Exception ex)
            {
                MessageBox.Show("Không chọn được giá trị này !" );
            }
        }

        private void pic_Sua_Click(object sender, EventArgs e)
        {
            string sql_khachhang = "Update khachhang set tenkhachhang = N'" + txt_Name.Text + "', maphong = '" + cb_RoomId.SelectedItem.ToString() + "' where CCCD =" + txt_CCCD.Text + "";
            string sql_phong = string.Format("update PHONG set trangthai = {0}, ngaydat = '{1}', songaythue = '{2}', vip = {3} where maphong = '{4}'",
                1, dtp_HireDay.Value, txt_NumberDayHire.Text, (cb_RoomType.Text == "VIP" ? 1 : 0), cb_RoomId.SelectedItem.ToString());
            int kq = util.ThemXoaSua(sql_khachhang);
            int kq2 = util.ThemXoaSua(sql_phong);
            if (kq > 0 && kq2 > 0) MessageBox.Show("Cập nhật khách hàng thành công");
            else MessageBox.Show("Cập nhật khách hàng thất bại");
            loadKH();
        }

        private void pc_Xoa_Click(object sender, EventArgs e)
        {
            string sql = "Delete from khachhang where CCCD = '" + txt_CCCD.Text + "' ";
            string sql_phong = string.Format("update PHONG set trangthai = {0}, ngaydat = {1}, songaythue = {2}, vip = {3} where maphong = '{4}'",
                0, "NULL", "NULL", (cb_RoomType.Text == "VIP" ? 1 : 0), cb_RoomId.SelectedItem.ToString());
            int kq = util.ThemXoaSua(sql);
            int kq2 = util.ThemXoaSua(sql_phong);
            if (kq >0 && kq2 >0) MessageBox.Show("Xóa khách hàng thành công");
            else MessageBox.Show("Xóa khách hàng thất bại");
            loadKH();
        }
    }
}
