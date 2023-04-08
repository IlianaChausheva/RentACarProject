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
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int PassengerSeats { get; set; }
        public string Description { get; set; }
        public double PricePerDay { get; set; }
        public virtual ICollection<Rental> Rentals { get; set; }
    }
}
