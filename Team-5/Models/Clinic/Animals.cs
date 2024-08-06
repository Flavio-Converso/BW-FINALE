using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models.Clinic
{
    public class Animals
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdAnimal { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        public required DateTime RegistrationDate { get; set; }

        [Required]
        public DateTime BirthDate { get; set; } = DateTime.MinValue;

        [StringLength(16)]
        public string? NumMicrochip { get; set; }

        public byte[]? Image { get; set; }

        [Required]
        [StringLength(20)]
        public required string Color { get; set; }

        //RIFERIMENTI EF
        public int? OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public Owners? Owner { get; set; }

        public List<Hospitalizations>? Hospitalization { get; set; } = new();

        public List<Examinations>? Examination { get; set; } = new();

        [Required]
        public required Breeds Breed { get; set; }
    }
}
