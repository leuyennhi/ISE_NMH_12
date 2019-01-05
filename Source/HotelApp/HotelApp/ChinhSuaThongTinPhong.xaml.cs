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
    /// Interaction logic for ChinhSuaThongTinPhong.xaml
    /// </summary>
    public partial class ChinhSuaThongTinPhong : UserControl
    {

        private ConnectData connectData;
        DetailOfRoom infoRoom = new DetailOfRoom();
        List<ListViewDataRoom> infoTypeOfRoom = new List<ListViewDataRoom>();
        List<string> listTypeOfRoom = new List<string>();
        int typeOfRoom = 0;
        string maP;

        public ChinhSuaThongTinPhong(ConnectData conData, string maPhong)
        {
            InitializeComponent();
            connectData = conData;
            maP = maPhong;
            infoRoom = connectData.GetDetailOfRoom(maPhong);
            infoTypeOfRoom = connectData.getTypeOfRoom();

            for (int i = 0; i < infoTypeOfRoom.Count; i++)
            {
                listTypeOfRoom.Add(infoTypeOfRoom[i].LoaiPhong);

                if (infoTypeOfRoom[i].LoaiPhong.Trim() == infoRoom.TenLP.Trim())
                {
                    typeOfRoom = i;
                }
            }

            lblMP.Content = maPhong;
            lbldonGia.Content = infoRoom.DonGia;
            lblsoKhachToiDa.Content = infoRoom.SoKhachToiDa;

            if (infoRoom.GhiChu != null)
            {
                txtghiChu.Text = infoRoom.GhiChu.Trim();
            }
        }

        private void click_btnSave(object sender, RoutedEventArgs e)
        {
            string loaiPhong = cbbLP.SelectedItem.ToString();

            int i = 0;
            while (infoTypeOfRoom[i].LoaiPhong.Trim() != loaiPhong)
            {
                i++;
            }

            connectData.updateInfoRoom(maP, infoTypeOfRoom[i].MaLP, txtghiChu.Text);

            var newWin = new MainScreen();
            newWin.Show();

            UserControl usc = null;
            usc = new RoomChecking(connectData, maP);
            newWin.GridMain.Children.Add(usc);

            Window.GetWindow(this).Close();


        }

        private void click_btnCancel(object sender, RoutedEventArgs e)
        {
            var newWin = new MainScreen();
            newWin.Show();

            UserControl usc = null;
            usc = new RoomChecking(connectData, maP);
            newWin.GridMain.Children.Add(usc);

            Window.GetWindow(this).Close();

        }

        private void cbbLP_Loaded(object sender, RoutedEventArgs e)
        {

            var combo = sender as ComboBox;
            combo.ItemsSource = listTypeOfRoom;
            combo.SelectedIndex = typeOfRoom;
        }

        private void cbbLP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedComboItem = sender as ComboBox;
            typeOfRoom = selectedComboItem.SelectedIndex;

            lbldonGia.Content = infoTypeOfRoom[typeOfRoom].Dongia;
            lblsoKhachToiDa.Content = infoTypeOfRoom[typeOfRoom].SoKhachToiDa;

        }
    }
}
