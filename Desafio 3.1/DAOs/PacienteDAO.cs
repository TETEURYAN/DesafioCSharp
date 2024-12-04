using Desafio_3._1.Models;
using Desafio_3._1.Singleton;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace Desafio_3._1.DAOs
{
    public class PacienteDAO
    {
        private SqliteConnection _connection;

        public PacienteDAO()
        {
            _connection = DatabaseConnection.GetInstance().GetConnection();
        }

        public void AddPaciente(Paciente paciente)
        {
            var query = "INSERT INTO Pacientes (CPF, Nome, DataNascimento) VALUES (@CPF, @Nome, @DataNascimento)";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@CPF", paciente.CPF);
            cmd.Parameters.AddWithValue("@Nome", paciente.Nome);
            cmd.Parameters.AddWithValue("@DataNascimento", paciente.DataNascimento);
            cmd.ExecuteNonQuery();
        }

        public List<Paciente> GetAllPacientes()
        {
            var pacientes = new List<Paciente>();
            var query = "SELECT * FROM Pacientes";
            using var cmd = new SqliteCommand(query, _connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                pacientes.Add(new Paciente
                {
                    CPF = reader["CPF"].ToString(),
                    Nome = reader["Nome"].ToString(),
                    DataNascimento = DateTime.Parse(reader["DataNascimento"].ToString())
                });
            }
            return pacientes;
        }

        public Paciente GetPaciente(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                throw new ArgumentException("CPF inválido.", nameof(cpf));

            var query = "SELECT * FROM Pacientes WHERE CPF = @CPF";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@CPF", cpf);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Paciente
                {
                    CPF = reader["CPF"].ToString(),
                    Nome = reader["Nome"].ToString(),
                    DataNascimento = DateTime.Parse(reader["DataNascimento"].ToString())
                };
            }

            throw new InvalidOperationException("Paciente não encontrado.");
        }

        public void RemovePaciente(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf) || cpf.Length != 11)
                throw new ArgumentException("CPF inválido.", nameof(cpf));

            // Verifica se o paciente existe no banco de dados
            GetPaciente(cpf); // Lança exceção se o paciente não for encontrado

            var query = "DELETE FROM Pacientes WHERE CPF = @CPF";
            using var cmd = new SqliteCommand(query, _connection);
            cmd.Parameters.AddWithValue("@CPF", cpf);

            int rowsAffected = cmd.ExecuteNonQuery();
            if (rowsAffected == 0)
                throw new InvalidOperationException("Erro ao remover o paciente.");
        }
    }
}
