using System;
using System.Collections.Generic;

namespace EnumerarArchivos
{










    // ==++==
    // 
    //   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
    // 
    // ==--==
    /*============================================================    ** Proyecto: Capitulo11_EnumeradorDeArchivos    ** Clase:  Mamifero    **     ** <OWNER>MarceVolta</OWNER>    **    ** Propósito: Proveer ejemplos de código para el capítulo 11 del libro    ** "De Cabeza a C#"    ** Clase creada para escribir ejemplos de LINQ, y modificada    ** para implementar la interfaz IEquatable&lt;Mamifero&gt;    ===========================================================*/


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

    // ==++==
    // 
    //   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
    // 
    // ==--==
    /*============================================================    ** Proyecto: Capitulo11_EnumeradorDeArchivos    ** Clase:  ComparadorDeMamiferos    **     ** <OWNER>MarceVolta</OWNER>    **    ** Propósito: Proveer ejemplos de código para el capítulo 11 del libro    ** "De Cabeza a C#"    ** Clase creada para poder utilizar comparaciones en colecciones de
    ** objetos de la clase Mamifero. Implementa la interfaz
    ** IEqualityComparer&lt;Mamifero&gt;    ** para implementar la interfaz IEquatable&lt;Mamifero&gt;    ===========================================================*/

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
