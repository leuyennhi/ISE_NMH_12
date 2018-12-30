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
		public AddRoomScreen()
		{
			InitializeComponent();
			listTypeRoom.Add("A");
			listTypeRoom.Add("B");
			listTypeRoom.Add("C");
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

		private void DanhSachPhong(object sender, RoutedEventArgs e)
		{

		}

		private void LoaiPhong(object sender, RoutedEventArgs e)
		{
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
			}
		}
	}

}
