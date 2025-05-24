using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Form.Control
{
    /// <summary>
    /// Menu.xaml 的交互逻辑
    /// </summary>
    public partial class Menu : UserControl
    {
        // 定义一个事件委托
        public delegate void MenuSelectEventHandler(MenuItem menu, EventArgs eventArgs);

        // 定义事件
        public event MenuSelectEventHandler MenuSelectEvent;

        public Menu()
        {
            InitializeComponent();
        }

        public void Select(MenuItem tab)
        {
            foreach (MenuItem btn in this.stk.Children)
            {
                btn.IsSelect = false;
            }
            if (tab != null)
            {
                tab.IsSelect = true;
            }
        }

        private void Btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (MenuItem btn in this.stk.Children)
            {
                btn.IsSelect = false;
            }
            var selectBtn = sender as MenuItem;
            selectBtn.IsSelect = true;
            MenuSelectEvent?.Invoke(selectBtn, EventArgs.Empty);
        }

        public void Add(MenuItem menuItem)
        {
            menuItem.MouseDown += Btn_MouseDown;
            this.stk.Children.Add(menuItem);
        }
    }
}