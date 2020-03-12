using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_Compresion.LZW.Lectura
{
    public class AlgorithmLZW
    {
        string Name = string.Empty;

        string Direction = string.Empty;

        public AlgorithmLZW(string Name, string Direction)
        {
            Name = this.Name;
            Direction = this.Direction;

        }

        public void LecturaArchivo(string direction)
        {
            StreamReader Archivo = new StreamReader(direction);
            var Contenido = Archivo.ReadToEnd();



        }

        public Dictionary<string,int> DiccionarioInicial(string Archivo)
        {
            Dictionary<string, int> DiccionarioInicial = new Dictionary<string, int>();
            foreach (char caracter in Archivo)
            {
                if (!DiccionarioInicial.ContainsKey(Convert.ToString(caracter))) ;
                {
                    DiccionarioInicial.Add(caracter.ToString(), DiccionarioInicial.Count + 1);
                }
            } 
            return DiccionarioInicial;
        }

        public byte[] CompressFile(string file, Dictionary<string,int> diccionarioinicial)
        {

        }

        


        


    }
}
