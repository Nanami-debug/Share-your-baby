using Dapper;
using Form.Models;
using Form.Page.Chat;
using Form.Page.Dialog;
using Form.Page.Login;
using Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Form.Page.Good
{
    /// <summary>
    /// GoodView.xaml 的交互逻辑
    /// </summary>
    public partial class GoodView : System.Windows.Controls.Page
    {
        public GoodView()
        {
            InitializeComponent();
            //LoadData();
        }

        public void LoadData(string id = "")
        {
            using (var db = DBHelper.GetConn())
            {
                var sql = $@"
                    SELECT
	                 *
                    FROM
	                 Goods A
                    Where 1=1 ";
                if (!string.IsNullOrEmpty(id))
                {
                    sql += $@" Id like '%{id}%' ";
                }
                var gs = db.Query<Goods>(sql).ToList();
                Ic.ItemsSource = gs.Select(x => new GoodViewModel
                {
                    Name = x.Id,
                    Des = x.des,
                    UserId = x.UserId,
                    Price = x.price,
                    Pic = x.img,
                    IsSelf = x.UserId == LoginViewModel.User.Id,
                    Solded = x.solded
                }).ToList();
            }
        }

        public static BitmapImage LoadImageFromBytes(byte[] bytes)
        {
            using (var ms = new System.IO.MemoryStream(bytes))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                image.Freeze();
                return image;
            }
        }

        private void AddGood(object sender, RoutedEventArgs e)
        {
            new DialogView("添加商品", new AddModiGood(null, "ADD")).ShowDialog();
            LoadData();
        }

        private void Modi(object sender, RoutedEventArgs e)
        {
            var goodvm = (sender as Button).DataContext as GoodViewModel;
            Goods gd = new Goods();
            gd.Id = goodvm.Name;
            gd.price = goodvm.Price;
            gd.des = goodvm.Des;
            gd.img = goodvm.Pic;
            gd.UserId = goodvm.UserId;
            gd.solded = goodvm.Solded;
            new DialogView("修改商品", new AddModiGood(gd, "EDIT")).ShowDialog();
            LoadData();
        }

        private void Buy(object sender, RoutedEventArgs e)
        {
            var goodvm = (sender as Button).DataContext as GoodViewModel;
            Goods gd = new Goods();
            gd.Id = goodvm.Name;
            gd.price = goodvm.Price;
            gd.des = goodvm.Des;
            gd.img = goodvm.Pic;
            gd.UserId = goodvm.UserId;
            gd.solded = goodvm.Solded;
            var dig = new DialogView("购买", new SaleView(gd));
            dig.Width = 500;
            dig.Height = 500;
            dig.ShowDialog();
            LoadData();
        }

        private void Chat(object sender, RoutedEventArgs e)
        {
            var goodvm = (sender as Button).DataContext as GoodViewModel;
            using (var db = DBHelper.GetConn())
            {
                var user = db.Get<Users>(goodvm.UserId);
                var dig = new DialogView($"与{goodvm.UserId}的聊天", new ChatView(user));
                dig.Width = 410;
                dig.Height = 620;
                dig.Background = Brushes.Black;
                dig.ShowDialog();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            LoadData(txtId.Text);
        }
    }
}