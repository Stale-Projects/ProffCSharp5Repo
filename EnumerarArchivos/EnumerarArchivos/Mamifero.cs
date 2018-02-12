using System;
using System.Collections.Generic;

namespace EnumerarArchivos
{
    class Mamifero : IEquatable<Mamifero>
    {
        public int Peso { get; set; }
        public int Edad { get; set; }

        public string Especie { get; set; }

        public bool Equals(Mamifero otro)
        {
            return otro == null ?
                false :
                Equals(Peso, otro.Peso) &&
                Equals(Edad, otro.Edad) &&
                Equals(Especie.ToLower(), otro.Especie.ToLower());

        }

        public override bool Equals(object obj)
        {
            //Si obj no es un Mamifero lo siguiente da null
            //Cubre también el caso en que obj sea null
            var otroMamifero = obj as Mamifero;

            return Equals(otroMamifero);


        }



        public override int GetHashCode()
        {
            //Utilizando un método con primos (simplón)
            int hash = 113;
            hash = hash * (13 + Peso);
            hash = hash * (13 + Edad);
            hash = hash * (13 + Especie != null ? Especie.GetHashCode() : 0);

            return hash;
        }

        public override string ToString()
        {
            return "Un mamifero de la especie " + Especie
                + ", que pesa " + Peso.ToString() + " Kg."
                + " y tiene " + Edad.ToString()
                + " años de edad";


        }



    }

    class ComparadorDeMamiferos : IEqualityComparer<Mamifero>
    {
        public int GetHashCode(Mamifero m1)
        {
            return m1.GetHashCode();
        }

        public bool Equals(Mamifero m1, Mamifero m2)
        {
            return m1.Equals(m2);
        }
    }
}
