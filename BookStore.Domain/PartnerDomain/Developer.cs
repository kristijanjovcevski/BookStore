using BookStore.Domain.Domain;

namespace BookStore.Domain.PartnerDomain
{
    public class Developer : BaseEntity
    {
        public string? DevName { get; set; }
        public string? DevDesc { get; set; }
        public DateTime YearFormed { get; set; }

        public ICollection<Game>? Games { get; set; }
    }
}
