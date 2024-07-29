namespace EscalaSegurancaAPI.Filters;
    public class PoliciaisFiltro : PagedParameters
    {
        public string? Nome { get; set; }
        public string? CPF { get; set; }
    }

