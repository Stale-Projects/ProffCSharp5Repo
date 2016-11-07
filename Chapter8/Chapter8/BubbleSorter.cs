using System;
using System.Collections.Generic;

namespace Chapter8
{
    class BubbleSorter
    {

        //Esta versión de BubbleSort ordena sólo enteros
        //Acá está todo fijo: el tipo de elementos que ordena
        //y la operación de comparación (operador >)
        public static void BubbleSort(int[] array)
        {
            int temp;
            bool swapped = true;
            do
            {
                swapped = false;
                for (int i = 0; i < array.Length - 1; i++)
                {
                    if (array[i] > array[i + 1])
                    {
                        temp = array[i];
                        array[i] = array[i + 1];
                        array[i + 1] = temp;
                        swapped = true;
                    }
                }

            } while (swapped == true);
            return;
        }

        //Esta versión de BubbleSort, en cambio, es completamente genérica: no sólo admite cualquier tipo de elemento 
        //<T>, sino que además permite que el cliente de esta función defina la operación de comparación (que en verdad viene dada por el type T
        //Pero además, la colección de objetos que puedo pasarle es flexible, ya que la declaré del tipo IList, con lo cual puede admitir un array o una List por ejemplo
        //Acá se ve el real poder de los Generics (ya que puedo pasarle cualquier tipo T) y de los delegates (puedo pasarle a este método cuál es la función de comparación
        //Y también veo que en este tipo de funciones que maneja diferentes elementos, es fundamental la posibilidad de usar un Generic Delegate
        //Es decir, esta función es un algoritmo puro, y no está atada a ninguna implementación particular
        public static void GenericBubbleSort<T>(IList<T> list, Func<T, T, int> comparer)
        {
            T temp;
            bool swapped;
            do
            {
                swapped = false;
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (comparer(list[i], list[i + 1]) < 0)
                    {
                        temp = list[i];
                        list[i] = list[i + 1];
                        list[i + 1] = temp;
                        swapped = true;
                    }
                }

            } while (swapped == true);
            return;
        }

    }
}
