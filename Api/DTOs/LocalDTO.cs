using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EscalaSegurancaAPI.DTOs;

public class LocalDTO
{
    public int LocalId { get; set; }
    
    [Required]
    public string? Nome { get; set; }

    [Required]
    public string? Descricao { get; set; }

}
