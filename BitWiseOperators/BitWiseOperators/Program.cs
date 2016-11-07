using System;

namespace BitWiseOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            int A = 60;
            int B = 13;
            int C = -1073741824;
            Console.WriteLine("Representación de A: {0}", Funciones.GetIntBinaryString(A));
            Console.WriteLine("Representación de A: {0}", Funciones.GetIntBinaryStringMV(A));
            Console.WriteLine("Representación de B: {0}", Funciones.GetIntBinaryString(B));
            Console.WriteLine("Representación de B: {0}", Funciones.GetIntBinaryStringMV(B));
            Console.WriteLine("Representación de C: {0}", Funciones.GetIntBinaryString(C));
            Console.WriteLine("Representación de C: {0}", Funciones.GetIntBinaryStringMV(C));

            int result = 0;
            Console.WriteLine("(0 * 397) ^ 500 = {0}", Funciones.GetIntBinaryStringMV((result * 397) ^ 500));
            Console.WriteLine("397 ^ 500 = {0}", Funciones.GetIntBinaryStringMV(397 ^ 500));

        }
    }
}
