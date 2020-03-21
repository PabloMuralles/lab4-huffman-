using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Lab4_Compresion.LZW.Lectura
{
    public class AlgoritomoCompresion
    {
        string Name = string.Empty;

        string Direction = string.Empty;
         
        Dictionary<string, int> DiccionarioTotal;
         
        public AlgoritomoCompresion(string name, string direction)
        {
            Name = name;
            Direction = direction;
            LecturaArchivo(Direction);

        }

        public void LecturaArchivo(string direction)
        {
            StreamReader Archivo = new StreamReader(direction);
            var Contenido = Archivo.ReadToEnd();
            Compresion(Contenido);



        }

        public Dictionary<string,int> DiccionarioInicial(string text)
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach (char character in text)
            {
                if (!dict.ContainsKey(character.ToString()))
                    dict.Add(character.ToString(), dict.Count + 1);
            }
            return dict;
        }

        public int[] CompressFile(string file, Dictionary<string, int> diccionarioinicialc)
        {
            var DiccionarioTemp = new Dictionary<string, int>(diccionarioinicialc);
            var Contador = 0;
            var CaracteresLeidos = string.Empty;
            var Bytes = new List<int>();
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
                    Bytes.Add((DiccionarioTemp[CaracteresLeidos]));
                }
                else
                {
                    var Llave = CaracteresLeidos.Substring(0, CaracteresLeidos.Length - 1);
                    Bytes.Add((DiccionarioTemp[Llave]));
                    DiccionarioTemp.Add(CaracteresLeidos, DiccionarioTemp.Count + 1);
                    Contador--;
                    CaracteresLeidos = string.Empty;


                }

            }
            DiccionarioTotal = new Dictionary<string, int>(DiccionarioTemp);
            return Bytes.ToArray();



        }

        public void Compresion(string archivo)
        {
            Dictionary<string, int> Diccionario_Inicial = DiccionarioInicial(archivo);
            int[] ComprimirArchivo = CompressFile(archivo, Diccionario_Inicial);
            Escritur(ComprimirArchivo, Diccionario_Inicial);
        }

        
        public void Escritur(int[] Compresion , Dictionary<string, int> diccionario)
        {
             

            string CarpetaCompress = Environment.CurrentDirectory;

            if (!Directory.Exists(Path.Combine(CarpetaCompress, "Compress")))
            {
                Directory.CreateDirectory(Path.Combine(CarpetaCompress, "Compress"));
            }

            using (var streamWriter = new FileStream(Path.Combine(CarpetaCompress, "Compress", "hola.lzw"), FileMode.OpenOrCreate))
            {
                using (var write = new BinaryWriter(streamWriter))
                {

                    write.Write(Encoding.UTF8.GetBytes(Convert.ToString(diccionario.Count).PadLeft(8, '0').ToCharArray()));
                    

                    foreach (var item in diccionario)
                    {   
                        write.Write(Convert.ToByte(Convert.ToChar(item.Key)));
                    }

                    var CantidadMaxima = Math.Log2(DiccionarioTotal.Count());

                    if (CantidadMaxima % 1 >= 0.5)
                    {
                        CantidadMaxima += 1;
                        CantidadMaxima = Convert.ToInt32(CantidadMaxima);
                    }
                    else
                    {
                        CantidadMaxima = Convert.ToInt32(CantidadMaxima);
                    }

                    write.Write(Convert.ToByte(CantidadMaxima));

                    var CompresionEnBinario = new List<string>();

                    foreach (var item in Compresion)
                    {
                        var prueba = Convert.ToString(item, 2).PadLeft(Convert.ToInt32(CantidadMaxima), '0');
                        CompresionEnBinario.Add(Convert.ToString(item , 2).PadLeft(Convert.ToInt32(CantidadMaxima),'0'));
                    }

                    var EscrituraBitesCompresion = new List<byte>();

                    string Auxiliar = string.Empty;

                    foreach (var item in CompresionEnBinario)
                    {
                        Auxiliar = Auxiliar + item;
                        if (Auxiliar.Length >= 8) 
                        {
                            var caskldsdf = Auxiliar.Length;
                            int CantiadadMaximaBits = Auxiliar.Length / 8;

                            for (int i = 0; i < CantiadadMaximaBits; i++)
                            {
                                EscrituraBitesCompresion.Add(Convert.ToByte(Convert.ToInt32(Auxiliar.Substring(0,8),2)));
                                Auxiliar = Auxiliar.Substring(8);
                            }

                        }
                    }
                    if (Auxiliar.Length != 0)
                    {
                        EscrituraBitesCompresion.Add(Convert.ToByte(Convert.ToInt32(Auxiliar.PadRight(8,'0'),2)));
                    }

                    write.Write(EscrituraBitesCompresion.ToArray());
                }

            } 
        }



       

         
    }
}
