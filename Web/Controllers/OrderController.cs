using System.Collections.Generic;
using System.Threading.Tasks;
using CMC.Commands.Order;
using CMC.Models.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> NewOrder(CreateOrderRequest orderCheckoutRequest) =>
            (await _mediator.Send(new CreateOrderCommand {OrderCheckoutRequest = orderCheckoutRequest}))
            .BuildResponse();

        [HttpPost("cost")]
        public async Task<IActionResult> CalculateCost(IEnumerable<CartItemDto> cartItems) =>
            (await _mediator.Send(new CalculateOrderCostCommand { CartItems = cartItems }))
            .BuildResponse();
    }
}
