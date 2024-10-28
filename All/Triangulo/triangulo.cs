using All.vertice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace All.triangulo
{
    public enum TypeTriangle
    {
        Equilatero,
        Isosceles,
        Escaleno
    }

    public class Triangulo
    {
        public Vertice V1 { get; private set; }
        public Vertice V2 { get; private set; }
        public Vertice V3 { get; private set; }

        public Triangulo(Vertice v1, Vertice v2, Vertice v3)
        {
            if (!IsTriangle(v1, v2, v3))
            {
                throw new ArgumentException("Os vértices não formam um triângulo.");
            }

            this.V1 = v1;
            this.V2 = v2;
            this.V3 = v3;
        }

        private bool IsTriangle(Vertice v1, Vertice v2, Vertice v3)
        {
            double lado1 = v1.Distancia(v2);
            double lado2 = v2.Distancia(v3);
            double lado3 = v3.Distancia(v1);
            return lado1 + lado2 > lado3 && lado1 + lado3 > lado2 && lado2 + lado3 > lado1;
        }

        public double Perimetro
        {
            get
            {
                double a = V1.Distancia(V2);
                double b = V2.Distancia(V3);
                double c = V3.Distancia(V1);
                return a + b + c;
            }
        }

        public TypeTriangle Type
        {
            get
            {
                double a = V1.Distancia(V2);
                double b = V2.Distancia(V3);
                double c = V3.Distancia(V1);

                if (a == b && b == c)
                {
                    return TypeTriangle.Equilatero;
                }
                else if (a == b || b == c || a == c)
                {
                    return TypeTriangle.Isosceles;
                }
                else
                {
                    return TypeTriangle.Escaleno;
                }
            }
        }

        public double Area
        {
            get
            {
                double a = V1.Distancia(V2);
                double b = V2.Distancia(V3);
                double c = V3.Distancia(V1);
                double s = Perimetro / 2; 
                return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
            }
        }

        public bool Equals(Triangulo triangle)
        {
            return (V1.Equals(triangle.V1) && V2.Equals(triangle.V2) && V3.Equals(triangle.V3) ||
                    V1.Equals(triangle.V2) && V2.Equals(triangle.V3) && V3.Equals(triangle.V1) ||
                    V1.Equals(triangle.V3) && V2.Equals(triangle.V2) && V3.Equals(triangle.V1) ||
                    V1.Equals(triangle.V3) && V2.Equals(triangle.V1) && V3.Equals(triangle.V2));
        }
    }
}