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
using System.Collections.Specialized;

namespace Capitulo10_BitArrays
{
    class Program
    {
        /// <summary>
        /// Ejemplo 1
        /// En el ejemplo 1 fijamos valores en un BitArray utilizando las dos opciones:
        /// por medio del indexador o por medio del método Set
        /// Luego representamos los valores de cada bit usando la función <see cref="RepresentarBitArray(BitArray)"/> 
        /// Ejemplo 2
        /// En el Ejemplo 2 usamos tres BitArrays para operar sencillamente sobre dos enteros hacien un OR exclusivo (XOR)
        /// de los bits de dos enteros dados. La operación resulta muy sencilla en código. Por economía utilizo un único
        /// array de enteros para construir los dos BitArrays que uso para la operación. El valor del primer y único 
        /// elemento de este array se fija al primer entero con el que quiero operarar, y luego al segundo
        /// Ejemplo 3
        /// Aquí trabajamos con un BitVector32. Para ello primero creamos un array de enteros para las máscaras. Dado que 
        /// el BitVector32 tiene 32 bits, necesitamos un array de 32 elementos, uno para acceder a cada bit del BitVector32
        /// Nota que cada máscara es en realidad un entero que tiene determinados bits seteados a 1, dependiendo como creo la máscara
        /// El proceso de creación de las máscaras utiliza el método CreateMask que es estático de la clase BitVector32
        /// Cuando lo invoco sin argumentos, me permite acceder al primer bit, su valor es 1 (00000000 00000000 00000000 00000001) 
        /// Cuando la invoco usando un entero como parámetro, lo que se hace es multiplicar a ese entero por 2
        /// En este caso simple, para poder fijar los bits uno por uno, creo cada máscara usando como parámetro la máscara anterior
        /// lo que permite tener una serie de 32 máscaras, cada una de las cuales tiene solo fijado en true un bit, el que corresponde
        /// al índice de la máscara (teniendo en cuenta que el array es 0-based). 
        /// Por ejemplo, _masks[15] = {00000000 00000000 10000000 00000000}
        /// Finalmente usamos las máscaras para acceder en forma simple a los bits del BitVector32 y fijar su valor por medio de un 
        /// indexador. 
        /// Ten en cuenta que, si usas una máscara que tenga más de un bit seteado en 1, cuando la utilices para cambiar el valor
        /// del BitVector32 pasándola como parámetro, lo que estarás haciendo en verdad es fijar el valor de todos los bits de la
        /// máscara que están seteados a 1. Este tipo de código puede volverse muy confuso
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


            #region Ejemplo 3: Uso simple de BitVerctor32
            separador.EscribirEncabezado("Ejemplo 3: Uso simple de BitVector32");

            var _masks = new int[32];
            _masks[0] = BitVector32.CreateMask();
            for (int i = 1; i < _masks.Length; i++)
            {
                _masks[i] = BitVector32.CreateMask(_masks[i - 1]);
            }

            var bitVector = new BitVector32();
            bitVector[_masks[0]] = true;
            bitVector[_masks[15]] = true;
            bitVector[_masks[31]] = true;

            Console.WriteLine("El valor del bit 16 en el BitVector32 es: {0}", (bitVector[_masks[15]] == true) ? "1" : "0");
            Console.WriteLine();
            Console.WriteLine("A continuación el valor de las máscaras");
            for (int i = 0; i < _masks.Length; i++)
            {
                Console.WriteLine("Máscara {0} - Valor {1}", i.ToString(), _masks[i].ToString());
            }

            Console.WriteLine();
            Console.WriteLine("La representación del BitVector32 usando la sobrecarga de ToString es la siguiente:");
            Console.WriteLine(bitVector.ToString());

            separador.EscribirPie("Fin Ejemplo 3");
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
