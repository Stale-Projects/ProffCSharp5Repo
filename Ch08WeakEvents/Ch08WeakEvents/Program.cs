using System.Windows;

namespace Ch08WeakEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            CarDealer deAutos = new CarDealer("deAutos.com");
            PotentialCarBuyer roberto = new PotentialCarBuyer("Roberto Planta");
            PotentialCarBuyer ricardo = new PotentialCarBuyer("Ricky Fort");
            PotentialCarBuyer jose = new PotentialCarBuyer("José López");

            WeakEventManager<CarDealer, NewCarEventArgs>.AddHandler(deAutos, "NewCarEvent", roberto.NewCarEventListener);
            deAutos.NewCar("Renault18");
            WeakEventManager<CarDealer, NewCarEventArgs>.AddHandler(deAutos, "NewCarEvent", ricardo.NewCarEventListener);
            deAutos.NewCar("Torino");
            WeakEventManager<CarDealer, NewCarEventArgs>.AddHandler(deAutos, "NewCarEvent", jose.NewCarEventListener);
            deAutos.NewCar("Toyota Celica");
            WeakEventManager<CarDealer, NewCarEventArgs>.RemoveHandler(deAutos, "NewCarEvent", jose.NewCarEventListener);
            deAutos.NewCar("Ford T");




        }
    }
}
