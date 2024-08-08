using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Team_5.Models.Clinic
{
    public class Animals
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAnimal { get; set; }

        [Required(ErrorMessage = "Il nome dell'animale è obbligatorio.")]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "La data di registrazione è obbligatoria.")] 
        public required DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage = "La data di nascita è obbligatoria.")]
        public DateTime BirthDate { get; set; } = DateTime.MinValue;

        [StringLength(16)]
        public int NumMicrochip { get; set; }

        public byte[]? Image { get; set; }

        [Required(ErrorMessage ="Il colore dell'animale è obbligatorio.")]
        [StringLength(20)]
        public required string Color { get; set; }

        //RIFERIMENTI EF
        public int? OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Owners? Owner { get; set; }

        public List<Hospitalizations>? Hospitalization { get; set; } = new();

        [JsonIgnore]
        public List<Examinations>? Examination { get; set; } = new();

        [Required]
        public Breeds Breed { get; set; }
    }
}
