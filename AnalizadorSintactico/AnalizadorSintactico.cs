using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintactico
{
    class AnalizadorSintactico
    {

        private Gramatica gramatica;
        private List<Regla> reglas;

        public AnalizadorSintactico(Gramatica gramatica)
        {
            this.gramatica = gramatica;
            reglas = new List<Regla>();
            ConvertirGramaticaAutomata();
        }

        public void Analizar(string cadena)
        {
            for(int i = 0; i < reglas.Count; i++)
            {
                Console.WriteLine(reglas[i].AString());
            }

            AutomataPila automata = new AutomataPila(reglas);
            automata.Simular(cadena);
        }

        public void ConvertirGramaticaAutomata()
        {
            List<string> noTerminales = gramatica.GetNoTerminales();
            List<string> terminales = gramatica.GetTerminales();
            string noTerminalActual = "";

            Regla regla = new Regla("q0", "#", "Z", "q1", gramatica.GetSimboloInicial());
            reglas.Add(regla);

            for (int i = 0; i < gramatica.GetNoTerminales().Count; i++)
            {
                noTerminalActual = noTerminales[i].ToString();

                //Para cada regla variable con no terminales
                for (int j = 0; j < gramatica.GetProducciones().Count; j++)
                {
                    if (noTerminalActual.Equals(gramatica.GetProducciones()[j].GetLadoIzquierdo().ToString()))
                    {
                        regla = new Regla("q1", "#", noTerminalActual, "q1", gramatica.GetProducciones()[j].GetLadoDerecho().ToString());
                        reglas.Add(regla);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            //Para cada terminal 
            for (int i = 0; i < terminales.Count; i++)
            {
                regla = new Regla("q1", terminales[i].ToString(), terminales[i].ToString(), "q1", "#");
                reglas.Add(regla);
            }

            regla = new Regla("q1", "#", "Z", "q2", "#");
            reglas.Add(regla);
        }

    }
}