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
	/// Interaction logic for TypeOfRoom.xaml
	/// </summary>
	public partial class TypeOfRoom : Window
	{
		public TypeOfRoom()
		{
			InitializeComponent();

			List<ListViewData> items = new List<ListViewData>();
			items.Add(new ListViewData() { Col1 = "join", Col2 = "21" });
			items.Add(new ListViewData() { Col1 = "zach", Col2 = "18" });
			items.Add(new ListViewData() { Col1 = "mark", Col2 = "23" });
			listView1.ItemsSource = items;

		}

		private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
		{
			RefreshListView(textBox1.Text, textBox2.Text);
		}

		private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
		{
			RefreshListView(textBox1.Text, textBox2.Text);
		}

		private bool stopRefreshControls;
		private bool dataChanged;

		private void RefreshListView(string value1, string value2)
		{
			ListViewData lvc = (ListViewData)listView1.SelectedItem;
			if (lvc != null && !stopRefreshControls)
			{
				setDataChanged(true);

				lvc.Col1 = value1;
				lvc.Col2 = value2;

				listView1.Items.Refresh();
			}
		}
		private void setDataChanged(bool value)
		{
			dataChanged = value;
			saveButton.IsEnabled = value;
		}

		private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListViewData lvc = (ListViewData)listView1.SelectedItem;
			if (lvc != null)
			{
				stopRefreshControls = true;
				textBox1.Text = lvc.Col1;
				textBox2.Text = lvc.Col2;
				stopRefreshControls = false;
			}
		}

		private void addButton_Click(object sender, RoutedEventArgs e)
		{
			setDataChanged(true);
			AddRow();
		}

		private void AddRow()
		{
			listView1.Items.Add(new ListViewData("", ""));
			listView1.SelectedIndex = listView1.Items.Count - 1;

			textBox1.Text = "";
			textBox2.Text = "";
			textBox1.Focus();
		}

		private void removeButton_Click(object sender, RoutedEventArgs e)
		{
			setDataChanged(true);
			int selectedIndex = listView1.SelectedIndex;

			listView1.Items.Remove(listView1.SelectedItem);

			// if no rows left, add a blank row
			if (listView1.Items.Count == 0)
			{
				AddRow();
			}
			// otherwise select next row
			else if (selectedIndex <= listView1.Items.Count - 1)
			{
				listView1.SelectedIndex = selectedIndex;
			}
			else // not above cases? Select last row
			{
				listView1.SelectedIndex = listView1.Items.Count - 1;
			}
		}

		public void Save(System.Windows.Data.CollectionView items)
		{
			//XDocument xdoc = new XDocument();

			//XElement xeRoot = new XElement("Data");
			//XElement xeSubRoot = new XElement("Rows");

			//foreach (var item in items)
			//{
			//	ListViewData lvc = (ListViewData)item;

			//	XElement xRow = new XElement("Row");
			//	xRow.Add(new XElement("col1", lvc.Col1));
			//	xRow.Add(new XElement("col2", lvc.Col2));

			//	xeSubRoot.Add(xRow);
			//}
			//xeRoot.Add(xeSubRoot);
			//xdoc.Add(xeRoot);

			//xdoc.Save("MyData.xml");
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			ShowData();

			if (listView1.Items.Count == 0)
			{
				AddRow();
			}
			else
			{
				listView1.SelectedIndex = 0;
			}
			setDataChanged(false);
			textBox1.Focus();
		}

		private void ShowData()
		{
			//MyData md = new MyData();
			//listView1.Items.Clear();

			//foreach (var row in md.GetRows())
			//{
			//	listView1.Items.Add(row);
			//}
		}

		public IEnumerable<object> GetRows()
		{
			List<ListViewData> rows = new List<ListViewData>();

			//if (File.Exists("MyData.xml"))
			//{
			//	// Create the query 
			//	var rowsFromFile = from c in XDocument.Load(
			//						"MyData.xml").Elements(
			//						"Data").Elements("Rows").Elements("Row")
			//					   select c;

			//	// Execute the query 
			//	foreach (var row in rowsFromFile)
			//	{
			//		rows.Add(new ListViewData(row.Element("col1").Value,
			//						row.Element("col2").Value));
			//	}
			//}
			return rows;
		}
	}

	public class ListViewData
	{
		public ListViewData()
		{
			// default constructor
		}

		public ListViewData(string col1, string col2)
		{
			Col1 = col1;
			Col2 = col2;
		}

		public string Col1 { get; set; }
		public string Col2 { get; set; }
	}
}
