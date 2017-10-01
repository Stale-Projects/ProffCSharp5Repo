using System.Collections.Generic;

namespace Yield_Ejemplo
{
    class Naturales
    {
        private int[] _numeros;
        public int LimiteSuperior { get; private set; }

        public Naturales(int limiteSuperior)
        {
            LimiteSuperior = limiteSuperior;
            _numeros = new int[limiteSuperior];
            CargarNumeros();
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i <= LimiteSuperior; i++)
            {
                yield return i;
            }
        }

        public IEnumerable<int> NumerosPares()
        {
            int _limiteSuperiorPares = (int)LimiteSuperior / 2;
            for (int i = 0; i <= _limiteSuperiorPares; i++)
            {
                yield return i * 2;
            }
        }

        public IEnumerable<int> SecuenciaInvertida()
        {
            for (int i = LimiteSuperior; i >= 0; i--)
            {
                yield return i;
            }
        }



        private void CargarNumeros()
        {
            for (int i = 0; i < LimiteSuperior; i++)
            {
                _numeros[i] = i;
            }
        }

    }
}
