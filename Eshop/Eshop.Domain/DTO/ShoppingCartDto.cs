﻿using Eshop.Domain.Domain;

namespace Eshop.Domain.DTO
{
    public class ShoppingCartDto
    {
        public List<ProductInShoppingCart> AllProducts { get; set; }
        public double TotalPrice { get; set; }
    }
}
