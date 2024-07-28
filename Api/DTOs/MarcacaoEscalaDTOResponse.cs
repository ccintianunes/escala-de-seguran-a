using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EscalaSegurancaAPI.Models;

namespace EscalaSegurancaAPI.DTOs;

public class MarcacaoEscalaDTOResponse
{
    public MarcacaoEscalaDTOResponse(MarcacaoEscala marcacaoEscala){
        this.MarcacaoEscalaId = marcacaoEscala.MarcacaoEscalaId;
        this.PolicialId = marcacaoEscala.PolicialId;
        this.EscalaId = marcacaoEscala.EscalaId;
        this.LocalId = marcacaoEscala.LocalId;
    }
    public int MarcacaoEscalaId { get; set; }
        
    public int PolicialId { get; set; }
  
    public int EscalaId { get; set; }
   
    public int LocalId { get; set; }

    public string? PolicialNome { get; set; }

    public DateTime? DataHoraEntrada { get; set; }

    public DateTime? DataHoraSaida { get; set; }

    public string? LocalNome { get; set; }
}
