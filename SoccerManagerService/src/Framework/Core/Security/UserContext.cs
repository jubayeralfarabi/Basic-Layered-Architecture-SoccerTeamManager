using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core
{
    public class UserContext
    {
        public UserContext(int userId, string firstName, string lastName, string email)
        {
            this.UserId = userId;
            this.FirstName = firstName; 
            this.LastName = lastName;
            this.Email = email;
        }

        public readonly int UserId;
        public readonly string FirstName;
        public readonly string LastName;
        public readonly string Email;
    }
}
