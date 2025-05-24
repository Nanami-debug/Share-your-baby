using System.ComponentModel;
using System.Windows.Controls;

namespace Form.Control
{
    /// <summary>
    /// MenuItem.xaml 的交互逻辑
    /// </summary>
    public partial class MenuItem : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSelect { get; set; } = false;
        public string Text { get; set; }

        public MenuItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}