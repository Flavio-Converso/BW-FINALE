using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models
{
    public class Examinations
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdExamination { get; set; }

        [Required]
        public required DateTime ExaminationDate { get; set; }

        [Required]
        [StringLength(50)]
        public required string ExaminationName { get; set; }//Tipologia di visita

        [Required]
        [StringLength(50)]
        public required string Treatment { get; set; }

        //RIFERIMENTI EF
        public int AnimalId { get; set; }

        [ForeignKey(nameof(AnimalId))]
        public required Animals Animal { get; set; }
    }
}
