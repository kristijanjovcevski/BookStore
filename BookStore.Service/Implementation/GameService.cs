using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.PartnerDomain;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;

namespace BookStore.Service.Implementation
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> repository;

        public GameService(IRepository<Game> repository)
        {
            this.repository = repository;
        }

        public Game CreateNewGame(Game game)
        {
            return this.repository.Insert(game);
        }

        public Game DeleteGame(Guid id)
        {
            return this.repository.Delete(repository.Get(id));
        }

        public Game GetGameById(Guid? id)
        {
            return this.repository.Get(id);
        }

        public List<Game> GetGames()
        {
            return repository.GetAll().ToList();
        }

        public Game UpdateGame(Game game)
        {
            return repository.Update(game);
        }
    }
}
