using System;
using System.Diagnostics.Contracts;

namespace Chapter_10_LinkedLists
{
    class Documento
    {
        private const int prioridadMinima = 0;
        private const int prioridadMaxima = 9;

        public string Titulo { get; private set; }
        public string Contenido { get; private set; }

        public byte Prioridad { get; private set; }

        public Documento(string titulo, string contenido, byte prioridad)
        {
            Contract.Requires<ValorDePrioridadFueraDeRangoException>(prioridad >= prioridadMinima && prioridad <= prioridadMaxima);
            Titulo = titulo;
            Contenido = contenido;
            Prioridad = prioridad;
        }

        public override string ToString()
        {
            return "Este documento tiene el título: " + this.Titulo + ", el contenido: " + this.Contenido + ", Prioridad: " + this.Prioridad;
        }

    }
    /// <summary>
    /// Crear una Custom Exception
    /// </summary>

    class ValorDePrioridadFueraDeRangoException : Exception
    {
        /// <summary>
        /// Constructor default sin argumentos
        /// </summary>
        public ValorDePrioridadFueraDeRangoException() : base()
        {

        }

        /// <summary>
        /// Constructor de Excepción con con mensaje custom
        /// </summary>
        /// <param name="message"></param>
        public ValorDePrioridadFueraDeRangoException(string message) : base(message)
        {

        }


        /// <summary>
        /// Constructor de Excepción con con mensaje custom y causa interna
        /// Se utiliza cuando esta excepción es causada por otra excepcion
        /// No es probable que ocurra. Por adherencia a las buenas prácticas
        /// </summary>
        /// <param name="message"></param>
        public ValorDePrioridadFueraDeRangoException(string message, Exception innerException) : base(message, innerException)
        {

        }

        /// <summary>
        /// Crear excepcion desde datos serializados
        /// Escenario: La Exception ocurre en una workstation remota y tengo
        /// que reproducir el error en una máquina local
        /// No lo uso porque está diseñada para correr sólo localmente
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        //protected ValorDePrioridadFueraDeRangoException(SerializationInfo info, StreamingContext context) : base(info, context)
        //{

        //}


    }
}
