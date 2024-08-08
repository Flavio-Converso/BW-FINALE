using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models.Pharmacy
{
    public class Lockers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLocker { get; set; }
        [Required]
        public int NumLocker { get; set; }

        public List<Drawers> Drawers { get; set; } = [];
    }
}
