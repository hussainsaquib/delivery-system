namespace deliverysys.Models
{
    public class OrderResponse
    {
        public int OrderID { get; set; }
        public string? CustomerName { get; set; }
        public string? OrderDetails { get; set; }
        public string? OrderStatus { get; set; }
        public int? DriverID { get; set; }
    }

    public class AvailableDriverResponse
    {
        public int DriverID { get; set; }
        public string? DriverName { get; set; }
    }
}
