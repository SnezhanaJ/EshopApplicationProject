using Eshop.Domain.Identity;

namespace Eshop.Domain.Domain

{
    public class Order : BaseEntity
    {
        public string OrderOwnerId { get; set; }    
        public virtual EShopApplicationUser OrderOwner { get; set; }
        public virtual ICollection<ProductInOrder> ProductInOrder { get; set; }

    }
}
