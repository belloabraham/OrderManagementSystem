using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Domain.Interfaces;
using OrderManagementSystem.Domain.Responses;

namespace OrderManagementSystem.API.Controllers
{
    /// <summary>
    /// Manages operations related to order items.
    /// </summary>
    [Route("api/order-items")]
    [ApiController]
    public class OrderItemsController(IOrderItemsService orderItemService) : ControllerBase
    {
        /// <summary>
        /// Retrieves all order items in the system.
        /// </summary>
        /// <returns>A list of order item records.</returns>
        /// <response code="200">Returns the full list of order items.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<OrderItemResponse>))]
        public async Task<ActionResult<List<OrderItemResponse>>> GetAll()
        {
            var result = await orderItemService.GetAllAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retrieves a specific order item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the order item.</param>
        /// <returns>The order item if found; otherwise, a 404 response.</returns>
        /// <response code="200">Returns the requested order item.</response>
        /// <response code="404">If the order item is not found.</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderItemResponse))]
        public async Task<ActionResult<OrderItemResponse>> GetById(Guid id)
        {
            var result = await orderItemService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }
    }
}