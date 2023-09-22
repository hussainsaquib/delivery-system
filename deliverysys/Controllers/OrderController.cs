using deliverysys.Models;
using deliverysys.provider;
using Microsoft.AspNetCore.Mvc;

namespace deliverysys.Controllers
{
    [Route("orders")]
    public class OrderController : Controller
    {
        private readonly OrderProvider _orderProvider;

        public OrderController(OrderProvider orderProvider)
        {
            _orderProvider = orderProvider;
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] OrderRequest req)
        {
            var response = _orderProvider.CreateOrder(req);
            return Ok(response);
        }

        [HttpPost("assign-driver")]
        public IActionResult AssignDriverToOrder(int orderId)
        {
            _orderProvider.AssignDriverToOrder(orderId);
            return Ok();
        }

        [HttpGet("available-drivers")]
        public IActionResult GetAllAvailableDrivers()
        {
            var drivers = _orderProvider.GetAllAvailableDrivers();
            return Ok(drivers);
        }
    }

}
