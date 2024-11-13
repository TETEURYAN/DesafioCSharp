using Desafio_1._2.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Desafio_1._2.Models
{
    internal class Patient
    {
        public string CPF { get; private set; }
        public string Nome { get; private set; }

        public DateTime DataNascimento { get; private set; }

        public string DataNaoValidada { get; private set; }

        public List<Attendace> Consultas { get; }

        public Attendace? ConsultaFutura { get; set; }

        public Validate validation;

        public Patient(string cpf, string nome, string data)
        {
            CPF = cpf;
            Nome = nome;
            this.DataNaoValidada = data;

            Consultas = new List<Attendace>();
        }

        public void AtualizarConsulta()
        {
            if (ConsultaFutura != null && DateTime.Now > ConsultaFutura.DataConsulta)
                ConsultaFutura = null;

        }

        public void AdicionarConsulta(Attendace c)
        {
            Consultas.Add(c);
        }

        public bool Equals(Patient? other)
        {
            return other is not null && other.CPF == CPF;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Patient paciente)
            {
                return Equals(paciente);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return CPF.GetHashCode();
        }

        public Dictionary<string, string> ValidarDados()
        {
            Dictionary<string, string> listaErros = new Dictionary<string, string>();

            if ()
                listaErros.Add("CPF", "ERRO: CPF Inválido");

            if (Nome.Length < 5)
                listaErros.Add("Nome", "ERROR: Nome precisa ter pelo menos 5 caracteres.");

            DateTime trezeAnos = DateTime.Now.AddYears(-13);

            Match m = Regex.Match(DataNaoValidada, @"^([0][1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/([0-9]{4})$");

            if (!m.Success)
            {
                listaErros.Add("DataNascimento", "ERROR: Data de nascimento fora do formato DD/MM/YYYY");
            }
            else
            {
                DateTime aux;

                DateTime.TryParse(DataNaoValidada, out aux);

                DataNascimento = aux;

                if (DataNascimento > trezeAnos)
                {
                    listaErros.Add("DataNascimento", "Paciente precisa ter mais de 13 anos de idade");

                }
            }


            return listaErros;
        }
    }
}
