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
using System.Data;
using System.Data.SqlClient;
using System.Security;
using System.Globalization;
using System.Threading;

namespace HotelApp
{
    /// <summary>
    /// Interaction logic for RegisterScreen.xaml
    /// </summary>
    public partial class RegisterScreen : UserControl
    {
        
        private string typeSite = "";
        private List<string> listTypeSite = new List<string>();

        public RegisterScreen()
        {
            InitializeComponent();
            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
            listTypeSite.Add("--None--");
            listTypeSite.Add("Nhân viên");
            listTypeSite.Add("Quản lý");

        }

        string connectionString = @"Data Source=DESKTOP-8GM7A4F\SQLEXPRESS;Initial Catalog=DataForHotelApp;Integrated Security=True";

        private bool checkStringNumber(string a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < '0' || a[i] > '9')
                    return false;
            }
            return true;
        }
        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {

            string count = @"select count(*) from NhanVien";
            string duplicate = "select TenDangNhap from NhanVien where TenDangNhap = @TenDangNhap";

            if (txtUsername.Text == "" || txtPassword.Password == "" || txtName.Text == "" || datePicker.ToString() == "" || txtAddress.Text == "" || txtPhoneNumber.Text == "" || txtCMND.Text == "" || txtChucVu.Text == "" || txtPassword.Password == "" || txtConfirmPassword.Password == "")
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
            else if (txtPassword.Password != txtConfirmPassword.Password)
                MessageBox.Show("Mật khẩu xác nhận không trùng khớp");
            else if (!checkStringNumber(txtCMND.Text))
            {
                MessageBox.Show("Vui lòng kiểm tra lại CMND");
            }
            else if (!checkStringNumber(txtPhoneNumber.Text))
            {
                MessageBox.Show("Vui lòng kiểm tra lại số điện thoại");
            }
            else if (txtPassword.Password.Length < 6)
            {
                MessageBox.Show("Vui lòng nhập password có hơn 6 kí tự");
            }
            else if (txtChucVu.Text == "--None--")
            {
                MessageBox.Show("Vui lòng chọn chức vụ");
            }
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("UserAdd", sqlCon);
                    SqlCommand sqlCmdCount = new SqlCommand(count, sqlCon);
                    SqlCommand sqlCmdDuplicateAccount = new SqlCommand(duplicate, sqlCon);
                    sqlCmdDuplicateAccount.Parameters.AddWithValue("@TenDangNhap", txtUsername.Text);
                    string duplicateAccount = (string)sqlCmdDuplicateAccount.ExecuteScalar();
                    if (duplicateAccount == txtUsername.Text)
                    {
                        MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.");
                    }
                    else
                    {
                        int n = (int)sqlCmdCount.ExecuteScalar();
                        string tmp = "NV" + (n + 1);
                        sqlCmd.CommandType = CommandType.StoredProcedure;
                        sqlCmd.Parameters.AddWithValue("@MaNV", tmp.Trim());
                        sqlCmd.Parameters.AddWithValue("@HoTen", txtName.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@NgaySinh", datePicker.ToString().Trim());
                        sqlCmd.Parameters.AddWithValue("@DiaChi", txtAddress.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@SDT", txtPhoneNumber.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@CMND", txtCMND.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@ChucVu", txtChucVu.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@TenDangNhap", txtUsername.Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@MatKhau", txtPassword.Password.Trim());
                        sqlCmd.ExecuteNonQuery();
                        MessageBox.Show("Đăng kí tài khoản thành công");
                        ClearTextbox();
                    }
                }
            }
        }

        void ClearTextbox()
        {
            this.txtUsername.Clear();
            this.txtName.Clear();
            this.txtAddress.Clear();
            this.txtChucVu.Text = "--None--";
            this.txtCMND.Clear();
            this.txtConfirmPassword.Clear();
            this.txtPassword.Clear();
            this.txtPhoneNumber.Clear();
            this.datePicker.SelectedDate = null;
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void btnDangXuat_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        private void Combobox_Loaded(object sender, RoutedEventArgs e)
        {
            var combo = sender as ComboBox;
            combo.ItemsSource = listTypeSite;
            combo.SelectedIndex = 0;
            combo.Background = Brushes.Yellow;
        }

        private void Combobox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var selectedComboItem = sender as ComboBox;
            typeSite = selectedComboItem.SelectedItem as string;

        }

    }
}
