using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_1._2.Exceptions
{
    internal class TheresPatiencExceptcion : Exception
    {
        public TheresPatiencExceptcion() : base("ERROR: Já há um paciente com essas informações cadastrado.") { }
    }
}
