using LocadoraAPI.Model;
using System.Data.Entity;

namespace LocadoraAPI.Data
{
    public class Contexto : DbContext
    {
        public DbSet<Filme> Filme { get; set; }
        public DbSet<Genero> Genero { get; set; }
    }
}