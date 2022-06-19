using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMC.ReadStack;
using MediatR;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts() =>
            (await _mediator.Send(new GetProductsQuery()))
                .BuildResponse();

        [HttpGet("products/{currency}")]
        public async Task<IActionResult> GetProductsByCurrency(string currency) =>
            (await _mediator.Send(new GetProductsPriceByGivenCurrencyQuery{Currency = currency}))
            .BuildResponse();

    }
}
