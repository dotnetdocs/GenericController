using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GenericController.Models;
using GenericController.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GenericController.Controllers
{
    public class UserController : GenericController<Person, IPersonRepository>
    {
        public UserController(IPersonRepository personRepository)
            : base(personRepository)
        {
        }
    }
}