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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HotelApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class BaoCaoDoanhThu : UserControl
    {
        private List<ListViewTurnoverReport> items = new List<ListViewTurnoverReport>();
        private ConnectData connectData;
        public BaoCaoDoanhThu(ConnectData conData)
        {
            InitializeComponent();
            connectData = conData;
            //connectData.getTurnoverReport("12");
        }

        private void Button_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)month.SelectedItem;
            string value = typeItem.Content.ToString();

            items = connectData.getTurnoverReport(value);
            if (items.Count() == 0)
            {
                MessageBox.Show("Hiện chưa có dữ liệu để thông kê báo cáo doanh thu cho tháng " + value + "\nVui lòng lựa chọn tháng khác!!!", "Thông Báo!!!", MessageBoxButton.OK);
                return;
            }
            lvTurnoverReport.ItemsSource = items;
        }
    }
}
