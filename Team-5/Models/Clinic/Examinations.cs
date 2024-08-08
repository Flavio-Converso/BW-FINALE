using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models.Clinic
{
    public class Examinations
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdExamination { get; set; }

        [Required(ErrorMessage = "La data è obbligatoria.")]
        public required DateTime ExaminationDate { get; set; }

        [Required(ErrorMessage = "La tipologia di visita è obbligatoria.")]
        [StringLength(50)]
        public required string ExaminationName { get; set; }//Tipologia di visita

        [Required(ErrorMessage = "Il trattamento è obbligatorio.")]
        [StringLength(50)]
        public required string Treatment { get; set; }

        //RIFERIMENTI EF

        [Required(ErrorMessage = "Seleziona l'animale.")]
        public int AnimalId { get; set; }

       
        [ForeignKey(nameof(AnimalId))]
        
        public required Animals Animal { get; set; }
    }
}
