﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Lab4_Compresion.Lectura
{
    public class Lectura
    {
        static Dictionary<char, Caracteres> diccionario = new Dictionary<char, Caracteres>();
        static List<byte> Frecuencia = new List<byte>();
        List<ArbolHuffman.Elementos> Ocurrencia = new List<ArbolHuffman.Elementos>();
        ArbolHuffman.Elementos elementos = null;
        public int num_total = 0;
        private static Lectura _instance = null;
        public static Lectura Instance
        {
            get
            {
                if (_instance == null) _instance = new Lectura();
                return _instance;
            }
        }
        const int longitud = 1000000;
        Caracteres caracter = null;
        public void Ingresar(string leer)
        {
            using (var stream = new FileStream(leer, FileMode.Open))
            {
                using (var reader = new BinaryReader(stream))
                {
                    var buffer = new byte[longitud];
                    buffer = reader.ReadBytes(longitud);
                    foreach (byte bit in buffer)
                    {
                        caracter = new Caracteres();
                        if (!diccionario.ContainsKey((char)bit))
                        {
                            caracter.cantidad = 1;
                            diccionario.Add((char)bit, caracter);
                        }
                        else
                        {
                            diccionario[(char)bit].cantidad++;
                        }
                        Frecuencia.Add(bit);
                    }
                }
            }
            Probabilidad();
            //ArbolHuffman.Arbol Arbol = new ArbolHuffman.Arbol(Ocurrencia);
        }
        public void Probabilidad()
        {
            foreach (var item in diccionario)
            {
                elementos = new ArbolHuffman.Elementos();
                double cantidad = (Convert.ToDouble(item.Value.cantidad));
                elementos.caracter = item.Key;
                elementos.probabilidad = Convert.ToDouble((cantidad / Frecuencia.Count()));
                Ocurrencia.Add(elementos);
            }
        }
    }
}
