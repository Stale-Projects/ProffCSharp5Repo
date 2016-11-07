using System;
using System.Drawing;

namespace Chapter03
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("The background color is {0}", UserPreferences.BackgroundColor.Name);
            UserPreferences ThisUserPreferences = new UserPreferences();
            ThisUserPreferences.PrintColor();
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

    class UserPreferences
    {
        public static readonly Color BackgroundColor;

        static UserPreferences()
        {
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

        public UserPreferences()
        { }

        public void PrintColor()
        {
            Console.WriteLine("Printcolor is {0}", BackgroundColor.Name);
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


}
