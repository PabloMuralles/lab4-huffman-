using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_Compresion.ArbolHuffman
{
    public class Arbol
    {


        Dictionary<string, string> TablaPrefijos = new Dictionary<string, string>();                
        private List<Nodo> NodosProbabilidades = new List<Nodo>();
        private List<Elementos> TablaProbabilidades = new List<Elementos>();
        private List<Nodo> Sacar = new List<Nodo>();
        Nodo Raiz = new Nodo();
        public Arbol(List<Elementos> TablaProbabilidadesLectura)
        {
            TablaProbabilidades = TablaProbabilidadesLectura;
            CrearArbol();
           

        }

        public void CrearArbol()
        {
            foreach (var item in TablaProbabilidades)
            {
                NodosProbabilidades.Add(new Nodo { Caracter = item.caracter, Probabilidad = item.probabilidad });

            }

            while (NodosProbabilidades.Count > 1)
            {
                List<Nodo> NodosProbabilidadesOrdenados = NodosProbabilidades.OrderBy(x => x.Probabilidad).ToList();

                if (NodosProbabilidades.Count >= 1)
                {
                    Sacar = NodosProbabilidadesOrdenados.Take(2).ToList<Nodo>();

                    Nodo Padre = new Nodo() { Caracter = '*', Probabilidad = (Sacar[0].Probabilidad + Sacar[1].Probabilidad), HijoDerecho = Sacar[0], HijoIzquierdo = Sacar[1] };
                    Padre.HijoDerecho.Padre = Padre;
                    Padre.HijoIzquierdo.Padre = Padre;
                    NodosProbabilidades.Remove(Sacar[0]);
                    NodosProbabilidades.Remove(Sacar[1]);
                    Padre.HijoDerecho.camino = 1;
                    Padre.HijoIzquierdo.camino = 0;
                    NodosProbabilidades.Add(Padre);
                    Sacar.Clear();



                }
                Raiz = NodosProbabilidades.FirstOrDefault();


            }
            Recorrido(Raiz);
            Compresion.Compresion.Instance.Comprimir(TablaPrefijos);
        }

        public void Recorrido(Nodo raiz)
        {
            if (raiz != null)
            {
                Recorrido(raiz.HijoIzquierdo);
                if (raiz.EsHoja)
                {
                    TablaCompresion NuevoElemento = new TablaCompresion();
                    NuevoElemento.Caracter = raiz.Caracter;
                    GenerarCodigo(raiz);
                    NuevoElemento.Prefijo = Invertir(Camino);

                    TablaPrefijos.Add(Convert.ToString(NuevoElemento.Caracter), NuevoElemento.Prefijo);

                    

                    Camino = string.Empty;

                }

                Recorrido(raiz.HijoDerecho);
            }


        }

        string Camino = string.Empty;

        public void GenerarCodigo(Nodo Nodo)
        {
            if (Nodo.Padre != null)
            {
                Camino += Nodo.camino;
                GenerarCodigo(Nodo.Padre);
            }


        }

        public string Invertir(string CaminoInvertir)
        {
            char[] charArray = CaminoInvertir.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

    }
}
