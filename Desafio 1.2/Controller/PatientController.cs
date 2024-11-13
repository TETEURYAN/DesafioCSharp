using Desafio_1._2.Manager;
using Desafio_1._2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_1._2.Controller
{
    internal class PatientController
    {
        private Database persistencia;

        public ControladoraPaciente(Database p)
        {
            persistencia = p;
        }

        public bool CadastrarPaciente(string cpf, string nome, string data, out Dictionary<string, string> listaErros)
        {

            Patient p = new Patient(cpf, nome, data);
            listaErros = p.ValidarDados();
            if (listaErros.Count > 0)
                return false;


            return persistencia.SalvarPaciente(p);
        }

        public bool VerificaCPF(string cpf, bool deveExistir)
        {
            Patient p = new Patient(cpf, "", "");
            return deveExistir ? persistencia.CpfExiste(p) : persistencia.CpfNaoExiste(p);
        }

        public Patient PegarPaciente(string cpf)
        {
            return persistencia.FindPaciente(cpf);
        }

        public IReadOnlyCollection<Patient> PegarPacientes(string ordenacao)
        {
            return persistencia.PegarPacientes(ordenacao);
        }

        public void ExcluirPaciente(string cpf)
        {
            persistencia.ExcluirPaciente(cpf);
        }
    }
}
