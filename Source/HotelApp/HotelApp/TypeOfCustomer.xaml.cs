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
    /// Interaction logic for TypeOfCustomer.xaml
    /// </summary>
    public partial class TypeOfCustomer : UserControl
    {
		private List<ListViewDataCustommer> items = new List<ListViewDataCustommer>();
		private bool editAction = false;
		private int Stttext = 0;
		private ConnectData connectData;
		public TypeOfCustomer(ConnectData conData)
        {
            InitializeComponent();
			connectData = conData;
			items = conData.getTypeOfCustommer();
			lvTypeCustommer.ItemsSource = items;

		}

		public void RemoveTOCText(object sender, EventArgs e)
		{
			//TOCText.Text = "";
		}

		public void AddTOCText(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(TOCText.Text)) { }
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
			
			int selectedIndex = lvTypeCustommer.SelectedIndex;
			MessageBoxResult result = MessageBox.Show("Bạn muốn xóa hàng " + (selectedIndex + 1).ToString() + "?", "Cảnh báo!!!", MessageBoxButton.YesNo);
	
			if (result == MessageBoxResult.Yes)
			{
				List<ListViewDataCustommer> tempArr = new List<ListViewDataCustommer>();
				int j = 1;

				for (int i = 0; i < items.Count; i++)
				{
					if (i == selectedIndex) continue;
					ListViewDataCustommer temp = new ListViewDataCustommer();
					temp = items[i];
					temp.STT = j;
					tempArr.Add(temp);
					j++;
				}
				lvTypeCustommer.ItemsSource = tempArr;
				items = tempArr;

				connectData.deleteTypeOfCustomer(selectedIndex + 1);
			}
		}

		private void ThemHang(object sender, RoutedEventArgs e)
		{

			if (!kiemtraHeSo(CoeffText.Text) || TOCText.Text.Length < 1)
			{
				MessageBox.Show("Bạn chưa nhập thông tin hoặc hệ số sai (hệ số bao gồm số từ 0->9 hoặc thêm dấu chấm nếu là số thực.", "Cảnh báo!!!", MessageBoxButton.OK);
				return;
			}
			MessageBoxResult result = MessageBox.Show("Bạn muốn thêm khách hàng?", "Xác nhận!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				List<ListViewDataCustommer> tempArr = new List<ListViewDataCustommer>();

				int i;

				for (i = 0; i < items.Count; i++)
				{
					tempArr.Add(items[i]);
				}

				tempArr.Add(new ListViewDataCustommer() { STT = i + 1, LoaiKhach = TOCText.Text, HeSo = float.Parse(CoeffText.Text) });

				lvTypeCustommer.ItemsSource = tempArr;
				items = tempArr;

				TOCText.Text = "";
				CoeffText.Text = "";

				connectData.setTypeOfCustomer(items[i]);
				
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
			if (!kiemtraHeSo(CoeffText.Text) || TOCText.Text.Length < 1)
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
				List<ListViewDataCustommer> tempArr = new List<ListViewDataCustommer>();

				for (int i = 1; i <= items.Count; i++)
				{
					if (i == Stttext)
					{
						tempArr.Add(new ListViewDataCustommer() { STT = i, LoaiKhach = TOCText.Text, HeSo = float.Parse(CoeffText.Text) });
						connectData.updateTypeOfCustomer(tempArr[i - 1]);
						continue;
					}
					tempArr.Add(items[i - 1]);
				}
				lvTypeCustommer.ItemsSource = tempArr;

				items = tempArr;

				TOCText.Text = "";
				CoeffText.Text = "";
			}
		}

		private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListViewDataCustommer lvc = (ListViewDataCustommer)lvTypeCustommer.SelectedItem;
			if (lvc != null)
			{
				if (editAction)
				{
					numSuportEdit = 1;
					Stttext = lvc.STT;
					TOCText.Text = lvc.LoaiKhach.ToString();
					CoeffText.Text = lvc.HeSo.ToString();
				}

			}
		}

		private void DSPhong(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Danh sách khách");
		}

		private void LoaiPhong(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("loại khách");
		}

	}
}
