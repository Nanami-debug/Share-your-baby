using Form.Control;
using Form.Models;
using Form.Page.Chat;
using Form.Page.Good;
using Form.Page.MyInfo;
using Form.Page.Order;
using Stylet;
using System;
using System.Collections.Generic;

namespace Form.Page.MainMenu
{
    public class MainMenuViewModel : Screen
    {
        public MainMenuView SView { get; set; }

        public void Load()
        {
            SView = this.View as MainMenuView;

            List<MenuPageItem> menuPageItems = new List<MenuPageItem>
            {
                new MenuPageItem { Name = "我的信息", Page = new MyInfoView() },
                new MenuPageItem { Name = "购物", Page = new GoodView() },
                new MenuPageItem { Name = "我的订单", Page = new OrderView() },
                new MenuPageItem { Name = "我的聊天", Page = new ChatListView() },
            };
            SView.MenuInit(menuPageItems);
        }

        public void MenuSelect(MenuItem menuItem, EventArgs e)
        {
        }

        public void HeaderSelect(TabHeaderItem headItem, EventArgs e)
        {
            this.SView.frmMain.Navigate(headItem.Page);
        }

        public void Close()
        {
            this.RequestClose();
        }
    }
}