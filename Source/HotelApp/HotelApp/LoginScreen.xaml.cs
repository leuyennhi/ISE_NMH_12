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
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
			
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlCon = new SqlConnection(@"Data Source=DESKTOP-8GM7A4F\SQLEXPRESS;Initial Catalog=DataForHotelApp;Integrated Security=True");
            try
            {
                sqlCon.Open();
                string username = txtUsername.Text;
                string password = txtPassword.Password;
                string sql = "select * from NhanVien where TenDangNhap = '" + username + "' and MatKhau = '" + password + "'";
                string sqlGetName = "select HoTen from NhanVien where TenDangNhap = @TenDangNhap";
                string sqlGetChucVu = "select ChucVu from NhanVien where TenDangNhap = @TenDangNhap";
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                SqlCommand sqlCmdGetName = new SqlCommand(sqlGetName, sqlCon);
                SqlCommand sqlCmdGetChucVu = new SqlCommand(sqlGetChucVu, sqlCon);
                sqlCmdGetName.Parameters.AddWithValue("TenDangNhap", txtUsername.Text);
                sqlCmdGetChucVu.Parameters.AddWithValue("TenDangNhap", txtUsername.Text);
                string name = (string)sqlCmdGetName.ExecuteScalar();
                string chucVu = (string)sqlCmdGetChucVu.ExecuteScalar();
                SqlDataReader dta = cmd.ExecuteReader();
                if (dta.Read() == true)
                {


                    MainScreen mScreen = new MainScreen();
                    mScreen.ten.Content = name;
                    mScreen.chucVu.Content = chucVu;
                    mScreen.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Đăng nhập không thành công");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối");
            }

        }

        private void btnForget_Click(object sender, RoutedEventArgs e)
        {
            PasswordChanging passwordChanging = new PasswordChanging();
            //passwordChanging.Show();
            this.Close();
        }
    }
    
}
