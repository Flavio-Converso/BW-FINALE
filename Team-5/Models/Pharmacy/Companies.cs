using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models.Pharmacy
{
    public class Companies
    {
        [Required(ErrorMessage = "Seleziona un'azienda farmaceutica.")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCompany { get; set; }
        [Required(ErrorMessage = "Il nome dell' azienda è obbligatorio.")]
        [StringLength(50)]
        public required string CompanyName { get; set; }
        [Required(ErrorMessage = "Il numero di cellulare è obbligatorio.")]
        [StringLength(15)]
        public required string PhoneNumber { get; set; }
        [Required(ErrorMessage = "L'indirizzo è obbligatorio.")]
        [StringLength(50)]
        public required string Address { get; set; }

        public List<Products> Products { get; set; } = [];

    }
}
