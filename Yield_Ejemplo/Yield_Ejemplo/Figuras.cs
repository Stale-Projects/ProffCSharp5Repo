using System.Collections;
using System.Collections.Generic;

namespace Yield_Ejemplo
{
    class Figuras
    {
        private string _colorActual;
        private string _formaActual;
        private bool _figuraCompleta;
        private int _iteracion;

        public string[] Colores { get; private set; }
        public string[] Formas { get; private set; }

        public Figuras()
        {
            Colores = new string[] { "rojo", "amarillo", "azul", "blanco" };
            Formas = new string[] { "círculo", "triángulo", "rectángulo" };

        }

        public IEnumerator<string> GetEnumerator()
        {
            _figuraCompleta = false;
            _iteracion = 0;
            IEnumerator enumerador = EnumerarFormas();

            while (enumerador.MoveNext())
            {
                enumerador = enumerador.Current as IEnumerator;
                if (_figuraCompleta)
                {
                    _figuraCompleta = false;
                    _iteracion++;
                    yield return _formaActual + " " + _colorActual;
                }

            }
        }

        private IEnumerator EnumerarColores()
        {

            //while (_iteracion < Colores.Length)
            //{
            //    //_colorActual = Colores[_iteracion];
            //    //_figuraCompleta = true;
            //    //yield return EnumerarFormas();
                
            //}

            //Lo siguiente no funcionará:
            foreach (var color in Colores)
            {
                _colorActual = color;
                _figuraCompleta = true;
                yield return EnumerarFormas();
            }

        }

        private IEnumerator EnumerarFormas()
        {
            while (_iteracion < Formas.Length)
            {
                _formaActual = Formas[_iteracion];
                yield return EnumerarColores();
            }
        }
    }
}
