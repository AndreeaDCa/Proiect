using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect.Models
{
    public class MakeupArtist
    {
        public int ID { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Numele trebuie sa inceapa cu majuscula (ex. Popescu)")]
        [StringLength(30, MinimumLength = 3)]
        public string LastName { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Prenumele trebuie sa inceapa cu majuscula (ex. Ana)")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Experiența trebuie să fie de minim 1 an.")]
        public int Experience { get; set; }
       
        public int? ServiceID { get; set; }
        public Service? Service { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ServiceCost { get; set; }

        [NotMapped]

        [Display(Name = "Full Name")]
        public string? FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        public List<Review> Reviews { get; set; }
        public List<Product> Products { get; set; }

    }
}
