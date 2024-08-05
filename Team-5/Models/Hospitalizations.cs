using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models
{
    public class Hospitalizations//ricovero
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdHospitalization { get; set; }

        [Required]
        public required bool IsHospitalized { get; set; }

        public DateTime HospDate { get; set; }

        //RIFERIMENTI EF
        public int AnimalId { get; set; }

        [ForeignKey(nameof(AnimalId))]
        public required Animals Animal { get; set; }

    }
}
