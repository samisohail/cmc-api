using System.Threading.Tasks;
using CMC.ReadStack;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Web.Helpers;

namespace Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("products/{currency}")]
        public async Task<IActionResult> GetProductsByCurrency(string currency) =>
            (await _mediator.Send(new GetProductsQuery{Currency = currency}))
            .BuildResponse();

    }
}
