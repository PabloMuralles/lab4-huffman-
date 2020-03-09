using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_Compresion.Descomprecion
{
    public class Arbol
    {
        Dictionary<string, string> Tabla_Descomprimir = new Dictionary<string, string>();

        string Nombre = string.Empty;
        string DatoDescomprimido = string.Empty;

        string TextoDescomprimir = string.Empty;

        private List<Nodo> NodosFrecuencias = new List<Nodo>();
        private List<Frecuencias> TablaFrecuencias = new List<Frecuencias>();
        private List<Nodo> Sacar = new List<Nodo>();
        Nodo Raiz = new Nodo();
        public Arbol(List<Frecuencias> TablaProbabilidadesLectura,string nombre,string texto)
        {
            TablaFrecuencias = TablaProbabilidadesLectura;
            Nombre = nombre;
            TextoDescomprimir = texto;

            CrearArbol();


        }

        public void CrearArbol()
        {
            foreach (var item in TablaFrecuencias)
            {
                NodosFrecuencias.Add(new Nodo { Caracter = item.Caracter, Frecuencia = item.Frecuencia });

            }

            while (NodosFrecuencias.Count > 1)
            {
                List<Nodo> NodosProbabilidadesOrdenados = NodosFrecuencias.OrderBy(x => x.Frecuencia).ToList();

                if (NodosFrecuencias.Count >= 1)
                {
                    Sacar = NodosProbabilidadesOrdenados.Take(2).ToList<Nodo>();

                    Nodo Padre = new Nodo() { Caracter = "*", Frecuencia = (Sacar[0].Frecuencia + Sacar[1].Frecuencia), HijoDerecho = Sacar[0], HijoIzquierdo = Sacar[1] };
                    Padre.HijoDerecho.Padre = Padre;
                    Padre.HijoIzquierdo.Padre = Padre;
                    NodosFrecuencias.Remove(Sacar[0]);
                    NodosFrecuencias.Remove(Sacar[1]);
                    Padre.HijoDerecho.camino = 1;
                    Padre.HijoIzquierdo.camino = 0;
                    NodosFrecuencias.Add(Padre);
                    Sacar.Clear();



                }
                Raiz = NodosFrecuencias.FirstOrDefault();


            }
            Recorrido(Raiz);

            Escritura();

           
        }

        public void Recorrido(Nodo raiz)
        {
            if (raiz != null)
            {
                Recorrido(raiz.HijoIzquierdo);
                if (raiz.EsHoja)
                {
                    TablaDescompresion NuevoElemento = new TablaDescompresion();
                    NuevoElemento.Caracter = raiz.Caracter;
                    GenerarCodigo(raiz);
                    NuevoElemento.Prefijo = Invertir(Camino);

                    Tabla_Descomprimir.Add(NuevoElemento.Caracter, NuevoElemento.Prefijo);



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



        public void Escritura()
        {
            string textonuevo = string.Empty;
            int contador = 0;
            while (TextoDescomprimir.Substring(contador,1) !=null )
            {
                var text = TextoDescomprimir.Substring(contador, 1);
                textonuevo += text;
                foreach (var item in Tabla_Descomprimir)
                {
                    if (textonuevo == item.Value)
                    {
                        DatoDescomprimido = DatoDescomprimido + item.Key;
                        textonuevo = string.Empty;
                        break;
                    }
                }
                contador++;

            }

        }




    }
}
