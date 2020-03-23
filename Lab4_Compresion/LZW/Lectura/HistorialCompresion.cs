using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab4_Compresion.LZW.Lectura
{
    public class HistorialCompresion
    {
        private static HistorialCompresion _instance = null;
        public static HistorialCompresion Instance
        {
            get
            {
                if (_instance == null) _instance = new HistorialCompresion ();
                return _instance;
            }
        }

        public List<DatosArchivoCompresion>ArchivosComprimidosPila = new List<DatosArchivoCompresion>();

        public List<DatosArchivoDescomprido> ArchivosDescomprimidosPils = new List<DatosArchivoDescomprido>();




    }
}
