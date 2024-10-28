using All.Certidão_Nascimento;

namespace All.Certidão_Nascimento
{
    public class Pessoa
    {
        public string Nome { get; }
        public CertidaoNascimento Certidao { get; private set; }

        // Construtor
        public Pessoa(string nome, CertidaoNascimento certidao = null)
        {
            Nome = nome;
            Certidao = certidao;
        }

        public bool AssociarCertidao(CertidaoNascimento certidao)
        {
            if (Certidao == null) 
            {
                Certidao = certidao;
                return true; 
            }
            return false;
        }
    }

}
