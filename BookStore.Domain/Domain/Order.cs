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
        public BookStoreApplicationUser? User { get; set; }
        public virtual ICollection<BookInOrder>? BookInOrders { get; set; }
    }
}
