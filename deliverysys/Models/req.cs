namespace deliverysys.Models
{
    public class OrderRequest
    {
        public string? CustomerName { get; set; }
        public string? OrderDetails { get; set; }
    }

    public class AssignDriverRequest
    {
        public int OrderID { get; set; }
        public int DriverID { get; set; }
    }
    public class DatabaseSettings
    {
        public string? ConnectionString { get; set; }
    }


}
