using All.intervalo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace All.intervalo
{
    public class ListaIntervalo
    {
        private List<Intervalo> intervalos;

        public ListaIntervalo()
        {
            intervalos = new List<Intervalo>();
        }

        public bool Add(Intervalo novoIntervalo)
        {
            if (intervalos.Any(intervalo => intervalo.TemIntersecao(novoIntervalo)))
            {
                return false;
            }

            intervalos.Add(novoIntervalo);
            return true; 
        }

        public IReadOnlyList<Intervalo> Intervalos => intervalos.OrderBy(i => i.Init).ToList().AsReadOnly();
    }
}
