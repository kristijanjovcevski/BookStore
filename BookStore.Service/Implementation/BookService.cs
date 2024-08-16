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
    
        public BookService(IRepository<Book> BookRepository)
        {
            _bookRepository = BookRepository;
           
        }

        public Book CreateNewBook(Book book)
        {
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
