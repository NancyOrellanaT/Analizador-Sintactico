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
            //Archivo.LeerArchivo("../../Reglas.txt");

            List<Regla> lista = new List<Regla>();
            lista.Add(new Regla("q0", "a", "Z", "q1", "AZ"));
            lista.Add(new Regla("q1", "a", "A", "q0", "#"));

            AutomataPila automata = new AutomataPila("a", lista);
            automata.Simular();

            Console.ReadKey();
        }
    }
}
