using Dapper;
using Form.Models;
using Form.Page.Login;
using HandyControl.Data;
using Model;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Form.Page.Chat
{
    /// <summary>
    /// ChatView.xaml 的交互逻辑
    /// </summary>
    public partial class ChatView : System.Windows.Controls.Page
    {
        private Users User;
        private Timer _timer;
        private int count = 0;
        public ChatView(Users user)
        {
            InitializeComponent();
            this.User = user;
            // 初始化定时器，但不立即启动
            _timer = new Timer(LoadData, null, Timeout.Infinite, Timeout.Infinite);
            _timer.Change(0, 1000);
            this.Unloaded += PageUnloaded;
            //LoadData(null);
        }

        private void PageUnloaded(object sender, EventArgs e)
        {
            _timer?.Dispose(); // 释放定时器资源
        }

        public void LoadData(object state)
        {
            using (var db = DBHelper.GetConn())
            {
                var chat = db.Query<Model.Chat>($@"SELECT
	                                                    *
                                                    FROM
	                                                    Chat
                                                    WHERE
	                                                    (id1 = '{User.Id}' and id2 = '{LoginViewModel.User.Id}')
	                                                    or (id1 = '{LoginViewModel.User.Id}' and id2 = '{User.Id}')").ToList();
                
                if (chat != null && chat.Count() > 0)
                {
                    var list = db.GetList<ChatDetail>(new { chat[0].ChatNo }).ToList();
                    var vm = list.Select(x => new ChatViewModel
                    {
                        Msg = x.Msg,
                        SendTime = x.SendTime,
                        Role = x.UserId == LoginViewModel.User.Id ? ChatRoleType.Sender : ChatRoleType.Receiver,
                    }).OrderBy(t => t.SendTime).ToList();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Ic.ItemsSource = null;
                        Ic.ItemsSource = vm;
                        if(vm.Count != count)
                        {
                            count = vm.Count;
                            Scroller.ScrollToEnd();
                        }
                    });
                }
            }
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            using (var db = DBHelper.GetConn())
            {
                Model.Chat chat = null;
                var chats = db.Query<Model.Chat>($@"SELECT
	                                                    *
                                                    FROM
	                                                    Chat
                                                    WHERE
	                                                    (id1 = '{User.Id}' and id2 = '{LoginViewModel.User.Id}')
	                                                    or (id1 = '{LoginViewModel.User.Id}' and id2 = '{User.Id}')").ToList();
                if(chats != null && chats.Count > 0)
                {
                    chat = chats[0];
                }
                var tr = db.BeginTransaction();
                try
                {
                    if (chat == null)
                    {
                        chat = new Model.Chat();
                        chat.ChatNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        chat.Id1 = User.Id;
                        chat.Id2 = LoginViewModel.User.Id;
                        db.Insert<string, Model.Chat>(chat, tr);
                    }

                    ChatDetail chatDetail = new ChatDetail();
                    chatDetail.ChatDetailNo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    chatDetail.ChatNo = chat.ChatNo;
                    chatDetail.UserId = LoginViewModel.User.Id;
                    chatDetail.Msg = txtSendMsg.Text;
                    chatDetail.SendTime = DateTime.Now;
                    var id = db.Insert<string, ChatDetail>(chatDetail, tr);
                    tr.Commit();
                    txtSendMsg.Text = "";
                    Scroller.ScrollToEnd();
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    throw ex;
                }
            }
        }
    }
}