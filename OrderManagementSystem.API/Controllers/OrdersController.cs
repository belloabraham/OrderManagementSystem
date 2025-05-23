using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Request;
using OrderManagementSystem.Domain.Response;

namespace OrderManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService, IOrderItemsService orderItemService) : ControllerBase
    {
        
        // GET: api/orders?pageNumber=1&pageSize=20
        [HttpGet]
        public async Task<ActionResult<List<OrderResponse>>> GetOrders([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var orders = await orderService.GetOrdersAsync(pageNumber, pageSize);
            return Ok(orders);
        }
        
        // GET: api/orders/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderResponse>> GetOrderById([FromRoute] Guid id)
        {
            var order = await orderService.GetByIdAsync(id);
            return order is not null ? Ok(order) : NotFound();
        }
        
        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<OrderCreateRequest>> CreateOrder([FromBody] OrderCreateRequest orderCreateRequest)
        {
            var createdOrder = await orderService.AddAsync(orderCreateRequest);
            return  CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }
        
        //PUT /api/orders/{id}/status
        [HttpPut("{id:guid}/status")]
        public async Task<ActionResult> UpdateOrderStatus([FromRoute] Guid id, [FromBody] OrderStatusUpdateRequest orderStatusUpdateRequest)
        {
            var result = await orderService.UpdateOrderStatusAsync(id, orderStatusUpdateRequest.StatusId);
            return result > 0 ? NoContent() : NotFound();
        }
        
        //GET /api/orders/{id}/items 
        [HttpGet("{id:guid}/items")]
        public async Task<ActionResult<List<OrderItemResponse>>> GetItemsByOrderId([FromRoute] Guid id)
        {
            var result = await orderItemService.GetByOrderIdAsync(id);
            return Ok(result);
        }
        
    }
}