using System.Collections.Generic;

namespace MecanismosDeSoloLectura
{
    class Orden
    {
        public List<LineaDeOrden> LineasDeOrden { get; private set; }
        public Orden(List<LineaDeOrden> lineasDeOrden)
        {
            LineasDeOrden = lineasDeOrden;
        }

    }
}
