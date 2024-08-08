using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models.Auth
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class Users
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdUser { get; set; }


        [StringLength(50, MinimumLength = 3, ErrorMessage = "L'username deve essere di 3-50 caratteri!")]
        [Required(ErrorMessage = "Inserisci un Username!")]
        public required string Username { get; set; }

        [DataType(DataType.Password)]
        [StringLength(255, ErrorMessage = "La password può avere max 255 caratteri!")]
        [Required(ErrorMessage = "Inserisci una Password!")]
        public required string Password { get; set; }

        [EmailAddress(ErrorMessage = "Inserisci un indirizzo email valido!")]
        [StringLength(50, ErrorMessage = "L'email può avere max 50 caratteri!")]
        [Required(ErrorMessage = "Inserisci una Email!")]
        public required string Email { get; set; }

        //RIFERIMENTI EF
        [Required]
        public required List<Roles> Roles { get; set; } = [];

    }
}
