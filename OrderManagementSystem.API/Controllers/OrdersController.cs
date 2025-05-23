using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Domain.Entities;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Requests;
using OrderManagementSystem.Domain.Responses;

namespace OrderManagementSystem.API.Controllers
{
    /// <summary>
    /// Manages customer orders including creation, status updates, and retrieval.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService, IOrderItemsService orderItemService) : ControllerBase
    {
        
        /// <summary>
        /// Retrieves all orders with optional pagination.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A list of customer orders.</returns>
        /// <response code="200">Returns the list of orders.</response>
        /// <response code="400">If pagination parameters are invalid.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderResponse>))]
        public async Task<ActionResult<List<OrderResponse>>> GetOrders([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
        {
            var orders = await orderService.GetOrdersAsync(pageNumber, pageSize);
            return Ok(orders);
        }
        
        /// <summary>
        /// Retrieves a specific order by its ID.
        /// </summary>
        /// <param name="id">The unique ID of the order.</param>
        /// <returns>The order if found.</returns>
        /// <response code="200">Returns the order.</response>
        /// <response code="404">If the order is not found.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderResponse))]
        public async Task<ActionResult<OrderResponse>> GetOrderById([FromRoute] Guid id)
        {
            var order = await orderService.GetByIdAsync(id);
            return order is not null ? Ok(order) : NotFound();
        }
        
        /// <summary>
        /// Creates a new customer order.
        /// </summary>
        /// <param name="orderCreateRequest">The order creation details.</param>
        /// <returns>The created order details.</returns>
        /// <response code="201">Returns the newly created order.</response>
        /// <response code="400">If validation fails or request is malformed.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderCreateRequest))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderCreateRequest>> CreateOrder([FromBody] OrderCreateRequest orderCreateRequest)
        {
            var createdOrder = await orderService.AddAsync(orderCreateRequest);
            return  CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }
        
        
        /// <summary>
        /// Updates the status of an existing order.
        /// </summary>
        /// <param name="id">The ID of the order to update.</param>
        /// <param name="orderStatusUpdateRequest">The new status value.</param>
        /// <returns>No content on success.</returns>
        /// <response code="204">If the update is successful.</response>
        /// <response code="400">If the status is invalid or out of sequence.</response>
        /// <response code="404">If the order does not exist.</response>
        [HttpPut("{id:guid}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateOrderStatus([FromRoute] Guid id, [FromBody] OrderStatusUpdateRequest orderStatusUpdateRequest)
        {
            var result = await orderService.UpdateOrderStatusAsync(id, orderStatusUpdateRequest.StatusId);
            return result > 0 ? NoContent() : NotFound();
        }
        
        /// <summary>
        /// Retrieves all order items for a specific order.
        /// </summary>
        /// <param name="id">The order ID.</param>
        /// <returns>List of order items.</returns>
        /// <response code="200">Returns the list of order items.</response>
        [HttpGet("{id:guid}/items")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderItemResponse>))]
        public async Task<ActionResult<List<OrderItemResponse>>> GetItemsByOrderId([FromRoute] Guid id)
        {
            var result = await orderItemService.GetByOrderIdAsync(id);
            return Ok(result);
        }
        
    }
}