using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.Security.Claims;
using Eshop.Service.Interface;
using Eshop.Domain.DTO;


namespace Eshop.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }


        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
           
            ShoppingCartDto model = _shoppingCartService.getShoppingCartInfo(userId);

            return View(model);
        }


        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> DeleteProductFromShoppingCart(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result =_shoppingCartService.deleteProductFromShoppingCart(userId, (Guid)id);

            if(result)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            return RedirectToAction("Index", "ShoppingCarts");

        }

        public async Task<IActionResult> Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _shoppingCartService.order(userId);

            return RedirectToAction("Index", "ShoppingCarts");

        }
    }
}
