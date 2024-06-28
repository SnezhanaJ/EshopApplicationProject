using Eshop.Domain.Identity;

namespace Eshop.Domain.Domain
{
    public class ShoppingCart : BaseEntity
    {
        public virtual ICollection<ProductInShoppingCart>?  Products { get; set; }
        public string? OwnerId { get; set; }
        public virtual EShopApplicationUser? Owner { get; set; }
    }
}
