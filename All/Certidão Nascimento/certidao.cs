using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace All.Certidão_Nascimento
{
    public class CertidaoNascimento
    {
        public DateTime DataEmissao { get; }
        public Pessoa Pessoa { get; }

        // Construtor
        public CertidaoNascimento(DateTime dataEmissao, Pessoa pessoa)
        {
            DataEmissao = dataEmissao;
            Pessoa = pessoa ?? throw new ArgumentNullException(nameof(pessoa), "A certidão deve estar associada a uma pessoa.");
        }
    }
}
