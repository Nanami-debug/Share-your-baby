using Form.Control;

namespace Form.Models
{
    public class MenuPageItem
    {
        public string Name { get; set; }
        public MenuItem MenuItem { get; set; }
        public TabHeaderItem TabHeaderItem { get; set; }
        public System.Windows.Controls.Page Page { get; set; }
    }
}