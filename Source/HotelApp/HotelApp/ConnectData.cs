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
				string q = "SELECT TenLP,DonGia,SoLuong,MaLP FROM LOAIPHONG";
				SqlCommand cmd = new SqlCommand(q, sql);
				SqlDataReader reader = cmd.ExecuteReader();
				int i = 1;
				while (reader.Read())
				{
					//	var time = reader.GetDateTime(1).ToString("dd/MM/yyyy");
					temp.Add(new ListViewDataRoom() { STT = i, LoaiPhong = reader.GetString(0), Dongia = (float)reader.GetDouble(1), SoLuong = reader.GetInt32(2), MaLP = reader.GetString(3) });
					i++;
				}
				reader.Close();
			}
			sql.Close();
			return temp;
		}

		public bool setTypeOfRoom(ListViewDataRoom item)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{

				string q = "SELECT Count(*) FROM LOAIPHONG WHERE TenLP = N'" + item.LoaiPhong + "'";
				SqlCommand cmd = new SqlCommand(q, sql);
				int count = Convert.ToInt32(cmd.ExecuteScalar());
				if (count == 0)
				{
					q = "SELECT top 1 MaLP FROM LOAIPHONG ORDER BY MaLP desc";
					cmd = new SqlCommand(q, sql);

					SqlDataReader reader = cmd.ExecuteReader();
					reader.Read();
					string malp = reader.GetString(0);
					malp = malp.Remove(0, 2);
					int newIndex = Convert.ToInt32(malp);
					newIndex++;
					reader.Close();

					q = "insert into LOAIPHONG(MaLP,TenLP,MaPT,DonGia,SoKhachToiDa,SoLuong)values('LP" + newIndex + "',N'" + item.LoaiPhong + "','PT1','" + item.Dongia + "','3','" + item.SoLuong + "')";
					cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();
				}
				else
				{
					MessageBox.Show("Tên loại phòng đã tồn tại!!", "Cảnh báo");
					sql.Close();
					return false;
				}
			}
			sql.Close();
			return true;
		}

		public bool updateTypeOfRoom(ListViewDataRoom item, double dongia, string tenLp)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				if (item.Dongia != dongia && tenLp == item.LoaiPhong)
				{
					string q = "UPDATE LOAIPHONG set TenLP = N'" + item.LoaiPhong + "',DonGia = " + item.Dongia + " where MaLP = '" + item.MaLP + "'";
					SqlCommand cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();
				}
				else
				{
					string q = "SELECT Count(*) FROM LOAIPHONG WHERE TenLP = N'" + item.LoaiPhong + "'";
					SqlCommand cmd = new SqlCommand(q, sql);
					int count = Convert.ToInt32(cmd.ExecuteScalar());
					if (count == 0)
					{
						q = "UPDATE LOAIPHONG set TenLP = N'" + item.LoaiPhong + "',DonGia = " + item.Dongia + " where MaLP = '" + item.MaLP + "'";
						cmd = new SqlCommand(q, sql);
						cmd.ExecuteNonQuery();
					}
					else
					{
						MessageBox.Show("Tên loại phòng đã tồn tại!!", "Cảnh báo");
						sql.Close();
						return false;
					}
				}

			}
			sql.Close();
			return true;
		}

		public bool deleteTypeOfRoom(string loaiPhong)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "SELECT Top(1) MaLP FROM LOAIPHONG WHERE TenLP = N'" + loaiPhong + "' order by MaLP asc";
				SqlCommand cmd = new SqlCommand(q, sql);
				SqlDataReader reader = cmd.ExecuteReader();
				reader.Read();
				string malp = reader.GetString(0);
				reader.Close();

				q = "SELECT Count(*) FROM PHONG WHERE MaLP = '" + malp + "'";
				cmd = new SqlCommand(q, sql);
				int count = Convert.ToInt32(cmd.ExecuteScalar());
				if (count == 0)
				{
					q = "DELETE FROM LOAIPHONG WHERE MaLP = '" + malp + "'";
					cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();
				}
				else
				{
					MessageBox.Show("Phải đổi hết loại phòng " + loaiPhong + " sang loại phòng khác ở danh sách phòng trước khi xóa!!", "Cảnh báo");
					sql.Close();
					return false;
				}
			}
			sql.Close();
			return true;
		}

		public List<ListViewDataCustommer> getTypeOfCustommer()
		{
			List<ListViewDataCustommer> temp = new List<ListViewDataCustommer>();
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "SELECT TenLK,HeSo,MaLK FROM LOAIKHACH";
				SqlCommand cmd = new SqlCommand(q, sql);
				SqlDataReader reader = cmd.ExecuteReader();
				int i = 1;
				while (reader.Read())
				{
					temp.Add(new ListViewDataCustommer() { STT = i, LoaiKhach = reader.GetString(0), HeSo = (float)reader.GetDouble(1), MaLK = reader.GetString(2) });
					i++;
				}
				reader.Close();
			}
			sql.Close();
			return temp;
		}

		public bool setTypeOfCustomer(ListViewDataCustommer item)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "SELECT Count(*) FROM LOAIKHACH WHERE TenLK = N'" + item.LoaiKhach + "'";
				SqlCommand cmd = new SqlCommand(q, sql);
				int count = Convert.ToInt32(cmd.ExecuteScalar());
				if (count == 0)
				{
					q = "SELECT top 1 MaLK FROM LOAIKHACH ORDER BY MaLK desc";
					cmd = new SqlCommand(q, sql);

					SqlDataReader reader = cmd.ExecuteReader();
					reader.Read();
					string malp = reader.GetString(0);
					malp = malp.Remove(0, 2);
					int newIndex = Convert.ToInt32(malp);
					newIndex++;
					reader.Close();

					q = "insert into LOAIKHACH(MaLK,TenLK,HeSo)values('LK" + newIndex + "',N'" + item.LoaiKhach + "','" + item.HeSo + "')";
					cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();
				}
				else
				{
					MessageBox.Show("Tên loại khách đã tồn tại!!", "Cảnh báo");
					sql.Close();
					return false;
				}
			}
			sql.Close();
			return true;
		}

		public bool updateTypeOfCustomer(ListViewDataCustommer item, float heso, string tenLK)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				if (item.HeSo != heso && item.LoaiKhach == tenLK)
				{
					string q = "UPDATE LOAIKHACH set TenLK = N'" + item.LoaiKhach + "',HeSo = " + item.HeSo + " where MaLK = '" + item.MaLK + "'";
					SqlCommand cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();
				}
				else
				{
					string q = "SELECT Count(*) FROM LOAIKHACH WHERE TenLK = N'" + item.LoaiKhach + "'";
					SqlCommand cmd = new SqlCommand(q, sql);
					int count = Convert.ToInt32(cmd.ExecuteScalar());
					if (count == 0)
					{
						q = "UPDATE LOAIKHACH set TenLK = N'" + item.LoaiKhach + "',HeSo = " + item.HeSo + " where MaLK = '" + item.MaLK + "'";
						cmd = new SqlCommand(q, sql);
						cmd.ExecuteNonQuery();
					}
					else
					{
						MessageBox.Show("Tên loại khách đã tồn tại!!", "Cảnh báo");
						sql.Close();
						return false;
					}
				}

			}
			sql.Close();
			return true;
		}

		public bool deleteTypeOfCustomer(string loaiKhach)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "SELECT Top(1) MaLK FROM LOAIKHACH WHERE TenLK = N'" + loaiKhach + "' order by MaLK asc";
				SqlCommand cmd = new SqlCommand(q, sql);
				SqlDataReader reader = cmd.ExecuteReader();
				reader.Read();
				string malk = reader.GetString(0);
				reader.Close();

				q = "SELECT Count(*) FROM KHACHHANG WHERE MaLK = '" + malk + "'";
				cmd = new SqlCommand(q, sql);
				int count = Convert.ToInt32(cmd.ExecuteScalar());
				if (count == 0)
				{
					q = "DELETE FROM LOAIKHACH WHERE MaLK = '" + malk + "'";
					cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();
				}
				else
				{
					MessageBox.Show("Phải đổi hết loại khách " + loaiKhach + " sang loại khách khác ở danh sách khách hàng trước khi xóa!!", "Cảnh báo");
					sql.Close();
					return false;
				}
			}
			sql.Close();
			return true;
		}

		public List<ListViewSurtax> getSurtax()
		{
			List<ListViewSurtax> temp = new List<ListViewSurtax>();
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "SELECT LP.MaPT , LP.MaLP, PT.TiLePhuThu FROM LOAIPHONG LP, PHUTHU PT WHERE LP.MaPT = PT.MaPT order by MaLP asc";
				SqlCommand cmd = new SqlCommand(q, sql);
				SqlDataReader reader = cmd.ExecuteReader();
				while (reader.Read())
				{
					temp.Add(new ListViewSurtax() { MaPT = reader.GetString(0), MaLP = reader.GetString(1), TiLePhuThu = (float)reader.GetDouble(2) });
				}
				reader.Close();
			}
			sql.Close();
			return temp;
		}

		public void setSurtax(string type, float phuthu)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				//neu phụ thu có trong bảng thì UPdate bảng LP
				//nếu khong có thì thêm mới phụ thu và update bảng LP
				if (type == "All")
				{
					string q = "SELECT COUNT(*) FROM PHUTHU WHERE MaPT = 'PT0'";
					SqlCommand cmd = new SqlCommand(q, sql);
					int count = Convert.ToInt32(cmd.ExecuteScalar());

					if (count == 0)
					{
						q = "insert into PHUTHU(MaPT,TiLePhuThu)values('PT0','" + phuthu + "')";

					}
					else
					{
						q = "UPDATE PHUTHU set TiLePhuThu = " + phuthu + " where MaPT = 'PT0'";
					}
					cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();

					q = "UPDATE LOAIPHONG set MaPT = 'PT0'";
					cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();
					Console.WriteLine("ahihi 123");
				}
				else
				{
					string q = "SELECT COUNT(*) FROM PHUTHU WHERE TiLePhuThu = " + phuthu;
					SqlCommand cmd = new SqlCommand(q, sql);
					int count = Convert.ToInt32(cmd.ExecuteScalar());

					Console.WriteLine("ahihi {0}", count);

					//neeu gia tri trung vs gias trij All thif cho nos veef PT0
					if (count >= 1)
					{
						q = "SELECT Top(1) MaPT FROM PHUTHU WHERE TiLePhuThu = " + phuthu + " order by MaPT asc";
						cmd = new SqlCommand(q, sql);
						SqlDataReader reader = cmd.ExecuteReader();
						reader.Read();
						string maPhuThu = reader.GetString(0);
						reader.Close();

						if (maPhuThu[2] == '0')
						{

							Console.WriteLine(maPhuThu);
							q = "UPDATE LOAIPHONG set MaPT = 'PT0' where MaLP = '" + type + "'";
							cmd = new SqlCommand(q, sql);
							cmd.ExecuteNonQuery();
							sql.Close();
							return;
						}
					}

					string index = type.Remove(0, 2);
					q = "SELECT COUNT(*) FROM PHUTHU WHERE MaPT = 'PT" + index + "'";
					cmd = new SqlCommand(q, sql);
					count = Convert.ToInt32(cmd.ExecuteScalar());

					if (count == 0)
					{
						//them moi
						q = "insert into PHUTHU(MaPT,TiLePhuThu)values('PT" + index + "','" + phuthu + "')";
					}
					else
					{
						q = "UPDATE PHUTHU set TiLePhuThu = " + phuthu + " where MaPT = 'PT" + index + "'";
					}
					cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();

					q = "UPDATE LOAIPHONG set MaPT = 'PT" + index + "' where MaLP = '" + type + "'";
					cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();
				}
			}
			sql.Close();
		}

		public void setNewRoom(string maLP, string maPhong)
		{
			sql.Open();
			if (sql.State == System.Data.ConnectionState.Open)
			{
				string q = "SELECT Count(*) FROM PHONG WHERE MaPhong = '" + maPhong + "'";
				SqlCommand cmd = new SqlCommand(q, sql);
				int count = Convert.ToInt32(cmd.ExecuteScalar());
				if (count == 0)
				{
					q = "insert into PHONG(MaPhong,MaLP,TinhTrang)values('" + maPhong + "','" + maLP + "','false')";
					cmd = new SqlCommand(q, sql);
					cmd.ExecuteNonQuery();
				}
				else
				{
					MessageBox.Show("Phòng " + maPhong + " đã tồn tại!");
				}
			}
			sql.Close();
			return;
		}
	}

	public class ListViewDataRoom
	{
		public int STT { get; set; }

		public string LoaiPhong { get; set; }

		public double Dongia { get; set; }

		public int SoLuong { get; set; }

		public string MaPT {get; set;}

		public int SoKhachToiDa { get; set; }

		public string Ghichu { get; set; }

		public string MaLP { get; set; }
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
