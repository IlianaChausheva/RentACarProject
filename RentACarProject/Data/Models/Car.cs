using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RentACarProject.Data.Models
{
    public class Car
    {

        public Car()
        {
            Rentals= new List<Rental>();   
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Car brand cannot be shorter than two letters")]
        public string Brand { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Car model cannot be shorter than two letters")]
        public string Model { get; set; }

        [Range(1990, 2024, ErrorMessage = "The year must be after 1950 and cannot be after 2024")]
        public int Year { get; set; }

        public int PassengerSeats { get; set; }

        public string Description { get; set; }

        [Range(1, 300, ErrorMessage = "The price cannot be less than 1 and more than 300")]
        public double PricePerDay { get; set; }

        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
