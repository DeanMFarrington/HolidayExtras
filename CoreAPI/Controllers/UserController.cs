using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreAPI.Helpers;
using CoreAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static readonly List<User> _users = new List<User>
        {
            new User(new Guid("940aa289-95f4-4424-b183-a9599f1ddb1d"), "DeanFarrington@outlook.com", "Dean", "Farrington", DateTime.Now),
            new User(new Guid("c3be39f8-1b08-4e38-a3ea-67e29d673b54"), "MadeUpEmail1@live.co.uk", "Fake", "User", DateTime.Now),
            new User(new Guid("5bc171dd-951a-45d0-a4bc-19f53dcf44f6"), "AnotherMadeUpAccount@helloworld.com", "Test", "Account", DateTime.Now),
            new User(new Guid("0471ef4f-8ab9-4b6a-93dc-c9f3044fa21b"), "MadeUpEmail2@aol.com", "Admin", "Admin", DateTime.Now),
        };

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string[] accumulatedString = new string[_users.Count];

            for (int i = 0; i < _users.Count; i++)
            {
                accumulatedString[i] = _users[i].UserString();
            }

            return accumulatedString;
        }

        // GET: api/User/940aa289-95f4-4424-b183-a9599f1ddb1d
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            if (id is null)
                return "Invalid request: No id provided.";

            Guid idToGuid;
            if(!Guid.TryParse(id, out idToGuid))
            {
                return "Invalid request: Invalid Id format.";
            }

            for (int i = 0; i < _users.Count; i++)
            {
                if(_users[i].Id == idToGuid)
                {
                    return _users[i].UserString();
                }
            }

            return "User not found.";
        }

        // PUT: api/User/940aa289-95f4-4424-b183-a9599f1ddb1e/blankemail@email.com/Dean/Farrington
        [HttpPut("{id}/{email}/{givenName}/{familyName}")]
        public void Put(string id, string email, string givenName, string familyName)
        {
            if (id is null || email is null || givenName is null || familyName is null)
                return;

            email = Formatter.FormatEmail(email);
            givenName = Formatter.FormatName(givenName);
            familyName = Formatter.FormatName(familyName);

            Guid idToGuid;
            if (!Guid.TryParse(id, out idToGuid))
            {
                return;
            }

            bool userExists = false;
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Id == idToGuid)
                {
                    userExists = true;
                    _users[i].Email = email;
                    _users[i].GivenName = givenName;
                    _users[i].FamilyName = familyName;
                }
            }

            if(!userExists)
            {
                _users.Add(new User(Guid.NewGuid(), email, givenName, familyName, DateTime.Now));
            }
        }

        // PUT: api/User/testemailer@email.com/Dean/Farrington
        [HttpPut("{email}/{givenName}/{familyName}")]
        public void Put(string email, string givenName, string familyName)
        {
            if (email is null || givenName is null || familyName is null)
                return;

            email = Formatter.FormatEmail(email);
            givenName = Formatter.FormatName(givenName);
            familyName = Formatter.FormatName(familyName);

            bool userExists = false;
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Email == email)
                {
                    userExists = true;
                    _users[i].GivenName = givenName;
                    _users[i].FamilyName = familyName;
                }
            }

            if (!userExists)
            {
                _users.Add(new User(Guid.NewGuid(), email, givenName, familyName, DateTime.Now));
            }
        }

        // DELETE: api/User/0471ef4f-8ab9-4b6a-93dc-c9f3044fa21b
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            if (id is null)
                return;

            Guid idToGuid;
            if (!Guid.TryParse(id, out idToGuid))
            {
                return;
            }

            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Id == idToGuid)
                {
                    _users.RemoveAt(i);
                }
            }
        }
    }
}
