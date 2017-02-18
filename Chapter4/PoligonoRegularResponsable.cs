namespace Chapter4
{
    /// <summary>
    /// Esta versión del Polígono regular provee un constructor sin parámetros 
    /// para que no sea necesario decirle a la clase derivada, que llame al constructor base
    /// El comportamiento es un poco diferente, porque tengo que asegurarme que todos los campos se inicialicen
    /// </summary>
    abstract class PoligonoRegularResponsable
    {
        private int _NumeroDeLados;
        private float _LargoDelLado;

        /// <summary>
        /// Este constructor acepta dos parámetros, para el número de lados y el largo de cada lado
        /// </summary>
        /// <param name="NumeroDeLados"></param>
        /// <param name="LargoDelLado"></param>
        public PoligonoRegularResponsable(int NumeroDeLados, float LargoDelLado)
        {
            _NumeroDeLados = NumeroDeLados;
            _LargoDelLado = LargoDelLado;
        }


        /// <summary>
        /// Este constructor default asigna valores default
        /// para asegurarme de tener siempre un polígono viable
        /// </summary>
        public PoligonoRegularResponsable()
        {
            _NumeroDeLados = 3;
            _LargoDelLado = 1f;
        }

        public int NumeroDeLados
        {
            get
            {
                return _NumeroDeLados;
            }

            protected set
            {
                _NumeroDeLados = value;
            }
        }

        public float LargoDelLado
        {
            get
            {
                return _LargoDelLado;
            }

            set
            {
                _LargoDelLado = value;
            }


        }



        /// <summary>
        /// Retorna el perímetro. Para cualquier polígono regular es el largo del lado multiplicado por el 
        /// número de lados
        /// </summary>
        /// <returns>El perímetro como float</returns>
        public float Perimetro()
        {
            return _LargoDelLado * _NumeroDeLados;
        }

        /// <summary>
        /// El área no se puede saber de antemano, depende del polígono
        /// </summary>
        /// <returns>Retorna el área como float</returns>
        public abstract float Area();

    }
}
