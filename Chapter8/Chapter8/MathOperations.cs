namespace Chapter8
{
    class MathOperations
    {
        public static long MultiplicarPorDos(int x)
        {
            long result = (long)x * 2;
            return result;
        }

        public static long Cuadrado(int x)
        {
            long result = (long)(x * x);
            return result;
        }

        public static void MutiplicarPorDosyMostrar(int x)
        {
            System.Console.WriteLine("El resultado de multiplicar por dos a {0} es: {1}", x.ToString(), (x * 2).ToString());
        }

        public static void CuadradoYMostrar(int x)
        {
            System.Console.WriteLine("El resultado de elevar {0} al cuadrado es: {1}", x.ToString(), (x * x).ToString());
        }

        public static void Dividir5PorX(int x)
        {
            System.Console.WriteLine("El resultado de dividir 5 por {0} es: {1}", x.ToString(), (5 / x).ToString());
        }

    }
}
