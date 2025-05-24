namespace Model
{
    [Table("SerialNumber")]
    public class SerialNumber
    {
        [Key, Required]
        public int BranchID { get; set; }

        [Key, Required]
        public int Year { get; set; }

        public string SN { get; set; }
    }
}