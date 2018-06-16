using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocadoraAPI.Model
{
    public class Locacao
    {
        public int Id { get; set; }
        [EnsureOneElement(ErrorMessage = "No mínimo 1 filme")]
        public List<Filme> Filmes { get; set; }
        [Required(ErrorMessage = "Nome não pode ser branco.")]
        public string CpfCliente { get; set; }
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataDeCriacao { get; set; }
    }

    public class EnsureOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as IList;
            if (list != null)
            {
                return list.Count > 0;
            }
            return false;
        }
    }
}