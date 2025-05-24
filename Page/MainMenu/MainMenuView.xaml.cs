using Form.Control;
using Form.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MenuItem = Form.Control.MenuItem;

namespace Form.Page.MainMenu
{
    /// <summary>
    /// TestView.xaml 的交互逻辑
    /// </summary>
    public partial class MainMenuView : Window
    {
        public List<MenuPageItem> Menus = new List<MenuPageItem>();

        public MainMenuView()
        {
            InitializeComponent();
        }

        private void Max(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Min(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        public void brdBarEnter(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.SizeWE;

            // 使用 VisualStateManager 更改控件的外观
            VisualStateManager.GoToElementState((FrameworkElement)sender, "MouseOver", true);
        }

        public void brdBarLeave(object sender, MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;

            // 使用 VisualStateManager 更改控件的外观
            VisualStateManager.GoToElementState((FrameworkElement)sender, "Normal", true);
        }

        public bool IsMouseDown { set; get; }
        public Point _lastMouseDownPosition { set; get; }

        public void brdMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                _lastMouseDownPosition = e.GetPosition(this);
                ((Border)sender).CaptureMouse();
                IsMouseDown = true;
            }
        }

        public void brdMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (IsMouseDown)
            {
                ((Border)sender).ReleaseMouseCapture();
                IsMouseDown = false;
            }
        }

        public void brdMouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseDown)
            {
                try
                {
                    Point currentPosition = e.GetPosition(this);
                    double horizontalOffset = currentPosition.X - _lastMouseDownPosition.X;

                    // 将 Grid 控件的 Width 更新为当前宽度加上 horizontalOffset
                    codFileBox.Width = new GridLength(codFileBox.Width.Value + horizontalOffset);
                    _lastMouseDownPosition = currentPosition;
                }
                catch { }
            }
        }

        public void MenuInit(List<MenuPageItem> menuPageItems)
        {
            Menus = menuPageItems;
            foreach (var item in Menus)
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Text = item.Name;
                TabHeaderItem tabHeaderItem = new TabHeaderItem();
                tabHeaderItem.Text = item.Name;
                item.MenuItem = menuItem;
                item.TabHeaderItem = tabHeaderItem;
                this.muLeftList.Add(menuItem);
            }
            muLeftList_MenuSelectEvent(Menus[0].MenuItem, null);
        }

        private void muLeftList_MenuSelectEvent(MenuItem menu, EventArgs eventArgs)
        {
            var m = Menus.Where(x => x.MenuItem == menu).FirstOrDefault();
            this.muTopList.Select(m.TabHeaderItem);
            this.frmMain.Navigate(m.Page);
        }

        private void muTopList_MenuSelectEvent(TabHeaderItem menu, EventArgs eventArgs)
        {
            var t = Menus.Where(x => x.TabHeaderItem == menu).FirstOrDefault();
            this.muLeftList.Select(t.MenuItem);
            this.frmMain.Navigate(t.Page);
        }

        private void muTopList_CloseSelectEvent(TabHeaderItem menu, EventArgs eventArgs)
        {
            var cnt = this.muTopList.stk.Children.Count;
            if (cnt > 0)
            {
                if (menu.IsSelect)
                {
                    var btn = this.muTopList.stk.Children[cnt - 1] as TabHeaderItem;
                    var t = Menus.Where(x => x.TabHeaderItem == btn).FirstOrDefault();
                    this.muLeftList.Select(t.MenuItem);
                    this.muTopList.Select(btn);
                    this.frmMain.Navigate(t.Page);
                }
            }
            else
            {
                this.frmMain.Navigate(null);
                this.muLeftList.Select(null);
            }
        }
    }
}