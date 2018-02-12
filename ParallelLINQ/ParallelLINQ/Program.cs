using System;


namespace ParallelLINQ
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(EsFibonacci(Int32.MaxValue).ToString());
            //Int32[] secuenciaFibonacci = new Int32[] { 0, 1 };

            //for (Int32 i = 2; i < 1000000000U; i++)
            //{

            //}

        }

        /// <summary>
        /// https://math.stackexchange.com/questions/67707/how-many-numbers-are-in-the-fibonacci-sequence
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        private static Int32 EstimarCuantosFibonacci(Int32 valor)
        {
            double phi = (0.5D) * (1 + Math.Sqrt(5D));
            double primerTermino = Math.Log10((double)valor + 0.5D);

            return (Int32)((primerTermino + (0.5 * Math.Log10(5))) / Math.Log10(phi));

        }

        private static bool EsFibonacci(int valor)
        {
            ulong unCuadrado = (ulong)(5 * (Math.Pow(valor, 2))) + 4;
            ulong otroCuadrado = (ulong)(5 * (Math.Pow(valor, 2))) - 4;
            return ((Math.Pow(Math.Sqrt(unCuadrado), 2) == unCuadrado) |
                (Math.Pow(Math.Sqrt(otroCuadrado), 2) == otroCuadrado)) ? true : false;


        }
    }
}
