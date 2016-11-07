using System;

namespace Ch08WeakEvents
{
    class PotentialCarBuyer
    {
        public string Name { get; private set; }

        public PotentialCarBuyer(string name)
        {
            this.Name = name;
        }

        public void NewCarEventListener(object sender, NewCarEventArgs e)
        {
            CarDealer unDealer = (CarDealer)sender;
            Console.WriteLine("{0} dice: Parece que {1} me avisa que llegó un nuevo {2}!",
                this.Name, unDealer.Name, e.CarName);
        }
    }
}
