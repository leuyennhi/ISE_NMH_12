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
    /// Interaction logic for BaoCaoTyLe.xaml
    /// </summary>
    public partial class BaoCaoMatDo : UserControl
    {
        private List<ListViewDensityReport> items = new List<ListViewDensityReport>();
        private ConnectData connectData;
        public BaoCaoMatDo(ConnectData conData)
        {
            InitializeComponent();
            connectData = conData;
        }

        private void month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem typeItem = (ComboBoxItem)month.SelectedItem;
            string value = typeItem.Content.ToString();

            items = connectData.getDensityReport(value);
            lvDensityReport.ItemsSource = items;

            if (items.Count() == 0)
            {
                MessageBox.Show("Hiện chưa có dữ liệu để thống kê Báo cáo mật độ cho tháng " + value + "\n\nVui lòng lựa chọn tháng khác!!!", "Chưa Có Dữ Liệu!!!", MessageBoxButton.OK);
            }
        }
    }
}
