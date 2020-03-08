using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_Compresion.Lectura
{
    public class Nodo
    {
        public char Caracter;
        //public int frecuencia;
        public double Probabilidad;
        public Nodo hijoDerecho;
        public Nodo hijoIzquierdo;
        public Nodo()
        {
            hijoDerecho = null;
            hijoIzquierdo = null;
        }
    }
}
