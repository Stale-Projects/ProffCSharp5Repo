// ==++==
// 
//   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
// 
// ==--==
/*============================================================
** Proyecto: Capitulo10_ColeccionesObservables
** Clase:  Program
** 
** <OWNER>MarceVolta</OWNER>
**
** Propósito: Proveer ejemplos de código para el capítulo 10 del libro
** "De Cabeza a C#"
** Este proyecto provee ejemplos de Colecciones Observables (ObservableCollection)
* Ten en cuenta que debes referenciar el namespace System.Collections.ObjectModel
* La funcionalidad está provista por el assembly WindowsBase
* Además debemos referenciar System.Collections.Specialized para los 
* argumentos del(los) Delegate(s) que maneja(n) el evento
** Descripciones debajo en el summary
===========================================================*/


using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Capitulo10_ColeccionesObservables
{
    /// <summary>
    /// Este es un ejemplo simple de Colecciones observables en el que simplemente creamos
    /// la ObservableCollection y le asignamos un delegado a la propiedad CollectionChanged
    /// En este delegado mostramos dos o tres mensajes que tienen que ver con el tipo de 
    /// acción que desencadenó el evento. 
    /// Para definir los mensajes usamos una Struct <see cref="MensajePersonalizado"/> 
    /// de modo que el código de tratamiento de los eventos quede más limpio
    /// En el manejador de evento recibimos dos objetos: el sender (de tipo object) 
    /// y NotifyCollectionChangedEventArgs que contiene información sobre el evento: 
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// Action: es una enumeración del tipo de acción que originó el evento
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// OldItems es una lista que se llena con los elementos que fueron removidos
    /// (si es que éste es el tipo de acción que disparó el evento)
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// OldStartingIndex es el índice del primer elemento que fue removido o reemplazado
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// NewItems es análogo a OldItems pero contiene los elementos que fueron agregados
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// NewStartingIndex es análogo a OldStartingIndex para el primer elemento agregado
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var numeros = new ObservableCollection<string>();
            numeros.CollectionChanged += Numeros_Change;

            numeros.Add("Uno");
            numeros.Add("Tres");
            numeros.Add("Cinco");
            numeros.Add("Siete");
            numeros.Add("Nueve");
            numeros.Insert(1, "Dos");
            numeros.RemoveAt(4);
            numeros.Move(4, 2);
            numeros[4] = "Reemplazado";
        }

        static void Numeros_Change(object sender, NotifyCollectionChangedEventArgs e)
        {

            string[] mensajes = MensajePersonalizado.Mensaje(e.Action).Split(',');
            Console.WriteLine(mensajes[0]);
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Replace:
                    Console.WriteLine(mensajes[1] + e.NewStartingIndex);
                    foreach (var item in e.NewItems)
                    {
                        Console.WriteLine(mensajes[2] + item.ToString());
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Move:
                    Console.WriteLine(mensajes[1] + e.OldStartingIndex);
                    foreach (var item in e.OldItems)
                    {
                        Console.WriteLine(mensajes[2] + item.ToString());
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Console.WriteLine(mensajes[1]);
                    break;
                default:
                    break;
            }

        }

    }
}
