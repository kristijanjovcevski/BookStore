using BookStore.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.Domain
{
    public class Order : BaseEntity
    {
        public string userId { get; set; }
        public BookStoreApplicationUser Owner { get; set; }
        public IEnumerable<BookInOrder> BooksInOrder { get; set; }
    }
}
