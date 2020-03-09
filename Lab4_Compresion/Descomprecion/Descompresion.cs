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
         
        string TextoDescomprimir = string.Empty;

         

        Dictionary<string, string> Caracteres = new Dictionary<string, string>();

        List<Frecuencias> Frecuencias = new List<Frecuencias>();





        public Descompresion(string nombrearchivocontrolller, string leercontroller)
        {
            NombreArchivo = nombrearchivocontrolller;
            Descomprimir(leercontroller);
        }
        public void Descomprimir(string leer)
        {
          
            StreamReader TextLeer = new StreamReader(leer);
            var Texto = TextLeer.ReadLine();

            while (Texto != null)
            {
                if(Texto.Substring(0, 1) == "0" || Texto.Substring(0, 1) == "1")
                {

                    TextoDescomprimir = Texto.Trim();
                }
                else
                {
                    Texto = Texto.Trim().Replace("[", " ").Replace("]", " ");
                    var Caracter = Texto.Split(',');
                    Caracteres.Add(Caracter[0], Caracter[1]);
                }
                Texto = TextLeer.ReadLine();
            }
            foreach (var item in Caracteres)
            {
                string CaracterFrecuencia;
                Frecuencias NuevaFrecuencias = new Frecuencias();
                int FrecuenciaDiccionario = Convert.ToInt32(item.Value);
                if (item.Key == "  ")
                {
                    CaracterFrecuencia = item.Key;
                }
                else
                {
                    CaracterFrecuencia = item.Key.Trim();
                }
                Frecuencias.Add(new Frecuencias { Caracter = CaracterFrecuencia, Frecuencia = FrecuenciaDiccionario });

            }

            Arbol arbol = new Arbol(Frecuencias,NombreArchivo,TextoDescomprimir);

        }    
    }
}
