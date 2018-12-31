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
				string q = "SELECT TenLP,DonGia,SoLuong FROM LOAIPHONG";
				SqlCommand cmd = new SqlCommand(q, sql);
				SqlDataReader reader = cmd.ExecuteReader();
				int i = 1;
				while (reader.Read())
				{
					//	var time = reader.GetDateTime(1).ToString("dd/MM/yyyy");
						temp.Add(new ListViewDataRoom() { STT = i, LoaiPhong = reader.GetString(0), Dongia = (float)reader.GetDouble(1), SoLuong = reader.GetInt32(2) });
					i++;
				}
				reader.Close();
			}
			sql.Close();
			return temp;
		}

		public void setTypeOfRoom(ListViewDataRoom item)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				
				string q = "SELECT top 1 MaLP FROM LOAIPHONG ORDER BY MaLP desc";
				SqlCommand cmd = new SqlCommand(q, sql);

				SqlDataReader reader = cmd.ExecuteReader();
				reader.Read();
				string malp = reader.GetString(0);
				malp = malp.Remove(0, 2);
				int newIndex = Convert.ToInt32(malp);
				newIndex++;
				reader.Close();

				q = "insert into LOAIPHONG(MaLP,TenLP,MaPT,DonGia,SoKhachToiDa,SoLuong)values('LP" + newIndex + "','" + item.LoaiPhong + "','PT1','" + item.Dongia + "','3','" + item.SoLuong + "')";
				SqlCommand cmdAdd = new SqlCommand(q, sql);
				cmdAdd.ExecuteNonQuery();
			}
			sql.Close();
			return;
		}

		public void updateTypeOfRoom(ListViewDataRoom item)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "UPDATE LOAIPHONG set TenLP = '"+ item.LoaiPhong +"',DonGia = "+ item.Dongia +" where MaLP = 'LP" + item.STT + "'";
				SqlCommand cmdAdd = new SqlCommand(q, sql);
				cmdAdd.ExecuteNonQuery();
			}
			sql.Close();
			return;
		}

		public void deleteTypeOfRoom(int index)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "DELETE FROM LOAIPHONG WHERE MaLP = 'LP" + index + "'";
				SqlCommand cmdAdd = new SqlCommand(q, sql);
				cmdAdd.ExecuteNonQuery();
			}
			sql.Close();
			return;
		}

		public List<ListViewDataCustommer> getTypeOfCustommer()
		{
			List<ListViewDataCustommer> temp = new List<ListViewDataCustommer>();
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "SELECT TenLK,HeSo FROM LOAIKHACH";
				SqlCommand cmd = new SqlCommand(q, sql);
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

		public void setTypeOfCustomer(ListViewDataCustommer item)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{

				string q = "SELECT top 1 MaLK FROM LOAIKHACH ORDER BY MaLK desc";
				SqlCommand cmd = new SqlCommand(q, sql);

				SqlDataReader reader = cmd.ExecuteReader();
				reader.Read();
				string malp = reader.GetString(0);
				malp = malp.Remove(0, 2);
				int newIndex = Convert.ToInt32(malp);
				newIndex++;
				reader.Close();

				q = "insert into LOAIKHACH(MaLK,TenLK,HeSo)values('LK" + newIndex + "','" + item.LoaiKhach + "','" + item.HeSo + "')";
				SqlCommand cmdAdd = new SqlCommand(q, sql);
				cmdAdd.ExecuteNonQuery();
			}
			sql.Close();
			return;
		}

		public void updateTypeOfCustomer(ListViewDataCustommer item)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "UPDATE LOAIKHACH set TenLK = '" + item.LoaiKhach + "',HeSo = " + item.HeSo + " where MaLK = 'LK" + item.STT + "'";
				SqlCommand cmdAdd = new SqlCommand(q, sql);
				cmdAdd.ExecuteNonQuery();
			}
			sql.Close();
			return;
		}

		public void deleteTypeOfCustomer(int index)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "DELETE FROM LOAIKHACH WHERE MaLK = 'LK" + index + "'";
				SqlCommand cmdAdd = new SqlCommand(q, sql);
				cmdAdd.ExecuteNonQuery();
			}
			sql.Close();
			return;
		}

		public List<ListViewSurtax> getSurtax()
		{
			List<ListViewSurtax> temp = new List<ListViewSurtax>();
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "SELECT DISTINCT  LP.MaPT , LP.MaLP, PT.TiLePhuThu FROM LOAIPHONG LP, PHUTHU PT WHERE LP.MaPT = PT.MaPT";
				SqlCommand cmd = new SqlCommand(q, sql);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					temp.Add(new ListViewSurtax() {MaPT = reader.GetString(0), MaLP = reader.GetString(1), TiLePhuThu = (float)reader.GetDouble(2) });
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

		public string MaPT {get; set;}

		public int SoKhachToiDa { get; set; }

		public string Ghichu { get; set; }
	}

	public class ListViewDataCustommer
	{
		public int STT { get; set; }

		public string LoaiKhach { get; set; }

		public float HeSo { get; set; }

		public string MaLK { get; set; }

		public string MoTa { get; set; }
	}

	public class ListViewSurtax
	{

		public string MaPT { get; set; }

		public string MaLP { get; set; }

		public float TiLePhuThu { get; set; }
	}
}
