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
    public partial class frm_TrangChu : Form
    {
        LOPDUNGCHUNG util;
        public frm_TrangChu()
        {
            InitializeComponent();
            util = new LOPDUNGCHUNG();
        }

        private void frm_TrangChu_Load(object sender, EventArgs e)
        {
            string sql_total = "select count(*) from PHONG";
            string sql_empty = "select count(*) from PHONG where trangthai = 0";
            int total = (int) util.getData(sql_total);
            int empty = (int)util.getData(sql_empty);
            double booked_ratio = ((double)(total - empty) / total)*100;
            lb_TotalRoom.Text = total.ToString();
            lb_EmptyRoom.Text = empty.ToString();
            circle_BookedRoom.ValueByTransition = (int) booked_ratio;
        }
    }
}
