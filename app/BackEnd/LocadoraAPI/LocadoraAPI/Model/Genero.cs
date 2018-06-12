using LocadoraAPI.Data;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraAPI.Model
{
    [Table("Genero")]
    public class Genero : IEntityBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome não pode ser branco.")]
        public string Nome { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataDeCriacao { get; set; }
        public bool Ativo { get; set; }
    }
}
