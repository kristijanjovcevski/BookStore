using BookStore.Domain.Domain;
using BookStore.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCart AddBookToShoppingCart(string userId, AddToCartDTO model);
        AddToCartDTO getBookInfo(Guid id);
        ShoppingCartDTO getShoppingCartDetails(string userId);
        Boolean deleteFromShoppingCart(string userId, Guid? Id);
    }
}
