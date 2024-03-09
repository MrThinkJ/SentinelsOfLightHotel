using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bunifu.UI.WinForm.BunifuShadowPanel;
using QuanLyKhachSan;

namespace DemoADO
{
    public partial class frm_RoomManager : Form
    {
        LOPDUNGCHUNG util;
        public frm_RoomManager()
        {
            util = new LOPDUNGCHUNG();
            InitializeComponent();
            CreateRoomButton();
        }

        public void frm_RoomManager_Load(object sender, EventArgs e)
        {
        }

        private List<List<Object>> getRoomData()
        {
            string sql_count = "select count(*) from PHONG";
            string sql_getRoom = "select maphong from PHONG";
            string sql_state = "select trangthai from PHONG";
            string sql_bookTime = "select ngaydat from PHONG";
            string sql_hireDay = "select songaythue from PHONG";
            string sql_vip = "select vip from PHONG";
            int roomNumber = (int)util.getData(sql_count);
            List<List<Object>> list = new List<List<Object>>
            {
                util.getListData(sql_getRoom),
                util.getListData(sql_state),
                util.getListData(sql_bookTime),
                util.getListData(sql_hireDay),
                util.getListData(sql_vip)
            };
            return list;
        }

        private string createButtonContent(string roomName, bool state, string bookTime, string hireDay, bool vip)
        {
            string content = "";
            content += roomName + "\n";
            content += "Trạng thái: " + (state ? "Đã đặt" : "Trống") + "\n";
            content += "Ngày đặt: " + bookTime + "\n";
            content += "Số ngày thuê: " + hireDay + "\n";
            content += "VIP: " + vip;
            return content;
        }

        public void LoadRoom()
        {
            List<List<Object>> roomData = getRoomData();
            List<string> listRoomName = roomData[0].Select(s => (string)s).ToList();
            List<bool> listRoomState = roomData[1].Select(b => (bool)b).ToList();
            List<string> listBookTime = roomData[2].Select(s => (s == DBNull.Value) ? "" : s.ToString()).ToList();
            List<string> listHireDay = roomData[3].Select(s => (s == DBNull.Value) ? "" : s.ToString()).ToList();
            List<bool> listVip = roomData[4].Select(b => (bool)b).ToList();
            foreach (Button button in Controls.OfType<Button>())
            {
                if (button.Name.StartsWith("btn_"))
                {
                    string roomName = button.Name.Split('_')[2];
                    int index = listRoomName.IndexOf(roomName);
                    if (index != -1)
                    {
                        button.Name = "btn_" + listRoomState[index] + "_" + listRoomName[index];
                        button.Text = createButtonContent(listRoomName[index], listRoomState[index],
                            listBookTime[index], listHireDay[index], listVip[index]);
                        button.BackColor = listRoomState[index] ? Color.Pink : Color.Cyan;
                        button.MouseEnter += (s, e) =>
                        {
                            button.BackColor = Color.Yellow;
                        };
                        button.MouseLeave += (s, e) =>
                        {
                            string name = button.Name;
                            string state = button.Name.Split('_')[1];
                            if (state == "False")
                                button.BackColor = Color.Cyan;
                            else
                                button.BackColor = Color.Pink;
                        };
                        if (listRoomState[index])
                        {
                            button.Click -= checkInClick;
                            button.Click += checkOutClick;
                        }
                        else
                        {
                            button.Click -= checkOutClick;
                            button.Click += checkInClick;
                        }
                    }
                }
            }
        }

        public void CreateRoomButton()
        {
            int roomNumber = (int)util.getData("select count(*) from PHONG");
            List<List<Object>> roomData = getRoomData();
            List<string> listRoomName = roomData[0].Select(s => (string)s).ToList();
            List<bool> listRoomState = roomData[1].Select(b => (bool)b).ToList();
            List<string> listBookTime = roomData[2].Select(s => (s == DBNull.Value) ? "" : s.ToString()).ToList();
            List<string> listHireDay = roomData[3].Select(s => (s == DBNull.Value) ? "" : s.ToString()).ToList();
            List<bool> listVip = roomData[4].Select(b => (bool)b).ToList();
            int col = 4;
            int row = roomNumber / col;
            int col_remain = col;
            for (int i = 0; i<= row; i++)
            {
                for (int j = 0; j < col_remain; j++)
                {
                    Button button = new Button();
                    if (listRoomState[i * col + j])
                        button.BackColor = Color.Pink;
                    else
                        button.BackColor = Color.Cyan;
                    button.Name = "btn_"+ listRoomState[i * col + j]+"_"+ listRoomName[i * col + j];
                    button.Text = createButtonContent(listRoomName[i * col + j], listRoomState[i * col + j],
                            listBookTime[i * col + j], listHireDay[i * col + j], listVip[i * col + j]);
                    button.Size = new Size(200, 160);
                    button.Font = new Font("Arial", 9);
                    button.ForeColor = Color.Black;
                    button.Location = new Point(15 + 210 * j, 70 + 170 * i);
                    button.MouseEnter += (s, e) =>
                    {
                        button.BackColor = Color.Yellow;
                    };
                    button.MouseLeave += (s, e) =>
                    {
                        string name = button.Name;
                        string state = button.Name.Split('_')[1];
                        if (state == "False")
                            button.BackColor = Color.Cyan;
                        else
                            button.BackColor = Color.Pink;
                    };
                    if (listRoomState[i * col + j])
                        button.Click += checkOutClick;
                    else
                        button.Click += checkInClick;
                    Controls.Add(button);
                    roomNumber--;
                }
                if (roomNumber < col)
                {
                    col_remain = roomNumber;   
                }
            }
        }

        public void checkInClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomId = button.Name.Split('_')[2];
            frm_ChiTietDatPhong chiTietDatPhong = new frm_ChiTietDatPhong(roomId, this);
            chiTietDatPhong.Show();
        }

        public void checkOutClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string roomId = button.Name.Split('_')[2];
            frm_ThongTinPhong thanhToan = new frm_ThongTinPhong(roomId, this);
            thanhToan.Show();
        }
    }
}
