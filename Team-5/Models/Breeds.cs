using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models
{
    public class Breeds
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdBreed { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }



    }
}
