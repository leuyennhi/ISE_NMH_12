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
			ListStaff currentUser;
			currentUser = connectData.getCurrentUser();
			if (currentUser.ChucVu == "Quản lý")
            {
                UserControl usc = null;
                usc = new ChinhSuaThongTinPhong(connectData, maPhong);
                Global.mainNavigate.Children.Add(usc);
            } else
            {
                MessageBox.Show("Bạn không có quyền Chỉnh sửa phòng");
            }
        }

        private void click_btnBack(object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                MessageBox.Show("Chưa lưu thông tin đang chỉnh sửa!!!\n\nVui lòng lưu trước khi rời khỏi trang!!!", "Chưa Lưu Thay Đổi!!!", MessageBoxButton.OK);
                return;
            }

            else
            {
                UserControl usc = null;
                usc = new ListRoom(connectData);
                Global.mainNavigate.Children.Add(usc);
            }
        }

        private void BtnDat_Click(object sender, RoutedEventArgs e)
        {
			UserControl usc = null;
			if (lblTinhTrang.Content.ToString() == "Hết phòng")
			{
				usc = new PayScreen(connectData, maPhong, false);
				Global.mainNavigate.Children.Add(usc);
				return;
			}
			
            usc = new BookRoom(connectData, maPhong);
            Global.mainNavigate.Children.Add(usc);
        }

       

        private void BtnDeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if (isEditing == false)
            {
                if (MessageBox.Show("Bạn có thực sự muốn xóa phòng này???", "Xác Nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<string> temp = new List<string>();
                    temp.Add(item.MaPhong);
                    if (connectData.deleteRoom(temp))
                    {

                        UserControl usc = null;
                        usc = new ListRoom(connectData);
                        Global.mainNavigate.Children.Add(usc);

                    }
                }
            }
        }
    }
}
