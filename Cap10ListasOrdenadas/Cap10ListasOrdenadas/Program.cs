using System;
using System.Collections.Generic;

namespace Cap10ListasOrdenadas
{
    class Program
    {
        static void Main(string[] args)
        {
            EncabezadoYPieConsola separador = new EncabezadoYPieConsola();

            #region Ejemplo 1
            separador.EscribirEncabezado("Ejemplo 1: Ejemplo simple de lista ordenada");

            //Constructor sin argumentos, capacidad inicial y método de comparación por defecto
            //dado que uso strings se comparan las strings según las definiciones del CLR
            //Vamos a crear una lista de libros de Borges, ordenados por ISBN
            SortedList<string, string> librosDeBorges = new SortedList<string, string>();

            //Agrego por medio de Add
            librosDeBorges.Add("978-8-499-08952-2", "El Libro de Arena");
            librosDeBorges.Add("978-8-420-63311-4", "El Aleph");
            //Agrego por medio de un índice que coincide con la clave de orden
            librosDeBorges["978-9-875-66833-1"] = "La Rosa Profunda";
            librosDeBorges["978-8-423-34218-1"] = "Ficciones";


            //Ahora iteramos por la lista y vemos que se ordenó automáticamente por ISBN
            Console.WriteLine("Lista completa de Libros");
            foreach (var libro in librosDeBorges)
            {
                Console.WriteLine("ISBN: {0}, Título: {1}", libro.Key, libro.Value);
            }

            //También podemos iterar por las claves solas
            Console.WriteLine("Lista de ISBNs ordenada");
            foreach (var clave in librosDeBorges.Keys)
            {
                Console.WriteLine(clave);
            }

            //O por los títulos solos
            Console.WriteLine("Lista de Títulos (ordenados por su ISBN");
            foreach (var titulo in librosDeBorges.Values)
            {
                Console.WriteLine(titulo);
            }

            separador.EscribirPie("Fin Ejemplo 1");
            #endregion

            #region Ejemplo 2
            separador.EscribirEncabezado("Ejemplo 2: Buscar valores");

            //Usamos el método TryGetValue para buscar un valor
            //No hace falta un Tr...Catch, si no se encuentra, devuelve un valor vacío
            string unISBN = "978-8-420-63311-4";
            string unTitulo;

            if (librosDeBorges.TryGetValue(unISBN, out unTitulo))
            {
                Console.WriteLine("Encontré que el título \"{0}\" tiene el ISBN: {1}", unTitulo, unISBN);
            }
            else
            {
                Console.WriteLine("El ISBN buscado no existe");
            }

            //Usando el indexador. Primero validamos la existencia (si no existe el uso del indexador arroja una excepción
            string otraClave = "978-9-875-66833-1";
            if (librosDeBorges.ContainsKey(otraClave))
            {
                Console.WriteLine("Encontré un título con el ISBN {0}: \"{1}\"", otraClave, librosDeBorges[otraClave]);
            }
            else
            {
                Console.WriteLine("No encontré ningún título con el ISBN: {0}", otraClave);
            }

            separador.EscribirPie("Fin Ejemplo 2");
            #endregion


        }

    }

    #region EncabezadoYPie
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
    #endregion

}
