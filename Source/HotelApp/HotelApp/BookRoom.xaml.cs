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
using System.Globalization;
using System.Threading;

namespace HotelApp
{
	/// <summary>
	/// Interaction logic for BookRoom.xaml
	/// </summary>
	public partial class BookRoom : UserControl
	{
		private List<string> listTypeCustomer = new List<string>();
		private List<Customer> listCustomer = new List<Customer>(); //add vào data báase
		private List<ListViewDataCustommer> listTypeOfCustomer = new List<ListViewDataCustommer>(); //show ra để chọn loại kahchs
		private bool editAction = false;
		private ConnectData connectData;
		private string valueCus;
		private string valuedayStart;
		private string valuedayEnd = "";
		private string maPhong = "P1";
		private int vitriChon = 0;
		private ListBookRoom room;

		public BookRoom(ConnectData conData, string MP)
		{
			InitializeComponent();
			connectData = conData;
			maPhong = MP; //mã phognf

			listTypeOfCustomer = conData.getTypeOfCustommer();

			foreach (ListViewDataCustommer item in listTypeOfCustomer)
			{
				listTypeCustomer.Add(item.LoaiKhach);
			}


			if (Global.valuedayStart != "")
			{
				room = Global.room;
				dpkDayBegin.Text = Global.valuedayStart;
				dpkDayEnd.Text = Global.valuedayEnd;
				listCustomer = Global.listCustomer;
			}
			else
			{
				room = connectData.getBookRoom(maPhong);
			}


			txtLoaiPhong.Text = room.TenLP;
			txtPhong.Text = room.MaPhong;
			txtDongia.Text = room.DonGia.ToString();
			lvCustommer.ItemsSource = listCustomer;

			CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name); ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
			Thread.CurrentThread.CurrentCulture = ci;

		}


		private void SelectedDateChangedBegin(object sender, SelectionChangedEventArgs e)
		{
			var selectedDate = sender as DatePicker;
			valuedayStart = selectedDate.SelectedDate.Value.Date.ToShortDateString();
		}

		private void SelectedDateChangedEnd(object sender, SelectionChangedEventArgs e)
		{
			var selectedDate = sender as DatePicker;
			valuedayEnd = selectedDate.SelectedDate.Value.Date.ToShortDateString();

		}

		private void Combobox_LoadedLK(object sender, RoutedEventArgs e)
		{
			var combo = sender as ComboBox;
			combo.ItemsSource = listTypeCustomer;
			combo.SelectedIndex = 0;
		}

		private void Combobox_SelectionChangedLK(object sender, SelectionChangedEventArgs e)
		{
			var selectedComboItem = sender as ComboBox;
			valueCus = selectedComboItem.SelectedItem as string;
		}

		private void XoaHang(object sender, RoutedEventArgs e)
		{
			int selectedIndex = lvCustommer.SelectedIndex;
			MessageBoxResult result = MessageBox.Show("Bạn muốn xóa hàng " + (selectedIndex + 1).ToString() + "?", "Cảnh báo!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				List<Customer> tempArr = new List<Customer>();
				int j = 1;

				for (int i = 0; i < listCustomer.Count; i++)
				{
					if (i == selectedIndex) continue;
					Customer temp = new Customer();
					temp = listCustomer[i];
					temp.STT = j;
					tempArr.Add(temp);
					j++;
				}
				lvCustommer.ItemsSource = tempArr;
				listCustomer = tempArr;
			}
		}

		private bool kiemtraCMND(String str)
		{

			for (var i = 0; i < str.Length; i++)
			{
				if (str[i] > '9' || str[i] < '0')
				{
					return false;
				}
			}

			if (str.Length == 9 || str.Length == 12)
			{
				return true;
			}
			return false;
		}

		private bool kiemtraSDT(String str)
		{

			for (var i = 0; i < str.Length; i++)
			{
				if (str[i] > '9' || str[i] < '0')
				{
					return false;
				}
			}

			if (str.Length >= 10 || str.Length == 0)
			{
				return true;
			}
			return false;
		}

		private void ThemHang(object sender, RoutedEventArgs e)
		{
			if (!kiemtraCMND(txtCMND.Text) || txtTenKhach.Text.Length < 1 || txtDiaChi.Text.Length < 1 || !kiemtraSDT(txtSDT.Text))
			{
				MessageBox.Show("Bạn chưa nhập thông tin khách hàng hoặc CMND sai hoặc chưa đủ số hoặc trùng (CMND chỉ bao gồm số từ 0->9).", "Cảnh báo!!!", MessageBoxButton.OK);
				return;
			}
			for (int i = 0; i < listCustomer.Count; i++)
			{
				if (txtCMND.Text == listCustomer[i].CMND)
				{
					MessageBox.Show("Trùng số CMND!!!");
					return;
				}
			}
			MessageBoxResult result = MessageBox.Show("Bạn muốn thêm khách hàng?", "Xác nhận!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				if (listCustomer.Count == room.SoKhachToiDa)
				{
					MessageBox.Show("Vượt quá số khách tối đa cho phòng " + room.SoKhachToiDa + " người.");
					return;
				}
				List<Customer> temp = new List<Customer>();
				for (int i = 0; i < listCustomer.Count; i++)
				{
					temp.Add(listCustomer[i]);
				}
				temp.Add(new Customer() { STT = listCustomer.Count + 1, TenKH = txtTenKhach.Text, TenLK = valueCus, MaLK = listTypeOfCustomer[cbbLoaiKhach.SelectedIndex].MaLK, CMND = txtCMND.Text, DiaChi = txtDiaChi.Text, SDT = txtSDT.Text });

				lvCustommer.ItemsSource = temp;
				listCustomer = temp;

				txtTenKhach.Text = "";
				txtCMND.Text = "";
				txtSDT.Text = "";
				txtDiaChi.Text = "";
				cbbLoaiKhach.SelectedIndex = 0;
			}

		}

