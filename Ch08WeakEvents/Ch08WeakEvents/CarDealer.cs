using System;

namespace Ch08WeakEvents
{
    class CarDealer
    {
        public string Name { get; private set; }

        public CarDealer(string name)
        {
            this.Name = name;
        }

        public event EventHandler<NewCarEventArgs> NewCarEvent;



        protected virtual void RaiseNewCarEvent(string carName)
        {
            if (NewCarEvent != null)
            {
                NewCarEvent(this, new NewCarEventArgs(carName));
            }

        }

        public void NewCar(string carName)
        {
            Console.WriteLine("{0} dice: Llegó un nuevo {1}", this.Name, carName);
            RaiseNewCarEvent(carName);

        }
    }
}
