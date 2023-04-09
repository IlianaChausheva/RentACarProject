using Microsoft.AspNetCore.Identity;
using System.Data;

namespace RentACarProject.Data.Models
{
    public class Users : IdentityUser
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PIN { get; set; }

    }
}
