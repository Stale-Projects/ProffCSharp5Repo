// ==++==// //   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.// // ==--==/*============================================================** Proyecto: MecanismosDeSoloLectura** Clase:  Casa** ** <OWNER>MarceVolta</OWNER>**** Propósito: Proveer ejemplos de código para el capítulo 10 del libro*  "De Cabeza a C#"** Casa es un objeto simple que se usa para demostrar las limitaciones *  de los campos de sólo lectura para proteger la integridad de los datos* ===========================================================*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MecanismosDeSoloLectura
{
    class Casa
    {
        public string Direccion { get; set; }

        public int Superficie { get; set; }

        public Casa(string direccion, int superficie)
        {
            Direccion = direccion;
            Superficie = superficie;
        }
    }
}
