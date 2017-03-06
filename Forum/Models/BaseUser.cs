using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Forum.Models
{
    public class BaseUser : BaseObject
    {
        // Fields common to all user objects go in here
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RoleId { get; set; }
        public string ResetToken { get; set; }
        public string LoginToken { get; set; }
    }
}