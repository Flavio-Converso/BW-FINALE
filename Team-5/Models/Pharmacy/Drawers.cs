using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Team_5.Models.Pharmacy
{
    public class Drawers
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdDrawer { get; set; }

        [Required]
        public required int LockerId { get; set; }

        [ForeignKey(nameof(LockerId))]
        public required Lockers Lockers { get; set; }
    }
}