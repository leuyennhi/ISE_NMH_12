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
    /// Interaction logic for PasswordChanging.xaml
    /// </summary>
    public partial class PasswordChanging : UserControl
    {
        string connectionString = @"Data Source=DESKTOP-8GM7A4F\SQLEXPRESS;Initial Catalog=DataForHotelApp;Integrated Security=True";

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (txtNewPassword.Password != txtConfirmPassword.Password)
            {
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp. Vui lòng nhập lại");
            }
            else if (txtNewPassword.Password == txtCurrentPassword.Password)
            {
                MessageBox.Show("Mật khẩu bạn vừa đặt trùng với mật khẩu cũ. Vui lòng nhập lại.");
            }
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string sqlUpdate = "update NhanVien set MatKhau = @MatKhau where TenDangNhap = @TenDangNhap";
                    string sqlCurrentPassword = "select MatKhau from NhanVien where TenDangNhap = @TenDangNhap";
                    string sqlInvalidUsername = "select TenDangNhap from NhanVien where TenDangNhap = @TenDangNhap";
                    SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, sqlCon);
                    SqlCommand cmdCurrentPassword = new SqlCommand(sqlCurrentPassword, sqlCon);
                    SqlCommand cmdInvalidUsername = new SqlCommand(sqlInvalidUsername, sqlCon);
                    cmdInvalidUsername.Parameters.AddWithValue("TenDangNhap", txtUsername.Text);
                    string invalidUsername = (string)cmdInvalidUsername.ExecuteScalar();
                    if (txtUsername.Text != invalidUsername)
                    {
                        MessageBox.Show("Username không hợp lệ. Vui lòng nhập lại.");
                    } else if(txtNewPassword.Password.Length < 6)
                    {
                        MessageBox.Show("Vui lòng nhập password có hơn 6 kí tự.");
                    }
                    else
                    {
                        cmdCurrentPassword.Parameters.AddWithValue("TenDangNhap", txtUsername.Text);
                        string currentPassword = (string)cmdCurrentPassword.ExecuteScalar();
                        if (txtCurrentPassword.Password != currentPassword)
                        {
                            MessageBox.Show("Mật khẩu không hợp lệ");
                        }
                        else
                        {
                            cmdUpdate.Parameters.AddWithValue("@MatKhau", txtNewPassword.Password.Trim());
                            cmdUpdate.Parameters.AddWithValue("@TenDangNhap", txtUsername.Text);
                            cmdUpdate.ExecuteNonQuery();
                            MessageBox.Show("Đổi mật khẩu thành công");
                            ClearTextbox();
                        }
                    }
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        //Clear textbox
        void ClearTextbox()
        {
            this.txtUsername.Clear();
            this.txtCurrentPassword.Clear();
            this.txtNewPassword.Clear();
            this.txtConfirmPassword.Clear();
        }
    }
}
