namespace CMC.Models.Order
{
    public class CartItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Currency { get; set; }
    }
}
