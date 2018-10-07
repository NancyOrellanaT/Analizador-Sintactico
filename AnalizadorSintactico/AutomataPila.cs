using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintactico
{
    class AutomataPila
    {
        private List<Regla> reglas;
        private List<string> listaTerminales;
        private List<int> listaNumeroTerminales;

        private string cadena;
        private bool aceptado;
        private Gramatica gramatica;

        public AutomataPila(List<Regla> reglas, Gramatica gramatica)
        {
            aceptado = false;
            this.reglas = reglas;
            this.gramatica = gramatica;
            listaTerminales = new List<string>();
            listaNumeroTerminales = new List<int>();
        }

        public void Simular(string cadena)
        {
            this.cadena = cadena;

            ContarSimbolos();

            List<string> pila = new List<string>();
            pila.Add("z0");

            long t1 = CurrentTimeMillis();
            ProbarRegla("q0", 0, pila, new List<Regla>(), "");
            long t2 = CurrentTimeMillis();

            if (aceptado)
            {
                Console.WriteLine("Aceptado");
            }
            else
            {
                Console.WriteLine("No aceptado");
            }

            Console.WriteLine("Se simuló en " + (t2-t1) + " ms.");
        }
        
        private void ProbarRegla(string estado, int posicion, List<string> pila, List<Regla> camino, string cad)
        {
            bool seguir = Optimizar(new List<string>(pila));
            
            for(int i = 0; i < cad.Length; i++)
            {
                if (!cad[i].Equals(cadena[i]))
                {
                    seguir = false;
                }
            }

            if (!aceptado && seguir)
            {
                //Console.WriteLine(cad);

                String caracter = "";
                //El caracter actual
                if (posicion < cadena.Length)
                {
                    caracter = cadena[posicion] + "";
                }

                String estadoOriginal = estado;
                //Revisamos todas las reglas
                for (int i = 0; i < reglas.Count; i++)
                {
                    Regla regla = reglas[i];

                    List<String> pilaCopia = new List<String>(pila);

                    List<Regla> caminoCopia = new List<Regla>(camino);

                    estado = estadoOriginal;

                    //Comprobamos si es la regla adecuada 
                    if (regla.GetEstadoActual().Equals(estado))
                    {
                        //Comprobamos el tope de la pila
                        if (regla.GetCimaPila().Equals(pilaCopia.ElementAt(pilaCopia.Count - 1)) || regla.GetCimaPila().Equals("Z"))
                        {
                            //Si la entrada está bien probamos una nueva regla
                            if (regla.GetEntrada().Equals(caracter))
                            {
                                estado = regla.GetEstadoNuevo();
                                //Apilamos o desapilamos
                                if (regla.GetAccion().Equals("#"))
                                {
                                    List<String> pilaAux = new List<String>();
                                    for(int k = 0; k < pilaCopia.Count - 1; k++)
                                    {
                                        pilaAux.Add(pilaCopia.ElementAt(k));
                                    }
                                    pilaCopia = pilaAux;
                                }
                                else if (!regla.GetAccion().Equals("Z"))
                                {
                                    int menos = 2;

                                    if (regla.GetAccion()[regla.GetAccion().Length - 1] != 'Z')
                                    {
                                        pilaCopia.RemoveAt(pilaCopia.Count - 1);
                                        menos = 1;
                                    }

                                    for (int j = regla.GetAccion().Length - menos; j >= 0; j--)
                                    {
                                        pilaCopia.Add(regla.GetAccion()[j] + "");
                                    }
                                }
                                caminoCopia.Add(regla);

                                cad += regla.GetEntrada();
                                ProbarRegla(estado, posicion + 1, new List<String>(pilaCopia), new List<Regla>(caminoCopia), cad);
                            }
                            else if (regla.GetEntrada().Equals("#"))
                            {
                                estado = regla.GetEstadoNuevo();
                                //Apilamos o desapilamos
                                if (regla.GetAccion().Equals("#"))
                                {
                                    List<String> pilaAux = new List<String>();
                                    for (int k = 0; k < pilaCopia.Count - 1; k++)
                                    {
                                        pilaAux.Add(pilaCopia.ElementAt(k));
                                    }
                                    pilaCopia = pilaAux;
                                }
                                else if (!regla.GetAccion().Equals("Z"))
                                {
                                    int menos = 2;

                                    if (regla.GetAccion()[regla.GetAccion().Length - 1] != 'Z')
                                    {
                                        pilaCopia.RemoveAt(pilaCopia.Count - 1);
                                        menos = 1;
                                    }
                                    
                                    for (int j = regla.GetAccion().Length - menos; j >= 0; j--)
                                    {
                                        pilaCopia.Add(regla.GetAccion()[j] + "");
                                    }
                                }

                                caminoCopia.Add(regla);
                                ProbarRegla(estado, posicion, new List<String>(pilaCopia), new List<Regla>(caminoCopia), cad);
                            }
                        }
                    }
                }
            }

            if (cad.Equals(cadena) && (posicion == cadena.Length || (posicion == 0 && cadena.Equals("#"))))
            {
                if (pila.Count - 1 >= 0 && pila.ElementAt(pila.Count - 1).Equals("z0"))
                {
                    aceptado = true;
                }
            }
        }

        private bool Optimizar(List<string> pila)
        {
            int terminales = 0;
            

            for (int i = 0; i < pila.Count; i++)
            {
                for (int j = 0; j < gramatica.GetTerminales().Count; j++)
                {
                    if (pila[i].Equals(gramatica.GetTerminales()[j]))
                    {
                        ++terminales;

                        if (cadena.Length < terminales)
                        {
                            return false;
                        }
                       
                        break;
                    }
                }
            }

            for(int i = 0; i < listaTerminales.Count; i++)
            {
                int conteo = 0;

                for(int j = 0; j < cadena.Length; j++)
                {
                    if (cadena[j].ToString().Equals(listaTerminales[i]))
                    {
                        conteo++;
                    }
                }

                if (conteo > listaNumeroTerminales[i])
                {
                    return false;
                }
            }


            return true;
        }

        private void ContarSimbolos()
        {
            bool nuevoTerminal = true;

            for (int i = 0; i < cadena.Length; i++)
            {
                for (int j = 0; j < gramatica.GetTerminales().Count; j++)
                {
                    if ((cadena[i] + "").Equals(gramatica.GetTerminales()[j]))
                    {
                        nuevoTerminal = false;
                        for (int k = 0; k < listaTerminales.Count; k++)
                        {
                            if ((cadena[i] + "").Equals(listaTerminales[k].ToString()))
                            {
                                listaNumeroTerminales[k]++;
                                nuevoTerminal = true;
                            }
                        }
                        if (!nuevoTerminal)
                        {
                            listaTerminales.Add(cadena[i].ToString());
                            listaNumeroTerminales.Add(1);
                            nuevoTerminal = false;
                        }
                    }
                }
            }
        }

        private static readonly DateTime Jan1st1970 = new DateTime
        (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }


    }
}
