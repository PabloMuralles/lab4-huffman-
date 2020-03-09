using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_Compresion.Descomprecion
{
    public class Nodo
    {
        public string Caracter;

        public int Frecuencia;

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
