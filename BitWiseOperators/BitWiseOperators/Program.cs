using System;
using System.Collections.Specialized;


namespace BitWiseOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            int A = 60;
            int B = 13;
            int C = -1073741824;
            Console.WriteLine("Representación de A=60 según método de DotNetPearls: {0}", Funciones.GetIntBinaryString(A));
            Console.WriteLine("Representación de A=60 según método propio: {0}", Funciones.GetIntBinaryStringMV(A));
            Console.WriteLine("Representación de B=13según método de DotNetPearls: {0}", Funciones.GetIntBinaryString(B));
            Console.WriteLine("Representación de B=13 según método propio: {0}", Funciones.GetIntBinaryStringMV(B));
            Console.WriteLine("Representación de C = -1073741824 según DotNetPearls: {0}", Funciones.GetIntBinaryString(C));
            Console.WriteLine("Representación de C = -1073741824 método propio: {0}", Funciones.GetIntBinaryStringMV(C));
            Console.WriteLine("Estas son las representaciones con la función más corta");
            Funciones.EscribirBitsDeInteger(A);
            Funciones.EscribirBitsDeInteger(B);

            Console.WriteLine("Representación de Máscaras");
            int bit1 = BitVector32.CreateMask();
            int bit2 = BitVector32.CreateMask(bit1);
            int bit2_otro = bit1 << 1;
            int otraMask = BitVector32.CreateMask(A);

            BitVector32 bv32 = new BitVector32();
            bv32[otraMask] = true;


            Console.WriteLine("Representación de bit1:");
            Funciones.EscribirBitsDeInteger(bit1);
            Console.WriteLine("Representación de bit2:");
            Funciones.EscribirBitsDeInteger(bit2);
            Console.WriteLine("Representación de bit2_otro:");
            Funciones.EscribirBitsDeInteger(bit2_otro);
            Console.WriteLine("Representación de A");
            Funciones.EscribirBitsDeInteger(A);
            Console.WriteLine("Representación de otraMask");
            Funciones.EscribirBitsDeInteger(otraMask);
            Console.WriteLine("Representación de bv32 {0}", bv32.ToString());

            //192.168.15.100


            BitVector32.Section seccionA = BitVector32.CreateSection(0xf);
            BitVector32.Section seccionB = BitVector32.CreateSection(0xf, seccionA);
            BitVector32.Section seccionC = BitVector32.CreateSection(0xf, seccionB);
            BitVector32.Section seccionD = BitVector32.CreateSection(0xf, seccionC);

            //int direccionIP = 0xA42fded;
            //BitVector32 direccionIPV4 = new BitVector32(direccionIP);
            BitVector32 direccionIPV4 = new BitVector32();
            direccionIPV4[seccionA] = 10101100;

            Console.WriteLine("La representación de la dirección IP 172.16.254.1 es:");
            Console.WriteLine(direccionIPV4.ToString());



            //int result = 0;
            //Console.WriteLine("(0 * 397) ^ 500 = {0}", Funciones.GetIntBinaryStringMV((result * 397) ^ 500));
            //Console.WriteLine("397 ^ 500 = {0}", Funciones.GetIntBinaryStringMV(397 ^ 500));

        }
    }
}
