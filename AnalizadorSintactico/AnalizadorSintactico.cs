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

        public AnalizadorSintactico()
        {
        }

        public void Analizar(Gramatica gramatica)
        {
            this.gramatica = gramatica;
        }

        public void ConvertirGramaticaAutomata()
        {
            
        }
    }
}
