using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Response;

namespace OrderManagementSystem.API.Controllers
{
    [Route("api/order-items")]
    [ApiController]
    public class OrderItemsController(IOrderItemsService orderItemService) : ControllerBase
    {
        //GET /api/order-items 
        [HttpGet]
        public async Task<ActionResult<List<OrderItemResponse>>> GetAll()
        {
            var result = await orderItemService.GetAllAsync();
            return Ok(result);
        }

        //GET /api/order-items/{id} 
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderItemResponse>> GetById(Guid id)
        {
            var result = await orderItemService.GetByIdAsync(id);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        
    }
}
