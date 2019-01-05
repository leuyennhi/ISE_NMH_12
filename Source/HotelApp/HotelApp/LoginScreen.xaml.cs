using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
	/// Interaction logic for LoginScreen.xaml
	/// </summary>
	public partial class LoginScreen : UserControl
	{
		public LoginScreen()
		{
			InitializeComponent();
		}

		private void BtnLogin_Click(object sender, RoutedEventArgs e)
		{
			SqlConnection sqlCon = new SqlConnection(Global.connectionString);
			try
			{
				sqlCon.Open();
				string username = txtUsername.Text;
				string password = txtPassword.Password;
				string sql = "select count(*) from NhanVien where TenDangNhap = '" + username + "' and MatKhau = '" + password + "'";
				SqlCommand cmd = new SqlCommand(sql, sqlCon);
				int count = Convert.ToInt32(cmd.ExecuteScalar());
				if (count > 0)
				{
					sql = "select MaNV from NhanVien where TenDangNhap = '" + username + "' and MatKhau = '" + password + "'";
					cmd = new SqlCommand(sql, sqlCon);
					SqlDataReader reader = cmd.ExecuteReader();
					reader.Read();
					Global.MaNV = reader.GetString(0);
					reader.Close();

					UserControl urc = new MainScreen();
					Global.registernavigation.Children.Add(urc);
				}
				else
				{
					MessageBox.Show("Đăng nhập không thành công");
				}
			}

			catch (Exception ex)
			{
				MessageBox.Show("Lỗi kết nối "+ex);
			}

		}

		private void BtnForget_Click(object sender, RoutedEventArgs e)
		{
			UserControl urc = new PasswordChanging();
			Global.registernavigation.Children.Add(urc);
		}

		private void BtnRegister_Click(object sender, RoutedEventArgs e)
		{
			UserControl urc = new RegisterScreen();
			Global.registernavigation.Children.Add(urc);
		}
	}
}
