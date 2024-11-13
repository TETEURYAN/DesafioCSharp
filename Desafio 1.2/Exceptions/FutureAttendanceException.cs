
namespace Desafio_1._2.Exceptions
{
    internal class FutureAttendaceException : Exception
    {
        public FutureAttendaceException() : base("ERROR: O paciente já possui uma consulta a ser realizada") { }
    }
}
