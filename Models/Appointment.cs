using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;

namespace Proiect.Models
{
    public class Appointment
    {
        public int ID { get; set; }

        [Required]

        [Display(Name = "Makeup Artist")]

        public int? MakeupArtistID { get; set; }

        [ForeignKey("MakeupArtistID")]

        public MakeupArtist? MakeupArtist { get; set; }
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Numele este obligatoriu.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Prenumele trebuie sa inceapa cu majuscula (ex. Ana)")]
        [StringLength(30, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Prenumele este obligatoriu.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Numele trebuie sa inceapa cu majuscula (ex. Popescu)")]
        [StringLength(30, MinimumLength = 3)]

        public string LastName { get; set; }

        [Required(ErrorMessage = "Emailul este obligatoriu.")]
        [RegularExpression(@"^.+@.+\..+$", ErrorMessage = "Introduceți o adresă de email validă.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Numărul de telefon este obligatoriu.")]
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Telefonul trebuie sa fie de forma '0722-123-123' sau '0722.123.123' sau '0722 123 123'")]

        public string Phone { get; set; }

        public bool IsOccupied { get; set; }
    }
}
