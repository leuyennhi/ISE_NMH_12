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

    public partial class ListCustomer : UserControl
    {
        private List<Customer> listCustomers;
        private ConnectData connectData;
        List<string> listDelete = new List<string>();

        public ListCustomer(ConnectData conData)
        {
            InitializeComponent();
            connectData = conData;
            listCustomers = conData.getListCustomer();
            this.lvListCustomer.ItemsSource = listCustomers;
        }

        private void click_btnDelete(object sender, RoutedEventArgs e)
        {
            if (listDelete.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khách hàng trước khi xóa!!!", "Chưa Chọn Đối Tượng!!!", MessageBoxButton.OK);
                return;
            }

            if (MessageBox.Show("Bạn có thực sự muốn xóa khách hàng này???", "Xác Nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                connectData.deleteCustomer(listDelete);
                listCustomers = connectData.getListCustomer();
                lvListCustomer.ItemsSource = listCustomers;
            }
        }

        private void ListviewDoubleClick(object sender, MouseButtonEventArgs e)
        {

            var selectedItem = sender as ListViewItem;

            Customer dataItem = (Customer)lvListCustomer.ItemContainerGenerator.ItemFromContainer(selectedItem);

			//UserControl usc = null;
			//usc = new InfoCustomer(connectData, dataItem.MaNV);
			//Global.mainNavigate.Children.Add(usc);
			/*
             
            //Viết cái dòng chuyển màn hình vô nhaaaaaaaaaaaaaaaaaaaaa
           
            var newWin = new MainScreen();
            newWin.Show();

           
            UserControl usc = null;
            usc = new InfoCustomer(connectData, dataItem.MaNV);
            newWin.GridMain.Children.Add(usc);

            Window.GetWindow(this).Close();
            */
		}

        private void ListviewClick(object sender, MouseButtonEventArgs e)
        {

            listDelete = new List<string>();

            var selectedItem = sender as ListViewItem;
            Customer dataItem = (Customer)lvListCustomer.ItemContainerGenerator.ItemFromContainer(selectedItem);

            listDelete.Add(dataItem.MaKH);

        }

        private void btnFindName_Click(object sender, RoutedEventArgs e)
        {
            listCustomers = connectData.findCustommer_Name(txtSearch.Text);
            lvListCustomer.ItemsSource = listCustomers;
        }

        private void btnFindType_Click(object sender, RoutedEventArgs e)
        {
            listCustomers = connectData.findCustommer_Type(txtSearch.Text);
            lvListCustomer.ItemsSource = listCustomers;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text == "")
            {
                listCustomers = connectData.getListCustomer();
                lvListCustomer.ItemsSource = listCustomers;
            }
        }
    }
}
