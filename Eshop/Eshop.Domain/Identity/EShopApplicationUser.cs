using Eshop.Domain.Domain;
using Microsoft.AspNetCore.Identity;

namespace Eshop.Domain.Identity
{
    public class EShopApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public string? Address { get; set; }
        public virtual ShoppingCart? ShoppingCart { get; set; }


    }
}
