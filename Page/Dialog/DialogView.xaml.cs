using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Form.Page.Dialog
{
    /// <summary>
    /// DialogView.xaml 的交互逻辑
    /// </summary>
    public partial class DialogView : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Text { get; set; }

        public DialogView(string name, System.Windows.Controls.Page Page)
        {
            InitializeComponent();
            this.DataContext = this;
            Page.Tag = this;
            Text = name;
            this.frmMain.Navigate(Page);
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
            this.Close();
        }

        private void Min(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}