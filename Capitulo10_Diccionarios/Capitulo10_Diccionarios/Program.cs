using System;
using System.Collections.Generic;
using System.Linq;

namespace Capitulo10_Diccionarios
{
    /// <summary>
    /// Ejemplo 1: Evaluación de dos funciones de Hash. 
    /// Vamos a ver una función de Hash creada por mí y una 
    /// realizada en base al enfoque de ReSharper. La idea es ver 
    /// qué tan bien reparte cada una, una serie de enteros (desde 
    /// 0 hasta 1.000.000) en un número de buckets (190.000)
    /// La mejor función de hash será la que reparte más uniformememente
    /// Como siempre, usamos una instancia de la struct <see cref="EncabezadoYPieConsola"/> 
    /// para separar la presentación de la ejecución de los ejemplos en la consola
    /// 
    /// Ejemplo 2: Ejemplo simple de Diccionario
    /// En este ejemplo usamos las estructuras definidas en el archivo Futbolista.cs para
    /// crear una lista de los futbolistas mejor pagos del futbol mundial según la revista 
    /// France Football (año 2017). Una vez construida la lista, creamos un loop para que
    /// el usuario proporcione un código de fichaje. Con ese código construimos un ID y lo buscamos
    /// en el diccionario, mostrando luego un mensaje acorde al resultado de la búsqueda
    /// Es interesante ver cómo funciona la struct IDDeFutbolista, lanzando una excepción
    /// cuando el código de fichaje proporcionado por el usuario tiene una estructura incorrecta
    /// En lugar de TryGetValue hubiéramos podido usar un indexador para la búsqueda, del siguiente modo:
    /// Console.WriteLine(futbolistas[idABuscar].ToString()); 
    /// En ese caso, si el ID no existe, se lanza una excepción del tipo KeyNotFoundException
    /// Dado que estamos dentro de un bloque try catch la misma sería atrapada sin problemas y el código quedaría 
    /// más compacto. Como ejercicio, prueba modificar el código para usar ese método de búsqueda
    /// Observa que el objeto <see cref="Futbolista"/> puede ser tan complejo como querramos sin afectar el mecanismo 
    /// de búsqueda ni su rapidez. La búsqueda utiliza solamente el ID,
    /// si ntener en cuenta el resto de los campos o propiedades de Futbolista. Este es el poder del Diccionario 
    /// </summary>
    class Program
    {

        static void Main(string[] args)
        {
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


            #region Ejemplo 2: Uso de Diccionario con UDTs
            separador.EscribirEncabezado("Ejemplo 2: Uso de Diccionario con UDTs");

            Dictionary<IDDeFutbolista, Futbolista> futbolistas = new Dictionary<IDDeFutbolista, Futbolista>();

            IDDeFutbolista idDeCRonaldo = new IDDeFutbolista("A123789");
            Futbolista CRonaldo = new Futbolista(idDeCRonaldo, "Cristiano", "Ronaldo", "Real Madrid", "Portuguesa", 87.5f);
            Console.WriteLine(CRonaldo.ToString());
            futbolistas.Add(idDeCRonaldo, CRonaldo);

            IDDeFutbolista idDeLMessi = new IDDeFutbolista("X779");
            Futbolista LMessi = new Futbolista(idDeLMessi, "Lionel", "Messi", "Barcelona", "Argentina", 76.5f);
            Console.WriteLine(LMessi.ToString());
            futbolistas.Add(idDeLMessi, LMessi);


            IDDeFutbolista idDeNDaSilva = new IDDeFutbolista("B2587");
            Futbolista NDaSilva = new Futbolista(idDeNDaSilva, "Neymar", "Da Silva", "Barcelona", "Brasileña", 55.5f);
            Console.WriteLine(NDaSilva.ToString());
            futbolistas.Add(idDeNDaSilva, NDaSilva);

            IDDeFutbolista idDeGBale = new IDDeFutbolista("S1055");
            Futbolista GBale = new Futbolista(idDeGBale, "Gareth", "Bale", "Real Madrid", "Galesa", 41.0f);
            Console.WriteLine(GBale.ToString());
            futbolistas.Add(idDeGBale, GBale);

            IDDeFutbolista idDeELavezzi = new IDDeFutbolista("T3301");
            Futbolista ELavezzi = new Futbolista(idDeELavezzi, "Ezequiel", "Lavezzi", "Hebei Fortune", "Argentina", 28.5f);
            Console.WriteLine(ELavezzi.ToString());
            futbolistas.Add(idDeELavezzi, ELavezzi);

            string codigoABuscar = "";
            IDDeFutbolista idABuscar;
            Futbolista futbolistaEncontrado;
            while (codigoABuscar.ToUpper() != "S")
            {
                Console.WriteLine("Ingrese un código de fichaje para buscar");
                codigoABuscar = Console.ReadLine();

                if (codigoABuscar.ToUpper() == "S")
                {
                    break;
                }

                try
                {
                    idABuscar = new IDDeFutbolista(codigoABuscar);
                    futbolistas.TryGetValue(idABuscar, out futbolistaEncontrado);

                    if (futbolistaEncontrado == null)
                    {
                        Console.WriteLine("No se encontró ningún futbolista con ese código de fichaje");
                    }
                    else
                    {
                        Console.WriteLine("El siguiente futbolista tiene un código de fichaje que coincide con el valor ingresado:");
                        Console.WriteLine(futbolistaEncontrado.ToString());
                    }
                }
                catch (Exception e)
                {

                    Console.WriteLine("Se Encontró una excapción al procesar el código de fichaje ingresado: {0}", e.Message);
                }

            }

            separador.EscribirPie("Fin de Ejemplo 2");
            #endregion



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
        /// En esta función utilizo el enfoque que usa Resharper(R)
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
    /// <summary>
    /// Esta struct se utiliza para ayudar a separar la salida a consola de los ejemplos
    /// </summary>
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
