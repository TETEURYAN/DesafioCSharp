using Desafio_1._2.Exceptions;
using Desafio_1._2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_1._2.Manager
{
    internal class Database
    {
        private List<Patient> ListaPacientes;
        private List<Attendace> ListaConsultas;
        private bool listaConsultasFoiModificada;

        public IReadOnlyCollection<Patient> PacientesReadOnly
        {
            get { return ListaPacientes.AsReadOnly(); }
        }

        public IReadOnlyCollection<Attendace> ConsultasReadOnly
        {
            get { return ListaConsultas.AsReadOnly(); }
        }

        public Database()
        {
            ListaPacientes = new List<Patient>();
            ListaConsultas = new List<Attendace>();
            listaConsultasFoiModificada = false;
        }



        public bool SalvarPaciente(Patient p)
        {
            if (CpfNaoExiste(p))
            {
                ListaPacientes.Add(p);
                return true;

            }

            return false;
        }

        public void ExcluirPaciente(string cpf)
        {
            Patient p = FindPaciente(cpf);
            p.AtualizarConsulta();
            if (p.ConsultaFutura != null)
            {
                throw new RemovePatientExepction();

            }

            ListaConsultas.RemoveAll(c => p.Consultas.Contains(c));
            ListaPacientes.Remove(p);
        }


        public bool CpfNaoExiste(Patient p)
        {

            return !ListaPacientes.Contains(p) ? true : throw new TheresPatiencExceptcion();
        }

        public bool CpfExiste(Patient p)
        {
            return ListaPacientes.Contains(p) ? true : throw new NotFindPatientException();

        }

        public bool SaveAttendace(Attendace c)
        {
            Patient? pSalvo = ListaPacientes.Find((p) => p.Equals(c.Paciente));


            if (pSalvo == null)
            {
                throw new NotFindPatientException();
            }

            if (ListaConsultas.Contains(c))
            {
                throw new AnotherAttendaceException();
            }

            pSalvo.AtualizarConsulta();
            if (pSalvo.ConsultaFutura != null)
            {
                throw new FutureAttendaceException();
            }

            pSalvo.Consultas.Add(c);
            pSalvo.ConsultaFutura = c;
            ListaConsultas.Add(c);
            listaConsultasFoiModificada = true;

            return true;
        }

        public IReadOnlyCollection<Attendace> PegarConsultas()
        {
            if (listaConsultasFoiModificada)
            {
                ListaConsultas.Sort();
                listaConsultasFoiModificada = false;
            }

            return ConsultasReadOnly;

        }

        public IReadOnlyCollection<Attendace> PegarConsultasPorPeriodo(DateTime inicio, DateTime fim)
        {
            if (listaConsultasFoiModificada)
            {
                ListaConsultas.Sort();
                listaConsultasFoiModificada = false;
            }



            return ListaConsultas.Where((c) => c.DataConsulta >= inicio && c.DataConsulta <= fim).ToList().AsReadOnly();
        }


        public Patient FindPaciente(string cpf)
        {
            Patient? pSalvo = ListaPacientes.Find((p) => p.CPF == cpf);
            if (pSalvo == null)
                throw new NotFindPatientException();

            return pSalvo;
        }

        public IReadOnlyCollection<Patient> PegarPacientes(string ordenacao)
        {
            List<Patient> listaPacientes = ListaPacientes;

            if (ordenacao == "cpf")
            {
                listaPacientes.Sort((p, p2) => p.CPF.CompareTo(p2.CPF));
            }
            else if (ordenacao == "nome")
            {
                listaPacientes.Sort((p, p2) => p.Nome.CompareTo(p2.Nome));
            }

            return listaPacientes.AsReadOnly();
        }

        public void CancelAttendace(string cpf, DateTime dataConsulta, DateTime horaInicial)
        {
            Patient? pSalvo = ListaPacientes.Find((p) => p.CPF == cpf);

            if (pSalvo == null)
            {
                throw new NotFindPatientException();
            }
            Attendace? c = pSalvo.ConsultaFutura;
            if (c == null || c.DataConsulta != dataConsulta.AddHours(horaInicial.Hour).AddMinutes(horaInicial.Minute))
                throw new NotFindAttendaceException();

            pSalvo.ConsultaFutura = null;
            int i = ListaConsultas.FindIndex((c) => c.Paciente.CPF == cpf && c.DataConsulta == dataConsulta.AddHours(horaInicial.Hour).AddMinutes(horaInicial.Minute));
            ListaConsultas.RemoveAt(i);

        }
    }
}
