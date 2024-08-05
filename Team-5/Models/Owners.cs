using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models
{
   
    public class Owners
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOwner { get; set; }
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }
        [Required]
        [StringLength(50)]
        public required string Surname { get; set; }
        
        public int PhoneNumber { get; set; }
        [Required]
        [StringLength(16)]
        public required string CF {  get; set; }

        [Required]
        public required Users User { get; set; }



    }
}
