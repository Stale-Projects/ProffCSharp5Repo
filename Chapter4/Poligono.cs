namespace Chapter4
{
    /// <summary>
    /// Clase base para ejemplificar jerarquía de constructores
    /// Esta es la versión 1 en la que el constructor toma parámetros y no proveo un constructor 
    /// sin parámetros. Esta clase ejemplifica una clase abstracta que tiene algunos métodos no abstractos
    /// </summary>
    abstract class PoligonoRegular
    {
        private int _NumeroDeLados;
        private float _LargoDelLado;

        public PoligonoRegular(int NumeroDeLados, float LargoDelLado)
        {
            _NumeroDeLados = NumeroDeLados;
            _LargoDelLado = LargoDelLado;
        }

        public int NumeroDeLados
        {
            get
            {
                return _NumeroDeLados;
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
