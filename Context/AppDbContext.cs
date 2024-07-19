using EscalaSegurancaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base ( options )
    {

    }
        public DbSet<Escala>? Escalas { get; set; }
        public DbSet<Local>? Locais { get; set; }
        public DbSet<Policial>? Policiais { get; set; }
        public DbSet<MarcacaoEscala>? Marcacoes { get; set; }
}
