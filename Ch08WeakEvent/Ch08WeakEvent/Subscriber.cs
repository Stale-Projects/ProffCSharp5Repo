using System.Windows;

namespace Ch08WeakEvent
{
    class Subscriber : IWeakEventListener
    {
        public string Name { get; private set; }

        public Subscriber(string name)
        {
            this.Name = name;
        }

        bool IWeakEventListener.ReceiveWeakEvent(System.Type managerType, object sender, System.EventArgs e)
        {
            NewCarEventListener(sender, e as NewCarEventArgs);
            return true;
        }
        public void NewCarEventListener(object sender, NewCarEventArgs args)
        {
            CarDealer unDealer = (CarDealer)sender;
            System.Console.WriteLine("{0} dice: Parece que {1} me avisa que hay un nuevo {2}", this.Name, unDealer.Name, args.CarName);
        }
    }
}
