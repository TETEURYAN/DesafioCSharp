using Desafio_1._2.Manager;
using Desafio_1._2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_1._2.Controller
{
    internal class ControladoraConsulta
    {
        private Database persistencia;

        public ControladoraConsulta(Database persistencia)
        {
            this.persistencia = persistencia;
        }

        public bool AddAttendace(Patient p, string data, string horaInicial, string horaFinal, out Dictionary<string, List<String>> listaErros)
        {
            Attendace c = new Attendace(p, data, horaInicial, horaFinal);
            listaErros = c.ValidateData();
            if (listaErros.Count > 0)
                return false;


            return persistencia.SaveAttendace(c);

        }

        public void CancelarConsulta(string cpf, DateTime dataConsulta, DateTime horaInicial)
        {
            persistencia.CancelAttendace(cpf, dataConsulta, horaInicial);
        }

        public IReadOnlyCollection<Attendace> PegarConsultas()
        {
            return persistencia.PegarConsultas();
        }

        public IReadOnlyCollection<Attendace> indAttendaceInterva(string dataInicio, string dataFim)
        {
            return persistencia.PegarConsultasPorPeriodo(DateTime.Parse(dataInicio), DateTime.Parse(dataFim));
        }
    }
}
