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

       

        public string MaPhong { get; internal set; }

        private void ListviewDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = sender as ListViewItem;

          
            var newWin = new MainScreen();
            newWin.Show();

            UserControl usc = null;
            usc = new RoomChecking(connectData, listRoom[LV_ListRoom.SelectedIndex].MaPhong);
            newWin.GridMain.Children.Add(usc);

            Window.GetWindow(this).Close();
        }

        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    items = connectData.findRoom(txtSearch.Text);
        //    LV_ListRoom.ItemsSource = items;
        //}



        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            bool check = true;
            items = connectData.getCheckedListRoom(check);
            LV_ListRoom.ItemsSource = items;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            items = connectData.getListRoom();
            LV_ListRoom.ItemsSource = items;
        }

        private void BtnSearchLP_Click(object sender, RoutedEventArgs e)
        {
            items = connectData.findRoom(txtSearch.Text);
            LV_ListRoom.ItemsSource = items;
        }

        private void BtnSearchTP_Click(object sender, RoutedEventArgs e)
        {
            items = connectData.findRoomName(txtSearch.Text);
            LV_ListRoom.ItemsSource = items;
        }

        private void ListviewClick(object sender, MouseButtonEventArgs e)
        {
            listDelete = new List<string>();

            var selectedItem = sender as ListViewItem;
            Room dataItem = (Room)LV_ListRoom.ItemContainerGenerator.ItemFromContainer(selectedItem);

            listDelete.Add(dataItem.MaPhong);
            return;
        }

        private void click_btnDelete(object sender, RoutedEventArgs e)
        {

            if (listDelete.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn phòng trước khi xóa!!!", "Chưa Chọn Đối Tượng!!!", MessageBoxButton.OK);
                return;
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa phòng này???", "Xác Nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                connectData.deleteRoom(listDelete);
                items = connectData.getListRoom();
                LV_ListRoom.ItemsSource = items;
            }
           
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(txtSearch.Text == "")
            {
                listRoom = connectData.getListRoom();
                this.LV_ListRoom.ItemsSource = listRoom;
            }
               
        }
    }
}
