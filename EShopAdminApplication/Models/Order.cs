namespace EShopAdminApplication.Models
{
    public class Order
    {

        public Guid Id { get; set; }
        public string OrderOwnerId { get; set; }
        public EShopApplicationUser OrderOwner { get; set; }
        public virtual ICollection<ProductInOrder> ProductInOrder { get; set; }
    }
}
