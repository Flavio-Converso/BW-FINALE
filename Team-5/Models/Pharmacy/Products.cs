using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Team_5.Models.Pharmacy
{
    public class Products
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduct { get; set; }
        [Required]
        [StringLength(50)]
        public required string ProductName { get; set; }
        [Required]
        [StringLength(50)]
        public required string Type { get; set; }
        [Required]
        [StringLength(50)]
        public required string Use { get; set; }
        [Required]
        public required int Quantity { get; set; }
        [Required]
        public required bool Availability { get; set; }

        [Required]
        public required Companies Company { get; set; }

        public Drawers? Drawers { get; set; }
        public List<Orders> Order { get; set; } = [];

    }
}
