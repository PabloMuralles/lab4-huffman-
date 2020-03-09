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
                        string value = valor[j];
                        Datos_Comp = Datos_Comp + value;
                    }
                }
            }
            generarArchivoDiccionario();
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
