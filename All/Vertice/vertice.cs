using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace All.vertice;

public class Vertice
{
    public double X { get; private set; }
    public double Y { get; private set; }

    public Vertice(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double Distancia(Vertice vertex)
    {
        double deltaX = vertex.X - this.X;
        double deltaY = vertex.Y - this.Y;
        return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
    }

    public void Move(double x, double y)
    {
        X = x;
        Y = y;
    }

    public bool Equals(Vertice vertex)
    {
        return this.X == vertex.X && this.Y == vertex.Y;
    }
}

