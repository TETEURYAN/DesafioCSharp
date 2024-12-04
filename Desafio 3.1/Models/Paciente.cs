using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio_3._1.Models
{
    public class Paciente
    {
        [Key]
        public string CPF { get; set; } 

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [Required]
        public DateTime DataNascimento { get; set; }

        [NotMapped]
        public int Idade => DateTime.Now.Year - DataNascimento.Year -
                            (DateTime.Now.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);
    }
}
