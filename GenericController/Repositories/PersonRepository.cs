using GenericController.Data;
using GenericController.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericController.Repositories
{
    public interface IPersonRepository : IGenericRepository<Person>
    {

    }
    public class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
