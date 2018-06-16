using System;
using LocadoraAPI.Model;

namespace LocadoraAPI.Data
{
    public class FilmeRepository : EntityBaseRepository<Filme>
    {
        public FilmeRepository(Contexto context) : base(context) { }
    }
}
