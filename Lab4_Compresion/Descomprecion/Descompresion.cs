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

        List<string> Frecuencias = new List<string>();

        string TextoDescomprimir = string.Empty;

       

         

        
        public Descompresion(string nombrearchivocontrolller, string leercontroller)
        {
            NombreArchivo = nombrearchivocontrolller;
            Descomprimir(leercontroller);
        }
        public void Descomprimir(string leer)
        {
            char[] CaracteresDelimitadores = { '\t', '\r', ' ' };
            StreamReader TextLeer = new StreamReader(leer);
            var Texto = TextLeer.ReadToEnd();
            var words = Texto.Split('\n');
            for (int i = 0; i < words.Length; i++)
            {
                words[i] = words[i].Trim(CaracteresDelimitadores);
            }

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != "")
                {
                    string Validar = words[i].Substring(0, 1);
                    if (Validar == "[")
                    {
                        Frecuencias.Add(words[i]);
                    }
                    else
                    {
                        if (Validar == "0" || Validar == "1")
                        {
                            TextoDescomprimir = words[i];
                        }
                    }

                }
              
            }

            foreach (var item in Frecuencias)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    var Caracteres = item.Substring(i, 1);
                    if (Caracteres !="" || Caracteres != "[")
                    {

                    }
                       

                }
            }



        }
    }
}
