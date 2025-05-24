namespace Model
{
    // 订单明细实体类
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int DetailID { get; set; }

        // 订单ID
        public int OrderID { get; set; }

        // 服装ID
        public int ClothingID { get; set; }

        // 数量
        public int Quantity { get; set; }

        // 单价
        public decimal UnitPrice { get; set; }
    }
}