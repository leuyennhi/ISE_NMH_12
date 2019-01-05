using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HotelApp
{
	
	public partial class AddRoomScreen : UserControl
	{
		private List<string> listTypeRoom = new List<string>();
		private string typeRoom = "";
		private List<ListViewDataRoom> list_Type_Rooms = new List<ListViewDataRoom>();
		private ConnectData connectData;
		public AddRoomScreen(ConnectData conData)
		{
			InitializeComponent();
			connectData = conData;

			list_Type_Rooms = conData.getTypeOfRoom();
			//kiểm tra tên phòng có hay chưa
			//lấy loại phòng và đơn giá của nó show ra
			//
			for (int i = 0; i < list_Type_Rooms.Count; i++)
			{
				listTypeRoom.Add(list_Type_Rooms[i].LoaiPhong);
			}
		}

		private void Combobox_Loaded(object sender, RoutedEventArgs e)
		{
			var combo = sender as ComboBox;
			combo.ItemsSource = listTypeRoom;
			combo.SelectedIndex = 0;
			combo.Background = Brushes.Yellow;
		}

		private void Combobox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			var selectedComboItem = sender as ComboBox;
			typeRoom = selectedComboItem.SelectedItem as string;
			//MessageBox.Show(typeRoom);
			int vitri = findIndex(list_Type_Rooms, typeRoom);
			txtDonGia.Text = list_Type_Rooms[vitri].Dongia.ToString();
			txtSoKhachToiDa.Text = list_Type_Rooms[vitri].SoKhachToiDa.ToString();
		}

		private int findIndex(List<ListViewDataRoom> temp, string type)
		{
			int i;
			for (i = 0; i < temp.Count; i++)
			{
				if (temp[i].LoaiPhong == type)
				{
					return i;
				}
			}
			return -1;
		}

		private bool kiemtraDonGia(String str)
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
		private bool kiemtraSoKhach(String str)
		{
			if (str.Length < 1)
			{
				return false;
			}
			for (var i = 0; i < str.Length; i++)
			{
				if (str[i] > '9' || str[i] < '0')
				{
					return false;
				}
			}
			return true;
		}

		private void Cancel(object sender, RoutedEventArgs e)
		{
			return;
		}
		private void AddRoom(object sender, RoutedEventArgs e)
		{
			if (txtTenPhong.Text.Length < 1 || !kiemtraDonGia(txtDonGia.Text) || !kiemtraSoKhach(txtSoKhachToiDa.Text))
			{
				MessageBox.Show("Bạn chưa nhập thông tin hoặc hệ số sai (hệ số bao gồm số từ 0->9 hoặc thêm dấu chấm nếu là số thực hoặc vượt quá số khách tối đa (phải là số nguyên).", "Cảnh báo!!!", MessageBoxButton.OK);
				return;
			}
			MessageBoxResult result = MessageBox.Show("Bạn muốn thêm loại phòng?", "Xác nhận!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				int vitri = findIndex(list_Type_Rooms, typeRoom);
				string maLP = list_Type_Rooms[vitri].MaLP;
				connectData.setNewRoom(maLP, txtTenPhong.Text, txtGhiChu.Text);
			}
		}
	}

}
