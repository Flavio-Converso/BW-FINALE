using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Team_5.Models.Auth;

namespace Team_5.Models.Clinic
{
    [Index(nameof(CF), IsUnique = true)]
    public class Owners
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOwner { get; set; }

        [Required(ErrorMessage = "Il nome è obbligatorio.")]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Il cognome è obbligatorio.")]
        [StringLength(50)]
        public required string Surname { get; set; }

        [StringLength(15)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Il codice fiscale è obbligatorio.")]
        [StringLength(16)]
        public required string CF { get; set; }
        //RIFERIMENTI EF

        [Required(ErrorMessage = "Lo user è obbligatorio.")]
        public required Users User { get; set; }

    }
}
