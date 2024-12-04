using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_3._1.Models
{
    public class Paciente
    {
        public string CPF { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        public int Idade => DateTime.Now.Year - DataNascimento.Year -
                            (DateTime.Now.DayOfYear < DataNascimento.DayOfYear ? 1 : 0);
    }
}
