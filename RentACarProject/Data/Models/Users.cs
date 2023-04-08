using Microsoft.AspNetCore.Identity;
using System.Data;

namespace RentACarProject.Data.Models
{
    public class Users : IdentityUser
    {
        public Users()
        {
            Rentals=new List<Rental>();
        }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PIN { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }    
    }
}