		int numSuportEdit = 0;
		private void ChinhSuaHang(object sender, RoutedEventArgs e)
		{
			editAction = true;
			if (!kiemtraCMND(txtCMND.Text) || txtTenKhach.Text.Length < 1 || txtDiaChi.Text.Length < 1 || !kiemtraSDT(txtSDT.Text))
			{
				if (numSuportEdit == 1)
				{
					MessageBox.Show("Bạn thông tin chỉnh sửa thiếu hoặc CMND sai hoặc trùng (CMND bao gồm số từ 0->9 và đủ 9 hoặc 12 số.", "Cảnh báo!!!", MessageBoxButton.OK);
				}
				return;
			}
			MessageBoxResult result = MessageBox.Show("Bạn muốn chỉnh sửa khách hàng?", "Xác nhận!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				List<Customer> tempArr = new List<Customer>();

				for (int i = 1; i <= listCustomer.Count; i++)
				{
					if (i == vitriChon)
					{
						tempArr.Add(new Customer() { STT = i, TenKH = txtTenKhach.Text, TenLK = valueCus, CMND = txtCMND.Text, DiaChi = txtDiaChi.Text, MaLK = listTypeOfCustomer[i - 1].MaLK, SDT = txtSDT.Text });
						continue;
					}

					if (txtCMND.Text == listCustomer[i - 1].CMND)
					{
						MessageBox.Show("Trùng số CMND!!!");
						return;
					}

					tempArr.Add(listCustomer[i - 1]);
				}
				lvCustommer.ItemsSource = tempArr;

				listCustomer = tempArr;

				txtTenKhach.Text = "";
				txtCMND.Text = "";
				txtDiaChi.Text = "";
				txtSDT.Text = "";
				cbbLoaiKhach.SelectedIndex = 0;
			}
		}

		private bool KTcountSDT()
		{
			for (int i = 0; i < listCustomer.Count; i++)
			{
				if (listCustomer[i].SDT.Length > 0)
				{
					return true;
				}
			}
			return false;
		}


		private void Book_Click(object sender, RoutedEventArgs e)
		{
			if (dpkDayEnd.Text.Length > 7 && dpkDayEnd.SelectedDate.Value.Date <= dpkDayBegin.SelectedDate.Value.Date)
			{
				MessageBox.Show("Vui lòng chọn ngày trả phòng hơn ngày thuê.");
				return;
			}
			if (listCustomer.Count == 0)
			{
				MessageBox.Show("Vui lòng nhập thông tin khách hàng");
				return;
			}
			if (!KTcountSDT())
			{
				MessageBox.Show("Vui lòng nhập Số điện thoại( ít nhất phải có 1 SDT của khách hàng).");
				return;
			}

			valuedayStart = dpkDayBegin.Text;


			Global.listCustomer = listCustomer;
			Global.room = room;
			Global.valuedayStart = valuedayStart;
			Global.valuedayEnd = valuedayEnd;
			Global.note = txtGhichu.Text;

			UserControl usc = null;
			if (dpkDayEnd.Text.Length > 7)          //đủ thông tin qua thanh toán
			{
				Global.songay = (int)((dpkDayEnd.SelectedDate.Value.Date - dpkDayBegin.SelectedDate.Value.Date).TotalDays + 1);
				
				usc = new PayScreen(connectData, "");
				Global.mainNavigate.Children.Add(usc);
				return;
			}

			connectData.setBookRoom(listCustomer, room, formatDate(valuedayStart), valuedayEnd, txtGhichu.Text, 0, -1, true);
			usc = new ListRoom(connectData);
			Global.mainNavigate.Children.Add(usc);
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

		private void Cancel(object sender, RoutedEventArgs e)
		{
			UserControl usc = null;
			usc = new RoomChecking(connectData, maPhong);
			Global.mainNavigate.Children.Add(usc);
		}

		private bool KTNgayTraPhong()
		{
			return true;
		}

		private int getIndexTypeOfCustomer(string LK)
		{
			for (int i = 0; i < listTypeCustomer.Count; i++)
			{
				if (LK == listTypeCustomer[i])
				{
					return i;
				}
			}
			return -1;
		}

		private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			Customer lvc = (Customer)lvCustommer.SelectedItem;
			if (lvc != null)
			{
				if (editAction)
				{
					//show to edit
					numSuportEdit = 1;
					vitriChon = lvc.STT;
					txtTenKhach.Text = lvc.TenKH;
					txtCMND.Text = lvc.CMND;
					cbbLoaiKhach.SelectedIndex = getIndexTypeOfCustomer(lvc.TenLK);
					txtDiaChi.Text = lvc.DiaChi;
				}

			}
		}
	}
}
