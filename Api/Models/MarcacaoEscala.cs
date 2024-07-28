using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using EscalaSegurancaAPI.Models;

[Table("Marcacoes")]
public class MarcacaoEscala
    {
        [Key]
        public int MarcacaoEscalaId { get; set; }
        
        public int PolicialId { get; set; }

        [JsonIgnore]
        public Policial? Policial { get; set; }
        
        public int EscalaId { get; set; }

        [JsonIgnore]
        public Escala? Escala { get; set; }
        
        public int LocalId { get; set; }
        
        public bool? Inativado { get; set; }
        
        [JsonIgnore]
        public Local? Local { get; set; }
    }