using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Team_5.Models.Clinic;

namespace Team_5.Models.Pharmacy
{
    [Index(nameof(PrescriptionNumber), IsUnique = true)]

    public class Orders
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdOrder { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Order quantity must be at least 1.")]
        public required int OrderQuantity { get; set; }
        [Required]
        public required DateTime OrderDate { get; set; }

        [StringLength(50)]
        public string? PrescriptionNumber { get; set; }
        [Required]
        public required int IdProduct {  get; set; }
        [ForeignKey(nameof(IdProduct))]
        public required Products Product { get; set; }
        [Required]
        public required int IdOwner { get; set; }
        [ForeignKey(nameof(IdOwner))]
        public required Owners Owner { get; set; }

    }
}
