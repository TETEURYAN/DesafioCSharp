using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Desafio_3._1.Models
{
    public class Agenda
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        [ForeignKey("Paciente")]
        public string CPF { get; set; } 

        [Required]
        public DateTime DataConsulta { get; set; }

        [Required]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        public TimeSpan HoraFim { get; set; }

        public Paciente Paciente { get; set; }

        [NotMapped]
        public TimeSpan Duracao => HoraFim - HoraInicio;

        public override string ToString()
        {
            return $"Consulta: {DataConsulta:dd/MM/yyyy}, {HoraInicio:hh\\:mm} - {HoraFim:hh\\:mm} (CPF: {CPF})";
        }
    }
}
