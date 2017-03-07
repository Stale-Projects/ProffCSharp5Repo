using System.Collections.Generic;

namespace Chapter6
{
    /// <summary>
    /// Esta clase construye una serie de enteros y permite enumerarlos de distintos modos
    /// Así, el código cliente queda muy compacto
    /// </summary>
    class SerieDeEnteros
    {
        int[] _serie;
        public int CuentaDeEnteros { get; private set; }
        public int PrimerEntero { get; private set; }

        public SerieDeEnteros(int primerEntero, int cuentaDeEnteros)
        {
            _serie = new int[cuentaDeEnteros];
            for (int i = 0; i < _serie.Length; i++)
            {
                _serie[i] = primerEntero + i;
            }
            CuentaDeEnteros = cuentaDeEnteros;
            PrimerEntero = primerEntero;
        }


        /// <summary>
        /// Enumerador default
        /// </summary>
        /// <returns></returns>
        public IEnumerator<int> GetEnumerator()
        {

            for (int i = 0; i < _serie.Length; i++)
            {
                yield return _serie[i];
            }

        }

        /// <summary>
        /// Enumeración Inversa. Este código hubiera estado en el cliente
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> EnumeracionReversa()
        {
            for (int i = _serie.Length - 1; i >= 0; i--)
            {
                yield return _serie[i];
            }

        }

        public IEnumerable<int> EnumerarPares()
        {
            for (int i = 0; i < _serie.Length; i++)
            {

                if (_serie[i] % 2 == 0)
                {
                    yield return _serie[i];
                }
            }
        }


    }
}
