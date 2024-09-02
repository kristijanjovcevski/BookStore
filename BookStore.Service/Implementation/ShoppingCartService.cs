using BookStore.Domain.Domain;
using BookStore.Domain.DTO;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace BookStore.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<Book> _bookRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<BookInOrder> _bookInOrderRepository;

        public ShoppingCartService(IUserRepository userRepository, IRepository<ShoppingCart> shoppingCartRepository, IRepository<Book> bookRepository, IRepository<Order> orderRepository, IRepository<BookInOrder> bookInOrderRepository)
        {
            _userRepository = userRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _bookInOrderRepository = bookInOrderRepository;
        }

        public ShoppingCart AddBookToShoppingCart(string userId, AddToCartDTO model)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);
                var userCart = loggedInUser?.UserCart;
                var selectedBook = _bookRepository.Get(model.SelectedBookId);
                if (selectedBook != null && userCart != null)
                {
                    userCart?.BookInShoppingCarts?.Add(new BookInShoppingCart
                    {
                        Book = selectedBook,
                        BookId = selectedBook.Id,
                        ShoppingCart = userCart,
                        ShoppingCartId = userCart.Id,
                        Quantity = model.Quantity,
                    });
                    return _shoppingCartRepository.Update(userCart);
                }
            }
            return null;
        }

        public bool deleteFromShoppingCart(string userId, Guid? Id)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);
                var book_to_delete = loggedInUser?.UserCart?.BookInShoppingCarts?.First(x => x.BookId == Id);
                loggedInUser?.UserCart?.BookInShoppingCarts?.Remove(book_to_delete);
                _shoppingCartRepository.Update(loggedInUser.UserCart);
                return true;    
                
            }
            return false;
        }

        public AddToCartDTO getBookInfo(Guid id)
        {
            var selectedBook = _bookRepository.Get(id);
            if (selectedBook != null)
            {
                var model = new AddToCartDTO
                {
                    SelectedBookName = selectedBook.Title,
                    SelectedBookId = selectedBook.Id,
                    Quantity = 1
                };
                return model;
            }
            return null;
        }

        public ShoppingCartDTO getShoppingCartDetails(string userId)
        {
            if (userId != null && !userId.IsNullOrEmpty()) 
            {
                var loggedInUser = _userRepository.Get(userId);
                var allBooks = loggedInUser?.UserCart?.BookInShoppingCarts?.ToList();
                var totalPrice = 0.0;
                foreach(var item in allBooks)
                {
                    totalPrice += Double.Round((item.Quantity * item.Book.Price), 2);
                }
                var model = new ShoppingCartDTO
                {
                    AllBooks = allBooks,
                    TotalPrice = totalPrice
                };
                return model;
            }
            return new ShoppingCartDTO
            {
                TotalPrice = 0.0,
                AllBooks = new List<BookInShoppingCart>()
            };
        }
        public bool order(string userId)
        {
            if (userId != null)
            {
                var loggedInUser = _userRepository.Get(userId);

                var userShoppingCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    OwnerId = userId,
                    Owner = loggedInUser
                };

                _orderRepository.Insert(order);

                List<BookInOrder> bookInOrder = new List<BookInOrder>();

                var lista = userShoppingCart?.BookInShoppingCarts?.Select(
                    x => new BookInOrder
                    {
                        Id = Guid.NewGuid(),
                        BookId = x.Book.Id,
                        Book = x.Book,
                        OrderId = order.Id,
                        Order = order,
                        Quantity = x.Quantity
                    }
                    ).ToList();


                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order conatins: ");

                for (int i = 1; i <= lista.Count(); i++)
                {
                    var currentItem = lista[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Book.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Book.Title + " with quantity of: " + currentItem.Quantity + " and price of: $" + currentItem.Book.Price);
                }

                sb.AppendLine("Total price for your order: " + totalPrice.ToString());

                bookInOrder.AddRange(lista);

                foreach (var book in bookInOrder)
                {
                    _bookInOrderRepository.Insert(book);
                }

                loggedInUser?.UserCart?.BookInShoppingCarts?.Clear();
                _userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
}
