using System;
using System.Drawing;



namespace Ch08WeakEvent
{
    class UserPreferences
    {
        public static readonly Color BackgroundColor;

        //Este constructor se ejecuta siempre, y no lo llama el código cliente, sino el CLR
        //El constructor estático se ejecuta la primera vez que se carga el módulo de la clase
        //Por ejemplo, al acceder a una propiedad estática, o, más grosero, cuando invoco el constructor no estático en este ejemplo
        //De este modo garantizo que se inicialice un juego de propiedades estáticas que quiero que se carguen antes de comenzar a usar la clase
        static UserPreferences()
        {
            Console.WriteLine("Se está ejecutando el constructor estático");
            DateTime now = DateTime.Now;
            if (now.DayOfWeek == DayOfWeek.Saturday
                || now.DayOfWeek == DayOfWeek.Sunday)
            {
                BackgroundColor = Color.Green;
            }
            else
            {
                BackgroundColor = Color.Red;
            }
        }

        //Al instanciar con el constructor no estático, se llama de todos modos al construtor estático
        public UserPreferences()
        {
            Console.WriteLine("Se está ejecutando el constructor default");
        }

        //Cuando estoy en condiciones de invocar un método no estático, ya los valores estáticos se inicializaron y puedo utilizarlos
        public void PrintColor()
        {
            Console.WriteLine("Printcolor is {0}", BackgroundColor.Name);
        }
    }

}
