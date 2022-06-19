using System;
using CMC.Models;
using CMC.Models.Order;
using CMC.Services.Interface;
using MediatR;

namespace CMC.Commands.Order
{
    public sealed class CreateOrderCommand : IRequest<Result<CreateOrderResponse>>
    {
        public CreateOrderRequest OrderCheckoutRequest { get; set; }

        public sealed class Handler : CommandBaseController<CreateOrderCommand, Result<CreateOrderResponse>>
        {
            private readonly IOrderService _orderService;
            public Handler(IOrderService orderService) // : base (configuration)
            {
                _orderService = orderService;
            }

            protected override Result<CreateOrderResponse> Handle(CreateOrderCommand request)
            {
                try
                {
                    var validateCartItemsResult = _orderService.ValidateOrderItems(request.OrderCheckoutRequest.CartItems);
                    if (!validateCartItemsResult.Success)
                        return Result.Fail<CreateOrderResponse>(validateCartItemsResult.Error, validateCartItemsResult.NotFoundToModify);

                    var orderResult = _orderService.Create(request.OrderCheckoutRequest);
                    if (!orderResult.Success)
                        return Result.Fail<CreateOrderResponse>(orderResult.Error);

                    return Result.OK(orderResult.Value);
                }
                catch (Exception e)
                {
                    // TODO: log exception here...
                    return Result.Fail<CreateOrderResponse>("Oops! something went wrong.");
                }
            }
        }
    }
}
