using Dapper;
using Form.Config;
using Form.Page.Dialog;
using Form.Page.Home;
using Form.Page.MainMenu;
using Form.Util;
using Model;
using Stylet;
using System;
using System.Linq;

namespace Form.Page.Login
{
    public class LoginViewModel : Screen
    {
        public static Model.Users User = null;
        private LoginView _loginView { get; set; }

        public void Login()
        {
            using (var db = DBHelper.GetConn())
            {
                User = db.GetList<Users>(new { Id = _loginView.txtUserNo.Text, password = _loginView.psdPassword.Password }).FirstOrDefault();
                if (User != null)
                {
                    Boot.Manager.ShowWindow(Boot.IOC.Get<MainMenuViewModel>());
                }
                else
                {
                    throw new Exception("账密错误！");
                }
            }
            this.Close();
        }

        public void Reg()
        {
            new DialogView("注册", new RegView()).ShowDialog();
        }

        public void Load()
        {
            _loginView = this.View as LoginView;
            IniParser iniParser = new IniParser("config.ini");
            var conn = iniParser.Read("Common", "连接字符串");
            DBHelper.Init(conn);
        }

        public void Close()
        {
            this.RequestClose();
        }
    }
}