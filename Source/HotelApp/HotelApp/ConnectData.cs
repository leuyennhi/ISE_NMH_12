using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelApp
{
	public class ConnectData
	{
		private string linkSql = "";
		private SqlConnection sql;
		public ConnectData()
		{
			linkSql = "Data Source=THANH_NHUT\\SQLEXPRESS;Initial Catalog=DataForHotelApp;Integrated Security=True";
			sql = new SqlConnection(linkSql);			
		}

		public List<ListViewDataRoom> getTypeOfRoom()
		{
			List<ListViewDataRoom> temp = new List<ListViewDataRoom>();
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				//string q = "insert into TEST(id,name)values('2','eraasd')";
				string q = "SELECT TenLP,DonGia,SoLuong FROM LOAIPHONG";
				SqlCommand cmd = new SqlCommand(q, sql);
				//cmd.ExecuteNonQuery();
				SqlDataReader reader = cmd.ExecuteReader();
				int i = 1;
				while (reader.Read())
				{
					//	var time = reader.GetDateTime(1).ToString("dd/MM/yyyy");
					//var utcTime = new DateTime(time.Ticks, DateTimeKind.Utc);
					//Console.WriteLine("{0} {1} {2}", reader.GetString(0), reader.GetDouble(1), reader.GetInt32(2));
					temp.Add(new ListViewDataRoom() { STT = i, LoaiPhong = reader.GetString(0), Dongia = (float)reader.GetDouble(1), SoLuong = reader.GetInt32(2) });
					i++;
				}
				reader.Close();
			}
			sql.Close();
			return temp;
		}

		public List<ListViewDataCustommer> getTypeOfCustommer()
		{
			List<ListViewDataCustommer> temp = new List<ListViewDataCustommer>();
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				//string q = "insert into TEST(id,name)values('2','eraasd')";
				string q = "SELECT TenLK,HeSo FROM LOAIKHACH";
				SqlCommand cmd = new SqlCommand(q, sql);
				//cmd.ExecuteNonQuery();
				SqlDataReader reader = cmd.ExecuteReader();
				int i = 1;
				while (reader.Read())
				{
					temp.Add(new ListViewDataCustommer() { STT = i, LoaiKhach = reader.GetString(0), HeSo = (float)reader.GetDouble(1) });
					i++;
				}
				reader.Close();
			}
			sql.Close();
			return temp;
		}
	}

	public class ListViewDataRoom
	{
		public int STT { get; set; }

		public string LoaiPhong { get; set; }

		public float Dongia { get; set; }

		public int SoLuong { get; set; }
	}

	public class ListViewDataCustommer
	{
		public int STT { get; set; }

		public string LoaiKhach { get; set; }

		public float HeSo { get; set; }
	}
}
