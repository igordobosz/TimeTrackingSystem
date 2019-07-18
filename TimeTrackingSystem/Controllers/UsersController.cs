using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TimeTrackingSystem.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private static User[] _users = new[]
        {
            new User()
            {
                ID = 1,
                UserName = "Igor Dobosz",
                Password = "IDobosz",
                Email = "igordobosz@gmail.com"
            }
        };
        // GET api/<controller>/5
        [HttpGet]
        public User[] Get()
        {
            return _users;
        }

        // POST api/<controller>
        [HttpPost]
        public User Post([FromBody]User user)
        {
            if (_users.Any(u => u.Email.Equals(user.Email) && u.Password.Equals(user.Password)))
            {
                _users[0].Token = "TEST-TOKEN";
                return _users[0];
            }
            else
                return new User();
        }
        public class User
        {
            public int ID { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Token { get; set; }
        }
    }
}
