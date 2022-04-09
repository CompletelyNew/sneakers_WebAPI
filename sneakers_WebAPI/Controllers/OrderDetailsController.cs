using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sneakers_WebAPI.Models;

namespace sneakers_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        OrderDetailsModel model = new OrderDetailsModel();

        [HttpPost]
        [Route("Place an Order")]
        public IActionResult AddOrder(OrderDetailsModel newOrder)
        {
            try
            {
                return Created(" ", model.AddOrder(newOrder));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("Cancel an Order")]
        public IActionResult DeleteOrder(int customerId)
        {
            try
            {
                return Accepted(model.CancelOrder(customerId));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("List Order History")]
        public IActionResult OrdersList()
        {
            return Ok(model.GetOrderHistory());
        }

        [HttpGet]
        [Route("Order History By Customer")]
        public IActionResult GetOrders(int Id)
        {
            try
            {
                return Ok(model.GetOrder(Id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
