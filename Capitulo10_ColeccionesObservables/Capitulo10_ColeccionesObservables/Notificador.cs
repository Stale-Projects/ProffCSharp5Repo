// ==++==
// 
//   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
// 
// ==--==
/*============================================================
** Proyecto: Capitulo10_ColeccionesObservables
** Struct:  MensajePersonalizado
** 
** <OWNER>MarceVolta</OWNER>
**
** Propósito: Proveer textos descriptivos para el manejador de eventos de la 
** ObservableCollection usada en Main(). 
** Es parte de los ejemplos de código para el capítulo 10 del libro
** "De Cabeza a C#"
** Descripciones debajo en el summary
===========================================================*/


using System.Collections.Specialized;

namespace Capitulo10_ColeccionesObservables
{
    /// <summary>
    /// Esta struct tiene un método estático que devuelve una string en base al valor del argumento
    /// Este valor viene de la enumeración usada para llenar la propiedad Action de 
    /// NotifyCollectionChangedAction
    /// Observa que las string devueltas tienen tres partes, una para cada mensaje que se muestra
    /// en la consola en respuesta al evento. 
    /// En el Delegate que se usa para responder al evento en <see cref="Program"/> 
    /// se separa la string usando una instrucción Split.
    /// La idea de que el método sea estátitco es para evitar instanciar la Struct cada vez que se
    /// dispara un evento. De este modo no es necesaria instanciarla.
    /// </summary>
    struct MensajePersonalizado
    {

        public static string Mensaje(NotifyCollectionChangedAction accion)
        {
            switch (accion)
            {
                case NotifyCollectionChangedAction.Add:
                    return "Se agregaron elementos, El primer elemento que se agregó en esta acción fue a la posición ,Se agregó el elemento ";
                    break;
                case NotifyCollectionChangedAction.Remove:
                    return "Se quitaron elementos, El primer elemento que se quitó en esta acción estaba en la posición ,Se quitó el elemento ";
                    break;
                case NotifyCollectionChangedAction.Replace:
                    return "Se reemplazaron elementos, El primer elemento que se reemplazó en esta acción estaba en la posición ,Se agregó el elemento ";
                    break;
                case NotifyCollectionChangedAction.Move:
                    return "Se movieron elementos, El primer elemento que se movió en esta acción estaba en la posición ,Se movió el elemento ";
                    break;
                case NotifyCollectionChangedAction.Reset:
                    return "Se reseteó la colección!";
                    break;
                default:
                    return null;
                    break;
            }
        }


    }
}
