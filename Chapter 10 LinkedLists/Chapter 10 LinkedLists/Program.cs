using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Chapter_10_LinkedLists
{
    class Program
    {
        static void Main(string[] args)
        {
            EncabezadoYPieConsola separador = new EncabezadoYPieConsola();

            #region Ejemplo 1: LinkedList, ejemplo simple usando sobrecargas de AddFirst y AddLast 
            separador.EscribirEncabezado("Ejemplo 1: Sobrecargas de AddFirst y AddLast");

            LinkedList<int> Impares = new LinkedList<int>();
            LinkedListNode<int> nodoUno = new LinkedListNode<int>(1);
            Impares.AddFirst(nodoUno);
            Impares.AddLast(3);
            Impares.AddLast(new LinkedListNode<int>(5));

            foreach (int item in Impares)
            {
                Console.WriteLine(item);
            }

            separador.EscribirPie("Fin Ejemplo 1");
            #endregion


            #region Ejemplo 2: Usar Find e Insertar
            separador.EscribirEncabezado("Ejemplo 2: Usar Find e Insertar");

            LinkedList<string> palabras = new LinkedList<string>();
            palabras.AddFirst("Primero");
            palabras.AddLast("Segundo");
            palabras.AddLast("Tercero");
            //Ahora busco para insertar entre segundo tercero
            LinkedListNode<string> segundo = palabras.Find("Segundo");
            LinkedListNode<string> nodoInsertado = palabras.AddAfter(segundo, "Insertado");
            palabras.AddAfter(nodoInsertado, new LinkedListNode<string>("Infiltrado"));
            foreach (string palabra in palabras)
            {
                Console.WriteLine(palabra);
            }

            palabras.fin
            separador.EscribirPie("Fin Ejemplo 2");
            #endregion


            #region Ejemplo 3: Remove
            //Lo siguiente remueve sólo una de las instancias de los items que tienen la misma stirng
            EscribirInicio("Ejemplo 3");
            palabras.Remove("Insertado");
            Console.WriteLine("Listar luego de borrar por primera vez");
            foreach (var palabra in palabras)
                Console.WriteLine(palabra);
            LinkedListNode<string> borrar = palabras.Find("Insertado");
            palabras.Remove(borrar);
            Console.WriteLine("Listar luego de borrar por segunda vez");
            foreach (var palabra in palabras)
                Console.WriteLine(palabra);
            EscribirCierre();
            #endregion


            #region Ejemplo 4: Prueba de Enforcement de contrato con prioridad
            EscribirInicio("Ejemplo 4");
            //Documento doc = new Documento("Doc no válido", "Este doc tiene una prioridad demasiado alta", 10);
            //Console.WriteLine("Documento con título: {0}, Contenido: {1}, Prioridad: {2}", doc.Titulo, doc.Contenido, doc.Prioridad.ToString());
            EscribirCierre();
            #endregion


            #region Ejemplo 5: Prueba simple de la Clase LinkedListManager
            EscribirInicio("Ejemplo 5");
            LinkedListManager manager = new LinkedListManager();
            Documento dox = null;
            for (int i = 0; i < 10; i++)
            {
                dox = new Documento("Documento " + i.ToString(), "Contenido del Documento " + i.ToString(), (byte)i);
                manager.AgregarDocumento(dox);
                dox = new Documento("Documento(R) " + i.ToString(), "Contenido del Documento(R) " + i.ToString(), (byte)i);
                manager.AgregarDocumento(dox);
            };
            Console.WriteLine("Marcadores");
            foreach (var item in manager.Marcadores)
            {
                Console.WriteLine(item.Value.ToString());
            }

            Console.WriteLine("Documentos");
            foreach (var item in manager.Documentos)
            {
                Console.WriteLine(item.ToString());
            }

            EscribirCierre();
            #endregion

            #region Ejemplo 6: Prueba de performance vs LinkedList y List
            //Vamos a insertar 100,000 elementos en cada una para ver cuanto tardan comparativamente

            Stopwatch sw = new Stopwatch();
            LinkedListManager llMgr = new LinkedListManager();
            string titulo;
            DateTime referencia = new DateTime(2016, 10, 17, 0, 0, 0);
            int diferencia;
            Random rnd = new Random(DateTime.Now.Millisecond);
            int prio;
            sw.Start();
            for (int i = 0; i < 100000; i++)
            {

                diferencia = DateTime.Now.Subtract(referencia).Milliseconds;

                titulo = "Doc " + diferencia.ToString();
                prio = rnd.Next(0, 10);
                llMgr.AgregarDocumento(new Documento(titulo, "Lame Content", (byte)prio));
            }
            sw.Stop();
            Console.WriteLine("Pasaron {0} milisegundos", sw.ElapsedMilliseconds.ToString());

            List<Documento> listaDocs = new List<Documento>();
            int indice;

            sw.Start();
            for (int i = 0; i < 100000; i++)
            {

                diferencia = DateTime.Now.Subtract(referencia).Milliseconds;

                titulo = "Doc " + diferencia.ToString();
                prio = rnd.Next(0, 10);

                indice = listaDocs.FindLastIndex(d => d.Prioridad == prio);
                //if
                listaDocs.Insert(indice, new Documento(titulo, "Lame content", (byte)prio));

            }
            sw.Stop();
            Console.WriteLine("Pasaron {0} milisegundos", sw.ElapsedMilliseconds.ToString());



            #endregion




        }

        public static void EscribirInicio(string titulo)
        {
            Console.WriteLine("======={0}===========", titulo);
        }
        public static void EscribirCierre()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("");
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
