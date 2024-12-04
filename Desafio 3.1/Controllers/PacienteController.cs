using Desafio_3._1.DAOs;
using Desafio_3._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_3._1.Controllers
{
    public class PacienteController
    {
        private readonly PacienteDAO _pacienteDAO;
        private readonly AgendaDAO _agendaDAO;

        public PacienteController()
        {
            _pacienteDAO = new PacienteDAO();
            _agendaDAO = new AgendaDAO();
        }

        public bool CadastrarPaciente(string cpf, string nome, DateTime dataNascimento)
        {
            if (_pacienteDAO.GetPaciente(cpf) != null)
            {
                Console.WriteLine("Erro: CPF já cadastrado.");
                return false;
            }

            if (nome.Length < 5)
            {
                Console.WriteLine("Erro: O nome deve ter pelo menos 5 caracteres.");
                return false;
            }

            if ((DateTime.Now.Year - dataNascimento.Year) < 13)
            {
                Console.WriteLine("Erro: O paciente deve ter pelo menos 13 anos.");
                return false;
            }

            _pacienteDAO.AddPaciente(new Paciente
            {
                CPF = cpf,
                Nome = nome,
                DataNascimento = dataNascimento
            });

            Console.WriteLine("Paciente cadastrado com sucesso!");
            return true;
        }

        public bool ExcluirPaciente(string cpf)
        {
            var paciente = _pacienteDAO.GetPaciente(cpf);

            if (paciente == null)
            {
                Console.WriteLine("Erro: Paciente não cadastrado.");
                return false;
            }

            var agendamentosFuturos = _agendaDAO.GetAgendamentosFuturosPorCPF(cpf);

            if (agendamentosFuturos.Count > 0)
            {
                Console.WriteLine("Erro: O paciente possui consultas futuras e não pode ser excluído.");
                return false;
            }

            _agendaDAO.RemoverAgendamentosPassadosPorCPF(cpf);
            _pacienteDAO.RemovePaciente(cpf);

            Console.WriteLine("Paciente excluído com sucesso!");
            return true;
        }

        public void ListarPacientes()
        {
            var pacientes = _pacienteDAO.GetAllPacientes();

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("CPF          Nome                           Dt.Nasc.  Idade");
            Console.WriteLine("------------------------------------------------------------");

            foreach (var paciente in pacientes)
            {
                Console.WriteLine($"{paciente.CPF,-12} {paciente.Nome,-30} {paciente.DataNascimento:dd/MM/yyyy,-10} {paciente.Idade,-3}");

                var agendamentoFuturo = _agendaDAO.GetAgendamentoFuturoPorCPF(paciente.CPF);
                if (agendamentoFuturo != null)
                {
                    Console.WriteLine($"  Agendado para: {agendamentoFuturo.DataConsulta:dd/MM/yyyy}");
                    Console.WriteLine($"  {agendamentoFuturo.HoraInicio:hh\\:mm} às {agendamentoFuturo.HoraFim:hh\\:mm}");
                }
            }

            Console.WriteLine("------------------------------------------------------------");
        }
    }
}
