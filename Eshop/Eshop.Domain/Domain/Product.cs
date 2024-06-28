namespace Eshop.Domain.Domain
{
    public class Product : BaseEntity
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductImage { get; set; }
        public double? Price { get; set;}
        public int? Rating { get; set; }
        public virtual ICollection<ProductInShoppingCart>? ProductInShoppingCarts { get; set; }
        public virtual ICollection<ProductInOrder>? Orders { get; set; }
    }
}
