using LocadoraAPI.Data;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoraAPI.Model
{
    [Table("Filme")]
    public class Filme : IEntityBase
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Nome não pode ser branco.")]
        public string Nome { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DateDeCriacao { get; set; }
        public bool Ativo { get; set; }
        public int GeneroID { get; set; }

        [ForeignKey("GeneroID")]
        [JsonIgnore]
        public virtual Genero Genero { get; set; }
    }
}


