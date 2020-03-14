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
            Dictionary<string, int> DiccionarioTemp = new Dictionary<string, int>(diccionarioinicial);
            int Contador = 0;
            string CaracteresLeidos = string.Empty;
            List<byte> Bytes = new List<byte>();
            while (Contador < file.Length)
            {
                CaracteresLeidos += file[Contador];
                Contador++;

                while (DiccionarioTemp.ContainsKey(CaracteresLeidos) && Contador < file.Length)
                {
                    CaracteresLeidos += file[Contador];
                    Contador++;
                }

                if (DiccionarioTemp.ContainsKey(CaracteresLeidos))
                {
                    Bytes.Add(Convert.ToByte(DiccionarioTemp[CaracteresLeidos]));
                }
                else
                {
                    string Llave = CaracteresLeidos.Substring(0, CaracteresLeidos.Length - 1);
                    Bytes.Add(Convert.ToByte(DiccionarioTemp[Llave]));
                    DiccionarioTemp.Add(CaracteresLeidos, DiccionarioTemp.Count + 1);
                    Contador--;
                    CaracteresLeidos = string.Empty;


                }

            }
            return Bytes.ToArray();
            
        }

        public (Dictionary<string, int>, byte[]) Compresion(string archivo)
        {
            Dictionary<string, int> Diccionario_Inicial = DiccionarioInicial(archivo);
            byte[] ComprimirArchivo = CompressFile(archivo, Diccionario_Inicial);
            return (Diccionario_Inicial, ComprimirArchivo);
        }

        


        


    }
}
