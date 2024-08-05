using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models.Pharmacy
{
    public class Companies
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompany { get; set; }
        [Required]
        [StringLength(50)]
        public required string CompanyName { get; set; }
        [Required]
        [StringLength(15)]
        public required string PhoneNumber { get; set; }
        [Required]
        [StringLength(50)]
        public required string Address { get; set; }

        public List<Products> Products { get; set; } = [];

    }
}
