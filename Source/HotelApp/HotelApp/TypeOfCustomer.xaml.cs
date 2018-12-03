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

		public TypeOfCustomer()
        {
            InitializeComponent();

			string indexPath = "pack://siteoforigin:,,,/Resources/add.png";
			BitmapImage image = new BitmapImage();
			image.BeginInit();
			image.UriSource = new Uri(indexPath);
			image.EndInit();

			items.Add(new ListViewDataCustommer() { STT = 1, LoaiKhach = "Khach trong nuoc", HeSo = (float)1.0 });
			items.Add(new ListViewDataCustommer() { STT = 2, LoaiKhach = "Khach nuoc ngoai", HeSo = (float)1.5 });
			items.Add(new ListViewDataCustommer() { STT = 3, LoaiKhach = "Khach vip", HeSo = (float)0.9 });
			lvTypeCustommer.ItemsSource = items;

		}

		private void XoaHang(object sender, RoutedEventArgs e)
		{
			int selectedIndex = lvTypeCustommer.SelectedIndex;

			List<ListViewDataCustommer> tempArr = new List<ListViewDataCustommer>();
			int j = 1;

			for ( int i=0;i<items.Count; i++)
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
			
		}

		private void ChinhSuaHang(object sender, RoutedEventArgs e)
		{

		}

		private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListViewDataCustommer lvc = (ListViewDataCustommer)lvTypeCustommer.SelectedItem;
			if (lvc != null)
			{
				
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
		
	}

	public class ListViewDataCustommer
	{
		public ListViewDataCustommer()
		{
			// default constructor
		}

		public ListViewDataCustommer(int stt, string loaiKhach, float heSo)
		{
			STT = stt;
			LoaiKhach = loaiKhach;
			HeSo = heSo;
		}

		public int STT { get; set; }

		public string LoaiKhach { get; set; }

		public float HeSo { get; set; }
	}

}
