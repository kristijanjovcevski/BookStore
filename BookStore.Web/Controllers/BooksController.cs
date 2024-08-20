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
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BookStore.Domain.DTO;

namespace BookStore.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookService _bookService;

        private readonly IShoppingCartService _shoppingCartService; 
        
        

        public BooksController(IBookService bookService, IShoppingCartService shoppingCartService)
        {
            this._bookService = bookService;
            this._shoppingCartService = shoppingCartService;
        }


        // GET: Books
        public IActionResult Index()
        {
           return View(_bookService.GetBooks());
        }

        // GET: Books/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create([Bind("Title,ISBN,BookImage,AuthorFullName,PublisherName,Price,PublishedDate,Id")] Book book)
        {
            if (ModelState.IsValid)
            {

                var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
                book.Id = Guid.NewGuid();
                _bookService.CreateNewBook(loggedInUser, book);
                
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        [Authorize]
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Edit(Guid id, [Bind("Title,ISBN,BookImage,AuthorFullName,PublisherName,Price,PublishedDate,Id")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookService.UpdateBook(book);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        [Authorize]
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var book = _bookService.GetBookById(id);
            if (book != null)
            {
                _bookService.DeleteBook(id);
            }

            return RedirectToAction(nameof(Index));
        }

        public bool BookExists(Guid id)
        {
            Book book = _bookService.GetBookById(id);
            if (book != null)
                return true;
            return false;
        }

        public IActionResult AddBookToCart(Guid id)
        {
            var result = _shoppingCartService.getBookInfo(id);
            if (result != null)
            {
                return View(result);
            }
            return View();
        }

        [HttpPost]
        public IActionResult AddBookToCart(AddToCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _shoppingCartService.AddBookToShoppingCart(userId, model);
            if (result != null)
            {
                return RedirectToAction("Index", "ShoppingCarts");
            }
            else
            {
                return View(model);
            }
        }
    }
}
