using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_Compresion.LZW.Historial
{
    public class Resultado
    {
        public int NumeroArchivo { get; set; }
        public string NombreOriginal { get; set; }

        public string NombreComprido { get; set; }

        public string RutaArchivoComprimido { get; set; }

        public double FactorCompresion { get; set; }

        public double Razoncompresion { get; set; }

        public double PorcentajeReduccion { get; set; }


    }
}
