namespace Form.Models
{
    public class OrderViewModel
    {
        public string Id { get; set; }
        public string goodId { get; set; }
        public string orderStatus { get; set; }
        public string buyerId { get; set; }
        public string sellerId { get; set; }
        public string address { get; set; }
        public byte[] img { get; set; }
        public string des { get; set; }
        public string transactionType { get; set; }
    }
}