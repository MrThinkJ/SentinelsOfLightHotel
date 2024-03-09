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
    public partial class frm_ThongKe : Form
    {
        LOPDUNGCHUNG util;
        public frm_ThongKe()
        {
            InitializeComponent();
            util= new LOPDUNGCHUNG();
        }

        private void bunifuShadowPanel1_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void frm_ThongKe_Load(object sender, EventArgs e)
        {
            string sql_total = "select count(*) from PHONG";
            string sql_empty = "select count(*) from PHONG where trangthai = 0";
            string sql_booked = "select count(*) from PHONG where trangthai = 1";
            string sql_revenue = "select SUM(tongthu) from hoadon";
            int total = (int) util.getData(sql_total);
            int empty = (int) util.getData(sql_empty);
            int booked = (int) util.getData(sql_booked);
            int revenue = (int)util.getData(sql_revenue);

            lb_TotalRoom.Text = total.ToString();
            lb_EmptyRoom.Text = empty.ToString();
            lb_BookedRoom.Text = booked.ToString();
            lb_Revenue.Text = revenue.ToString();
        }
    }
}
