using System;

namespace Chapter4
{
    /// <summary>
    /// Esta clase hereda de Polígono Regular
    /// El constructor llama al constructor de la clase base usando los parámetros necesarios
    /// ya que la clase base no tiene constructor sin parámetros
    /// El único método que necesito implementar es el método abstract Area. Lo demás me lo da la clase base
    /// </summary>
    class TrianguloRegular : PoligonoRegular
    {
        public TrianguloRegular(float LargoDelLado) : base(3, LargoDelLado)
        {

        }

        public override float Area()
        {
            float altura;
            altura = (float)Math.Sqrt(0.75 * LargoDelLado * LargoDelLado);
            return (LargoDelLado * altura) / 2;
        }


    }
}
