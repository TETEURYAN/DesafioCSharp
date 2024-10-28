using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace All.piramide
{
    internal class piramide
    {
        private int tam;

        public piramide (int altura)
        {
            this.tam = altura;
        }

        public void Desenha()
        {
            for (int i = 1; i <= tam; i++)
            {
                for (int j = 0; j < tam - i; j++)
                {
                    Console.Write(" ");
                }

                for (int k = 1; k <= i; k++)
                {
                    Console.Write(k);
                }

                for (int k = i - 1; k >= 1; k--)
                {
                    Console.Write(k);
                }

                Console.WriteLine();
            }
        }
    }
}

