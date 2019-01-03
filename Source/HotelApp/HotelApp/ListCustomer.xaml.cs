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
		public ListCustomer(ConnectData conData)
		{
			InitializeComponent();
			listCustomers = conData.getListCustomer();
			this.LV_ListCustomer.ItemsSource = listCustomers;
		}
	}
}
