using Form.Config;
using HandyControl.Controls;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dapper;
using Form.Util;
using Form.Page.Dialog;
using Model;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace Form.Page.Home
{
    /// <summary>
    /// RegView.xaml 的交互逻辑
    /// </summary>
    public partial class RegView : System.Windows.Controls.Page
    {
        public RegView()
        {
            InitializeComponent();
        }
        string FilePath { get; set; }
        private void Reg_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text))
            {
                throw new Exception("用户名为空!");
            }
            if (string.IsNullOrEmpty(txtPsd.Password))
            {
                throw new Exception("密码为空!");
            }
            if (txtPsd.Password != txtCPsd.Password)
            {
                throw new Exception("两次密码不一致!");
            }
            Users user = new Users();
            user.Id = txtId.Text;
            user.password = txtPsd.Password;
            user.call = txtCall.Text;
            user.img = File.ReadAllBytes(FilePath);
            using (var db = DBHelper.GetConn())
            {
                var id = db.Insert<string, Users>(user);
                Growl.InfoGlobal("注册成功!");
                var dg = this.Tag as DialogView;
                dg.Close();
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

        public static void LoadImageFromBytes(byte[] bytes, System.Windows.Controls.Image imageControl)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentException("输入的字节数组为空或无效。");
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
