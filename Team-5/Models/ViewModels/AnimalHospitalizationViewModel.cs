using Team_5.Models.Clinic;

namespace Team_5.Models.ViewModels
{
    public class AnimalHospitalizationViewModel
    {
        public Animals Animal { get; set; }
        public Hospitalizations Hospitalization { get; set; }

        public int? IdBreed { get; set; }
        public IFormFile? Image { get; set; }

    }
}
