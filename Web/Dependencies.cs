using CMC.Commands.Order;
using CMC.ReadStack;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Web
{
    public static class Dependencies
    {
        public static IServiceCollection RegisterRequestHandlers(this IServiceCollection services)
        {
            return services
                .AddMediatR(typeof(GetProductsQuery).Assembly, typeof(CalculateOrderCostCommand).Assembly);
        }
    }
}
