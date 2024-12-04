using Desafio_3._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_3._1.Controllers
{
    public class AgendaController
    {
        private readonly DAOs.AgendaDAO _agendaDAO;

        public AgendaController()
        {
            _agendaDAO = new DAOs.AgendaDAO();
        }

        public bool AgendarConsulta(string cpf, DateTime dataConsulta, TimeSpan horaInicio, TimeSpan horaFim)
        {
            if (horaFim <= horaInicio)
            {
                Console.WriteLine("Erro: A hora final deve ser maior que a hora inicial.");
                return false;
            }

            if (horaInicio < TimeSpan.FromHours(8) || horaFim > TimeSpan.FromHours(19))
            {
                Console.WriteLine("Erro: O horário de funcionamento é das 08:00 às 19:00.");
                return false;
            }

            if ((horaInicio.Minutes % 15 != 0) || (horaFim.Minutes % 15 != 0))
            {
                Console.WriteLine("Erro: As horas devem ser múltiplas de 15 minutos (08:00, 08:15, 08:30, etc.).");
                return false;
            }

            var agendamentos = _agendaDAO.GetAgendamentosPorData(dataConsulta);
            foreach (var agendamento in agendamentos)
            {
                if ((horaInicio < agendamento.HoraFim && horaFim > agendamento.HoraInicio))
                {
                    Console.WriteLine("Erro: Já existe um agendamento nesse horário.");
                    return false;
                }
            }

            _agendaDAO.AddAgendamento(agendamento: new Agenda
            {
                CPF = cpf,
                DataConsulta = dataConsulta,
                HoraInicio = horaInicio,
                HoraFim = horaFim
            });

            Console.WriteLine("Consulta agendada com sucesso!");
            return true;
        }

        public bool CancelarAgendamento(string cpf, DateTime dataConsulta, TimeSpan horaInicio)
        {
            var agendamento = _agendaDAO.GetAgendamento(cpf, dataConsulta, horaInicio);

            if (agendamento == null)
            {
                Console.WriteLine("Erro: Agendamento não encontrado.");
                return false;
            }

            if (dataConsulta < DateTime.Now.Date || (dataConsulta == DateTime.Now.Date && horaInicio <= DateTime.Now.TimeOfDay))
            {
                Console.WriteLine("Erro: Apenas agendamentos futuros podem ser cancelados.");
                return false;
            }

            _agendaDAO.RemoveAgendamento(agendamento);
            Console.WriteLine("Agendamento cancelado com sucesso!");
            return true;
        }

        public List<Agenda> ListarAgenda(DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            return _agendaDAO.GetAgendamentos(dataInicio, dataFim);
        }
    }
}
