using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Lab4_Compresion.Lectura
{
    public class Lectura
    { 
         Dictionary<char, int> elementos_enviar = new Dictionary<char, int>();

        private Dictionary<char, Caracteres> diccionario = new Dictionary<char, Caracteres>();

        private List<byte> Frecuencia = new List<byte>();

        List<ArbolHuffman.Elementos> Ocurrencia = new List<ArbolHuffman.Elementos>();

        List<char> Verificacion = new List<char>();


        ArbolHuffman.Elementos elementos = null;

        public int num_total = 0;
        
        private string NombreArchivo = string.Empty;
     
        const int longitud = 1000000;

        Caracteres caracter = null;


        public Lectura(string nombrearchivocontrolller,string leercontroller)
        {
            NombreArchivo = nombrearchivocontrolller;
            Ingresar(leercontroller, nombrearchivocontrolller);

        }
        public void Ingresar(string leer, string nombre)
        { 
            using (var stream = new FileStream(leer, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var buffer = new byte[longitud];
                    buffer = reader.ReadBytes(longitud);
                    foreach (byte bit in buffer)
                    {
                        caracter = new Caracteres();
                        if (!diccionario.ContainsKey((char)bit))
                        {
                            caracter.cantidad = 1;
                            diccionario.Add((char)bit, caracter);
                        }
                        else
                        {
                            diccionario[(char)bit].cantidad++;
                        }
                        Frecuencia.Add(bit);
                    }
                }
            }
            Probabilidad();
            ArbolHuffman.Arbol Arbol = new ArbolHuffman.Arbol(Ocurrencia);
            var DicTablaPrefijos = Arbol.CrearArbol();
            Compresion.Compresion Comprimir = new Compresion.Compresion(leer,nombre );
            Comprimir.Comprimir(DicTablaPrefijos, Ocurrencia);




        }
        public void Probabilidad()
        {
            foreach (var item in diccionario)
            {
                elementos = new ArbolHuffman.Elementos();
                double cantidad = (Convert.ToDouble(item.Value.cantidad));
                elementos.caracter = item.Key;
                elementos.cantidad = Convert.ToInt32(cantidad);
                elementos_enviar.Add(elementos.caracter,elementos.cantidad);
                elementos.probabilidad = Convert.ToDouble((cantidad));
                Ocurrencia.Add(elementos);
                Verificacion.Add(item.Key);
            }
            
        }
    }
}

