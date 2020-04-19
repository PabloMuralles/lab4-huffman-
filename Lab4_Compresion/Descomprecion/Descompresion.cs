using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace Lab4_Compresion.Descomprecion
{
    public class Descompresion
    {
        string NombreArchivo = string.Empty;
         
        string TextoDescomprimir = string.Empty;
         
        Dictionary<string, string> Caracteres = new Dictionary<string, string>();
         
        public Descompresion(string nombrearchivocontrolller, string leercontroller)
        {
            NombreArchivo = nombrearchivocontrolller;
            Descomprimir(leercontroller);
        }
        public void Descomprimir(string leer)
        {
            string CarpetaCompress = Environment.CurrentDirectory;

            if (!Directory.Exists(Path.Combine(CarpetaCompress, "DecompressHuffman")))
            {
                Directory.CreateDirectory(Path.Combine(CarpetaCompress, "DecompressHuffman"));
            }
            var ArchivoLeer = new FileStream(leer,FileMode.Open);
            using (var Lecturas = new BinaryReader(ArchivoLeer))
            {
                using (var Archivo = new FileStream(Path.Combine(CarpetaCompress, "DecompressHuffman",$"{NombreArchivo}.txt"), FileMode.OpenOrCreate))
                {
                    using (var Escritura = new BinaryWriter(Archivo))
                    {
                        var Probabilidades = new List<ArbolHuffman.Elementos>();

                        var Verficacion = new List<char>();

                        var CantidadDeCaracteres = Lecturas.ReadBytes(8);

                        var ContadorCaracteres = Convert.ToInt32(Encoding.UTF8.GetString(CantidadDeCaracteres));

                        var TamañoBuffer = 10000;

                        var buffer = new byte[TamañoBuffer];

                        TamañoBuffer = 1;

                        for (int i = 0; i < ContadorCaracteres; i++)
                        {
                           
                            buffer = Lecturas.ReadBytes(TamañoBuffer);

                            var Caracter = Convert.ToChar(buffer[0]);

                            buffer = Lecturas.ReadBytes(TamañoBuffer);

                            var ProbabilidadCaracter = string.Empty;

                            while (Convert.ToChar(buffer[0]) != '|')
                            {
                                ProbabilidadCaracter += Convert.ToString(Convert.ToChar(buffer[0]));

                                buffer = Lecturas.ReadBytes(TamañoBuffer);
                                 
                            }

                            Probabilidades.Add(new ArbolHuffman.Elementos { caracter = Caracter, probabilidad = Convert.ToDouble(ProbabilidadCaracter)});

                            Verficacion.Add(Caracter);

                            
                        }

                        ArbolHuffman.Arbol NuevoArbol = new ArbolHuffman.Arbol(Probabilidades);

                        var Indices = NuevoArbol.CrearArbol();

                        TamañoBuffer = 10000;

                        var CadenaEvalucar = string.Empty;

                        var CadenaEvaluarAux = string.Empty;

                        while (Lecturas.BaseStream.Position != Lecturas.BaseStream.Length)
                        {
                            buffer = Lecturas.ReadBytes(TamañoBuffer);

                            foreach (var item in buffer)
                            {
                                var preuba = Convert.ToChar(item);
                                var NumeroNormal  = Convert.ToInt32(Convert.ToString(item));
                                var NumeroBinario = Convert.ToString(NumeroNormal, 2).PadLeft(8,'0');

                                CadenaEvalucar += NumeroBinario;
                                 

                                while (CadenaEvalucar.Length > 0)
                                {
                                    if (Indices.Values.Contains(CadenaEvaluarAux))
                                    {
                                        var revision = Convert.ToChar(Indices.FirstOrDefault(x => x.Value == CadenaEvaluarAux).Key);
                                        Escritura.Write(Convert.ToByte(Convert.ToChar(Indices.FirstOrDefault(x => x.Value == CadenaEvaluarAux).Key)));
                                        CadenaEvaluarAux = string.Empty;
                                    }
                                    else
                                    {
                                        CadenaEvaluarAux += CadenaEvalucar.Substring(0, 1);
                                        CadenaEvalucar = CadenaEvalucar.Substring(1);
                                    }
                                }


                            }



                        }

                       

                        


                    }
                }
            }
        }
    }
}
