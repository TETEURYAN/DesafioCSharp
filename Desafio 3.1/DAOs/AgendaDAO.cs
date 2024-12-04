using Desafio_3._1.Models;
using Desafio_3._1.Singleton;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Desafio_3._1.DAOs
{
    public class AgendaDAO
    {
        private SqliteConnection _connection;

        public AgendaDAO()
        {
            _connection = DatabaseConnection.GetInstance().GetConnection();
        }

        // Adiciona um novo agendamento
        public void AddAgendamento(Agenda agendamento)
        {
            if (agendamento == null)
                throw new ArgumentNullException(nameof(agendamento));

            var query = "INSERT INTO Agendamentos (CPF, DataConsulta, HoraInicio, HoraFim) VALUES (@CPF, @DataConsulta, @HoraInicio, @HoraFim)";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@CPF", agendamento.CPF);
            cmd.Parameters.AddWithValue("@DataConsulta", agendamento.DataConsulta);
            cmd.Parameters.AddWithValue("@HoraInicio", agendamento.HoraInicio);
            cmd.Parameters.AddWithValue("@HoraFim", agendamento.HoraFim);
            cmd.ExecuteNonQuery();
        }

        public void RemoveAgendamento(Agenda agendamento)
        {
            if (agendamento == null)
                throw new ArgumentNullException(nameof(agendamento));

            var query = "DELETE FROM Agendamentos WHERE CPF = @CPF AND DataConsulta = @DataConsulta AND HoraInicio = @HoraInicio AND HoraFim = @HoraFim";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@CPF", agendamento.CPF);
            cmd.Parameters.AddWithValue("@DataConsulta", agendamento.DataConsulta);
            cmd.Parameters.AddWithValue("@HoraInicio", agendamento.HoraInicio);
            cmd.Parameters.AddWithValue("@HoraFim", agendamento.HoraFim);
            cmd.ExecuteNonQuery();
        }

        // Obtém um agendamento específico por CPF, data e hora de início
        public Agenda GetAgendamento(string cpf, DateTime dataConsulta, TimeSpan horaInicio)
        {
            var query = "SELECT * FROM Agendamentos WHERE CPF = @CPF AND DataConsulta = @DataConsulta AND HoraInicio = @HoraInicio";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@CPF", cpf);
            cmd.Parameters.AddWithValue("@DataConsulta", dataConsulta);
            cmd.Parameters.AddWithValue("@HoraInicio", horaInicio);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Agenda
                {
                    CPF = reader["CPF"].ToString(),
                    DataConsulta = DateTime.Parse(reader["DataConsulta"].ToString()),
                    HoraInicio = TimeSpan.Parse(reader["HoraInicio"].ToString()),
                    HoraFim = TimeSpan.Parse(reader["HoraFim"].ToString()) // Inclui HoraFim
                };
            }

            return null; 
        }

        public List<Agenda> GetAgendamentos(DateTime? dataInicio = null, DateTime? dataFim = null)
        {
            var query = "SELECT * FROM Agendamentos WHERE (@DataInicio IS NULL OR DataConsulta >= @DataInicio) AND (@DataFim IS NULL OR DataConsulta <= @DataFim) ORDER BY DataConsulta, HoraInicio";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@DataInicio", dataInicio.HasValue ? (object)dataInicio.Value : DBNull.Value);
            cmd.Parameters.AddWithValue("@DataFim", dataFim.HasValue ? (object)dataFim.Value : DBNull.Value);

            var agendamentos = new List<Agenda>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                agendamentos.Add(new Agenda
                {
                    CPF = reader["CPF"].ToString(),
                    DataConsulta = DateTime.Parse(reader["DataConsulta"].ToString()),
                    HoraInicio = TimeSpan.Parse(reader["HoraInicio"].ToString()),
                    HoraFim = TimeSpan.Parse(reader["HoraFim"].ToString()) // Inclui HoraFim
                });
            }
            return agendamentos;
        }

        // Obtém agendamentos por data específica
        public List<Agenda> GetAgendamentosPorData(DateTime dataConsulta)
        {
            var query = "SELECT * FROM Agendamentos WHERE DataConsulta = @DataConsulta ORDER BY HoraInicio";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@DataConsulta", dataConsulta);

            var agendamentos = new List<Agenda>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                agendamentos.Add(new Agenda
                {
                    CPF = reader["CPF"].ToString(),
                    DataConsulta = DateTime.Parse(reader["DataConsulta"].ToString()),
                    HoraInicio = TimeSpan.Parse(reader["HoraInicio"].ToString()),
                    HoraFim = TimeSpan.Parse(reader["HoraFim"].ToString()) // Inclui HoraFim
                });
            }
            return agendamentos;
        }

        // Obtém todos os agendamentos futuros de um paciente por CPF
        public List<Agenda> GetAgendamentosFuturosPorCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF inválido.", nameof(cpf));

            var query = "SELECT * FROM Agendamentos WHERE CPF = @CPF AND DataConsulta > @DataAtual ORDER BY DataConsulta, HoraInicio";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@CPF", cpf);
            cmd.Parameters.AddWithValue("@DataAtual", DateTime.Now.Date);

            var agendamentos = new List<Agenda>();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                agendamentos.Add(new Agenda
                {
                    CPF = reader["CPF"].ToString(),
                    DataConsulta = DateTime.Parse(reader["DataConsulta"].ToString()),
                    HoraInicio = TimeSpan.Parse(reader["HoraInicio"].ToString()),
                    HoraFim = TimeSpan.Parse(reader["HoraFim"].ToString()) // Inclui HoraFim
                });
            }
            return agendamentos;
        }

        // Obtém o primeiro agendamento futuro de um paciente por CPF
        public Agenda GetAgendamentoFuturoPorCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF inválido.", nameof(cpf));

            var query = "SELECT * FROM Agendamentos WHERE CPF = @CPF AND DataConsulta > @DataAtual LIMIT 1";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@CPF", cpf);
            cmd.Parameters.AddWithValue("@DataAtual", DateTime.Now.Date);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Agenda
                {
                    CPF = reader["CPF"].ToString(),
                    DataConsulta = DateTime.Parse(reader["DataConsulta"].ToString()),
                    HoraInicio = TimeSpan.Parse(reader["HoraInicio"].ToString()),
                    HoraFim = TimeSpan.Parse(reader["HoraFim"].ToString()) // Inclui HoraFim
                };
            }

            return null; // Se não encontrado, retorna null
        }

        public void RemoverAgendamentosPassadosPorCPF(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new ArgumentException("CPF inválido.", nameof(cpf));

            var query = "DELETE FROM Agendamentos WHERE CPF = @CPF AND DataConsulta <= @DataAtual";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@CPF", cpf);
            cmd.Parameters.AddWithValue("@DataAtual", DateTime.Now.Date);

            cmd.ExecuteNonQuery();
        }
    }
}
