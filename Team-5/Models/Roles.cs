using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models
{
    [Index(nameof(Name), IsUnique = true)]

    public class Roles
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdRole { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        public List<Users>Users { get; set; } = [];
    }
}