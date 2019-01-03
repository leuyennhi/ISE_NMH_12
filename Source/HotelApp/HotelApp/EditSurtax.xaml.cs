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
		private List<ListViewSurtax> listSurtax;
		ConnectData connectData;
		public EditSurtax(ConnectData conData)
		{
			InitializeComponent();

			connectData = conData;
			listSurtax = conData.getSurtax();

			if (checkSurtax(listSurtax))
			{
				txtPhuThu.Text = listSurtax[0].TiLePhuThu.ToString();
			}
			else
			{
				txtPhuThu.Text = "";
			}

			listTypeRoom.Add("All");
			for (int i = 0; i < listSurtax.Count; i++)
			{
				listTypeRoom.Add(listSurtax[i].MaLP);
			}

		}

		private bool checkSurtax(List<ListViewSurtax> temp1)
		{
			for (int i = 1; i < temp1.Count; i++)
			{
				if (temp1[i].TiLePhuThu != temp1[i - 1].TiLePhuThu)
				{
					return false;
				}
			}
			return true;
		}

		private int findIndexSurtax(List<ListViewSurtax> temp1, string type)
		{
			int i;
			for (i = 0; i < temp1.Count; i++)
			{
				if (temp1[i].MaLP == type)
				{
					return i;
				}
			}
			return -1;
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
			if (typeRoom != "All")
			{
				txtPhuThu.Text = listSurtax[findIndexSurtax(listSurtax, typeRoom)].TiLePhuThu.ToString();
			}
			else
			{
				txtPhuThu.Text = "";
			}
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

		private void insertSurtax()
		{
			int i = findIndexSurtax(listSurtax, typeRoom);
			float phuthu = listSurtax[i].TiLePhuThu;
			if (txtPhuThu.Text != phuthu.ToString())
			{
				connectData.setSurtax(typeRoom, (float)Convert.ToDouble(txtPhuThu.Text));
			}
		}

		private void AddSurTax(object sender, RoutedEventArgs e)
		{
			if (!kiemtraPhuThu(txtPhuThu.Text))
			{
				MessageBox.Show("Bạn chưa nhập phụ thu hoặc nhập sai (phụ thu bao gồm số từ 0->9 hoặc có dấu chấm nếu là số thực)", "Cảnh báo!!!", MessageBoxButton.OK);
				return;
			}
			MessageBoxResult result = MessageBox.Show("Bạn muốn chỉnh sửa phụ thu?", "Xác nhận!!!", MessageBoxButton.YesNo);

			if (result == MessageBoxResult.Yes)
			{
				if (typeRoom == "All")
				{
					//caapj nhat bang phu thu
					connectData.setSurtax("All", (float)Convert.ToDouble(txtPhuThu.Text));

				}
				else
				{
					//kieemr tra vaf chefn them hangf phu thu moi vaf update laij hangf loaij phongf thay doi
					insertSurtax();
				}
			}
		}
	}
}
