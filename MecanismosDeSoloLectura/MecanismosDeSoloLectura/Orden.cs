using System.Collections.Generic;
using System.Collections.Immutable;

namespace MecanismosDeSoloLectura
{
    class Orden
    {
        public ImmutableList<LineaDeOrden> LineasDeOrden { get; private set; }
        public Orden(IEnumerable<LineaDeOrden> lineasDeOrden)
        {
            LineasDeOrden = lineasDeOrden.ToImmutableList();
        }

        public Orden ConLineasDeOrden(ImmutableList<LineaDeOrden> lineasDeOrden)
        {
            return ReferenceEquals(lineasDeOrden, LineasDeOrden)
                ? this : new Orden(lineasDeOrden);
        }

        public Orden BorrarLineaDeOrden(LineaDeOrden lineaABorrar)
        {
            return ConLineasDeOrden(LineasDeOrden.Remove(lineaABorrar));
        }

        public Orden AgregarLineaDeOrden(LineaDeOrden nuevaLinea)
        {
            return ConLineasDeOrden(LineasDeOrden.Add(nuevaLinea));
        }

        public Orden ModificarLineaDeOrden(LineaDeOrden lineaOriginal, LineaDeOrden lineaModificada)
        {
            return lineaOriginal == lineaModificada
                ? this : ConLineasDeOrden(LineasDeOrden.Replace(lineaOriginal, lineaModificada));
        }

    }
}
