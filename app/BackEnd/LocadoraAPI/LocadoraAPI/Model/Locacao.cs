using System;
using System.Collections.Generic;

namespace LocadoraAPI.Model
{
    public class Locacao
    {
        public int Id { get; set; }
        public List<Filme> Filmes { get; set; }
        public string CpfCliente { get; set; }
        public DateTime DataDeCriacao { get; set; }
    }
}