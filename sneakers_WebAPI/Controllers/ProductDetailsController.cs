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
    public class ProductDetailsController : ControllerBase
    {
        ProductDetailsModel model = new ProductDetailsModel();

        [HttpGet]
        [Route("Search For Products")]
        public IActionResult SearchProductList(int Id)
        {
            try
            {
                return Ok(model.GetProduct(Id));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("Print Product List")]
        public IActionResult ProductList()
        {
            return Ok(model.GetProductList());
        }
    }
}
