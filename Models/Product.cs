using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Product
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Tipul este obligatoriu.")]
        [StringLength(50, ErrorMessage = "Tipul nu poate avea mai mult de 50 de caractere.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Brand-ul este obligatoriu.")]
        [StringLength(50, ErrorMessage = "Marca nu poate avea mai mult de 50 de caractere.")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Descrierea este obligatorie.")]
        [StringLength(1000, ErrorMessage = "Descrierea nu poate avea mai mult de 1000 de caractere.")]
        public string Description { get; set; }

        [Display(Name = "Recommended By Artist")]
        [ForeignKey("MakeupArtistID")]
        public int? MakeupArtistID { get; set; }
        public MakeupArtist? MakeupArtist { get; set; }
    }
}
