using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalizadorSintactico
{
    class Archivo
    {

        public static List<Produccion> LeerArchivo(string direccionArchivo)
        {
            List<Produccion> producciones = new List<Produccion>();

            string[] lado = new string[2];

            string line;
            try
            {
                StreamReader sr = new StreamReader(direccionArchivo);

                line = sr.ReadLine();

                lado = line.Split(',');

                producciones.Add(new Produccion(lado[0], lado[1]));

                while (line != null)
                {
                    line = sr.ReadLine();
                    lado = line.Split(',');
                    producciones.Add(new Produccion(lado[0], lado[1]));
                }

                sr.Close();
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }

            for (int i = 0; i < producciones.Count; i++)
            {
                Console.WriteLine("Produccion: " + producciones[i].GetLadoIzquierdo() + " " + producciones[i].GetLadoDerecho());
            }

            return producciones;
        }
    }
}
