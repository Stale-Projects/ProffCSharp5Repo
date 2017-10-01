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
using System.Collections.Immutable;
using System.Collections.ObjectModel;

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
            _separador.EscribirEncabezado("Ejemplo 3 - Colecciones de Sólo Lectura");

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

            _separador.EscribirPie("Fin ejemplo 3");
            #endregion

            #region Ejemplo 4: Uso simple de Colecciones Inmutables con métodos de extensión

            LineaDeOrden tornillos = new LineaDeOrden(500, 1.25m, 0.0f);
            LineaDeOrden tuercas = new LineaDeOrden(500, 0.75m, 0.0f);
            List<LineaDeOrden> lineas = new List<LineaDeOrden> { tornillos, tuercas };
            Orden ordenDeFerreteria = new Orden(lineas);
            Orden ordenDeFerreteriaConDescuento =
                ordenDeFerreteria.ModificarLineaDeOrden(tornillos, tornillos.ConDescuento(0.2f));



            #endregion

            #region Ejemplo 5: Otro modo de uso con métodos estáticos
            //Sin salida en la consola, sólo para mostrar como se utilizan

            int[] enterosPrimos = new int[] { 2, 3, 5, 7, 11 };
            ImmutableArray<int> enterosArrayInmutable = ImmutableArray.Create<int>(enterosPrimos);
            ImmutableArray<int> enterosArrayInmutableNuevo = ImmutableArray.ToImmutableArray<int>(enterosPrimos);



            ImmutableArray<int> enterosArrayInmutableUltimo = enterosPrimos.ToImmutableArray<int>();


            #endregion

            #region Ejemplo 6: Métodos encadenados (interfaces fluídas)
            _separador.EscribirEncabezado("Ejemplo 6: Métodos encadenados - Interfases fluidas");

            //Lo siguiente funciona pero no es eficiente

            ImmutableArray<int> enterosParesVacio = ImmutableArray.Create<int>();
            ImmutableArray<int> enterosParesCompleto = enterosParesVacio.Add(2).Add(4).Add(6).Add(8).Add(10);
            Console.WriteLine("Enumeración del primer array que comenzó vacío");
            foreach (int item in enterosParesVacio)
            {
                Console.WriteLine(item.ToString());

            }

            Console.WriteLine("Enumeración del segundo array");
            foreach (int item in enterosParesCompleto)
            {
                Console.WriteLine(item.ToString());

            }

            //Es preferible ésto:
            int[] enterosParesEficientes = new int[5] { 2, 4, 6, 8, 10 };
            ImmutableArray<int> enterosParesInmutables = enterosParesEficientes.ToImmutableArray<int>();

            Console.WriteLine("Veamos que hay en el tercer array");
            foreach (int item in enterosParesInmutables)
            {
                Console.WriteLine(item.ToString());

            }


            //Me había olvidado de agregar algunos pares más:

            ImmutableArray<int>.Builder bob = enterosParesInmutables.ToBuilder();
            bob.Add(12);
            bob.Add(14);

            ImmutableArray<int> nuevosEnterosPares = bob.ToImmutableArray<int>();


            Console.WriteLine("Sí pudimos! Gracias, Bob");
            foreach (int item in nuevosEnterosPares)
            {
                Console.WriteLine(item.ToString());

            }

            //Aún hay más!
            ImmutableList<int> listaDeEnteros = bob.ToImmutableList<int>();

            Console.WriteLine("Finalmente, iteremos sobre la lista armada en base al mismo constructor");
            foreach (int item in listaDeEnteros)
            {
                Console.WriteLine(item.ToString());
            }

            _separador.EscribirPie("Fin ejemplo 6");
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
