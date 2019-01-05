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
    /// Interaction logic for DanhSachNhanVien.xaml
    /// </summary>
    public partial class DanhSachNhanVien : UserControl
    {
        List<ListStaff> items = new List<ListStaff>();
        private ConnectData connectData;
        List<string> listDelete = new List<string>();
        //bool isDeleting = false;
        public DanhSachNhanVien(ConnectData conData)
        {
            InitializeComponent();
            connectData = conData;
            items = connectData.getListStaff();
            if (items.Count() == 0)
            {
                MessageBox.Show("Hiện chưa có dữ liệu về nhân viên" + "\n\nVui lòng thử lại sau!!!", "Chưa Có Dữ Liệu!!!", MessageBoxButton.OK);
            }
            lvListStaff.ItemsSource = items;
        }

        private void ListviewDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var selectedItem = sender as ListViewItem;

            ListStaff dataItem = (ListStaff)lvListStaff.ItemContainerGenerator.ItemFromContainer(selectedItem);

			UserControl usc = null;
			usc = new ThongTinTaiKhoan(connectData, dataItem.MaNV);
			Global.mainNavigate.Children.Add(usc);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            items = connectData.findStaff(txtSearch.Text);
            lvListStaff.ItemsSource = items;
        }

        private void ListviewClick(object sender, MouseButtonEventArgs e)
        {
            listDelete = new List<string>();

            var selectedItem = sender as ListViewItem;
            ListStaff dataItem = (ListStaff)lvListStaff.ItemContainerGenerator.ItemFromContainer(selectedItem);

            listDelete.Add(dataItem.MaNV);
        }

        private void click_btnDelete(object sender, RoutedEventArgs e)
        {

            if (listDelete.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên trước khi xóa!!!", "Chưa Chọn Đối Tượng!!!", MessageBoxButton.OK);
                return;
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa nhân viên này???", "Xác Nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                connectData.deleteStaffs(listDelete);
                items = connectData.getListStaff();
                lvListStaff.ItemsSource = items;
            }
        }	
	}

}
