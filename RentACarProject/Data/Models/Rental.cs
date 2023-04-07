using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentACarProject.Data.Models
{
    public class Rental
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int CarId { get; set; }
        public Car Car { get; set; }
        [Required]
        public string UserId { get; set; }
        public Users User { get; set; }
        [Required]
        public DateTime PickUpDate { get; set; }
        [Required]
        public DateTime DropOffDate { get; set; }
    }
}
