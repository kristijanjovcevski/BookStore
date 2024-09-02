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
using Stripe;
using BookStore.Domain;
using Microsoft.Extensions.Options;

namespace BookStore.Web.Controllers
{
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;



        public ShoppingCartsController(IShoppingCartService shoppingCartService, IOptions<StripeSettings> stripeSettings)
        {
            _shoppingCartService = shoppingCartService;
        }

        // GET: ShoppingCarts
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)??null;
            return View(_shoppingCartService.getShoppingCartDetails(userId??""));
        }

        // GET: ShoppingCarts/Delete/5
        public IActionResult DeleteBookFromShoppingCart(Guid? Id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? null;
            var result = _shoppingCartService.deleteFromShoppingCart(userId, Id);
            return RedirectToAction("Index", "ShoppingCarts");
        }

        public IActionResult Order()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = _shoppingCartService.order(userId);
            //if (result == true)
            return RedirectToAction("Index", "ShoppingCarts");


        }
        public IActionResult PayOrder(string stripeEmail, string stripeToken)
        {
            StripeConfiguration.ApiKey = "sk_test_51Pt8eTKo60Jd7cgAqqZZhbhEmfRc0kCtkpkX8Z0pGBTeSqQm1Pzty0XI9iJKrdASd2hfGUETS1V1q94E2gAQprVl00SenLMPN8";
            var customerService = new CustomerService();
            var chargeService = new ChargeService();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier ?? "");

            var order = this._shoppingCartService.getShoppingCartDetails(userId);

            var customer = customerService.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = chargeService.Create(new ChargeCreateOptions
            {
                Amount = (Convert.ToInt32(order.TotalPrice) * 100),
                Description = "BookStore Application Payment",
                Currency = "mkd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                this.Order();
                return RedirectToAction("SuccessPayment");

            }
            else
            {
                return RedirectToAction("NotsuccessPayment");
            }
        }

        public IActionResult SuccessPayment()
        {
            return View();
        }

        public IActionResult NotsuccessPayment()
        {
            return View();
        }

    }
}
