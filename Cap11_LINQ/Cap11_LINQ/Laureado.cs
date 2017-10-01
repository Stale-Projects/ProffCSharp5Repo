// ==++==
// 
//   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
// 
// ==--==
/*============================================================
** Proyecto: Capitulo11_LINQ
** Clase: Laureado
** 
** <OWNER>MarceVolta</OWNER>
**
** Propósito: Modelar premios Nobel otorgados desde 1901 
** para servir de ejemplo en el libro "De Cabeza a C#"
** Esta clase se usa para proveer ejemplos de sentencias LINQ
** Descripciones debajo en el summary
===========================================================*/
using System;
using System.Collections.Generic;

namespace Cap11_LINQ
{
    /// <summary>
    /// Esta clase encapsula propiedades de un Laureado con el premio Nobel
    /// </summary>
    [Serializable]
    class Laureado : IComparable<Laureado>, IFormattable
    {
        public string NombreCompleto { get; private set; }
        public DateTime FechaDeNacimiento { get; private set; }
        public string CiudadDeOrigen { get; private set; }
        public string PaisDeOrigen { get; private set; }
        public IEnumerable<Premio> Premios { get; set; }


        public Laureado(string nombreCompleto, DateTime fechaDeNacimiento,
            string ciudadDeOrigen, string paisDeOrigen, IEnumerable<Premio> premios)
        {
            NombreCompleto = nombreCompleto;
            FechaDeNacimiento = fechaDeNacimiento;
            CiudadDeOrigen = ciudadDeOrigen;
            PaisDeOrigen = paisDeOrigen;
            Premios = premios;
        }



    }




}
