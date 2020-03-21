using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;


namespace Lab4_Compresion.LZW.Lectura
{
    public class AlgoritmoDescompresion
    {
        public AlgoritmoDescompresion()
        {

        }

        public void Lectura()
        {
            string CarpetaCompress = Environment.CurrentDirectory;

            if (!Directory.Exists(Path.Combine(CarpetaCompress, "desscompressLZW")))
            {
                Directory.CreateDirectory(Path.Combine(CarpetaCompress, "desscompressLZW"));
            }
             
        }



    }
}
