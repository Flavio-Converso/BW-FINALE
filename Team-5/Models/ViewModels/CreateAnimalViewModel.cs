using System.ComponentModel.DataAnnotations;
using Team_5.Models.Clinic;

namespace Team_5.Models.ViewModels
{
    public class CreateAnimalViewModel
    {
        [Required(ErrorMessage="Il nome dell'animale è obbligatorio.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Il nome può contenere solo lettere e spazi.")]
        [StringLength(50)]
        public required string Name { get; set; }

        [Required(ErrorMessage ="La data di registrazione è obbligatoria.")]
        public required DateTime RegistrationDate { get; set; }

        [Required(ErrorMessage ="La data di nascita è obbligatoria.")]
        public DateTime BirthDate { get; set; } = DateTime.MinValue;

        [StringLength(16)]
        
        public int NumMicrochip { get; set; }

        public IFormFile? Image { get; set; }

        [Required(ErrorMessage ="Il colore dell'animale è obbligatorio.")]
        [StringLength(20)]
        public required string Color { get; set; }

        public int? OwnerId { get; set; }

        [Required(ErrorMessage ="La razza dell'animale è obbligatoria.")]
        public int SelectedBreedId { get; set; }

       
        public int? SelectedOwnerId { get; set; }

        public List<Breeds>? Breeds { get; set; } = [];
        public List<Owners>? Owners { get; set; } = [];
    }
}
