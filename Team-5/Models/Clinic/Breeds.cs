using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models.Clinic
{
    [Index(nameof(Name), IsUnique = true)]
    public class Breeds
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBreed { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

    }
}
