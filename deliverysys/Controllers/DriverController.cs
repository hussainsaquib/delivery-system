using deliverysys.repository;
using Microsoft.AspNetCore.Mvc;

namespace deliverysys.Controllers
{
    public class DriverController : Controller
    {
        private readonly OrderRepository _orderRepository;

        public DriverController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            var drivers = _orderRepository.GetAvailableDrivers();
            return View(drivers);
        }
    }
}
