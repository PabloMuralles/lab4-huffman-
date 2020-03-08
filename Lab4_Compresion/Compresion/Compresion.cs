using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Lab4_Compresion.Compresion
{
    public class Compresion
    {
        string archivo = string.Empty;
        List<string> Datos_comprimidos = new List<string>();
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
            var dato = new StreamReader(path);
            archivo = dato.ReadLine(); 
        }
        public void Comprimir(Dictionary<string, string> lectura)
        {
            List<string> llave = new List<string>(lectura.Keys);
            List<string> valor = new List<string> (lectura.Values);
            for (int i = 0; i < archivo.Length; i++)
            {
              var letra =  archivo.Substring(i, 1);
                for (int j = 0; j < llave.Count; j++)
                {
                    if (letra == llave[j])
                    {
                        string V = valor[j];
                        Datos_comprimidos.Add(V);
                    }
                }
            }
        }
    }
}
