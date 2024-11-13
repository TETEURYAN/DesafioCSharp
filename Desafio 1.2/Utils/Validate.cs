using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_1._2.Utils
{
    internal class Validate
    {
        public static bool ValidarCPF(string cpf)
        {
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
                return false;

            if (new string(cpf[0], 11) == cpf)
                return false;

            int soma = 0;
            int peso = 10;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * peso--;
            }
            int resto = soma % 11;
            int primeiroDigito = (resto < 2) ? 0 : 11 - resto;

            soma = 0;
            peso = 11;
            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(cpf[i].ToString()) * peso--;
            }
            resto = soma % 11;
            int segundoDigito = (resto < 2) ? 0 : 11 - resto;

            return cpf[9] == primeiroDigito.ToString()[0] && cpf[10] == segundoDigito.ToString()[0];
        }
    }
}
