using Dapper;
using Form.Page.Login;
using HandyControl.Controls;
using Model;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Form.Page.MyInfo
{
    /// <summary>
    /// MyInfoView.xaml 的交互逻辑
    /// </summary>
    public partial class MyInfoView : System.Windows.Controls.Page
    {
        public Users User { get; set; }
        private string FilePath { get; set; }

        public MyInfoView()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            using (var db = DBHelper.GetConn())
            {
                User = db.Get<Users>(LoginViewModel.User.Id);
                txtAddress.Text = User.address;
                txtPsd.Password = User.password;
                txtId.Text = User.Id;
                LoadImageFromBytes(User.img, Img);
            }
        }

        private void ChangePic(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // 设置文件筛选器和标题
                openFileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.gif;*.bmp|所有文件|*.*";
                openFileDialog.Title = "选择图片文件";

                // 显示对话框并处理结果
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = openFileDialog.FileName;
                    Img.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
            }
        }

        private void Modi(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPsd.Password))
            {
                throw new Exception("密码为空!");
            }
            User.Id = txtId.Text;
            User.password = txtPsd.Password;
            User.address = txtAddress.Text;
            if (!string.IsNullOrEmpty(FilePath))
            {
                User.img = File.ReadAllBytes(FilePath);
            }
            using (var db = DBHelper.GetConn())
            {
                db.Update(User);
                Growl.InfoGlobal("更改成功!");
            }
        }

        public static void LoadImageFromBytes(byte[] bytes, System.Windows.Controls.Image imageControl)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return;
            }

            try
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; // 确保流关闭后图像仍可用
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    bitmap.Freeze(); // 跨线程安全（可选）

                    imageControl.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("无法解析字节数组为有效图片。", ex);
            }
        }
    }
}