using Eshop.Domain.Domain;
using Eshop.Domain.DTO;
using Eshop.Repository.Interface;
using Eshop.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ProductInOrder> _productInOrderRepository;
        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, IRepository<Order> orderRepository, IRepository<ProductInShoppingCart> productInShoppingCartRepository, IUserRepository userRepository, IRepository<Product> productRepository, IRepository<ProductInOrder> productInOrderRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _productInOrderRepository = productInOrderRepository;
            _orderRepository = orderRepository;
            _productInOrderRepository = productInOrderRepository;
        }

        public bool deleteProductFromShoppingCart(string userId, Guid productId)
        {
            var user = _userRepository.Get(userId);
            var shoppingCart = user.ShoppingCart;

            var product_to_delete= shoppingCart.Products.Where(p => p.ProductId == productId).FirstOrDefault();
            
            shoppingCart.Products.Remove(product_to_delete);
            _shoppingCartRepository.Update(shoppingCart);
            return true;
        }

        public ShoppingCartDto getShoppingCartInfo(string userId)
        {
            var user = _userRepository.Get(userId);
            var shoppingCart = user.ShoppingCart;
            var allProducts = shoppingCart.Products.ToList();
            var totalPrice = 0.0;
            foreach (var product in allProducts)
            {
                totalPrice += Double.Round((double)(product.Quantity * product.Product.Price), 2);
            }
            ShoppingCartDto model = new ShoppingCartDto
            {
                AllProducts = allProducts,
                TotalPrice = totalPrice,
            };
            return model;
        }

        public bool order(string userId)
        {
            var user = _userRepository.Get(userId);
            var shoppingCart = user.ShoppingCart;

            var order = new Order
            {
                OrderOwner = user,
                OrderOwnerId = userId,
            };

            _orderRepository.Insert(order);


            var productInOrders = shoppingCart?.Products?.Select(z => new ProductInOrder
            {
                UserOrder = order,
                OrderId = (Guid)order.Id,
                ProductId = z.ProductId,
                SelectedProduct = z.Product,
                Quantity = z.Quantity,
            }).ToList();

            foreach(var product in productInOrders)
            {
                _productInOrderRepository.Insert(product);
            }
            shoppingCart.Products.Clear();
            _shoppingCartRepository.Update(shoppingCart);
            return true;
        }
    }
}
