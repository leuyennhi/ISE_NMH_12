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
    /// Interaction logic for ThongTinTaiKhoan.xaml
    /// </summary>
    public partial class ThongTinTaiKhoan : UserControl
    {
        DetailOfStaff item = new DetailOfStaff();
        private ConnectData connectData;
        bool isEditing = false;
        public ThongTinTaiKhoan(ConnectData conData, string maNV)
        {
            InitializeComponent();
            connectData = conData;
            item = connectData.GetDetailOfStaff(maNV);

            lblMaNV.Content = item.MaNV;
            lblTenNV.Content = item.TenNV;
            lblNgaySinh.Content = item.NgaySinh;
            lblDiaChi.Content = item.DiaChi;
            lblCmnd.Content = item.CMND;
            lblSdt.Content = item.SDT;
            lblChucVu.Content = item.ChucVu;

            txtChucVu.Visibility = Visibility.Hidden;
            txtSdt.Visibility = Visibility.Hidden;
            txtDiaChi.Visibility = Visibility.Hidden;
            borderCV.Visibility = Visibility.Hidden;
            borderDC.Visibility = Visibility.Hidden;
            BorderSdt.Visibility = Visibility.Hidden;
        }

        private void click_btnDelete(object sender, RoutedEventArgs e)
        {
            if (isEditing == false)
            {
                if (MessageBox.Show("Bạn có thực sự muốn xóa nhân viên này???", "Xác Nhận", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<string> temp = new List<string>();
                    temp.Add(item.MaNV);
                    if (connectData.deleteStaffs(temp))
                    {
                        //Viết cái dòng chuyển màn hình vô nhaaaaaaaaaaaaaaaaaaaaa
                        var newWin = new MainScreen();
                        newWin.Show();

                        UserControl usc = null;
                        usc = new DanhSachNhanVien(connectData);
                        newWin.GridMain.Children.Add(usc);

                        Window.GetWindow(this).Close();

                    }
                }
            }
            else
            {
                isEditing = false;

                txtChucVu.Visibility = Visibility.Hidden;
                txtSdt.Visibility = Visibility.Hidden;
                txtDiaChi.Visibility = Visibility.Hidden;
                borderCV.Visibility = Visibility.Hidden;
                borderDC.Visibility = Visibility.Hidden;
                BorderSdt.Visibility = Visibility.Hidden;

                btnDelete.Content = "Xóa Nhân Viên";
                btnEdit.Content = "Chỉnh Sửa";
            }
        }

        private void click_btnEdit(object sender, RoutedEventArgs e)
        {
            if (isEditing == false)
            {
                isEditing = true;

                txtChucVu.Visibility = Visibility.Visible;
                txtChucVu.Text = item.ChucVu.Trim();
                txtSdt.Visibility = Visibility.Visible;
                txtSdt.Text = item.SDT.Trim();
                txtDiaChi.Visibility = Visibility.Visible;
                txtDiaChi.Text = item.DiaChi.Trim();
                borderCV.Visibility = Visibility.Visible;
                borderDC.Visibility = Visibility.Visible;
                BorderSdt.Visibility = Visibility.Visible;

                btnEdit.Content = "Lưu Thay Đổi";
                btnDelete.Content = "Hủy Thay Đổi";
            }
            else
            {

                if (txtChucVu.Text.Trim() == "" || txtDiaChi.Text.Trim() == "" || txtSdt.Text.Trim() == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin!!!", "Thiếu Dữ Liệu!!!", MessageBoxButton.OK);
                    return;
                }
                for (int i = 0; i < txtSdt.Text.Length; i++)
                {
                    if (txtSdt.Text[i] < 48 || txtSdt.Text[i] > 57)
                    {
                        MessageBox.Show("Định dạng số điện thoại không đúng!!!\n\nVui lòng kiểm tra lại trước khi lưu!!!", "Sai Định Dạng!!!", MessageBoxButton.OK);
                        return;
                    }
                }

                string temp = txtChucVu.Text.ToLower();

                if (temp != "quản lý" && temp != "nhân viên")
                {
                    MessageBox.Show("Chức vụ được nhập không hợp lệ!!!\n\nVui lòng nhập \"Quản Lý\" hoặc \"Nhân Viên\"!!!", "Sai Định Dạng!!!", MessageBoxButton.OK);
                    return;
                }

                connectData.updateInfoStaff(item.MaNV, txtDiaChi.Text, txtSdt.Text, txtChucVu.Text);
                item = connectData.GetDetailOfStaff(item.MaNV);

                lblMaNV.Content = item.MaNV;
                lblTenNV.Content = item.TenNV;
                lblNgaySinh.Content = item.NgaySinh;
                lblDiaChi.Content = item.DiaChi;
                lblCmnd.Content = item.CMND;
                lblSdt.Content = item.SDT;
                lblChucVu.Content = item.ChucVu;

                isEditing = false;

                txtChucVu.Visibility = Visibility.Hidden;
                txtSdt.Visibility = Visibility.Hidden;
                txtDiaChi.Visibility = Visibility.Hidden;
                borderCV.Visibility = Visibility.Hidden;
                borderDC.Visibility = Visibility.Hidden;
                BorderSdt.Visibility = Visibility.Hidden;

                btnDelete.Content = "Xóa Nhân Viên";
                btnEdit.Content = "Chỉnh Sửa";

            }
        }

        private void click_btnBack(object sender, RoutedEventArgs e)
        {
            if (isEditing)
            {
                MessageBox.Show("Chưa lưu thông tin đang chỉnh sửa!!!\n\nVui lòng lưu trước khi rời khỏi trang!!!", "Chưa Lưu Thay Đổi!!!", MessageBoxButton.OK);
                return;
            }

            else
            {
                //Viết cái dòng chuyển màn hình vô nhaaaaaaaaaaaaaaaaaaaaa
                var newWin = new MainScreen();
                newWin.Show();

                UserControl usc = null;
                usc = new DanhSachNhanVien(connectData);
                newWin.GridMain.Children.Add(usc);

                Window.GetWindow(this).Close();
            }
        }
    }
}
