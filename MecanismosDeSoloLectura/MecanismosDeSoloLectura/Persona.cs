// ==++==// //   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.// // ==--==/*============================================================** Proyecto: MecanismosDeSoloLectura** Clase:  Program** ** <OWNER>MarceVolta</OWNER>**** Propósito: Proveer ejemplos de código para el capítulo 10 del libro*  "De Cabeza a C#"** Esta clase utiliza un campo de sólo lectura, y dos propiedades, también de sólo lectura*  Ninguno de éstos elementos puede ser reasignado y son fijados en el constructor*  Si alguien quisiera manipular una instancia y la asignara a un nuevo objeto del mismo tipo*  inicializado con otros valores, nos daríamos cuenta porque el Hash sería diferente ** * ===========================================================*/


using System;

namespace MecanismosDeSoloLectura
{
    class Persona
    {
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }

        public int Hash { get; private set; }

        public readonly Casa Hogar;

        public Persona(string nombre, string apellido, Casa hogar)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            Hash = (nombre.GetHashCode() ^ rnd.Next(713) +
                apellido.GetHashCode() ^ rnd.Next(713));
            Nombre = nombre;
            Apellido = apellido;
            Hogar = hogar;

        }

    }
}
