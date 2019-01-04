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
    /// Interaction logic for ListRoom.xaml
    /// </summary>
    public partial class ListRoom : UserControl
    {
        private List<Room> listRoom;
        public ListRoom(ConnectData conData)
        {
            InitializeComponent();
            listRoom = conData.getListRoom();
            this.LV_ListRoom.ItemsSource = listRoom;
        }

        private void ListviewClick (object sender, MouseButtonEventArgs e)
        {
            //this.Hide();
            //MainWindow a = new MainWindow();
            //a.Show();
            var item = sender as ListViewItem;
            var index = LV_ListRoom.ItemContainerGenerator.IndexFromContainer(item);
            MessageBox.Show( index + "");
        }
    }
}
