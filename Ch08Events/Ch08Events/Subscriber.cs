namespace Ch08Events
{
    class Subscriber
    {
        public string Name { get; private set; }

        public Subscriber(string name)
        {
            this.Name = name;
        }

        public void NewCarEventListener(object sender, NewCarEventArgs args)
        {
            CarDealer unDealer = (CarDealer)sender;
            System.Console.WriteLine("{0} dice: Parece que {1} me avisa que hay un nuevo {2}", this.Name, unDealer.Name, args.CarName);
        }
    }
}
