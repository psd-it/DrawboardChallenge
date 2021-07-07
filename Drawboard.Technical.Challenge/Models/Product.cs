namespace Drawboard.Technical.Challenge.Models
{
    public class Product
    {
        public char ProductCode { get; set; }
        public decimal UnitPrice { get; set; }
        public ProductPack Pack { get; set; } = default;
    }
}
