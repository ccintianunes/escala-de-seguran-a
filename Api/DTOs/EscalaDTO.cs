using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EscalaSegurancaAPI.DTOs;

public class EscalaDTO
{
    public int EscalaId { get; set; }
    
    [Required]
    public DateTime DataHoraEntrada { get; set; }

    [Required]
    public DateTime DataHoraSaida { get; set; }

}
