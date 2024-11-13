using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_1._2.Exceptions
{
    internal class RemovePatientExepction : Exception
    {
        public RemovePatientExepction() : base("ERROR: Não é possível realizar essa operação.") { }
    }
}
