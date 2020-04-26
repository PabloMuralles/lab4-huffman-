﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace Lab4_Compresion.Compresion
{
    public class Compresion
    {
        FileStream Archivo;

        string NombreNuevo = string.Empty;
      
        public Compresion(string path, string NombreArchivo)
        {
            Archivo = new FileStream(path, FileMode.Open);
            NombreNuevo = NombreArchivo;
        }
        public void Comprimir(Dictionary<string, string> lectura, List<ArbolHuffman.Elementos> Frecuencia)
        {
            string CarpetaCompress = Environment.CurrentDirectory;

            if (!Directory.Exists(Path.Combine(CarpetaCompress, "CompressHuffman")))
            {
                Directory.CreateDirectory(Path.Combine(CarpetaCompress, "CompressHuffman"));
            }

            using (var LecturaArchivo = new BinaryReader(Archivo))
            {
                using (var ArchivoEscritura = new FileStream(Path.Combine(CarpetaCompress, "CompressHuffman",$"{NombreNuevo}.huff"),FileMode.OpenOrCreate))
                {
                    using (var Escritura = new BinaryWriter(ArchivoEscritura))
                    {
                        Escritura.Write(Encoding.UTF8.GetBytes(Convert.ToString(lectura.Count).PadLeft(8, '0').ToArray()));
 
                        foreach (var item in Frecuencia)
                        { 
                            Escritura.Write(Convert.ToByte(item.caracter));
                            var Auxiliar = Convert.ToString(item.probabilidad);
                            Auxiliar += "|";
                            Escritura.Write(Auxiliar.ToCharArray());
                          
                        }
                        var CadenaBits = string.Empty;
                        int longitud = 10000;
                        var Buffer = new byte[longitud];


                        while (LecturaArchivo.BaseStream.Position != LecturaArchivo.BaseStream.Length)
                        {
                            Buffer = LecturaArchivo.ReadBytes(longitud);
                             
                            foreach (var item in Buffer)
                            { 
                                var caracter = Convert.ToString(Convert.ToChar(item));
                                lectura.TryGetValue(caracter, out var prefijo);
                                CadenaBits += prefijo;
                                if (CadenaBits.Length >= 8)
                                {
                                    var cantidad = CadenaBits.Length / 8;
                                    for (int i = 0; i < cantidad; i++)
                                    {

                                        Escritura.Write(Convert.ToByte(Convert.ToInt32((CadenaBits.Substring(0, 8)), 2)));
                                        CadenaBits = CadenaBits.Substring(8);
                                    }
                                }

                            
                            }

                        }
                        if (CadenaBits.Length <= 8 && CadenaBits != "")
                        {
                            var Nuevo = CadenaBits.PadRight(8,'0');
                            Escritura.Write(Convert.ToByte(Convert.ToInt32(Nuevo, 2)));
                        }
                    }
                } 
            } 
        } 
    }
}
