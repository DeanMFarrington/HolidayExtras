using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreAPI.Models
{
    public class User
    {
        public Guid Id;
        protected string _email;
        protected string _givenName;
        protected string _familyName;
        public DateTime Created;

        public string Email
        {
            get { return _email; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                _email = value;
            }
        }

        public string GivenName
        {
            get { return _givenName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                _givenName = value;
            }
        }

        public string FamilyName
        {
            get { return _familyName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    return;

                _familyName = value;
            }
        }

        public User(Guid id, string email, string givenName, string familyName, DateTime created)
        {
            this.Id = id;
            this.Email = email;
            this.GivenName = givenName;
            this.FamilyName = familyName;
            this.Created = created;
        }

        public string UserString()
        {
            return (Id.ToString() + "," + Email + "," + GivenName + "," + FamilyName + "," + Created.ToString());
        }

        public string[] UserStringArray()
        {
            return new string[] { Id.ToString(), Email, GivenName, FamilyName, Created.ToString() };
        }
    }
}
