using PropertyChanged;

namespace Form.Models
{
    [AddINotifyPropertyChangedInterface]
    public class GoodViewModel
    {
        public string Name { get; set; }
        public string Des { get; set; }
        public string UserId { get; set; }
        public int Price { get; set; }
        public bool Solded { get; set; }
        public byte[] Pic { get; set; }
        public bool IsSelf { get; set; }
    }
}