using System.Collections.Generic;

namespace Chapter_10_LinkedLists
{
    /// <summary>
    /// Esta clase se provee para combinar una LinkedListc on una List
    /// </summary>
    class LinkedListManager
    {


        public LinkedList<Documento> Documentos { get; private set; }
        public List<LinkedListNode<Documento>> Marcadores { get; private set; }

        public LinkedListManager()
        {
            Documentos = new LinkedList<Documento>();
            Marcadores = new List<LinkedListNode<Documento>>();
        }

        public void AgregarDocumento(Documento doc)
        {

            LinkedListNode<Documento> nuevoNodo = new LinkedListNode<Documento>(doc);
            //Primero busco en la List un documento con la misma prioridad
            LinkedListNode<Documento> marcador = BuscarMarcador(doc.Prioridad);
            //No encontre ninguno con prioridad igual o menor por lo tanto éste nodo debe ser el primero en la LinkedList
            if (marcador == null)
            {
                Marcadores.Add(nuevoNodo);
                Documentos.AddFirst(nuevoNodo);

            }
            //Si encontré un nodo, lo uso como referencia para agregar el nuevo a la LinkedList
            else
            {
                Documentos.AddAfter(marcador, nuevoNodo);
                //Para los marcadores tengo que fijarme si el nodo que recuperé tiene la misma prioridad que el que estoy agregando
                //Si es así, tengo que quitar el existente para reemplazar. Si no, sólo agrego
                if (marcador.Value.Prioridad == nuevoNodo.Value.Prioridad)
                {
                    Marcadores.Remove(marcador);
                }
                Marcadores.Add(nuevoNodo);

            }
        }

        private LinkedListNode<Documento> BuscarMarcador(int prioridad)
        {
            LinkedListNode<Documento> nodo = null;
            if (prioridad >= 0)
            {
                nodo = Marcadores.Find(n => n.Value.Prioridad == prioridad);
                if (nodo == null)
                {
                    nodo = BuscarMarcador(prioridad - 1);
                }
            }
            return nodo;
        }

    }
}
