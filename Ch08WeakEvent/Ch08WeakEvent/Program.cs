namespace Ch08WeakEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ejemplo de constructor estático
            //Cuando ejecuto esta línea, se llama sólo al constructor estático y puedo acceder a las propiedades estáticas que éste inicializa
            System.Console.WriteLine("The background color is {0}", UserPreferences.BackgroundColor.Name);

            //Cuando creo una instancia usando el constructor no estático, se invoca ese constructor, y, en caso de que esta sea la primera carga de la clase
            //se llama antes al constructor estático
            UserPreferences ThisUserPreferences = new UserPreferences();

            //En este punto ya las propiedades estáticas están inicializadas
            ThisUserPreferences.PrintColor();


            //En este ejemplo veo que es lo mismo usar la propiedad estática Instance 
            //que asignar una variable de tipo adecuado a la propiedad instance. Ambas referencias apuntan al mismo objeto
            System.Console.WriteLine("En la linea que sigue hago una declaración");
            SingletonClass unSingleton;
            System.Console.WriteLine("En la linea que sigue asigno instance a la variable declarada más arriba");
            unSingleton = SingletonClass.Instance;
            System.Console.WriteLine("Hecha la asignación");
            unSingleton.DoSomethingAlready();
            SingletonClass.Instance.DoSomethingAlready();


            //Ahora viene el código para los WeakEvents
            //Uso los mismos nombres que usé antes para poder comparar más fácilmente

            var deAutos = new CarDealer("deAutos.com");
            var carlos = new Subscriber("Ricky Fort");
            NewCarWeakEventManager.AddListener(deAutos, carlos);
            //Antes era: deAutos.NewCarEvent += carlos.NewCarEventListener;


            //En la sig. línea no se invoca al evento directamente, 
            //Se invoca un método que genera el evento. Esto quedó como antes
            deAutos.NewCar("Toyota Celica");

            var roberto = new Subscriber("Roberto Planta");
            NewCarWeakEventManager.AddListener(deAutos, roberto);
            //Antes era: deAutos.NewCarEvent += roberto.NewCarEventListener;
            deAutos.NewCar("Lumina");

            //Ahora quito un suscriptor
            NewCarWeakEventManager.RemoveListener(deAutos, roberto);
            //Antes era: deAutos.NewCarEvent -= roberto.NewCarEventListener;
            deAutos.NewCar("Rolls Royce");


        }
    }
}
