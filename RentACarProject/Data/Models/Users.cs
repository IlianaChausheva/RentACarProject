using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace RentACarProject.Data.Models
{
    public class Users : IdentityUser
    {
        public Users()
        {
            Rentals=new List<Rental>();
        }
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name cannot be shorter than two letters")]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name cannot be shorter than two letters")]
        public string LastName { get; set; }

        [StringLength(10, ErrorMessage = "The PIN must be 10 digits")]
        public string PIN { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }    
    }
}
