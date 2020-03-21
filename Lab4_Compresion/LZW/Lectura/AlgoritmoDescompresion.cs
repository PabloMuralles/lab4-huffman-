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
         


        public AlgoritmoDescompresion(IFormFile Archivo)
        {
            
            Lectura(Archivo);
        }

        public void Lectura(IFormFile file )
        {
            string Carpetadesscompress = Environment.CurrentDirectory;

            if (!Directory.Exists(Path.Combine(Carpetadesscompress, "desscompressLZW")))
            {
                Directory.CreateDirectory(Path.Combine(Carpetadesscompress, "desscompressLZW"));
            }
            using (var reader = new BinaryReader(file.OpenReadStream()))
            {
                using (var streamwitre = new FileStream(Path.Combine(Carpetadesscompress, "desscompressLZW","hola"),FileMode.OpenOrCreate))
                {
                    using (var streamwrite = new BinaryWriter(streamwitre))
                    {
                        var CantidadDiccioanriobytes = reader.ReadBytes(8);
                        
                        var CantidadDiccioanrioNumeros = Convert.ToInt32(Encoding.UTF8.GetString(CantidadDiccioanriobytes));

                        var DiccionarioCaracteres = reader.ReadBytes(CantidadDiccioanrioNumeros);

                        var CaracteresDic = Encoding.UTF8.GetString(DiccionarioCaracteres);

                        var DiccionarioInicial = new Dictionary<string, int>();

                        var Contador = 1;

                        foreach (var item in CaracteresDic)
                        {
                            if (Contador != CantidadDiccioanrioNumeros)
                            {
                                DiccionarioInicial.Add(Convert.ToString(item) , Contador);
                                Contador++;
                            }
                        }


                    }
                }

            }
        }



    }
}
