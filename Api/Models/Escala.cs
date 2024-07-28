using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EscalaSegurancaAPI.Models;

[Table("Escalas")]
public class Escala
{
    [Key]
    public int EscalaId { get; set; }

    [Required]
    public DateTime DataHoraEntrada { get; set; }

    [Required]
    public DateTime DataHoraSaida { get; set; }

    public bool? Inativado { get; set; }

    [JsonIgnore]
    public ICollection<MarcacaoEscala>? MarcacoesEscala { get; set; }

}