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

namespace HotelApp
{
	/// <summary>
	/// Interaction logic for TypeOfRoom.xaml
	/// </summary>
	public partial class TypeOfRoom : Window
	{
		private List<ListViewDataRoom> items = new List<ListViewDataRoom>();
		private bool editAction = false;
		private int Stttext = 0;

		public TypeOfRoom()
		{
			InitializeComponent();

			//string indexPath = "pack://siteoforigin:,,,/Resources/add.png";
			//BitmapImage image = new BitmapImage();
			//image.BeginInit();
			//image.UriSource = new Uri(indexPath);
			//image.EndInit();

			items.Add(new ListViewDataRoom() { STT = 1, LoaiPhong = "Phong vip", PhuThu = (float)50000});
			items.Add(new ListViewDataRoom() { STT = 2, LoaiPhong = "Phong thuong", PhuThu = (float)15000 });
			items.Add(new ListViewDataRoom() { STT = 3, LoaiPhong = "Phong bad", PhuThu = (float)3223.32});
			lvTypeRoom.ItemsSource = items;

		}

		public void RemoveTORText(object sender, EventArgs e)
		{
			//TOCText.Text = "";
		}

		public void AddTORText(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(TORText.Text)) { }
			//TOCText.Text = "Nhập loại khách hàng...";
		}

		public void RemoveCoefficientText(object sender, EventArgs e)
		{
			//CoeffText.Text = "";
		}

		public void AddCoefficientText(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(CoeffText.Text)) { }
			//CoeffText.Text = "Nhập hệ số...";
		}

		private void XoaHang(object sender, RoutedEventArgs e)
		{
			int selectedIndex = lvTypeRoom.SelectedIndex;

			List<ListViewDataRoom> tempArr = new List<ListViewDataRoom>();
			int j = 1;

			for (int i = 0; i < items.Count; i++)
			{
				if (i == selectedIndex) continue;
				ListViewDataRoom temp = new ListViewDataRoom();
				temp = items[i];
				temp.STT = j;
				tempArr.Add(temp);
				j++;
			}
			lvTypeRoom.ItemsSource = tempArr;
			items = tempArr;
		}

		private void ThemHang(object sender, RoutedEventArgs e)
		{

			if (!kiemtraHeSo(CoeffText.Text) || TORText.Text.Length < 1)
			{
				return;
			}

			List<ListViewDataRoom> tempArr = new List<ListViewDataRoom>();

			int i;

			for (i = 0; i < items.Count; i++)
			{
				tempArr.Add(items[i]);
			}

			tempArr.Add(new ListViewDataRoom() { STT = i + 1, LoaiPhong = TORText.Text, PhuThu = float.Parse(CoeffText.Text) });

			lvTypeRoom.ItemsSource = tempArr;
			items = tempArr;

			TORText.Text = "";
			CoeffText.Text = "";

		}

		private bool kiemtraHeSo(String str)
		{
			if (str.Length < 1)
			{
				return false;
			}
			for (var i = 0; i < str.Length; i++)
			{
				if ((str[i] > '9' || str[i] < '0') && str[i] != '.')
				{
					return false;
				}
			}
			return true;
		}

		private void ChinhSuaHang(object sender, RoutedEventArgs e)
		{

			editAction = true;

			if (!kiemtraHeSo(CoeffText.Text) || TORText.Text.Length < 1)
			{
				return;
			}

			List<ListViewDataRoom> tempArr = new List<ListViewDataRoom>();

			for (int i = 1; i <= items.Count; i++)
			{
				if (i == Stttext)
				{
					tempArr.Add(new ListViewDataRoom() { STT = i, LoaiPhong = TORText.Text, PhuThu = float.Parse(CoeffText.Text) });
					continue;
				}
				tempArr.Add(items[i - 1]);
			}
			lvTypeRoom.ItemsSource = tempArr;

			items = tempArr;

			TORText.Text = "";
			CoeffText.Text = "";
		}

		private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListViewDataRoom lvc = (ListViewDataRoom)lvTypeRoom.SelectedItem;
			if (lvc != null)
			{
				if (editAction)
				{
					Stttext = lvc.STT;
					TORText.Text = lvc.LoaiPhong.ToString();
					CoeffText.Text = lvc.PhuThu.ToString();
				}

			}
		}

		private void DSPhong(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Danh sách phòng");
		}

		private void LoaiPhong(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("loại phòng");
		}

	}

	public class ListViewDataRoom
	{
		public int STT { get; set; }

		public string LoaiPhong { get; set; }

		public float PhuThu { get; set; }
	}
}
