using System;
using LocadoraAPI.Model;

namespace LocadoraAPI.Data
{
    public class GeneroRepository : EntityBaseRepository<Genero>
    {
        public GeneroRepository(Contexto contexto) : base(contexto) { }

    }
}
