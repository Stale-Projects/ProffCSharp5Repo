using System;
using System.Collections.Generic;

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

        #region Implementación de IComparable<Guitarrista>
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

        #endregion

        #region Implementación de IEquatable
        public bool Equals(Guitarrista otro)
        {
            if (otro == null)
            {
                return false;
            }
            return ((this.Apellido == otro.Apellido) &&
                (this.Nombre == otro.Nombre));

        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Guitarrista)) return false;
            Guitarrista otro = (obj as Guitarrista);
            return Equals(otro);
            //return ((otro.Nombre == Nombre) && (otro.Apellido == Apellido));
        }

        public override int GetHashCode()
        {
            int i = 13;
            int j = 7;
            return (this.Nombre.GetHashCode() * i) + (this.Apellido.GetHashCode() * j);
        }
        #endregion

        #region Override de ToString() con formatos
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

        #endregion

        public static void ImprimirDatos(Guitarrista g)
        {
            string mensaje;
            mensaje = "Nombre: " + g.Nombre + " " + g.Apellido +
                " - Guitarra preferida: " + g.Guitarra;
            Console.WriteLine(mensaje);
        }

        #region Predicado para búsquedas
        //Proveer comparación para búsquedas
        public static bool PredicadoComparacion(Guitarrista g)
        {
            return true;
        }
        #endregion
    }

    class ComparadorDeGuitarristas : IComparer<Guitarrista>
    {
        public enum TipoDeComparacion
        {
            PorNombreCompleto,
            PorScore,
            PorGuitarra
        }

        public TipoDeComparacion Comparacion { get; private set; }

        public ComparadorDeGuitarristas(TipoDeComparacion comparacion)
        {
            Comparacion = comparacion;
        }

        public int Compare(Guitarrista lhs, Guitarrista rhs)
        {
            switch (Comparacion)
            {
                case TipoDeComparacion.PorNombreCompleto:
                    return CompararPorNombreCompleto(lhs, rhs);
                    break;
                case TipoDeComparacion.PorScore:
                    return CompararPorScore(lhs, rhs);
                    break;
                case TipoDeComparacion.PorGuitarra:
                    return CompararPorGuitarra(lhs, rhs);
                    break;
                default:
                    throw new IndexOutOfRangeException("El valor de la propiedad Comparacion provisto no está soportado");
                    break;
            }
        }

        private int CompararPorNombreCompleto(Guitarrista lhs, Guitarrista rhs)
        {
            if (lhs == null && rhs == null) return -1;
            if (lhs == null) return -1;
            if (rhs == null) return 1;
            string primerNombre = lhs.Nombre + " " + lhs.Apellido;
            string segundoNombre = rhs.Nombre + " " + rhs.Apellido;
            return primerNombre.CompareTo(segundoNombre);

        }

        private int CompararPorScore(Guitarrista lhs, Guitarrista rhs)
        {
            if (lhs == null && rhs == null) return -1;
            if (lhs == null) return -1;
            if (rhs == null) return 1;
            return lhs.Apreciacion.CompareTo(rhs.Apreciacion);

        }

        private int CompararPorGuitarra(Guitarrista lhs, Guitarrista rhs)
        {
            if (lhs == null && rhs == null) return -1;
            if (lhs == null) return -1;
            if (rhs == null) return 1;
            return lhs.Guitarra.CompareTo(rhs.Guitarra);
        }
    }
}

