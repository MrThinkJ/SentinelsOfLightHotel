using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace DemoADO
{
    internal class LOPDUNGCHUNG
    {
        SqlConnection conn;

        public LOPDUNGCHUNG()
        {
            string chuoikn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ACER\\Downloads\\Compressed\\LastADO\\QuanLyKhachSan\\QLKHACHSAN.mdf;Integrated Security=True";
            conn = new SqlConnection(chuoikn);
        }

        public int ThemXoaSua(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();
            int kq = cmd.ExecuteNonQuery();
            conn.Close();
            return kq;
        }

        public DataTable LoadData(string sql)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public Object getData(string sql)
        {
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            int data = (int)comm.ExecuteScalar();
            conn.Close();
            return data;
        }

        public List<Object> getListData(string sql)
        {
            List<Object> list = new List<Object>();
            SqlCommand comm = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                list.Add(reader[0]);
            }
            conn.Close();
            return list;
        }
    }
}
