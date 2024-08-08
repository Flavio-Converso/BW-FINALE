using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models.Auth
{
    [Index(nameof(Name), IsUnique = true)]

    public class Roles
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRole { get; set; }

        [Required(ErrorMessage = "Inserisci il nome per il nuovo ruolo!")]
        [StringLength(50)]
        public required string Name { get; set; }

        //RIFERIMENTI EF
        [Required]
        public List<Users> Users { get; set; } = [];
    }
}