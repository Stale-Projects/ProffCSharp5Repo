namespace MecanismosDeSoloLectura
{
    class LineaDeOrden
    {
        public int Unidades { get; set; }
        public decimal PrecioUnitario { get; set; }
        public float Descuento { get; set; }

        public decimal Total()
        {
            return PrecioUnitario * Unidades * (decimal)(1.0f - Descuento);
        }
    }
}
