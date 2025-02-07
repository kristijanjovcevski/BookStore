using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.Domain;
using BookStore.Domain.PartnerDomain;

namespace BookStore.Service.Interface
{
    public interface IGameService
    {
        List<Game> GetGames();
        Game GetGameById(Guid? id);
        Game CreateNewGame(Game game);
        Game UpdateGame(Game game);
        Game DeleteGame(Guid id);
    }
}
