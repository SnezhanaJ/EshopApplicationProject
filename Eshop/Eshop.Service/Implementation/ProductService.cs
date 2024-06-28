using Eshop.Domain.Domain;
using Eshop.Domain.DTO;
using Eshop.Repository.Interface;
using Eshop.Service.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        public ProductService(IRepository<Product> productRepository, IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
        }

        public void CreateNewProduct(Product p)
        {
            _productRepository.Insert(p);
        }

        public List<Product> GetAllProducts()
        {
           return _productRepository.GetAll().ToList();
        }
        public void UpdateExistingProduct(Product p)
        {
            _productRepository.Update(p);
        }
        public void DeleteProduct(Guid id)
        {
            var product = _productRepository.Get(id);
            _productRepository.Delete(product);
        }
        public Product GetDetailsForProduct(Guid? id)
        {
            var product = _productRepository.Get(id);
            return product;
        }
      
        public bool AddToShoppingCart(AddToShoppingCartDto item, string userId)
        {
            var user = _userRepository.Get(userId);
            if (user != null)
            {
                var userShoppingCart = user.ShoppingCart;
                var selectedProduct = _productRepository.Get(item.ProductId);

                if (userShoppingCart != null && selectedProduct !=null)
                {
                    userShoppingCart?.Products?.Add(new ProductInShoppingCart
                    {
                        Product = selectedProduct,
                        ProductId = (Guid)selectedProduct.Id,
                        Quantity = item.Quantity,
                        ShoppingCart = userShoppingCart,
                        ShoppingCartId = (Guid)userShoppingCart.Id,
                    });
                    _shoppingCartRepository.Update(userShoppingCart);
                    return true;
                }

            }
            return false;
        }

        public AddToShoppingCartDto GetShoppingCartInfo(Guid? id)
        {
            var product = _productRepository.Get(id);
            var model = new AddToShoppingCartDto
            {
                SelectedProduct = product,
                ProductId = (Guid)product.Id,
                Quantity = 1,
            };
            return model;
        }

     
    }
}
