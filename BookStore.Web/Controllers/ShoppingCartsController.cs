using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookStore.Domain.Domain;
using BookStore.Repository;
using BookStore.Service.Interface;
using System.Security.Claims;

namespace BookStore.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;
        
        

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)??null;
            return View(_shoppingCartService.getShoppingCartDetails(userId??""));
        }

        
      


       


   
        

        // GET: ShoppingCarts/Delete/5
        public async Task<IActionResult> DeleteBookFromShoppingCart(Guid? Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
            var result = _shoppingCartService.deleteFromShoppingCart(userId, Id);
            return RedirectToAction("Index", "ShoppingCarts");
        }
    }
}
