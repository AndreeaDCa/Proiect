using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Proiect.Models
{
    public class Review
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Rating is required.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Comentariul este obligatorie.")]
        [StringLength(1000, ErrorMessage = "Comentariul nu poate avea mai mult de 1000 de caractere.")]
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Display(Name = "Makeup Artist")]
        [ForeignKey("MakeupArtistID")]
        public int? MakeupArtistID { get; set; }
        public MakeupArtist? MakeupArtist { get; set; }
    }
}
