using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_Compresion.Descomprecion
{
    public class Descompresion
    {
        string NombreArchivo = string.Empty;

         

        const int longitud = 1000000;
        public Descompresion(string nombrearchivocontrolller, string leercontroller)
        {
            NombreArchivo = nombrearchivocontrolller;
            Descomprimir(leercontroller);
        }
        public void Descomprimir(string leer)
        {
            Dictionary<string, string> Caracteres = new Dictionary<string, string>();
            var archivo = new StreamReader(leer);
            var linea = archivo.ReadLine();           
            
            while (linea != null)
            {
                linea = linea.Trim().Replace("["," ").Replace("]"," ");
               var caracter = linea.Split(',');
              Caracteres.Add(caracter[0],caracter[1]);
            }
                //char[] CaracteresDelimitadores = { '\t', '\r', ' ' };
                //StreamReader TextLeer = new StreamReader(leer);
                //var Texto = TextLeer.ReadToEnd();
                //var words = Texto.Split('\n');
                //for (int i = 0; i < words.Length; i++)
                //{
                //    words[i] = words[i].Trim(CaracteresDelimitadores);
                //}

                //for (int i = 0; i < words.Length; i++)
                //{
                //    if (true)
                //    {

                //    }
                //}

            }
    }
}
