using System.ComponentModel.DataAnnotations;

namespace EscalaSegurancaAPI.DTOs;

public class PolicialDTO
{

    public int PolicialId { get; set; }

    [Required]
    [StringLength(11)]
    [MinLength(11)]
    [MaxLength(11)]
    public string? CPF { get; set; }

    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }

    [Required]
    [StringLength(20)]
    public string? Telefone { get; set; }
}
