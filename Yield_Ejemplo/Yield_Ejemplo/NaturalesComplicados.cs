using System;
using System.Collections;
using System.Collections.Generic;

namespace Yield_Ejemplo
{
    class NaturalesComplicados
    {
        private int[] _numeros;
        private int _actual;
        public int LimiteInferior { get; private set; }
        public int LimiteSuperior { get; private set; }

        public NaturalesComplicados(int limiteUnferior, int limiteSuperior)
        {
            LimiteInferior = limiteUnferior;
            LimiteSuperior = limiteSuperior;
            _numeros = new int[limiteSuperior];
            CargarNumeros();
        }

        public IEnumerator<int> GetEnumerator()
        {
            Console.WriteLine("Enumerando desde el enumerador por defecto");
            _actual = LimiteInferior;
            IEnumerator enumerador = (LimiteInferior % 2 == 0) ? Pares() : Impares();
            while (enumerador.MoveNext())
            {
                enumerador = enumerador.Current as IEnumerator;
                yield return _actual++;

            }

        }

        public void Enumerar()
        {
            Console.WriteLine("Enumerando desde Enumerar()");
            IEnumerator enumerador = (LimiteInferior % 2 == 0) ? Pares() : Impares();
            _actual = LimiteInferior;

            while (enumerador.MoveNext())
            {
                _actual++;
                enumerador = enumerador.Current as IEnumerator;
            }

        }

        private IEnumerator Impares()
        {
            //Entra un número impar pero anuncia el próximo que es par

            while (true)
            {

                if (_actual > LimiteSuperior)
                {
                    yield break;
                }
                Console.WriteLine("El próximo número es impar:");
                Console.WriteLine(_actual.ToString());
                yield return Pares();
            }



        }

        private IEnumerator Pares()
        {

            while (true)
            {
                if (_actual > LimiteSuperior)
                {
                    yield break;
                }
                Console.WriteLine("El próximo número es par:");
                Console.WriteLine(_actual.ToString());
                yield return Impares();
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
