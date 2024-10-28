using All.vertice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace All.poligono
{
    public class Poligono
    {
        private List<Vertice> vertices;

        public Poligono(IEnumerable<Vertice> vertices)
        {
            if (vertices.Count() < 3)
            {
                throw new ArgumentException("Um polígono deve ter pelo menos 3 vértices.");
            }

            this.vertices = new List<Vertice>(vertices);
        }

        public bool AddVertice(Vertice vertice)
        {
            if (vertices.Contains(vertice))
            {
                return false; 
            }

            vertices.Add(vertice);
            return true;
        }

        public void RemoveVertice(Vertice vertice)
        {
            if (!vertices.Remove(vertice))
            {
                throw new ArgumentException("Vértice não encontrado.");
            }

            if (vertices.Count < 3)
            {
                throw new InvalidOperationException("O polígono deve ter pelo menos 3 vértices.");
            }
        }

        public double Perimetro
        {
            get
            {
                double perimetro = 0;

                for (int i = 0; i < vertices.Count; i++)
                {
                    Vertice v1 = vertices[i];
                    Vertice v2 = vertices[(i + 1) % vertices.Count];
                    perimetro += v1.Distancia(v2);
                }

                return perimetro;
            }
        }

        public int NumeroDeVertices => vertices.Count;
    }
}

