using System.ComponentModel.DataAnnotations;

namespace BookStore.Domain.Domain
{
    public class Book : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string AuthorFullName { get; set; }


        [Required]
        public string PublisherName { get; set; }

        [Required]
        public float Price { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        public virtual ICollection<BookInShoppingCart>? BookInShoppingCarts { get; set; }

        public virtual ICollection<BookInOrder>? BookInOrders { get; set; }
    }
}
