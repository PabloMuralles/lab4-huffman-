using System;
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
        public void Comprimir(Dictionary<string, string> lectura)
        {
            string CarpetaCompress = Environment.CurrentDirectory;

            if (!Directory.Exists(Path.Combine(CarpetaCompress, "CompressHuffman")))
            {
                Directory.CreateDirectory(Path.Combine(CarpetaCompress, "CompressHuffman"));
            }

            using (var LecturaArchivo = new BinaryReader(Archivo))
            {
                using (var Archivo = new FileStream(Path.Combine(CarpetaCompress, "CompressHuffman",$"{NombreNuevo}.huff"),FileMode.OpenOrCreate))
                {
                    using (var Escritura = new BinaryWriter(Archivo))
                    {
                        Escritura.Write(Encoding.UTF8.GetBytes(Convert.ToString(lectura.Count).PadLeft(8, '0').ToArray()));

                        foreach (var item in lectura)
                        {
                            Escritura.Write(item.Key);
                            var Auxiliar = item.Value;
                            Auxiliar += "|";
                            Escritura.Write(Auxiliar);
                        }
                        var CadenaBits = string.Empty;
                        var Buffer = new byte[10000];

                        while (LecturaArchivo.BaseStream.Position != LecturaArchivo.BaseStream.Length)
                        {
                            Buffer = LecturaArchivo.ReadBytes(10000);

                            foreach (var item in Buffer)
                            {
                                var caracter = Convert.ToString(Convert.ToChar(item));
                                if (lectura.ContainsKey(caracter))
                                {
                                    lectura.TryGetValue(caracter, out var prefijo);

                                    CadenaBits += prefijo;
                                    if ((CadenaBits.Length / 8) != 0)
                                    {
                                        for (int i = 0; i < CadenaBits.Length / 8; i++)
                                        {
                                            var CadenaBitsAux = CadenaBits.Substring(0, 8);
                                            Escritura.Write(Convert.ToByte(Convert.ToInt32(CadenaBitsAux,2)));
                                            CadenaBits = CadenaBits.Substring(8);
                                        }
                                    }
                                }
                            }
                            if (CadenaBits.Length <= 8 )
                            {
                                Escritura.Write(CadenaBits);
                            }
                        }
                    }
                } 
            } 
        } 
    }
}
