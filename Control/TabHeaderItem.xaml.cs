using System.ComponentModel;
using System.Windows.Controls;

namespace Form.Control
{
    /// <summary>
    /// TabHeaderItem.xaml 的交互逻辑
    /// </summary>
    public partial class TabHeaderItem : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool IsSelect { get; set; } = false;
        public System.Windows.Controls.Page Page { get; set; }
        public string Text { get; set; }

        public TabHeaderItem()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}