using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintactico
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> terminales = new List<string>();
            List<string> noTerminales = new List<string>();

            terminales.Add("1");
            terminales.Add("+");
            terminales.Add("-");

            noTerminales.Add("S");
            noTerminales.Add("E");

            Gramatica gramatica = new Gramatica("S", Archivo.LeerArchivo("../../Reglas.txt"), terminales, noTerminales);

            AnalizadorSintactico analizador = new AnalizadorSintactico(gramatica);
            analizador.Analizar("1-1+1-1+1+1+1+1+1+1+1+1+1+1+1+1");
            
            Console.ReadKey();
        }
        
    }

}
