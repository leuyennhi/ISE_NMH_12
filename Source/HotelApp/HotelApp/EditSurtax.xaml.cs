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

namespace HotelApp
{
	/// <summary>
	/// Interaction logic for EditSurtax.xaml
	/// </summary>
	public partial class EditSurtax : UserControl
	{
		private List<string> listTypeRoom = new List<string>();
		private string typeRoom = "";
		ConnectData connectData;
		public EditSurtax( ConnectData conData)
		{
			InitializeComponent();
			InitializeComponent();

			conData.getSurtax();

			listTypeRoom.Add("All");
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

		private bool kiemtraPhuThu(String str)
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

		private void Cancel(object sender, RoutedEventArgs e)
		{
			return;
		}
		private void AddCoeff(object sender, RoutedEventArgs e)
		{
			if (!kiemtraPhuThu(txtPhuThu.Text))
			{
				MessageBox.Show("Bạn chưa nhập phụ thu hoặc nhập sai (phụ thu bao gồm số từ 0->9 hoặc có dấu chấm nếu là số thực)", "Cảnh báo!!!", MessageBoxButton.OK);
				return;
			}
			MessageBoxResult result = MessageBox.Show("Bạn muốn chỉnh sửa phụ thu?", "Xác nhận!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
			}
		}
	}
}
