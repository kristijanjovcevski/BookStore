using BookStore.Domain.Domain;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IUserRepository _userRepository;
    
        public BookService(IRepository<Book> BookRepository, IUserRepository userRepository)
        {
            _bookRepository = BookRepository;
            _userRepository = userRepository;
        }

        public Book CreateNewBook(string userId, Book book)
        {
            var createdBy = _userRepository.Get(userId);
            return _bookRepository.Insert(book);
        }

        public Book DeleteBook(Guid id)
        {
            var book = this.GetBookById(id);
            return _bookRepository.Delete(book);
        }

        public Book GetBookById(Guid? id)
        {
            return _bookRepository.Get(id);
        }

        public List<Book> GetBooks()
        {
            return _bookRepository.GetAll().ToList();
        }

     
        public Book UpdateBook(Book book)
        {
            return _bookRepository.Update(book);
        }

    }
}
