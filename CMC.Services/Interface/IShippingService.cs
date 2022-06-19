using CMC.Models;

namespace CMC.Services.Interface
{
    public interface IShippingService
    {
        public double GetShippingCost(double orderTotal);
        public Result<double> GetShippingCost(double orderTotal, string inCurrency);
    }
}
