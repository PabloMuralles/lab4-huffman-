using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_Compresion.Descomprecion
{
    public class Descompresion
    {
        string NombreArchivo = string.Empty;

        const int longitud = 1000000;
        public Descompresion(string nombrearchivocontrolller, string leercontroller)
        {
            NombreArchivo = nombrearchivocontrolller;
        }
        public void Descomprimir(string leer)
        {

            using (var stream = new FileStream(leer, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var buffer = new byte[longitud];
                    buffer = reader.ReadBytes(longitud);
                    foreach (byte bit in buffer)
                    {
                    
                    }
                }
            }
 
        }
    }
}
