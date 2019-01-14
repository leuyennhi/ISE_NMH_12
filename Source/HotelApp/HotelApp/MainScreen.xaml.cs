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
	/// Interaction logic for MainScreen.xaml
	/// </summary>
	public partial class MainScreen : UserControl
	{
		private ConnectData conData;
		private ListStaff currentUser;
		public MainScreen()
		{
			InitializeComponent();

			conData = new ConnectData();
			

			Global.mainNavigate = GridMain;

			currentUser = conData.getCurrentUser();
			lbltenNV.Content = currentUser.TenNV;
			lblchucVu.Content = currentUser.ChucVu;

			ListViewMenu.SelectedIndex = 0;
			//nếu là nhân viên thì ẩn đi
			if (currentUser.ChucVu == "Nhân viên")
			{
				MucDSNhanVien.Visibility = Visibility.Hidden;
				MucDSNhanVien.Visibility = Visibility.Collapsed;

				MucBaoCaoDoanhThu.Visibility = Visibility.Hidden;
				MucBaoCaoDoanhThu.Visibility = Visibility.Collapsed;

				MucBaoCaoMatDo.Visibility = Visibility.Hidden;
				MucBaoCaoMatDo.Visibility = Visibility.Collapsed;

				MucChinhSuaPhuThu.Visibility = Visibility.Hidden;
				MucChinhSuaPhuThu.Visibility = Visibility.Collapsed;

				MucLoaiKhach.Visibility = Visibility.Hidden;
				MucLoaiKhach.Visibility = Visibility.Collapsed;

				MucLoaiPhong.Visibility = Visibility.Hidden;
				MucLoaiPhong.Visibility = Visibility.Collapsed;
			}
		}

		private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
		{
			ButtonCloseMenu.Visibility = Visibility.Visible;
			ButtonOpenMenu.Visibility = Visibility.Collapsed;
		}

		private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
		{
			ButtonCloseMenu.Visibility = Visibility.Collapsed;
			ButtonOpenMenu.Visibility = Visibility.Visible;
		}

		private void SettingsAccount_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("move to screen setting!!");
		}

		private void InfoAccount_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Move to screen info user!!!");
		}

		private void Help_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("help user something!!!!");
		}

		private void Logout_Click(object sender, RoutedEventArgs e)
		{
			//chuyen sang login
			UserControl urc = new LoginScreen();
			Global.registernavigation.Children.Add(urc);
			Global.MaNV = "";
			
		}

		private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			UserControl usc = null;
			GridMain.Children.Clear();

			switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
			{
				case "MucDSPhong":
                    usc = new ListRoom(conData);
                    GridMain.Children.Add(usc);
                    break;
				case "MucDSKhach":
					usc = new ListCustomer(conData);
					GridMain.Children.Add(usc);
					break;
				case "MucLoaiKhach":
					usc = new TypeOfCustomer(conData);
					GridMain.Children.Add(usc);
					break;
				case "MucLoaiPhong":
					usc = new TypeOfRoom(conData);
					GridMain.Children.Add(usc);
					break;
				case "MucBaoCaoDoanhThu":
                    usc = new BaoCaoDoanhThu(conData);
                    GridMain.Children.Add(usc);
                    break;
				case "MucBaoCaoMatDo":
                    usc = new BaoCaoMatDo(conData);
                    GridMain.Children.Add(usc);
                    break;
				case "MucChinhSuaPhuThu":
					usc = new EditSurtax(conData);
					GridMain.Children.Add(usc);
					break;
				case "MucDSNhanVien":
                    usc = new DanhSachNhanVien(conData);
                    GridMain.Children.Add(usc);
                    break;
				default:
					break;
			}
		}
        
    }
}
