using deliverysys.Models;
using deliverysys.repository;

namespace deliverysys.provider
{
    public class OrderProvider
    {
        private readonly OrderRepository _orderRepository;

        public OrderProvider(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderResponse CreateOrder(OrderRequest req)
        {
            var orderId = _orderRepository.InsertOrder(req);
            return new OrderResponse
            {
                OrderID = orderId,
                CustomerName = req.CustomerName,
                OrderDetails = req.OrderDetails,
                OrderStatus = "Pending"
            };
        }

        public void AssignDriverToOrder(int orderId)
        {
            var availableDrivers = _orderRepository.GetAvailableDrivers().ToList();
            if (availableDrivers.Any())
            {
                var driver = availableDrivers.First(); // Assign the first available driver.
                _orderRepository.UpdateOrderWithDriver(orderId, driver.DriverID);
            }
            // Handle scenarios where no drivers are available.
        }

        public IEnumerable<AvailableDriverResponse> GetAllAvailableDrivers()
        {
            return _orderRepository.GetAvailableDrivers();
        }
    }

}
