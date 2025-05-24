using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Form.Control
{
    /// <summary>
    /// TabHeader.xaml 的交互逻辑
    /// </summary>
    public partial class TabHeader : UserControl
    {
        // 定义一个事件委托
        public delegate void HeaderSelectEventHandler(TabHeaderItem menu, EventArgs eventArgs);

        public delegate void HeaderCloseEventHandler(TabHeaderItem menu, EventArgs eventArgs);

        // 定义事件
        public event HeaderSelectEventHandler MenuSelectEvent;

        public event HeaderCloseEventHandler CloseSelectEvent;

        public TabHeader()
        {
            InitializeComponent();
        }

        public void Select(TabHeaderItem tab)
        {
            if (!this.stk.Children.Contains(tab))
            {
                tab.MouseDown += Btn_MouseDown;
                tab.btnClose.MouseDown += Btn_CloseDown;
                tab.btnClose.Tag = tab;
                this.stk.Children.Add(tab);
            }
            foreach (TabHeaderItem btn in this.stk.Children)
            {
                btn.IsSelect = false;
            }
            tab.IsSelect = true;
        }

        private void Btn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var btn = sender as TabHeaderItem;
            Select(btn);
            MenuSelectEvent?.Invoke(btn, EventArgs.Empty);
        }

        private void Btn_CloseDown(object sender, MouseButtonEventArgs e)
        {
            var btn = sender as TextBlock;
            var tab = btn.Tag as TabHeaderItem;
            tab.MouseDown -= Btn_MouseDown;
            tab.btnClose.MouseDown -= Btn_CloseDown;
            this.stk.Children.Remove(tab);
            e.Handled = true;
            CloseSelectEvent?.Invoke(tab, EventArgs.Empty);
        }
    }
}