namespace MecanismosDeSoloLectura
{
    class LineaDeOrden
    {
        public int Unidades { get; private set; }
        public decimal PrecioUnitario { get; private set; }
        public float Descuento { get; private set; }

        public LineaDeOrden(int unidades, decimal precioUnitario, float descuento)
        {
            Unidades = unidades;
            PrecioUnitario = precioUnitario;
            Descuento = descuento;
        }


        public LineaDeOrden ConUnidades(int unidades)
        {
            return unidades == Unidades
                ? this : new LineaDeOrden(unidades, PrecioUnitario, Descuento);

        }

        public LineaDeOrden ConPrecioUnitario(decimal precioUnitario)
        {
            return precioUnitario == PrecioUnitario
                ? this : new LineaDeOrden(Unidades, precioUnitario, Descuento);

        }

        public LineaDeOrden ConDescuento(float descuento)
        {
            return descuento == Descuento
                ? this : new LineaDeOrden(Unidades, PrecioUnitario, descuento);

        }

        public decimal Total()
        {
            return PrecioUnitario * Unidades * (decimal)(1.0f - Descuento);
        }
    }
}
