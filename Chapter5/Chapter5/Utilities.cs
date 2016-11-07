using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter5
{
    //Ejemplo de cómo definir una función que toma el máximo entre dos Types cualesquiera
    //que implementen la interfaz IComparable y que tengan un constructor
    //Ver ue implementar la interfaz IComparable implica definir una implementación del 
    //método CompareTo
    class Utilities<T> where T : IComparable, new()
    {

        public void DoSomething()
        {
            var obj = new T();
        }

        public int Max(int a, int b)
        {
        
            return (a > b) ? a : b;
        }

        public T GenericMax(T a, T b)
        {
            return a.CompareTo(b) > 0 ? a : b;

        }
    }
}
