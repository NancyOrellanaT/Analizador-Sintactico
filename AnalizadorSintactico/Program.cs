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
            terminales.Add("2");
            terminales.Add("3");
            terminales.Add("4");
            terminales.Add("5");
            terminales.Add("6");
            terminales.Add("7");
            terminales.Add("8");
            terminales.Add("9");
            terminales.Add("+");
            terminales.Add("-");
            terminales.Add("*");
            terminales.Add("/");
            terminales.Add("^");

            noTerminales.Add("S");
            noTerminales.Add("D");

            Gramatica gramatica = new Gramatica("S", Archivo.LeerArchivo("../../Reglas.txt"), terminales, noTerminales);
            
            AnalizadorSintactico analizador = new AnalizadorSintactico(gramatica);
            analizador.Analizar("9+5");

            Console.ReadKey();
        }
    }
}
