namespace Model
{
    // 服装实体类
    [Table("Clothing")]
    public class Clothing
    {
        [Key]
        public int ClothingID { get; set; }

        // 服装名称
        public string ClothingName { get; set; }

        // 分店ID
        public int BranchID { get; set; }

        // 价格
        public decimal Price { get; set; }

        // 库存数量
        public int StockQuantity { get; set; }

        // 图片（Base64编码）
        public string ImageBase64 { get; set; }

        [NotMapped]
        public int BuyQty { get; set; }

        [NotMapped]
        public int EmployeeID { get; set; }

        [NotMapped]
        public string EmployeeName { get; set; }

        [NotMapped]
        public string BranchName { get; set; }
    }
}