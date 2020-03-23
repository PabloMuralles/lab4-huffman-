﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Lab4_Compresion.LZW.Lectura
{
    public class AlgoritomoCompresion
    {
        string Name = string.Empty;
         
        Dictionary<string, int> DiccionarioTotal;
         
        public AlgoritomoCompresion(string name, IFormFile file)
        {
            Name = name;
            LecturaArchivo(file);
            DatosArchivoCompresion datos = new DatosArchivoCompresion();
            datos.NombreOriginal = file.FileName;
            datos.BytesOriginal = Convert.ToInt32(file.Length);
            datos.Ruta = Path.Combine(Environment.CurrentDirectory, "CompressLZW", $"{Name}.lzw");
            datos.NombreNuevo = $"{Name}.lzw";
            HistorialCompresion.Instance.ArchivosComprimidosPila.Add(datos);

        }

        public void LecturaArchivo(IFormFile direction)
        {
            using (var reader = new BinaryReader(direction.OpenReadStream()))
            {
                var LonguitudArchivo = Convert.ToInt32(reader.BaseStream.Length);
                byte[] buffer = new byte[LonguitudArchivo];
                buffer = reader.ReadBytes(LonguitudArchivo);
                Compresion(buffer);


            }
             
        }

        public Dictionary<string,int> DiccionarioInicial(byte[] text)
        {
            Dictionary<string, int> DiccionarioInicial = new Dictionary<string, int>();
            foreach (byte Caracter in text)
            {
                var Caracter2 = Convert.ToString(Convert.ToChar(Caracter));

                if (!DiccionarioInicial.ContainsKey(Caracter2))
                {
                    DiccionarioInicial.Add(Caracter2, DiccionarioInicial.Count + 1);
                }
           
            }
            return DiccionarioInicial;
        }

        public int[] CompressFile(byte[] file, Dictionary<string, int> diccionarioinicialc)
        {
            var DiccionarioTemp = new Dictionary<string, int>(diccionarioinicialc);
            var Contador = 0;
            var CaracteresLeidos = string.Empty;
            var Bytes = new List<int>();
            while (Contador < file.Length)
            {
                CaracteresLeidos += Convert.ToString(Convert.ToChar(file[Contador]));
                Contador++;

                while (DiccionarioTemp.ContainsKey(CaracteresLeidos) && Contador < file.Length)
                {
                    CaracteresLeidos += Convert.ToString(Convert.ToChar(file[Contador]));
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

        public void Compresion(byte[] archivo)
        {
            Dictionary<string, int> Diccionario_Inicial = DiccionarioInicial(archivo);
            int[] ComprimirArchivo = CompressFile(archivo, Diccionario_Inicial);
            Escritur(ComprimirArchivo, Diccionario_Inicial);
        }

        
        public void Escritur(int[] Compresion , Dictionary<string, int> diccionario)
        {
             

            string CarpetaCompress = Environment.CurrentDirectory;

            if (!Directory.Exists(Path.Combine(CarpetaCompress, "CompressLZW")))
            {
                Directory.CreateDirectory(Path.Combine(CarpetaCompress, "CompressLZW"));
            }

            using (var streamWriter = new FileStream(Path.Combine(CarpetaCompress, "CompressLZW", $"{Name}.lzw"), FileMode.OpenOrCreate))
            {
                using (var write = new BinaryWriter(streamWriter))
                {

                    write.Write(Encoding.UTF8.GetBytes(Convert.ToString(diccionario.Count).PadLeft(8, '0').ToCharArray()));
                    

                    foreach (var item in diccionario)
                    {   
                        write.Write(Convert.ToByte(Convert.ToChar(item.Key)));
                    }

                    double CantidadMaxima = Math.Log2(DiccionarioTotal.Count());

                    if (CantidadMaxima % 1 >= 0.5)
                    { 
                        CantidadMaxima = Convert.ToInt32(CantidadMaxima);
                    }
                    else
                    {
                        CantidadMaxima = Convert.ToInt32(CantidadMaxima) + 1;
                    }

                    write.Write(Convert.ToByte(CantidadMaxima));

                    var CompresionEnBinario = new List<string>();

                    foreach (var item in Compresion)
                    { 
                        CompresionEnBinario.Add(Convert.ToString(item , 2).PadLeft(Convert.ToInt32(CantidadMaxima),'0'));
                    }

                    var EscrituraBitesCompresion = new List<byte>();

                    string Auxiliar = string.Empty;

                    foreach (var item in CompresionEnBinario)
                    {
                        Auxiliar = Auxiliar + item;
                        if (Auxiliar.Length >= 8) 
                        {
                       
                            int CantiadadMaximaBits = Auxiliar.Length / 8;

                            for (int i = 0; i < CantiadadMaximaBits; i++)
                            {
                                var sdf = Convert.ToInt32(Auxiliar.Substring(0, 8), 2);
                                var asdf= Convert.ToInt32((Auxiliar.Substring(0, 8)), 2);
                                 
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
