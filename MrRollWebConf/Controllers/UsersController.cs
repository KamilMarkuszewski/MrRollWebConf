using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MrRollWebConf.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MrRollWebConf.Controllers
{
    public class UsersController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var list = new List<User>
            {
                new User {Id = 1, Name = "User1"},
                new User {Id = 2, Name = "User2"},
                new User {Id = 3, Name = "User3"}
            };
            return View(list);
        }
    }
}
