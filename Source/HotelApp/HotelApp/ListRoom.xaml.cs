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
        List<Room> items = new List<Room>();
        private ConnectData connectData;
        List<string> listDelete = new List<string>();

        private List<Room> listRoom;
        public ListRoom(ConnectData conData)
        {
            InitializeComponent();
            connectData = conData;
            listRoom = conData.getListRoom();
            this.LV_ListRoom.ItemsSource = listRoom;
        }

        private void ListviewDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = sender as ListViewItem;

            Room dataItem = (Room)LV_ListRoom.ItemContainerGenerator.ItemFromContainer(selectedItem);

            var newWin = new MainScreen();
            newWin.Show();

            UserControl usc = null;
            usc = new RoomChecking(connectData, dataItem.MaLP);
            newWin.GridMain.Children.Add(usc);

            Window.GetWindow(this).Close();
        }
    }
}
