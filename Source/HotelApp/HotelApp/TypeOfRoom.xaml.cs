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
				if (!connectData.deleteTypeOfRoom(items[selectedIndex].LoaiPhong))
				{
					return;
				}
				items = connectData.getTypeOfRoom();
				lvTypeRoom.ItemsSource = items;


			}
		}

		private void ThemHang(object sender, RoutedEventArgs e)
		{

			if (!kiemtraHeSo(CostText.Text) || TORText.Text.Length < 1 || !kiemtraHeSo(txtSKTD.Text))
			{
				MessageBox.Show("Bạn chưa nhập thông tin hoặc hệ số sai hoặc số khách tối đa sai (hệ số bao gồm số từ 0->9 hoặc thêm dấu chấm nếu là số thực. số khách tối đa phải là số nhỏ hơn bằng 3", "Cảnh báo!!!", MessageBoxButton.OK);
				return;
			}

			if(Convert.ToInt32(txtSKTD.Text)> Global.SoKhToiDa)
			{
				MessageBox.Show("Số khách tối đa phải là số nhỏ hơn bằng 3", "Cảnh báo!!!", MessageBoxButton.OK);
				return;
			}

			MessageBoxResult result = MessageBox.Show("Bạn muốn thêm loại phòng?", "Xác nhận!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				ListViewDataRoom temp = new ListViewDataRoom() { STT = items.Count + 1, LoaiPhong = TORText.Text, Dongia = float.Parse(CostText.Text), SoKhachToiDa = int.Parse(txtSKTD.Text) };
				
				if (connectData.setTypeOfRoom(temp))            //set data in server
				{
					items = connectData.getTypeOfRoom();
					lvTypeRoom.ItemsSource = items;
					TORText.Text = "";
					CostText.Text = "";
					txtSKTD.Text = "";
				}
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

			
			if (!kiemtraHeSo(CostText.Text) || TORText.Text.Length < 1 || !kiemtraHeSo(txtSKTD.Text))
			{
				if (numSuportEdit == 1)
				{
					MessageBox.Show("Bạn chưa nhập thông tin hoặc hệ số sai hoặc số khách tối đa sai (hệ số bao gồm số từ 0->9 hoặc thêm dấu chấm nếu là số thực. số khách tối đa phải là số nhỏ hơn bằng 3", "Cảnh báo!!!", MessageBoxButton.OK);
				}
				return;
			}

			if (Convert.ToInt32(txtSKTD.Text) > Global.SoKhToiDa)
			{
				if (numSuportEdit == 1)
				{
					MessageBox.Show("Số khách tối đa phải là số nhỏ hơn bằng 3", "Cảnh báo!!!", MessageBoxButton.OK);
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
						tempArr.Add(new ListViewDataRoom() { STT = i, LoaiPhong = TORText.Text, Dongia = float.Parse(CostText.Text), MaLP = items[i-1].MaLP , SoKhachToiDa = int.Parse(txtSKTD.Text)});
						if (!connectData.updateTypeOfRoom(tempArr[i - 1], items[i - 1].Dongia, items[i - 1].LoaiPhong))      //update data in server
						{
							return;
						}
						continue;
					}
					tempArr.Add(items[i - 1]);
				}
				items = connectData.getTypeOfRoom();
				lvTypeRoom.ItemsSource = items;


				TORText.Text = "";
				CostText.Text = "";
				txtSKTD.Text = "";
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
					txtSKTD.Text = lvc.SoKhachToiDa.ToString();
				}

			}
		}
	}
}
