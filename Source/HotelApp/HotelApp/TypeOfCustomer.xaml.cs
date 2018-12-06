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
    /// Interaction logic for TypeOfCustomer.xaml
    /// </summary>
    public partial class TypeOfCustomer : Window
    {
		private List<ListViewDataCustommer> items = new List<ListViewDataCustommer>();
		private bool editAction = false;
		private int Stttext = 0;

		public TypeOfCustomer()
        {
            InitializeComponent();

			//string indexPath = "pack://siteoforigin:,,,/Resources/add.png";
			//BitmapImage image = new BitmapImage();
			//image.BeginInit();
			//image.UriSource = new Uri(indexPath);
			//image.EndInit();

			items.Add(new ListViewDataCustommer() { STT = 1, LoaiKhach = "Khach trong nuoc", HeSo = (float)1.0 });
			items.Add(new ListViewDataCustommer() { STT = 2, LoaiKhach = "Khach nuoc ngoai", HeSo = (float)1.5 });
			items.Add(new ListViewDataCustommer() { STT = 3, LoaiKhach = "Khach vip", HeSo = (float)0.9 });
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
		}

		private void ThemHang(object sender, RoutedEventArgs e)
		{

			if (!kiemtraHeSo(CoeffText.Text) || TOCText.Text.Length < 1)
			{
				return;
			}

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

			if (!kiemtraHeSo(CoeffText.Text) || TOCText.Text.Length < 1)
			{
				return;
			}

			List<ListViewDataCustommer> tempArr = new List<ListViewDataCustommer>();

			for (int i = 1; i <= items.Count; i++)
			{ 
				if (i == Stttext)
				{
					tempArr.Add(new ListViewDataCustommer() { STT = i, LoaiKhach = TOCText.Text, HeSo = float.Parse(CoeffText.Text) });
					continue;
				}
				tempArr.Add(items[i-1]);
			}
			lvTypeCustommer.ItemsSource = tempArr;

			items = tempArr;
			
			TOCText.Text = "";
			CoeffText.Text = "";
		}

		private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListViewDataCustommer lvc = (ListViewDataCustommer)lvTypeCustommer.SelectedItem;
			if (lvc != null)
			{
				if (editAction)
				{
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

	public class ListViewDataCustommer
	{
		public int STT { get; set; }

		public string LoaiKhach { get; set; }

		public float HeSo { get; set; }
	}

}
