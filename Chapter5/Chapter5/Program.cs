using System;
using System.Collections.Generic;

namespace Chapter5
{
    class Program
    {

        static void Main(string[] args)
        {
            EncabezadoYPieConsola Escritor = new EncabezadoYPieConsola();

            #region Ejemplo 5.1
            //Ejemplo de Swap usando una generic function llamada Swap
            //ver la dfinición más abajo
            Escritor.EscribirEncabezado("Ejamplo 5.1: Swap usando generics");
            int x = 2;
            int y = 3;
            System.Console.WriteLine("Inicialmente x es: {0}, e y es {1}", x.ToString(), y.ToString());
            //Esta función se puede llamar como se llama debajo o así: Swap<int>(ref x, ref y);
            Swap(ref x, ref y);
            System.Console.WriteLine("Ahora x es: {0}, e y es {1}", x.ToString(), y.ToString());
            Escritor.EscribirPie("Fin de Ejemplo 5.1");
            #endregion


            //Ejemplo de uso de la función genérica 
            var accounts = new List<Account>()
            {
                new Account("Moe", 11.40M),
                new Account("Curly", 20.50M),
                new Account("Larry", 50.15M)
            };

            decimal sum = Algorithms.SimpleAccumulate(accounts);
            System.Console.WriteLine("Total: {0}", sum.ToString());

            int uno = 1;
            int? algo = uno;
            bool tieneValor = algo.HasValue;

        }

        //Funcion Generica para intercambiar dos Value Types cualesquiera 
        //(notar el atributo ref en los parametros)
        static void Swap<T>(ref T x, ref T y)
        {
            T temp;
            temp = x;
            x = y;
            y = temp;
        }

        public static void SwapV2<T>(ref T X, ref T Y) where T : struct
        {

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
}
