using Dapper;
using Form.Models;
using Form.Page.Dialog;
using Form.Page.Login;
using Model;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Form.Page.Chat
{
    /// <summary>
    /// ChatListView.xaml 的交互逻辑
    /// </summary>
    public partial class ChatListView : System.Windows.Controls.Page
    {
        public ChatListView()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            using (var db = DBHelper.GetConn())
            {
                var sql = $@"
                        SELECT
	                        *
                        FROM
	                        Chat
                        WHERE
	                        id2 = '{LoginViewModel.User.Id}'
	                        or id1 = '{LoginViewModel.User.Id}'
                ";
                var list = db.Query<Model.Chat>(sql).Select(x => new ChatListViewModel
                {
                    UserId = x.Id1 == LoginViewModel.User.Id ? x.Id2 : x.Id1
                }).ToList();
                this.dgdData.ItemsSource = null;
                this.dgdData.ItemsSource = list;
            }
        }

        private void Chat(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var vm = btn.CommandParameter as ChatListViewModel;
            var userId = vm.UserId;
            using (var db = DBHelper.GetConn())
            {
                var user = db.Get<Users>(userId);
                var dig = new DialogView($"与{userId}的聊天", new ChatView(user));
                dig.Width = 410;
                dig.Height = 620;
                dig.Background = Brushes.Black;
                dig.ShowDialog();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}