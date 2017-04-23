using System;
using System.Diagnostics.Contracts;

namespace Chapter10
{
    //Este objeto se crea para poder usar colecciones
    class Guitarist : IComparable<Guitarist>, IEquatable<Guitarist>, IFormattable
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Guitar { get; private set; }
        public int Score { get; private set; }

        public Guitarist(string firstName, string lastName, string guitar, int score)
        {
            FirstName = firstName;
            LastName = lastName;
            Guitar = guitar;
            Score = score;
        }

        #region IComparable
        public int CompareTo(Guitarist otro)
        {
            if (otro == null)
            {
                return -1;
            }
            else
            {
                int comparar = string.Compare(this.LastName, otro.LastName);
                if (comparar == 0)
                {
                    comparar = string.Compare(this.FirstName, otro.FirstName);
                }
                return comparar;
            }
        }

        #endregion

        #region IComparable
        public bool Equals(Guitarist otro)
        {
            if (otro == null) return false;

            return ((this.FirstName == otro.FirstName) && (this.LastName == otro.LastName));

        }


        public override bool Equals(object obj)
        {
            if ((obj == null) || !(obj is Guitarist)) return false;
            return this.Equals(obj as Guitarist);
        }
        public override int GetHashCode()
        {
            return (13 * this.FirstName.GetHashCode()) + (17 * this.LastName.GetHashCode());
        }

        #endregion

        //Overrideamos el método ToString para generar un output personalizado
        public override string ToString()
        {
            return String.Format("{0} {1} toca una {2} y su Score es: {3}", FirstName, LastName, Guitar, Score.ToString());
        }

        public static bool operator ==(Guitarist lhs, Guitarist rhs)
        {
            if (object.ReferenceEquals(lhs, rhs)) return true;
            if (object.ReferenceEquals(lhs, null)) return false;
            if (object.ReferenceEquals(rhs, null)) return false;

            return ((rhs.FirstName == lhs.FirstName) && (rhs.LastName == lhs.LastName));


        }

        public static bool operator !=(Guitarist lhs, Guitarist rhs)
        {
            return !(lhs == rhs);
        }


        //Ahora proveemos un método para formatear sobrecargado
        public string ToString(string formato, IFormatProvider formatProvider)
        {
            switch (formato.ToUpper())
            {
                case "N":
                    return ToString();
                case "G":
                    return String.Format("Este muchacho toca una {0}", Guitar);
                default:
                    throw new Exception(string.Format(formatProvider, "El formato {0} no está permitido", formato));

            }
        }

        public static void ImprimirDatos(Guitarist g)
        {
            string mensaje;
            mensaje = "Nombre: " + g.FirstName + g.LastName +
                " - Guitarra preferida: " + g.Guitar;
            Console.WriteLine(mensaje);
        }
    }

    class SelectScore
    {
        public int Score { get; set; }
        public SelectScore(int score)
        {
            Score = score;
        }

        public bool SelectScorePredicate(Guitarist guitarrista)
        {
            Contract.Requires<ArgumentNullException>(guitarrista != null);
            return guitarrista.Score > Score;
        }
    }

    class SelectNameAndGuitar
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Guitar { get; set; }
        public SelectNameAndGuitar(string FirstName, string LastName, string Guitar)
        {
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Guitar = Guitar;
        }

        public bool SelectNameAndGuitarPredicate(Guitarist guitarrista)
        {
            Contract.Requires<ArgumentNullException>(guitarrista != null);
            return guitarrista.FirstName == FirstName && guitarrista.LastName == LastName && guitarrista.Guitar == Guitar;
        }
    }
}
