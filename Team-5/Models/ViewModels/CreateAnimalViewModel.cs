using System.ComponentModel.DataAnnotations;
using Team_5.Models.Clinic;

namespace Team_5.Models.ViewModels
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

        public IFormFile? Image { get; set; }

        [Required]
        [StringLength(20)]
        public required string Color { get; set; }

        public int? OwnerId { get; set; }

        [Required]
        public int SelectedBreedId { get; set; }

       
        public int? SelectedOwnerId { get; set; }

        public List<Breeds>? Breeds { get; set; } = [];
        public List<Owners>? Owners { get; set; } = [];
    }
}
