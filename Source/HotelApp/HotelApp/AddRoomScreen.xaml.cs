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

namespace HotelApp
{
	
	public partial class AddRoomScreen : Window
	{
		private List<string> listTypeRoom = new List<string>();

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
			string type = selectedComboItem.SelectedItem as string;
		//	MessageBox.Show(type);
		}
	}

}
