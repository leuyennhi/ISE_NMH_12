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

		string connectionString = Global.connectionString;

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

           

            if (txtUsername.Text == "" || txtPassword.Password == "" || txtName.Text == "" || datePicker.ToString() == "" || txtAddress.Text == "" || txtPhoneNumber.Text == "" || txtCMND.Text == "" || txtChucVu.Text == "--None--" || txtPassword.Password == "" || txtConfirmPassword.Password == "")
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
				SqlConnection sqlCon = new SqlConnection(connectionString);
                    sqlCon.Open();
				int indexnow = 1;
				if (sqlCon.State == System.Data.ConnectionState.Open)
				{
					string duplicate = "select Count(*) from NhanVien where TenDangNhap = '" + txtUsername.Text + "'";
					SqlCommand cmd = new SqlCommand(duplicate, sqlCon);
					int count = Convert.ToInt32(cmd.ExecuteScalar());
					if (count > 0)
					{
						MessageBox.Show("Đã tồn tại tên đăng nhập");
						return;
					}
					string q = "SELECT count(*) FROM NHANVIEN";
					cmd = new SqlCommand(q, sqlCon);
					count = Convert.ToInt32(cmd.ExecuteScalar());
					
					if (count > 0)
					{
						q = "SELECT MaNV FROM NHANVIEN";

						cmd = new SqlCommand(q, sqlCon);
						SqlDataReader reader = cmd.ExecuteReader();
						string manv = "NV0";
						while (reader.Read())
						{

							manv = reader.GetString(0);
							manv = manv.Remove(0, 2);
							int indextemp = Convert.ToInt32(manv);
							if (indextemp > indexnow)
							{
								indexnow = indextemp;
							}
						}
						reader.Close();
						indexnow++;
					}
					q = "insert into NHANVIEN(MaNV, HoTen, DiaChi, SDT, CMND, NgaySinh,ChucVu, TenDangNhap, MatKhau, DaXoa)values('NV" +
						indexnow + "', N'" + txtName.Text + "', N'" + txtAddress.Text + "', '" + txtPhoneNumber.Text + "', '" + txtCMND.Text + "', '"
						+ datePicker.Text + "', N'" + txtChucVu.Text + "', '" + txtUsername.Text + "', '" + txtPassword.Password.ToString() + "', 'false')";
					cmd = new SqlCommand(q, sqlCon);
					cmd.ExecuteNonQuery();
				}

				sqlCon.Close();

				MessageBox.Show("Đăng kí tài khoản thành công");
				if (txtChucVu.Text == "Quản lý")
				{
					UserControl urc = new MainScreen();
					Global.registernavigation.Children.Add(urc);
					Global.MaNV = "NV" + indexnow;
				}
				ClearTextbox();
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
			UserControl urc = new LoginScreen();
			Global.registernavigation.Children.Add(urc);
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
