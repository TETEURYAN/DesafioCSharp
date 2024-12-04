using System;
using System.Collections.Generic;
using System.Linq;
using Desafio_3._1.Models;
using Desafio_3._1.Data;
using Microsoft.EntityFrameworkCore;

namespace Desafio_3._1.DAOs
{
    public class AgendaDAO
    {
        private readonly ApplicationDbContext _context;

        public AgendaDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddAgendamento(Agenda agendamento)
        {
            if (agendamento == null)
                throw new ArgumentNullException(nameof(agendamento));

            _context.Agendas.Add(agendamento);
            _context.SaveChanges();
        }

        public void RemoveAgendamento(int id)
        {
            var agendamento = _context.Agendas.Find(id);
            if (agendamento != null)
            {
                _context.Agendas.Remove(agendamento);
                _context.SaveChanges();
            }
        }

        public Agenda GetAgendamento(int id)
        {
            return _context.Agendas
                .Include(a => a.Paciente)
                .FirstOrDefault(a => a.Id == id);
        }

        public List<Agenda> GetAgendamentos(DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            var query = _context.Agendas.AsQueryable();

            if (dataInicio.HasValue)
                query = query.Where(a => a.DataConsulta >= dataInicio.Value);

            if (dataFim.HasValue)
                query = query.Where(a => a.DataConsulta <= dataFim.Value);

            return query.Include(a => a.Paciente).OrderBy(a => a.DataConsulta).ThenBy(a => a.HoraInicio).ToList();
        }

        public List<Agenda> GetAgendamentosFuturosPorCPF(string cpf)
        {
            return _context.Agendas
                .Where(a => a.CPF == cpf && a.DataConsulta > DateTime.Now)
                .Include(a => a.Paciente)
                .OrderBy(a => a.DataConsulta)
                .ThenBy(a => a.HoraInicio)
                .ToList();
        }
    }
}
