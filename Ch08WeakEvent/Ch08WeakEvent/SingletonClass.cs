using System;

namespace Ch08WeakEvent
{
    //Esta clase es un ejemplo de Singleton
    class SingletonClass
    {

        //La instancia se accede por medio de una propiedad que sólo puede setearse desde dentro
        public static SingletonClass Instance { get; private set; }

        //El constructor estático se ejecuta cuando se referencia
        //la clase por primera vez
        static SingletonClass()
        {
            //A su vez, el constructor estático llama al constructor privado y le pasa
            //valores para inicializar campos
            Instance = new SingletonClass(0);
            Console.WriteLine("Se acaba de ejecutar el Constructor estático!");
        }

        //Este constructor privado no puede ser invocado desde fuera
        //Lo llama el constructor estático
        //La idea es poder inicializar parámetros cuando instancio el singleton

        private int _state;
        private SingletonClass(int i)
        {
            _state = i;
            Console.WriteLine("Ahora se ejecutó el constructor privado");
        }

        public void DoSomethingAlready()
        {
            Console.WriteLine("I have done something for the {0} time", _state++);
        }
    }
}
