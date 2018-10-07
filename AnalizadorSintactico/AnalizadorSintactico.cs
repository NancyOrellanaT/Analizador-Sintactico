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
        private string entrada;
        private List<Regla> reglas;

        public AnalizadorSintactico()
        {
            reglas = new List<Regla>();
            entrada = "";
        }

        public void Analizar(Gramatica gramatica)
        {
            this.gramatica = gramatica;
            ConvertirGramaticaAutomata();
        }

        public List<Regla> ConvertirGramaticaAutomata()
        {
            List<string> noTerminales = gramatica.GetNoTerminales();
            List<string> terminales = gramatica.GetTerminales();
            string noTerminalActual = "";

            for (int i = 0; i < gramatica.GetNoTerminales().Count; i++)
            {
                noTerminalActual = noTerminales[i].ToString();

                //Para cada regla variable con no terminales
                for (int j = 0; j < gramatica.GetProducciones().Count; j++)
                {
                    if (noTerminalActual.Equals(gramatica.GetProducciones()[j].GetLadoIzquierdo().ToString()))
                    {
                        Regla regla = new Regla("q", "#", noTerminalActual, "q", gramatica.GetProducciones()[j].GetLadoDerecho().ToString());
                        reglas.Add(regla);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //Para cada terminal 
            for (int i = 0; i < terminales.Count; i++)
            {
                Regla regla = new Regla("q", terminales[i].ToString(), terminales[i].ToString(), "q", "#");
                reglas.Add(regla);
            }

            return reglas;
        }
    }
}