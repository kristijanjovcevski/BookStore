using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.DTO
{
    public class AddToCartDTO
    {
        public Guid SelectedBookId { get; set; }
        public string? SelectedBookName { get; set; }
        public int Quantity { get; set; }
    }
}
