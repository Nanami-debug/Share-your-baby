namespace Model
{
    // 员工实体类
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        // 分店ID
        public int BranchID { get; set; }

        // 角色
        public string Role { get; set; }

        // 员工姓名
        public string EmployeeName { get; set; }

        // 密码
        public string EmployeePassword { get; set; }

        // 联系电话
        public string ContactNumber { get; set; }

        // 销售金额
        public decimal SalesAmount { get; set; }
    }
}