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
using System.Windows.Navigation;

namespace HotelApp
{
    /// <summary>
    /// Interaction logic for PasswordChanging.xaml
    /// </summary>
    public partial class PasswordChanging : UserControl
    {
		string connectionString = Global.connectionString;

		public PasswordChanging()
		{
			InitializeComponent();
		}

		private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewPassword.Password != txtConfirmPassword.Password)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp. Vui lòng nhập lại");
            }
            else 
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string sqlUpdate = "update NhanVien set MatKhau = @MatKhau where TenDangNhap = @TenDangNhap";
                    string sqlInvalidUsername = "select TenDangNhap from NhanVien where TenDangNhap = @TenDangNhap";
					string sqlInvalidMaNV = "select MaNV from NhanVien where TenDangNhap = @TenDangNhap";
					SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, sqlCon);
                    SqlCommand cmdInvalidUsername = new SqlCommand(sqlInvalidUsername, sqlCon);
					SqlCommand cmdInvalidMaNV = new SqlCommand(sqlInvalidMaNV, sqlCon);
					cmdInvalidUsername.Parameters.AddWithValue("TenDangNhap", txtUsername.Text);
                    string invalidUsername = (string)cmdInvalidUsername.ExecuteScalar();
					if (txtUsername.Text != invalidUsername)
					{
						MessageBox.Show("Username không hợp lệ. Vui lòng nhập lại.");
					}
					else if (txtNewPassword.Password.Length < 6)
					{
						MessageBox.Show("Vui lòng nhập password có hơn 6 kí tự.");
					}
					else
					{
						cmdInvalidMaNV.Parameters.AddWithValue("TenDangNhap", txtUsername.Text);
						string currentMaNV = (string)cmdInvalidMaNV.ExecuteScalar();
						cmdUpdate.Parameters.AddWithValue("@MatKhau", txtNewPassword.Password.Trim());
						cmdUpdate.Parameters.AddWithValue("@TenDangNhap", txtUsername.Text);
						cmdUpdate.ExecuteNonQuery();
						Global.MaNV = currentMaNV;
						MessageBox.Show("Đổi mật khẩu thành công");
						UserControl urc = new LoginScreen();
						Global.registernavigation.Children.Add(urc);
						ClearTextbox();
					}
                    
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
			UserControl urc = new LoginScreen();
			Global.registernavigation.Children.Add(urc);
		}

        //Clear textbox
        void ClearTextbox()
        {
            this.txtUsername.Clear();
            this.txtNewPassword.Clear();
            this.txtConfirmPassword.Clear();
        }
    }
}
