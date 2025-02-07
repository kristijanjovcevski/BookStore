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
    public class DeveloperService : IDeveloperService
    {
        private readonly IRepository<Developer> _repository;

        public DeveloperService(IRepository<Developer> repository)
        {
            _repository = repository;
        }

        public Developer CreateNewDeveloper(Developer developer)
        {
            return this._repository.Insert(developer);
        }

        public Developer DeleteDeveloper(Guid id)
        {
            return this._repository.Delete(_repository.Get(id));
        }

        public Developer GetDeveloperById(Guid? id)
        {
            return this._repository.Get(id);
        }

        public List<Developer> GetDevelopers()
        {
            return this._repository.GetAll().ToList();
        }

        public Developer UpdateDeveloper(Developer developer)
        {
           return this._repository.Update(developer);
        }
    }
}
