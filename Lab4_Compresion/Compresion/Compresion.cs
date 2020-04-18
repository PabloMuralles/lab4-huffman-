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
        string Datos_Comp = "";
        public string RutaArchivos = string.Empty;
        public Dictionary<char, int> Ocurrencia = new Dictionary<char, int>();
        // List<string> Datos_comprimidos = new List<string>();
        private static Compresion _instance = null;
        public static Compresion Instance
        {
            get
            {
                if (_instance == null) _instance = new Compresion();
                return _instance;
            }
        }
       
       
        public void asignar(string path)
        {
            Archivo = new FileStream(path, FileMode.Open);
             
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
                using (var Archivo = new FileStream(Path.Combine(CarpetaCompress, "CompressHuffman",$"{RutaArchivos}.huff"),FileMode.OpenOrCreate))
                {
                    using (var Escritura = new BinaryWriter(Archivo))
                    {
                        Escritura.Write(Encoding.UTF8.GetBytes(Convert.ToString(lectura.Count).PadLeft(8, '0').ToArray()));

                        foreach (var item in lectura)
                        {
                            Escritura.Write(item.Key);
                            Escritura.Write(item.Value);
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




        public void Comprimir2(Dictionary<string, string> lectura)
        {
            //List<string> llave = new List<string>(lectura.Keys);
            //List<string> valor = new List<string>(lectura.Values);
            //for (int i = 0; i < archivo.Length; i++)
            //{
            //    var letra = archivo.Substring(i, 1);
            //    for (int j = 0; j < llave.Count; j++)
            //    {
            //        if (letra == llave[j])
            //        {
            //            string value = valor[j];
            //            Datos_Comp = Datos_Comp + value;
            //        }
            //    }
            //}
            //generarArchivoDiccionario();

        }


        public void generarArchivoDiccionario()
        {
            StreamWriter streamWriter = new StreamWriter(@$"c:\temp\{RutaArchivos}.huff");
            foreach (var item in Ocurrencia)
            {
              streamWriter.WriteLine("{0}",item);
               
            }
            streamWriter.WriteLine($"{Datos_Comp}");
            streamWriter.Close();
        }
    }
}
