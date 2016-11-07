using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ch08WeakEvent
{
    class CarDealer
    {
        public event EventHandler<NewCarEventArgs> NewCarEvent;

        public string Name { get; private set; }

        public CarDealer(string name)
        {
            this.Name = name;
        }

        //Este método para lanzar el evento es Protected Virtual
        //Protected para que sólo puedan verlo las clases derivadas de CarDealer
        //Virtual para que lo puedan overridear si lo necesitan
        protected virtual void RaiseNewCarEvent(string carName)
        {
            EventHandler<NewCarEventArgs> newCarEvent = NewCarEvent;
            //La siguiente llamada puede invocarse con un nullable type:
            //newCarEvent?.Invoke(this, new NewCarEventArgs(carName));
            if (newCarEvent != null) //Si alguien se suscribió
            {
                newCarEvent(this, new NewCarEventArgs(carName));
            }

        }

        public void NewCar(string carName)
        {
            Console.WriteLine("{0} dice: Llegó un nuevo auto del tipo: {1}", this.Name, carName);
            RaiseNewCarEvent(carName);
        }
    }
}
