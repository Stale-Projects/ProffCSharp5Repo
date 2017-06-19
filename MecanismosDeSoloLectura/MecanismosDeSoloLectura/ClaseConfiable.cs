// ==++==// //   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.// // ==--==/*============================================================** Proyecto: MecanismosDeSoloLectura** Clase:  ClaseConfiable** ** <OWNER>MarceVolta</OWNER>**** Propósito: Proveer ejemplos de código para el capítulo 10 del libro*  "De Cabeza a C#"** ClaseConfiable simula usar el objeto, pero en realidad manipula sus datos*  cambiando las propiedades del objeto referido por el campo Hogar*  No se cambia la referencia, sino el objeto mismo* ===========================================================*/

using System;
using System.Collections.ObjectModel;

namespace MecanismosDeSoloLectura
{
    class ClaseConfiable
    {
        public static void LeerSinCambiar(Persona unaPersona)
        {
            Console.WriteLine("Esta persona se llama: {0} {1} y vive en {2}",
                unaPersona.Nombre, unaPersona.Apellido, unaPersona.Hogar.Direccion);

            //Las siguientes dos líneas darían un error
            //unaPersona.Nombre = "Narciso";
            //unaPersona.Apellido = "Ibañez Menta";

            //Si bien no podemos cambiar la referencia (la linea siguiente no compila)
            //unaPersona.Hogar = new Casa("10 Downing Street", 2576);
            //Si puedo cambiar las propiedades de la referencia
            unaPersona.Hogar.Direccion = "10 Downing Street";
            unaPersona.Hogar.Superficie = 2576;

        }

        public static void LeerColeccionSoloLectura(ReadOnlyCollection<Persona> coleccion)
        {
            Casa hogarDeMoriarty = new Casa("46 Harley Street, London", 512);
            Persona moriarty = new Persona("James", "Moriarty", hogarDeMoriarty);
            //La siguientes dos líneas darían error de compilación
            //coleccion[0] = moriarty;
            //coleccion[0].Hogar = hogarDeMoriarty;
            //Pero sigue siendo posible ejecutar esto:
            coleccion[0].Hogar.Direccion = hogarDeMoriarty.Direccion;
            coleccion[0].Hogar.Superficie = hogarDeMoriarty.Superficie;
            
        }
    }
}
