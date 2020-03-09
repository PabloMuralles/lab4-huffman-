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
            char[] CaracteresDelimitadores = { '\t', '\r', ' ' };
            StreamReader TextLeer = new StreamReader(leer);
            string Texto = TextLeer.ReadToEnd();
            string[] words = Texto.Split('\n');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim(CaracteresDelimitadores);
            }

            for (int i = 0; i < words.Length; i++)
            {
                if (true)
                {

                }
            }



        }
    }
}
