using System.ComponentModel.DataAnnotations;

namespace Team_5.Models.Clinic
{
    public class CreateAnimalViewModel
    {
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

        public int? OwnerId { get; set; }

        [Required]
        public int SelectedBreedId { get; set; }

        // Optionally, include a list of breeds if needed for the view
        public List<Breeds>? Breeds { get; set; } = [];
    }
}
