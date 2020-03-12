using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Lab4_Compresion.LZW
{
    public class LecturaLZW
    {
        public Dictionary<char, int> Diccionario = new Dictionary<char, int>();

        string W = string.Empty;
        string K = string.Empty;
        string WK = string.Empty;
        

        int LongitudBuffer = 1000000;
        
       public void LecturaArchivo(string archivo)
        {
            var Letras = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,ñ,o,p,q,r,s,t,u,v,w,x,y,z,A,B,C,D,E,F,G,H,I,J,K,L,M,N,Ñ,O,P,Q,R,S,T,U,V,W,X,Y,Z,0,1,2,3,4,5,6,7,8,9";
            int NumIDDiccionario = 1;
            foreach (var item in Letras.Split(','))
            {
                Diccionario.Add(Convert.ToChar(item), NumIDDiccionario);
                NumIDDiccionario++;

            }
            


        }




    }
}
