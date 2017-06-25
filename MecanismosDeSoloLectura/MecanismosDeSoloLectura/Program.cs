// ==++==
// 
//   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
// 
// ==--==
/*============================================================
** Proyecto: MecanismosDeSoloLectura
** Clase:  Program
** 
** <OWNER>MarceVolta</OWNER>
**
** Propósito: Proveer ejemplos de código para el capítulo 10 del libro
*  "De Cabeza a C#"
** Este proyecto mostramos las distintas maneras en que se puede crear elementos
*  de sólo lectura, y las limitaciones de cada método
*   
** Mostramos cuatro mecanismos: 
* El primero se basa en campos y propiedades de solo lectura (Ejemplo 1)
* El Segundo se basa en utilizar clases de sólo lectura, como por ejemplo: ReadOnlyCollection<T> (Ejemplo 2)
* 
* 
* 
===========================================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Immutable;

namespace MecanismosDeSoloLectura
{
    class Program
    {
        static void Main(string[] args)
        {
            EncabezadoYPieConsola _separador = new EncabezadoYPieConsola();

            #region Ejemplo 1: Propiedades y campos de sólo lectura
            _separador.EscribirEncabezado("Ejemplo 1: Propiedades y campos de sólo lectura");

            Casa casaDeSherlock = new Casa("221B Baker Street, London", 221);
            Persona sherlock = new Persona("Sherlock", "Holmes", casaDeSherlock);

            Console.WriteLine("Valores antes de pasarle el objeto a ClaseConfiable");
            ImprimirDetalles(sherlock);
            //Ahora le pasamos el objeto a ClaseConfiable, confiando en que no va a cambiarlo
            //apoyados en la protección que le pusimos a las propiedades y a su único campo
            ClaseConfiable.LeerSinCambiar(sherlock);
            //Veamos ahora si cambió algo
            Console.WriteLine("Valores después de pasarle el objeto a ClaseConfiable");
            ImprimirDetalles(sherlock);
            //Volvemos a poner los valores en orden
            casaDeSherlock.Direccion = "221B Baker Street, London";
            casaDeSherlock.Superficie = 221;
            Console.WriteLine("Después de volver a ajustar los valores");
            ImprimirDetalles(sherlock);

            _separador.EscribirPie("Fin Ejemplo 1");
            #endregion

            #region Ejemplo 2: Clase de Solo Lectura
            _separador.EscribirEncabezado("Ejemplo 2: Clase de Solo Lectura");
            List<Persona> listaDePersonas = new List<Persona>();
            listaDePersonas.Add(sherlock);
            ReadOnlyCollection<Persona> listaSoloLectura = listaDePersonas.AsReadOnly();

            Console.WriteLine("Antes de pasar la lista a ClaseConfiable");
            Console.WriteLine("Contiene a la persona Sherlock? {0}", listaSoloLectura.Contains(sherlock).ToString());
            Console.WriteLine("Hogar de Sherlock: {0}", listaSoloLectura[0].Hogar.Direccion.ToString());
            ClaseConfiable.LeerColeccionSoloLectura(listaSoloLectura);
            Console.WriteLine("Después de pasar la lista a ClaseConfiable");
            Console.WriteLine("Contiene a la persona Sherlock? {0}", listaSoloLectura.Contains(sherlock).ToString());
            Console.WriteLine("Hogar de Sherlock: {0}", listaSoloLectura[0].Hogar.Direccion.ToString());
            _separador.EscribirPie("Fin Ejemplo 2");
            #endregion

            #region Ejemplo 3
            int[] arrayDeEnteros = new int[] { 1, 2, 3 };
            ReadOnlyCollection<int> enterosReadOnly = new ReadOnlyCollection<int>(arrayDeEnteros);

            Console.WriteLine("Antes de cambiar el array");
            foreach (int entero in enterosReadOnly)
            {
                Console.WriteLine(entero.ToString());
            }
            arrayDeEnteros[0] = 0;
            Console.WriteLine("Después de cambiar el array");
            foreach (int entero in enterosReadOnly)
            {
                Console.WriteLine(entero.ToString());
            }

            #endregion


        }


        /// <summary>
        /// Este método simplemente imprime en la consola los valores de las
        /// propiedades de la instancia de la clase Persona
        /// </summary>
        /// <param name="unaPersona">La instancia de la clase cuyas propiedades imprime en la consola</param>
        private static void ImprimirDetalles(Persona unaPersona)
        {
            Console.WriteLine("Nombre: {0}", unaPersona.Nombre + " " + unaPersona.Apellido);
            Console.WriteLine("Hogar: {0}", unaPersona.Hogar.Direccion + ", " + unaPersona.Hogar.Superficie.ToString() + " metros cuadrados");
        }

        



    }
    /// <summary>
    /// Esta struct encapsula dos métodos para mostrar un encabezado y un pie 
    /// para cada ejemplo, de modo que podamos verlos por separado en la salida de 
    /// consola e identificar los resultados de cada ejemplo
    /// </summary>
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
