using System;

namespace Chapter4
{
    /// <summary>
    /// Este triángulo regular hereda de PoligonoRegularResponsable
    /// No tiene que preocuparse de como llamar al constructor de la clase base, sólo se ocupa de 
    /// construirse a sí mismo
    /// </summary>
    class TrianguloRegularDespreocupado : PoligonoRegularResponsable
    {
        public TrianguloRegularDespreocupado(float LargoDelLado)
        {
            NumeroDeLados = 3;
            this.LargoDelLado = LargoDelLado;

        }

        public override float Area()
        {
            float altura;
            altura = (float)Math.Sqrt(0.75 * LargoDelLado * LargoDelLado);
            return (LargoDelLado * altura) / 2;

        }


    }
}
