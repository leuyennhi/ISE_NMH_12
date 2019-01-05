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
        string maPhong;
        bool isEditing = false;
        public RoomChecking(ConnectData conData, string maP)
        {
            InitializeComponent();
            connectData = conData;
            maPhong = maP;
            item = connectData.GetDetailOfRoom(maP);

            lblPhong.Content = item.MaPhong;
            lblLoaiPhong.Content = item.TenLP;
            lblDonGia.Content = item.DonGia + " VNĐ/đêm";
            lblTinhTrang.Content = item.TinhTrang.ToString();
            lblGhiChu.Content = item.GhiChu;
        }

        private void BtnEditRoom_Click(object sender, RoutedEventArgs e)
        {
            ChinhSuaThongTinPhong edit = new ChinhSuaThongTinPhong(connectData,maPhong);
            //edit.Show();
            
        }
    }
}
