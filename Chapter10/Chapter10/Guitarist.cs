using System;
using System.Diagnostics.Contracts;

namespace Chapter10
{
    //Este objeto se crea para poder usar colecciones
    class Guitarist : IComparable<Guitarist>, IFormattable
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

        //Overrideamos el método ToString para generar un output personalizado
        public override string ToString()
        {
            return String.Format("{0} {1} toca una {2} y su Score es: {3}", FirstName, LastName, Guitar, Score.ToString());
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
            return guitarrista.Score > this.Score;
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
