using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace NOR.Models
{
    public class RegistoUtilizador
    {
        [Key]
        public int RegistoId { get; set; }

        [Required]
        [StringLength(200)]
        public string? Nome { get; set; }

        [Required]
        [DisplayName("Válido")]
        [RegularExpression(("Ordinário|Especial|Super"))] // serve para validar os valores possível para preencher o campo (alternativa no controller)
        public string? Regime { get; set; } = "Ordinário";

        public bool Valido { get; set; } = false;
    }
}
