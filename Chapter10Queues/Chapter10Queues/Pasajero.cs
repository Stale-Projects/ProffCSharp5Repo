namespace Chapter10Queues
{
    class Pasajero
    {
        public string Nombre { get; private set; }
        public int Fila { get; private set; }
        public string Asiento { get; private set; }

        public Pasajero(string nombre, int fila, string asiento)
        {
            Nombre = nombre;
            Fila = fila;
            Asiento = asiento;
        }
    }
}
