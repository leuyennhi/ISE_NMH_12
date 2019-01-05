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
    /// Interaction logic for RoomChecking.xaml
    /// </summary>
    public partial class RoomChecking : UserControl
    {
        DetailOfRoom item = new DetailOfRoom();
        private ConnectData connectData;
        bool isEditing = false;
        public RoomChecking(ConnectData conData, string maLP)
        {
            InitializeComponent();
            connectData = conData;
            item = connectData.GetDetailOfRoom(maLP);

            lblPhong.Content = item.MaPhong;
            lblLoaiPhong.Content = item.MaLP;
            lblDonGia.Content = item.DonGia;
            //lblTinhTrang.Content = item.;
            //lblGhiChu.Content = item.GhiChu;
        }
    }
}
