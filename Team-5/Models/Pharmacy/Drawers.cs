using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models.Pharmacy
{
    public class Drawers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "INSERISCI!")]
        public int IdDrawer { get; set; }

        [Required]
        public int LockerId { get; set; }

        [ForeignKey(nameof(LockerId))]
        public Lockers Lockers { get; set; }
    }
}