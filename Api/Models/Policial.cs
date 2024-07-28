using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EscalaSegurancaAPI.Models;

[Table("Policiais")]
public class Policial
{
    [Key]
    public int PolicialId { get; set; }
    
    [Required]
    [StringLength(11)]
    public string? CPF { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(20)]
    public string? Telefone { get; set; }

    public bool? Inativado { get; set; }

    [JsonIgnore]
    public ICollection<MarcacaoEscala>? MarcacoesEscala { get; set; }

}