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
	/// Interaction logic for InfoCustomer.xaml
	/// </summary>
	public partial class InfoCustomer : UserControl
	{
		private ConnectData connectData;
		string maKH;
		Customer item = new Customer();
		int typeOfCustom;

		List<ListViewDataCustommer> listTypeOfCustom = new List<ListViewDataCustommer>();
		List<string> listNameTypeOfCustom = new List<string>();

		bool isEditing = false;

		public InfoCustomer(ConnectData conData, string MaKH)
		{
			InitializeComponent();
			connectData = conData;
			maKH = MaKH;
			item = connectData.getInfoCustomer(maKH);

			lblMaKH.Content = item.MaKH;
			lblTenKH.Content = item.TenKH;
			lblSDT.Content = item.SDT;
			lblCMND.Content = item.CMND;
			lblDiaChi.Content = item.DiaChi;
			lblLK.Content = item.LoaiKhach;
			lblPhong.Content = item.PhongDangO;

			txtDiaChi.Visibility = Visibility.Hidden;
			txtSDT.Visibility = Visibility.Hidden;
			cbbLK.Visibility = Visibility.Hidden;
			borderSDT.Visibility = Visibility.Hidden;
			borderDiaChi.Visibility = Visibility.Hidden;
			borderLK.Visibility = Visibility.Hidden;

			listTypeOfCustom = connectData.getTypeOfCustommer();

			for (int i = 0; i < listTypeOfCustom.Count; i++)
			{
				listNameTypeOfCustom.Add(listTypeOfCustom[i].LoaiKhach);

				if (listTypeOfCustom[i].LoaiKhach.Trim() == item.LoaiKhach.Trim())
				{
					typeOfCustom = i;
				}
			}

		}

		private void btnEdit_Click(object sender, RoutedEventArgs e)
		{
			if (isEditing == false)
			{
				isEditing = true;

				txtDiaChi.Visibility = Visibility.Visible;
				txtDiaChi.Text = item.DiaChi.Trim();
				txtSDT.Visibility = Visibility.Visible;
				txtSDT.Text = item.SDT.Trim();
				cbbLK.Visibility = Visibility.Visible;
				borderSDT.Visibility = Visibility.Visible;
				borderDiaChi.Visibility = Visibility.Visible;
				borderLK.Visibility = Visibility.Visible;

				btnEdit.Content = "Lưu Thay Đổi";
				btnDelete.Content = "Hủy Thay Đổi";

			}
			else
			{

				if (txtDiaChi.Text.Trim() == "" || txtSDT.Text.Trim() == "")
				{
					MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!!", "Thiếu Dữ Liệu!!!", MessageBoxButton.OK);
					return;
				}
				for (int i = 0; i < txtSDT.Text.Length; i++)
				{
					if (txtSDT.Text[i] < 48 || txtSDT.Text[i] > 57)
					{
						MessageBox.Show("Định dạng số điện thoại không đúng!!!\n\nVui lòng kiểm tra lại trước khi lưu!!!", "Sai Định Dạng!!!", MessageBoxButton.OK);
						return;
					}
				}

				string loaiKhach = cbbLK.SelectedItem.ToString();

				int j = 0;
				while (listTypeOfCustom[j].LoaiKhach.Trim() != loaiKhach)
				{
					j++;
				}

				connectData.updateCustomer(item.MaKH, txtSDT.Text, txtDiaChi.Text, listTypeOfCustom[j].MaLK);

				item = connectData.getInfoCustomer(item.MaKH);

				lblMaKH.Content = item.MaKH;
				lblTenKH.Content = item.TenKH;
				lblSDT.Content = item.SDT;
				lblCMND.Content = item.CMND;
				lblDiaChi.Content = item.DiaChi;
				lblLK.Content = item.LoaiKhach;
				lblPhong.Content = item.PhongDangO;

				txtDiaChi.Visibility = Visibility.Hidden;
				txtSDT.Visibility = Visibility.Hidden;
				cbbLK.Visibility = Visibility.Hidden;
				borderSDT.Visibility = Visibility.Hidden;
				borderDiaChi.Visibility = Visibility.Hidden;
				borderLK.Visibility = Visibility.Hidden;

				isEditing = false;

				btnDelete.Content = "Xóa Nhân Viên";
				btnEdit.Content = "Chỉnh Sửa";
			}

		}

		private void btnDelete_Click(object sender, RoutedEventArgs e)
		{

			if (isEditing == false)
			{
				if (MessageBox.Show("Bạn có thực sự muốn xóa khách hàng này???", "Xác Nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{
					List<string> temp = new List<string>();
					temp.Add(item.MaKH);
					if (connectData.deleteCustomer(temp))
					{

						UserControl usc = null;
						usc = new ListCustomer(connectData);
						Global.mainNavigate.Children.Add(usc);



					}
				}
			}
			else
			{
				isEditing = false;

				txtDiaChi.Visibility = Visibility.Hidden;
				txtSDT.Visibility = Visibility.Hidden;
				cbbLK.Visibility = Visibility.Hidden;
				borderSDT.Visibility = Visibility.Hidden;
				borderDiaChi.Visibility = Visibility.Hidden;
				borderLK.Visibility = Visibility.Hidden;

				btnDelete.Content = "Xóa KH";
				btnEdit.Content = "Chỉnh Sửa";
			}
		}

		private void cbbLK_Loaded(object sender, RoutedEventArgs e)
		{
			var combo = sender as ComboBox;
			combo.ItemsSource = listNameTypeOfCustom;
			combo.SelectedIndex = typeOfCustom;
		}

		private void click_btnBack(object sender, RoutedEventArgs e)
		{
			if (isEditing)
			{
				MessageBox.Show("Chưa lưu thông tin đang chỉnh sửa!!!\n\nVui lòng lưu trước khi rời khỏi trang!!!", "Chưa Lưu Thay Đổi!!!", MessageBoxButton.OK);
				return;
			}

			else
			{

				UserControl usc = null;
				usc = new ListCustomer(connectData);
				Global.mainNavigate.Children.Add(usc);
			}
		}
	}
}
