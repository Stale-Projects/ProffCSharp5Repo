using System;
using System.Collections.Generic;
using System.Linq;

namespace Capitulo10_Diccionarios
{
    class Program
    {
        /// <summary>
        /// Ejemplo 1: Evaluación de dos funciones de Hash. 
        /// Vamos a ver una función de Hash creada por mí y una 
        /// realizada en base al enfoque de ReSharper. La idea es ver 
        /// qué tan bien reparte cada una, una serie de enteros (desde 
        /// 0 hasta 1.000.000) en un número de buckets (190.000)
        /// La mejor función de hash será la que reparte más uniformememente
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Separador
            EncabezadoYPieConsola separador = new EncabezadoYPieConsola();

            #region Ejemplo 1: Dos funciones de Hash
            separador.EscribirEncabezado("Ejemplo 1: Evaluación de Función Hash");


            int[] bucket = new int[190000];

            for (int i = 0; i < 1000000; i++)
            {
                int hash = FuncionHash(i + 1);
                bucket[hash]++;
            }



            Console.WriteLine("Función Hash de MV");
            Console.WriteLine("El promedio de elementos en un bucket es: {0}", bucket.Average().ToString());
            Console.WriteLine("El número máximo de elementos en un bucket es: {0}", bucket.Max().ToString());
            Console.WriteLine("El número mínimo de elementos en un bucket es: {0}", bucket.Min().ToString());

            IEnumerable<int> cuenta;
            for (int k = 0; k <= bucket.Max(); k++)
            {
                cuenta = bucket.Where(x => x == k);
                Console.WriteLine("Hay {0} buckets con {1} elementos", cuenta.Count().ToString(), k.ToString());

            }


            int[] bucketv2 = new int[190000];

            for (int i = 0; i < 1000000; i++)
            {
                int hash = FuncionHashv2(i + 1);
                bucketv2[hash]++;
            }


            Console.WriteLine("Función Hash de Resharper");
            Console.WriteLine("El promedio de elementos en un bucket es: {0}", bucketv2.Average().ToString());
            Console.WriteLine("El número máximo de elementos en un bucket es: {0}", bucketv2.Max().ToString());
            Console.WriteLine("El número mínimo de elementos en un bucket es: {0}", bucketv2.Min().ToString());

            IEnumerable<int> cuentav2;
            for (int k = 0; k <= bucketv2.Max(); k++)
            {
                cuentav2 = bucketv2.Where(x => x == k);
                Console.WriteLine("Hay {0} buckets con {1} elementos", cuentav2.Count().ToString(), k.ToString());

            }
            separador.EscribirPie("Fin Ejemplo 1");
            #endregion

            string codigoDeFichaje = "";
            char prefijo;
            int numero;
            string codigoFinal;
            while (codigoDeFichaje.ToUpper() != "C")
            {
                Console.WriteLine("Ingresá el código:");
                codigoDeFichaje = Console.ReadLine().ToUpper();
                prefijo = codigoDeFichaje[0];
                int.TryParse(codigoDeFichaje.Substring(1), out numero);

                //codigoFinal = prefijo + numero.ToString("000000"); //string.Format("{0:000000}", codigoDeFichaje.Substring(1));
                codigoFinal = prefijo + string.Format("{0:000000}", numero);// numero.ToString("000000");
                Console.WriteLine("El código final es: {0}", codigoFinal);
                Console.ReadLine();

            }




        }


        /// <summary>
        /// Esta función de Hash utiliza un enfoque en el que mezclo los bits de un número
        /// Para un int32 tomo los 16 primeros bits y los pongo al final, y viceversa
        /// Luego hago una operación & con el hexa 0x7fffffff para quitar el signo
        /// Por último hago que el hash "caiga" en uno de los 190.000 buckets que tengo
        /// tomando el resto de la división por 190.000
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static int FuncionHash(int key)
        {
            int _hash;
            int _derecha16;
            int _izquierda16;
            _derecha16 = key >> 16;
            _izquierda16 = key << 16;
            _hash = _derecha16 ^ _izquierda16;
            _hash = _hash & 0x7fffffff;
            return _hash % 190000;
        }

        /// <summary>
        /// En esta función utilizo el enfoque que vi usa Resharper
        /// Tomando como base el hash del key (el que calcula el CLR)
        /// lo multiplico por un número primo grande (en este caso 397),
        /// le quito el signo y luego tomo el resto como en la función anterior
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static int FuncionHashv2(int key)
        {
            int _hash;
            _hash = key.GetHashCode() * 397;
            _hash = _hash & 0x7fffffff;
            return _hash % 190000;
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
