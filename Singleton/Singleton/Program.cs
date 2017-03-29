using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Este es el número de serie del Singleton: {0}", SingletonSimple.Instancia.NumeroDeSerie);
            //Llamado a la clase estática
            Console.WriteLine("Resultado con clase estática: {0}", UnaClaseEstatica.calcularAlgo(5, 6).ToString());
            //Llamado al singleton
            Console.WriteLine("Resultado con Singleton: {0}", UnSingleton.Instancia.CalcularAlgo(5, 6));
            //Llamado al factory
            Console.WriteLine("Resultado con Factory: {0}", OtroSingleton.Instancia.CalcularAlgo(5, 6));
            //Old School SIngleton
            Console.WriteLine("Resultado con Old School singleton: {0}", OldSchoolSingleton.Instancia.CalcularAlgo(5, 6));
            Console.WriteLine("Para ver la secuencia: {0}", OldSchoolSingleton.Instancia.CalcularAlgo(11, 22));
        }


        class UnaClaseEstatica
        {

            public static int calcularAlgo(int a, int b)
            {
                return a + b;
            }
        }

        public class UnSingleton
        {
            public static UnSingleton Instancia { get; private set; }


            private UnSingleton()
            {
                // Un constructor privado para que no se pueda instanciar desde fuera

            }

            static UnSingleton()
            {
                Instancia = new UnSingleton();
            }


            public int CalcularAlgo(int a, int b)
            {
                return a + b;
            }
        }


        public class OtroSingleton
        {

            //Esta propiedad siempre devuelve una nueva instancia
            public static OtroSingleton Instancia
            {
                get
                {
                    return new OtroSingleton();
                }

            }


            private OtroSingleton()
            {
                // Un constructor privado para que no se pueda instanciar desde fuera

            }



            public int CalcularAlgo(int a, int b)
            {
                return a + b;
            }
        }

        public class OldSchoolSingleton
        {
            private static OldSchoolSingleton _instancia;

            public static OldSchoolSingleton Instancia
            {
                get
                {
                    if (_instancia == null)
                    {
                        _instancia = new OldSchoolSingleton();
                    }
                    return _instancia;
                }


            }

            private OldSchoolSingleton()
            {

            }

            public int CalcularAlgo(int a, int b)
            {
                return a + b;
            }
        }

    }
}
