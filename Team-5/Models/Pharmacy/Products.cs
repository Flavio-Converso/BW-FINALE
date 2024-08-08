using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Team_5.Models.Pharmacy
{
    public class Products
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdProduct { get; set; }
        [Required(ErrorMessage = "Il nome del prodotto è obbligatorio.")]
        [StringLength(50)]
        public required string ProductName { get; set; }
        [Required(ErrorMessage = "Il tipo di prodotto è obbligatorio.")]
        [StringLength(50)]
        public required string Type { get; set; }
        [Required(ErrorMessage = "L'uso del prodotto è obbligatorio.")]
        [StringLength(50)]
        public required string Use { get; set; }
        [Required(ErrorMessage = "La quantità è obbligatoria.")]
        public required int Quantity { get; set; }
        [Required]
        public required bool Availability { get; set; }

        [Required]
        public required Companies Company { get; set; }

        public Drawers? Drawers { get; set; } = null;
        [JsonIgnore]
        public List<Orders> Order { get; set; } = [];

    }
}
