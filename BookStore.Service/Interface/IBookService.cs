using BookStore.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interface
{
    public interface IBookService
    {

        List<Book> GetBooks();
        Book GetBookById(Guid? id);
        Book CreateNewBook(Book Book);
        Book UpdateBook(Book Book);
        Book DeleteBook(Guid id);
        

    }
}
