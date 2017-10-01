using System;
using System.Collections;
using System.Collections.Generic;

namespace Yield_Ejemplo
{
    public class Program
    {
        static void Main()
        {

            //Llamando a un enumerador propio
            var saludo = new PrimerSaludo();
            foreach (var palabra in saludo)
            {
                Console.WriteLine(palabra);
            }

            Naturales numerosNaturales = new Naturales(7);
            Console.WriteLine("Enumeración Simple");

            foreach (var numero in numerosNaturales)
            {
                Console.WriteLine(numero.ToString());
            }


            Console.WriteLine("Enumeración de Pares");
            foreach (var numeroPar in numerosNaturales.NumerosPares())
            {
                Console.WriteLine(numeroPar.ToString());
            }

            Console.WriteLine("Enumeración Inversa");
            foreach (var numero in numerosNaturales.SecuenciaInvertida())
            {
                Console.WriteLine(numero.ToString());
            }

            //Console.WriteLine("Una enumeración complicada");
            //NaturalesComplicados numerosNaturalesComplicados = new NaturalesComplicados(0, 5);
            //numerosNaturalesComplicados.Enumerar();
            //Console.WriteLine();

            //Console.WriteLine("Una aún más complicada!");
            ////En este caso, los enumeradores escriben en la consola el valor actual
            ////pero a la vez lo puedo recuperar en el programa principal
            //foreach (var natural in numerosNaturalesComplicados)
            //{
            //    Console.WriteLine(natural.ToString());
            //}
            //Console.WriteLine();

            //Enumeración de figuras
            Figuras misFiguras = new Figuras();
            foreach (var figura in misFiguras)
            {
                Console.WriteLine(figura);
            }


            return;

            //Una instrucción foreach 
            List<string> lista = new List<string>() { "Alfa", "Beta", "Gama", "Delta" };
            foreach (var item in lista)
            {
                Console.WriteLine(item);
            }

            //Lo que esconde la instruccion foreach
            IEnumerator<string> enumerador = lista.GetEnumerator();
            while (enumerador.MoveNext())
            {
                var item = enumerador.Current;
                Console.WriteLine(item);
            }

            //Llamando a un enumerador propio
            //var saludo = new PrimerSaludo();
            //foreach (var palabra in saludo)
            //{
            //    Console.WriteLine(palabra);
            //}


            //
            // Compute two with the exponent of 30.
            //
            IEnumerable<int> potenciasDeDos = ComputePower(2, 30);

            foreach (int valor in potenciasDeDos)
            {
                Console.Write(valor);
                Console.Write(" ");
            }
            Console.WriteLine();
        }


        public class PrimerSaludo
        {
            public IEnumerator GetEnumerator()
            {
                return new Enumerador(0);
            }
            public class Enumerador : IEnumerator<string>, IEnumerator, IDisposable
            {
                private int estado;
                private string current;
                public Enumerador(int estado)
                {
                    this.estado = estado;
                }
                bool System.Collections.IEnumerator.MoveNext()
                {
                    switch (estado)
                    {
                        case 0:
                            current = "Hola";
                            estado = 1;
                            return true;
                        case 1:
                            current = "Mundo";
                            estado = 2;
                            return true;
                        case 2:
                            break;
                    }
                    return false;
                }
                void System.Collections.IEnumerator.Reset()
                {
                    throw new NotSupportedException();
                }
                string System.Collections.Generic.IEnumerator<string>.Current
                {
                    get
                    {
                        return current;
                    }
                }
                object System.Collections.IEnumerator.Current
                {
                    get
                    {
                        return current;
                    }
                }
                void IDisposable.Dispose()
                {
                }
            }
        }

        //private class PrimerSaludo
        //{
        //    public IEnumerator<string> GetEnumerator()
        //    {
        //        yield return "Hola";
        //        yield return " Mundo";
        //    }

        //}


        public static IEnumerable<int> ComputePower(int number, int exponent)
        {
            int exponentNum = 0;
            int numberResult = 1;
            //
            // Continue loop until the exponent count is reached.
            //
            while (exponentNum < exponent)
            {
                //
                // Multiply the result.
                //
                numberResult *= number;
                exponentNum++;
                //
                // Return the result with yield.
                //
                yield return numberResult;
            }
        }
    }
}