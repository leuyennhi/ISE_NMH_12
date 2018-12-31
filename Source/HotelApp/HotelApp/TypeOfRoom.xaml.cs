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
using System.Windows.Navigation;
using System.Data.SqlClient;

namespace HotelApp
{
	/// <summary>
	/// Interaction logic for TypeOfRoom.xaml
	/// </summary>
	public partial class TypeOfRoom : UserControl
	{
		private List<ListViewDataRoom> items = new List<ListViewDataRoom>();
		private bool editAction = false;
		private int Stttext = 0;
		ConnectData connectData;
		public TypeOfRoom(ConnectData conData)
		{
			InitializeComponent();
			connectData = conData;
			items = conData.getTypeOfRoom();        //get data in server
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
			//CostText.Text = "";
		}

		public void AddCoefficientText(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(CostText.Text)) { }
			//CostText.Text = "Nhập hệ số...";
		}

		private void XoaHang(object sender, RoutedEventArgs e)
		{
			int selectedIndex = lvTypeRoom.SelectedIndex;
			MessageBoxResult result = MessageBox.Show("Bạn muốn xóa hàng " + (selectedIndex + 1).ToString() + "?", "Cảnh báo!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
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

				connectData.deleteTypeOfRoom(selectedIndex + 1);
			}
		}

		private void ThemHang(object sender, RoutedEventArgs e)
		{

			if (!kiemtraHeSo(CostText.Text) || TORText.Text.Length < 1)
			{
				MessageBox.Show("Bạn chưa nhập thông tin hoặc hệ số sai (hệ số bao gồm số từ 0->9 hoặc thêm dấu chấm nếu là số thực.", "Cảnh báo!!!", MessageBoxButton.OK);
				return;
			}
			MessageBoxResult result = MessageBox.Show("Bạn muốn thêm loại phòng?", "Xác nhận!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				List<ListViewDataRoom> tempArr = new List<ListViewDataRoom>();

				int i;

				for (i = 0; i < items.Count; i++)
				{
					tempArr.Add(items[i]);
				}

				tempArr.Add(new ListViewDataRoom() { STT = i + 1, LoaiPhong = TORText.Text, Dongia = float.Parse(CostText.Text) });

				lvTypeRoom.ItemsSource = tempArr;
				items = tempArr;

				TORText.Text = "";
				CostText.Text = "";

				connectData.setTypeOfRoom(items[i]);            //set data in server


			}
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

		int numSuportEdit = 0;
		private void ChinhSuaHang(object sender, RoutedEventArgs e)
		{

			editAction = true;

			if (!kiemtraHeSo(CostText.Text) || TORText.Text.Length < 1)
			{
				if (numSuportEdit == 1)
				{
					MessageBox.Show("Bạn chưa chỉnh sửa thông tin hoặc hệ số sai (hệ số bao gồm số từ 0->9 hoặc thêm dấu chấm nếu là số thực.", "Cảnh báo!!!", MessageBoxButton.OK);
				}
				return;
			}
			MessageBoxResult result = MessageBox.Show("Bạn muốn chỉnh sửa khách hàng?", "Xác nhận!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				List<ListViewDataRoom> tempArr = new List<ListViewDataRoom>();

				for (int i = 1; i <= items.Count; i++)
				{
					if (i == Stttext)
					{
						tempArr.Add(new ListViewDataRoom() { STT = i, LoaiPhong = TORText.Text, Dongia = float.Parse(CostText.Text) });
						connectData.updateTypeOfRoom(tempArr[i - 1]);		//update data in server
						continue;
					}
					tempArr.Add(items[i - 1]);
				}
				lvTypeRoom.ItemsSource = tempArr;

				items = tempArr;

				TORText.Text = "";
				CostText.Text = "";
			}
		}

		private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListViewDataRoom lvc = (ListViewDataRoom)lvTypeRoom.SelectedItem;
			if (lvc != null)
			{
				if (editAction)
				{
					numSuportEdit = 1;
					Stttext = lvc.STT;
					TORText.Text = lvc.LoaiPhong.ToString();
					CostText.Text = lvc.Dongia.ToString();
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
		
		private void LoaiKhach(object sender, RoutedEventArgs e)
		{
			//Main.Content = new TypeOfCustomer();
		}
	}

	//public class ListViewDataRoom
	//{
	//	public int STT { get; set; }

	//	public string LoaiPhong { get; set; }

	//	public float Dongia { get; set; }
	//}
}
