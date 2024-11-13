using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_1._2.Exceptions
{
    internal class AnotherAttendaceException : Exception
    {
        public AnotherAttendaceException() : base("ERROR: Já há uma consulta com este agendamento") { }
    }
}
