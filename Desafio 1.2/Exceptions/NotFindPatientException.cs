
namespace Desafio_1._2.Exceptions
{
    internal class NotFindPatientException : Exception
    {
        public NotFindPatientException() : base("ERROR: Paciente não encontrado.") { }
    }
}
