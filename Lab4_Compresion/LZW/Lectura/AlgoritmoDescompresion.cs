using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Text;


namespace Lab4_Compresion.LZW.Lectura
{
    public class AlgoritmoDescompresion
    {

        string Nombre = string.Empty;

        public AlgoritmoDescompresion(IFormFile Archivo, string nombre)
        {
            Nombre = nombre;
            Lectura(Archivo);
        }


        public void Lectura(IFormFile file )
        {
            string Carpetadesscompress = Environment.CurrentDirectory;

            if (!Directory.Exists(Path.Combine(Carpetadesscompress, "decompressLZW")))
            {
                Directory.CreateDirectory(Path.Combine(Carpetadesscompress, "decompressLZW"));
            }
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                using (var streamwitre = new FileStream(Path.Combine(Carpetadesscompress, "decompressLZW",$"{Nombre}.txt"),FileMode.OpenOrCreate))
                {
                    using (var write = new BinaryWriter(streamwitre))
                    {
                        var CantidadDiccioanriobytes = reader.ReadBytes(8);
                        
                        var CantidadDiccioanrioNumeros = Convert.ToInt32(Encoding.UTF8.GetString(CantidadDiccioanriobytes));

                        var DiccionarioInicial = new Dictionary<string, int>();

                        for (int i = 0; i < CantidadDiccioanrioNumeros; i++)
                        {
                            var Caracter = reader.ReadBytes(1);                      
                            DiccionarioInicial.Add(Convert.ToChar(Caracter[0]).ToString(), DiccionarioInicial.Count + 1);
                        }

                        var CantiadaMax = reader.ReadBytes(1);

                        var cantidadMaxBits = Convert.ToInt32(CantiadaMax[0]);

                        var LonguitudArchivo =Convert.ToInt32(reader.BaseStream.Length);

                        var LonguitudLeer = Convert.ToInt32(LonguitudArchivo - reader.BaseStream.Position);

                        var Buffer = new byte[LonguitudArchivo+10000];

                        var TextoComprimidoBits = new List<int>();

                        Buffer = reader.ReadBytes(LonguitudLeer);

                        string Auxiliar = string.Empty;

                        foreach (var item in Buffer)
                        { 
                            Auxiliar = Auxiliar + Convert.ToString(item,2).PadLeft(8,'0');
                             
                            if (Auxiliar.Length >= cantidadMaxBits)
                            {
                                int CantidadParaescribirCaracter = Auxiliar.Length / cantidadMaxBits;

                                for (int i = 0; i < CantidadParaescribirCaracter; i++)
                                { 
                                    TextoComprimidoBits.Add(Convert.ToInt32(Auxiliar.Substring(0, cantidadMaxBits),2));
                                    Auxiliar = Auxiliar.Substring(cantidadMaxBits);
                                }

                            } 
                        }
                        var TextoOriginal = Descomprimir(TextoComprimidoBits.ToArray(), DiccionarioInicial);

                        foreach (var item in TextoOriginal)
                        {
                            write.Write(Convert.ToByte(Convert.ToChar (item)));
                        } 
                    }
                }

            }
        }


        public string Descomprimir(int[] ArchivoComprimido, Dictionary<string,int> diccionarioinicial)
        {
            var DiccionarioTotal = new Dictionary<string, int>(diccionarioinicial);
            var PosicionAnterior = ArchivoComprimido[0];
            var ArchivoDescompreso = DiccionarioTotal.FirstOrDefault(x => x.Value == PosicionAnterior).Key;
            var Contador = 1;

            while (Contador < ArchivoComprimido.Length)
            {
                var Texto = string.Empty;
                var Caracter = DiccionarioTotal.FirstOrDefault(x => x.Value == PosicionAnterior).Key;
                var ActualPosicion = ArchivoComprimido[Contador];
                Texto = DiccionarioTotal.FirstOrDefault(x => x.Value == ActualPosicion).Key;
                DiccionarioTotal.Add(Caracter + Texto[0], DiccionarioTotal.Count + 1);
                ArchivoDescompreso = ArchivoDescompreso + Texto;
                PosicionAnterior = ActualPosicion;
                Contador++;
                 
            }
            return ArchivoDescompreso;
        }
         
    }
}
