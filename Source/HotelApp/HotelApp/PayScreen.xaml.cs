using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml;
using System.Windows.Navigation;

namespace HotelApp
{
	/// <summary>
	/// Interaction logic for PayScreen.xaml
	/// </summary>
	public partial class PayScreen : UserControl
	{
		private List<string> listCustomer = new List<string>();
		private string valueNameCustomer;
		private ConnectData connectData;

		private string valuedayStart;
		private string valuedayEnd;
		private string note;
		private ListBookRoom room;
		private List<Customer> listCustomers;
		private float valueHeSo;
		private double thanhtien;
		private string maPhong;

		public PayScreen(ConnectData conData, string mp)
		{
			InitializeComponent();

			connectData = conData;
			maPhong = mp;

			if (Global.songay == 0)
			{
				connectData.getInfoPayRoom(maPhong);
			}
			else
			{
				////////
			}
			listCustomers = Global.listCustomer;

			room = Global.room;
			note = Global.note;
			valuedayStart = Global.valuedayStart;
			valuedayEnd = Global.valuedayEnd;

			List<string> loaikhach = new List<string>();
			for (int i = 0; i < listCustomers.Count; i++)
			{
				listCustomer.Add(listCustomers[i].TenKH);
				if (checkExistinList(loaikhach, listCustomers[i].TenLK))
				{
					loaikhach.Add(listCustomers[i].TenLK);
				}
			}

			txtMaLP.Content = room.TenLP;
			txtMaPhong.Content = room.MaPhong;
			txtDonGia.Content = room.DonGia.ToString();
			txtNgayBatDau.Content = valuedayStart;
			txtNgayTraPhong.Content = valuedayEnd;

			valueHeSo = connectData.getFactor(loaikhach);
			txtHeSo.Content = valueHeSo.ToString();

			thanhtien = tinhtien();

			txtThanhTien.Content = thanhtien.ToString() + " VNĐ";
		}

		private bool checkExistinList(List<string> loaikhach, string str)
		{
			for (int i = 0; i < loaikhach.Count; i++)
			{
				if (loaikhach[i] == str)
				{
					return false;
				}
			}
			return true;
		}

		private double tinhtien()
		{
			double kq = 1;

			double dongia = room.DonGia;

			int songuoi = listCustomers.Count;

			double phuthu = room.PhuThu;

			if (songuoi < 3)
			{
				phuthu = 1;
			}

			room.PhuThu = (float)phuthu;

			txtPhuThu.Content = phuthu.ToString();

			int songay = Global.songay;

			kq = dongia * songuoi * phuthu * valueHeSo * songay;


			return kq;
		}

		private string formatDate(string str)
		{
			StringBuilder strB = new StringBuilder(str);
			char temp;
			temp = strB[0];
			strB[0] = strB[3];
			strB[3] = temp;
			temp = strB[1];
			strB[1] = strB[4];
			strB[4] = temp;

			return strB.ToString();
		}

		private int findInexcus()
		{
			for (int i = 0; i < listCustomers.Count; i++)
			{
				if (listCustomers[i].SDT.Length > 0)
				{
					return i;
				}
			}
			return 0;
		}


		private void Combobox_Loaded(object sender, RoutedEventArgs e)
		{
			var combo = sender as ComboBox;
			combo.ItemsSource = listCustomer;
			combo.SelectedIndex = findInexcus();
		}

		private void Pay_Click(object sender, RoutedEventArgs e)
		{
			if (txtSDT.Text.Length < 9)
			{
				MessageBox.Show("vui lòng nhập số điện thoại!");
				return;
			}

			listCustomers[cbbKhach.SelectedIndex].SDT = txtSDT.Text;
			room.HeSo = valueHeSo;

			if (maPhong.Length < 1)
			{
				connectData.setBookRoom(listCustomers, room, formatDate(valuedayStart), formatDate(valuedayEnd), Global.note, thanhtien, cbbKhach.SelectedIndex, true);

			}
			else
			{

				connectData.setBookRoom(listCustomers, room, formatDate(valuedayStart), formatDate(valuedayEnd), Global.note, thanhtien, cbbKhach.SelectedIndex, false);

			}


			Global.listCustomer = null;
			Global.room = null;
			Global.valuedayStart = "";
			Global.valuedayEnd = "";
			Global.songay = 0;
			Global.note = "";
		}

		private void Cancel(object sender, RoutedEventArgs e)
		{

		}

		private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var selectedComboItem = sender as ComboBox;
			valueNameCustomer = selectedComboItem.SelectedItem as string;
			setCMNDAndAddress(valueNameCustomer);
		}

		private void setCMNDAndAddress(string value)
		{
			for (int i = 0; i < listCustomers.Count; i++)
			{
				if ((cbbKhach.SelectedIndex + 1) == listCustomers[i].STT)
				{
					txtCMND.Content = listCustomers[i].CMND;
					txtDiaChi.Content = listCustomers[i].DiaChi;
					txtSDT.Text = listCustomers[i].SDT;
					break;
				}
			}
		}
	}
}
