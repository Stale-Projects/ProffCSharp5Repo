using System;
using System.Collections.Generic;

namespace Chapter10Queues
{
    class Program
    {
        static void Main(string[] args)
        {
            EncabezadoYPieConsola separador = new EncabezadoYPieConsola();

            #region Ejemplo 1: Cola simple
            separador.EscribirEncabezado("Ejemplo 1: Cola simple");

            Queue<int> cola = new Queue<int>();
            cola.Enqueue(2);
            cola.Enqueue(4);
            cola.Enqueue(8);
            Console.WriteLine("El primer elemento de la cola es: {0}", cola.Dequeue().ToString());

            separador.EscribirPie("Fin Ejemplo 1");
            #endregion

            #region Ejemplo 2: Constructores
            separador.EscribirEncabezado("Ejemplo 2: Constructores");

            Queue<Pasajero> primeraClase = new Queue<Pasajero>(10);

            primeraClase.Enqueue(new Pasajero("Charles Lindbergh", 3, "A"));
            primeraClase.Enqueue(new Pasajero("Antoine de Saint Exupery", 1, "B"));
            primeraClase.Enqueue(new Pasajero("Amelia Earhart", 2, "C"));
            Pasajero elSiguiente = primeraClase.Dequeue();
            Console.WriteLine("El primero en llegar fue: {0} que se sienta en {1}-{2}", elSiguiente.Nombre, elSiguiente.Fila.ToString(), elSiguiente.Asiento);

            separador.EscribirPie("Fin de ejemplo 2");
            #endregion

            #region Ejemplo 3: Enumerador
            separador.EscribirEncabezado("Ejemplo 3: Enumerador");

            foreach (var pasajero in primeraClase)
            {
                Console.WriteLine("El siguiente es: {0} que se sienta en {1}-{2}", pasajero.Nombre, pasajero.Fila.ToString(), pasajero.Asiento);
            }

            separador.EscribirPie("Fin ejemplo 3");
            #endregion

        }
    }

    struct EncabezadoYPieConsola
    {
        public void EscribirEncabezado(string Titulo)
        {
            string encabezado = PrepararString(Titulo, '*');
            Console.WriteLine(encabezado);
            Console.WriteLine("");
        }

        public void EscribirPie(string Titulo)
        {
            string pie = PrepararString(Titulo, '-');
            Console.WriteLine("");
            Console.WriteLine(pie);
            Console.WriteLine("");
            Console.WriteLine("");
        }

        private string PrepararString(string Literal, char Caracter)
        {
            if (Literal.Length > 80)
                Literal = Literal.Substring(1, 80);
            int numeroDeAsteriscos = 80 - Literal.Length;
            string preparada = new string(Caracter, (int)(numeroDeAsteriscos / 2)) + Literal +
            new string(Caracter, (int)(numeroDeAsteriscos / 2));
            return preparada;
        }
    }

}
