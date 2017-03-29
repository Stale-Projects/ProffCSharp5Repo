using System;

namespace Singleton
{
    class SingletonSimple
    {
        //private SingletonSimple _instancia;

        public static SingletonSimple Instancia { get; private set; }
        public int NumeroDeSerie { get; private set; }

        static SingletonSimple()
        {
            Random asignador = new Random(DateTime.Now.Millisecond);
            Instancia = new SingletonSimple(asignador.Next(10000));
        }

        private SingletonSimple(int numeroDeSerie)
        {
            NumeroDeSerie = numeroDeSerie;
        }


    }
}
