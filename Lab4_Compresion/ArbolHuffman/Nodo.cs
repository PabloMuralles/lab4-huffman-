using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_Compresion.ArbolHuffman
{
    public class Nodo
    {
        public char Caracter;

        public double Probabilidad;

        public Nodo HijoDerecho;

        public Nodo HijoIzquierdo;

        public Nodo Padre;

        public int camino;
        public Nodo()
        {
            Padre = null;
            HijoDerecho = null;
            HijoIzquierdo = null;
        }

        public bool EsHoja => HijoIzquierdo == null && HijoDerecho == null;




    }
}
