using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace All.intervalo
{
    public class Intervalo
    {
        public DateTime Init { get; }
        public DateTime Finish { get; }

        public Intervalo(DateTime inicio, DateTime fim)
        {
            if (inicio > fim)
            {
                throw new ArgumentException("A data/hora inicial não pode ser maior que a data/hora final.");
            }

            Init = inicio;
            Finish = fim;
        }

        public bool TemIntersecao(Intervalo outro)
        {
            return Init < outro.Finish && Finish > outro.Init;
        }

        public bool Equals(Intervalo outro)
        {
            return Init == outro.Init && Finish == outro.Finish;
        }

        public TimeSpan Duracao => Finish - Init;
    }
}