using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EscalaSegurancaAPI.Models;

[Table("Locais")]
public class Local
{
    [Key]
    public int LocalId { get; set; }
    
    [Required]
    public string? Nome { get; set; }

    [Required]
    public string? Descricao { get; set; }

    public bool? Inativado { get; set; }
    
    [JsonIgnore]
    public ICollection<MarcacaoEscala>? MarcacoesEscala { get; set; }

}