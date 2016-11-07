using System.Windows;

namespace Ch08Events
{
    class Program
    {
        static void Main(string[] args)
        {
            var deAutos = new CarDealer("deAutos.com");
            var carlos = new Subscriber("Ricky Fort");
            deAutos.NewCarEvent += carlos.NewCarEventListener;
            //En la sig. línea no se invoca al evento directamente, 
            //Se invoca un método que genera el evento
            deAutos.NewCar("Toyota Celica");

            var roberto = new Subscriber("Roberto Planta");
            deAutos.NewCarEvent += roberto.NewCarEventListener;
            deAutos.NewCar("Lumina");

            deAutos.NewCarEvent -= roberto.NewCarEventListener;
            deAutos.NewCar("Rolls Royce");

            //Ahora hacemos lo mismo pero con Generic WeakEventManager

            var carOne = new CarDealer("carone.com");
            var natan = new Subscriber("Natán Pinzón");
            WeakEventManager<CarDealer, NewCarEventArgs>.AddHandler(carOne, "NewCarEvent", natan.NewCarEventListener);

            carOne.NewCar("Torino Coupé");

            var johnny = new Subscriber("Johnny B. Goode");
            WeakEventManager<CarDealer, NewCarEventArgs>.AddHandler(carOne, "NewCarEvent", johnny.NewCarEventListener);
            carOne.NewCar("Renault 18");
            WeakEventManager<CarDealer, NewCarEventArgs>.RemoveHandler(carOne, "NewCarEvent", johnny.NewCarEventListener);
            carOne.NewCar("Mercedes 380");


        }
    }
}
