// ==++==
// 
//   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
// 
// ==--==
/*============================================================
** Proyecto: Capitulo10_BitArrays
** Clase:  Program
** 
** <OWNER>MarceVolta</OWNER>
**
** Propósito: Proveer ejemplos de código para el capítulo 10 del libro
** "De Cabeza a C#"
** Este proyecto provee ejemplos de operaciones con BitArrays
* y muestra como simplificar operaciones con bits individuales 
* entre dos enteros, evitando tener que hacer corrimiento de bits
** Descripciones debajo en los summary
===========================================================*/


using System;
using System.Collections;

namespace Capitulo10_BitArrays
{
    class Program
    {
        /// <summary>
        /// En el ejemplo 1 fijamos valores en un BitArray utilizando las dos opciones:
        /// por medio del indexador o por medio del método Set
        /// Luego representamos los valores de cada bit usando la función <see cref="RepresentarBitArray(BitArray)"/> 
        /// En el Ejemplo 2 usamos tres BitArrays para operar sencillamente sobre dos enteros hacien un OR exclusivo (XOR)
        /// de los bits de dos enteros dados. La operación resulta muy sencilla en código. Por economía utilizo un único
        /// array de enteros para construir los dos BitArrays que uso para la operación. El valor del primer y único 
        /// elemento de este array se fija al primer entero con el que quiero operarar, y luego al segundo
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var separador = new EncabezadoYPieConsola();

            #region Ejemplo 1: Ejemplo Simple de Operaciones con BitArray
            separador.EscribirEncabezado("Ejemplo 1: Ejemplo Simple de Operaciones con BitArray");

            BitArray arrayDeBits = new BitArray(32);
            arrayDeBits[1] = true;
            for (int i = 0; i < 16; i++)
            {
                arrayDeBits.Set(2 * i, true);
            }

            RepresentarBitArray(arrayDeBits);


            separador.EscribirPie("Fin Ejemplo 1");
            #endregion

            #region Ejemplo 2: Ejemplo de Operaciones de bits
            separador.EscribirEncabezado("Ejemplo 2: Ejemplo de Operaciones de bits");

            int primerEntero = 2576;
            int segundoEntero = 15051505;

            int[] arrayDeEnteros = new int[1];
            arrayDeEnteros[0] = primerEntero;
            var primerArrayDeBitsEjemplo2 = new BitArray(arrayDeEnteros);
            arrayDeEnteros[0] = segundoEntero;
            var segundoArrayDeBitsEjemplo2 = new BitArray(arrayDeEnteros);

            var tercerArrayDeEnterosEjemplo2 = primerArrayDeBitsEjemplo2.Xor(segundoArrayDeBitsEjemplo2);
            RepresentarBitArray(tercerArrayDeEnterosEjemplo2);


            separador.EscribirPie("Fin Ejemplo 2");
            #endregion



        }
        /// <summary>
        /// Esta función encapsula la representación de un BitArray en la consola
        /// utilizando como auxiliar un booleano
        /// </summary>
        /// <param name="arrayDeBits">BitArray que vamos a representar</param>
        private static void RepresentarBitArray(BitArray arrayDeBits)
        {
            bool valor;
            string representacion = "";

            for (int i = 0; i < arrayDeBits.Length; i++)
            {
                valor = arrayDeBits.Get(i);
                representacion += (valor == true ? 1 : 0).ToString();
            }
            Console.WriteLine("Representación del Array de Bits: {0}", representacion);
        }

    }

    /// <summary>
    /// Esta struct encapsula dos métodos para mostrar un encabezado y un pie 
    /// para cada ejemplo, de modo que podamos verlos por separado en la salida de 
    /// consola e identificar los resultados de cada ejemplo
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
