using BookStore.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.DTO
{
    public class ShoppingCartDTO
    {
        public List<BookInShoppingCart>? AllBooks { get; set; }
        public double TotalPrice { get; set; }
    }
}
