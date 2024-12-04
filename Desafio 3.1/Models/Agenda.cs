using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_3._1.Models
{
    public class Agenda
    {
        public string CPF { get; set; }
        public DateTime DataConsulta { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }

        public Agenda(string CPF, DateTime dataConsulta, TimeSpan horaInicio, TimeSpan horaFim)
        {
            if (string.IsNullOrWhiteSpace(CPF) || CPF.Length != 11)
                throw new ArgumentException("CPF inválido.", nameof(CPF));

            if (dataConsulta.Date < DateTime.Now.Date)
                throw new ArgumentException("A data da consulta deve ser futura.", nameof(dataConsulta));

            if (horaInicio >= horaFim)
                throw new ArgumentException("A hora de início deve ser menor que a hora de término.", nameof(horaInicio));

            if (horaInicio.Minutes % 15 != 0 || horaFim.Minutes % 15 != 0)
                throw new ArgumentException("Os horários devem estar alinhados aos intervalos de 15 minutos.");

            if (horaInicio < TimeSpan.FromHours(8) || horaFim > TimeSpan.FromHours(19))
                throw new ArgumentException("Horário fora do período de funcionamento (08:00 às 19:00).");

            this.CPF = CPF;
            DataConsulta = dataConsulta;
            HoraInicio = horaInicio;
            HoraFim = horaFim;
        }

        public TimeSpan GetDuracao()
        {
            return HoraFim - HoraInicio;
        }

        public override string ToString()
        {
            return $"Consulta: {DataConsulta:dd/MM/yyyy}, {HoraInicio:hh\\:mm} - {HoraFim:hh\\:mm} (CPF: {CPF})";
        }
    }
}
