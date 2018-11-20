// ==++==
// 
//   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
// 
// ==--==

/*============================================================
    ** Proyecto: Capitulo03
    ** 
    ** <OWNER>MarceVolta</OWNER>
    **
    ** Propósito: Proveer ejemplos de código para el capítulo 3 del libro
    ** "De Cabeza a C#"
    ** Este proyecto provee ejemplos de Tipos y Objetos en C# 
    ** Descripciones debajo
    ===========================================================*/

using System;
using System.Drawing;

namespace Chapter03
{
    // ==++==
    // 
    //   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
    // 
    // ==--==
    /*============================================================
            ** Proyecto: Capitulo03
            ** Clase:  Program
            ** 
            ** <OWNER>MarceVolta</OWNER>
            **
            ** Propósito: Proveer ejemplos de código para el capítulo 3 del libro
            ** "De Cabeza a C#"
                     ===========================================================*/
    class Program
    {
        private static EncabezadoYPieConsola separador = new EncabezadoYPieConsola();

        static void Main(string[] args)
        {

            PreferenciasDeUsuario Preferencias = new PreferenciasDeUsuario();
            Preferencias.MostrarColor();

            PhoneCustomerStruct aCustomer = new PhoneCustomerStruct();
            aCustomer.FirstName = "Jorge";
            aCustomer.LastName = "Borges";
            Console.WriteLine("Así se ve un Customer: {0}", aCustomer.ToString());

            //Uso de WeakReference
            //En la declaración WeakReference es un tipo base
            //cuyo constructor toma un objeto como parámetro
            WeakReference MathWeakRef = new WeakReference(new MathTest());
            MathTest MathRef;


            if (MathWeakRef.IsAlive)
            {   //Target devuelve el objeto que le pasé al constructor, lo casteo 
                //porque es de tipo object
                MathRef = MathWeakRef.Target as MathTest;
                MathRef.Value = 2;
                int cuadrado = MathRef.GetSquare();
                Console.WriteLine("Usando la WeakReference obtuve este valor cuadrado: {0}", cuadrado.ToString());
            }
            else
            {
                Console.WriteLine("La WeakReference ya no es válida");
            }

            //Llamamos al GC
            GC.Collect();

            if (MathWeakRef.IsAlive)
                Console.WriteLine("La WeakReference sigue siendo válida después de llamar el GC!");
            else
                Console.WriteLine("Ahora la WeakReference ya no es válida, después de llamar al GC");
        }
    }

    class PreferenciasDeUsuario
    {
        public static readonly Color ColorDeFondo;

        static PreferenciasDeUsuario()
        {
            DateTime now = DateTime.Now;
            if (now.DayOfWeek == DayOfWeek.Saturday
                || now.DayOfWeek == DayOfWeek.Sunday)
            {
                ColorDeFondo = Color.Green;
            }
            else
            {
                ColorDeFondo = Color.Red;
            }
        }

        public PreferenciasDeUsuario()
        { }

        public void MostrarColor()
        {
            Console.WriteLine("El color de fondo es {0}", ColorDeFondo.Name);
        }
    }

    struct PhoneCustomerStruct
    {
        public const string DayOfSendingBill = "Monday";
        public int CustomerID;
        public string FirstName;
        public string LastName;

        //Override de una función heredada de System.Object
        public override string ToString()
        {
            return this.FirstName + " " + this.LastName;
        }
    }

    class MathTest
    {
        public int Value { get; set; }

        public int GetSquare()
        {
            return this.Value * this.Value;
        }
    }



    //Una clase con un co0nstructor que llama a otro constructor
    class Car
    {
        private string description;
        private uint nWheels;
        public Car(string description, uint nWheels)
        {
            this.description = description;
            this.nWheels = nWheels;
        }
        public Car(string description) : this(description, 4) //A esta llamada al otro construtor se la llama inicializador
        {
            //Cualquier código que ponga acá se ejecuta luego del inicializador
        }

    }


    /// <summary>    /// Esta struct encapsula dos métodos para mostrar un encabezado y un pie     /// para cada ejemplo, de modo que podamos verlos por separado en la salida de     /// consola e identificar los resultados de cada ejemplo    /// </summary>
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
