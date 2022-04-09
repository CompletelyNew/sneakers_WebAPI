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
    public class SneakerDetailsController : ControllerBase
    {
        SneakerDetailsModel model = new SneakerDetailsModel();

        [HttpGet]
        [Route("List Customers")]
        public IActionResult CustomerList()
        {
            return Ok(model.GetCustomersList());
        }

        [HttpGet]
        [Route("Search For a Customer")]
        public IActionResult GetCustomerDetails(int Id)
        {
            try
            {
                return Ok(model.GetCustomers(Id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("Add a Customer")]
        public IActionResult AddCustomer(SneakerDetailsModel newCustomer)
        {
            try
            {
                return Created(" ", model.AddCustomer(newCustomer));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("Delete a Customer")]
        public IActionResult DeleteCustomer(int Id)
        {
            try
            {
                return Accepted(model.DeleteCustomer(Id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("Update a Customer Record")]
        public IActionResult UpdateCustomer(SneakerDetailsModel updates)
        {
            try
            {
                return Accepted(model.UpdateCustomer(updates));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
