using System.Collections.Generic;

namespace Chapter10
{
    class GuitaristComparer : IComparer<Guitarist>
    {
        public int Compare(Guitarist PrimerGuitarrista, Guitarist SegundoGuitarrista)
        {

            if (PrimerGuitarrista == null ^ SegundoGuitarrista == null)
                return -1;
            if (PrimerGuitarrista == null && SegundoGuitarrista == null)
                return 0;
            return SegundoGuitarrista.FirstName.CompareTo(PrimerGuitarrista.FirstName);

        }
    }
}
