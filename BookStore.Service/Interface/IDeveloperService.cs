using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Domain.PartnerDomain;

namespace BookStore.Service.Interface
{
    public interface IDeveloperService
    {
        List<Developer> GetDevelopers();
        Developer GetDeveloperById(Guid? id);
        Developer CreateNewDeveloper(Developer developer);
        Developer UpdateDeveloper(Developer developer);
        Developer DeleteDeveloper(Guid id);
    }
}
