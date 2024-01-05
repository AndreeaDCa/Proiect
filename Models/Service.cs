using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Service
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Numele serviciului este obligatoriu.")]
        [Display(Name = "Service Name")]
        [StringLength(100, ErrorMessage = "Numele serviciului nu poate avea mai mult de 100 de caractere.")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "Costul serviciului este obligatoriu.")]
        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 9999.99, ErrorMessage = "Costul serviciului trebuie să fie între 0.01 și 9999.99.")]
        public decimal Cost { get; set; }

        // Proprietate de navigare pentru a accesa artiștii de make-up asociați cu acest serviciu
        public ICollection<MakeupArtist>? MakeupArtists { get; set; }
    }
}
