namespace Model
{
    // 分店实体类
    [Table("Branch")]
    public class Branch
    {
        [Key]
        public int BranchID { get; set; }

        // 分店名称
        public string BranchName { get; set; }

        // 地址
        public string Address { get; set; }

        // 联系电话
        public string ContactNumber { get; set; }
    }
}