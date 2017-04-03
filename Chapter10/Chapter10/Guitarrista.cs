using System;

namespace Chapter10
{
    class Guitarrista : IComparable<Guitarrista>, IFormattable, IEquatable<Guitarrista>
    {
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string Guitarra { get; private set; }
        public int Apreciacion { get; private set; }

        public Guitarrista(string nombre, string apellido, string guitarra, int apreciacion)
        {
            Nombre = nombre;
            Apellido = apellido;
            Guitarra = guitarra;
            Apreciacion = apreciacion;
        }

        public int CompareTo(Guitarrista otro)
        {
            if (otro == null)
            {
                return -1;
            }
            else
            {
                int comparar = string.Compare(this.Apellido, otro.Apellido);
                if (comparar == 0)
                {
                    comparar = string.Compare(this.Nombre, otro.Nombre);
                }
                return comparar;
            }
        }

        //Hacemos override del método ToString para generar un output personalizado
        public override string ToString()
        {
            return String.Format("{0} {1} toca una {2} y su Índice de Apreciacion es: {3}", Nombre, Apellido, Guitarra, Apreciacion.ToString());
        }

        //Ahora proveemos un método para formatear sobrecargado
        public string ToString(string formato, IFormatProvider formatProvider)
        {
            switch (formato.ToUpper())
            {
                case "N":
                    return ToString();
                case "G":
                    return String.Format("{0} toca una {1}", Nombre, Guitarra);
                default:
                    throw new Exception(string.Format(formatProvider, "El formato {0} no está permitido", formato));

            }
        }

        public static void ImprimirDatos(Guitarrista g)
        {
            string mensaje;
            mensaje = "Nombre: " + g.Nombre + " " + g.Apellido +
                " - Guitarra preferida: " + g.Guitarra;
            Console.WriteLine(mensaje);
        }

        //Proveer comparación para búsquedas
        public static bool PredicadoComparacion(Guitarrista g)
        {
            return true;
        }
    }

}

