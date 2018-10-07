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

            terminales.Add("0");
            terminales.Add("1");
            terminales.Add("a");
            terminales.Add("b");
            terminales.Add("+");
            terminales.Add("*");
            terminales.Add("(");
            terminales.Add(")");

            noTerminales.Add("E");
            noTerminales.Add("I");

            Gramatica gramatica = new Gramatica("E", Archivo.LeerArchivo("../../Reglas.txt"), terminales, noTerminales);
            
            AnalizadorSintactico analizador = new AnalizadorSintactico(gramatica);
            analizador.Analizar("a+b");

            Console.ReadKey();
        }
    }
}
