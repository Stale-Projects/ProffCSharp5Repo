using System;
using System.Collections.Generic;
using System.Linq;

namespace EnumerarArchivos
{
    static class mvExtensiones
    {
        /// <summary>
        /// Esta versión se aplica del siguiente modo: 
        /// var desvEst = datos.DesvioEstandarSimple()
        /// Donde datos es una colección de doubles
        /// Su limitación es que necesito sí o sí una colección de 
        /// doubles obtenidos quizá por un query a partir de una 
        /// colección genérica
        /// Referencia del algoritmo: http://warrenseen.com/blog/2006/03/13/how-to-calculate-standard-deviation/
        /// Ese blog ya no existe pero se puede obtener con WayBackMachine:
        /// https://web.archive.org/web/20060321182147/http://warrenseen.com/blog/2006/03/13/how-to-calculate-standard-deviation/
        /// </summary>
        /// <param name="valores">Una colección de valores de tipo double sobre la que quiero calcular el desvio estandar</param>
        /// <returns>Devuelve un double que es el desvio estandar de los valores</returns>
        public static double DesvioEstandarSimple(this IEnumerable<double> valores)
        {
            // ref: http://warrenseen.com/blog/2006/03/13/how-to-calculate-standard-deviation/
            double media = 0.0;
            double suma = 0.0;
            double desvioEstandar = 0.0;
            int n = 0;
            foreach (double val in valores)
            {
                n++;
                double delta = val - media;
                media += delta / n;
                suma += delta * (val - media);
            }
            if (1 < n)
                desvioEstandar = Math.Sqrt(suma / (n - 1));

            return desvioEstandar;
        }

        /// <summary>
        /// Esta versión es completamente genérica pero hay que proveer una Lambda que 
        /// obtenga un valor de tipo double a partir de un objeto genérico
        /// Su uso es del siguiente modo: 
        /// var desvEst = datos.DesvioEstandar(x => x.Valor);
        /// Reemplazar x => x.Valor por una Lambda que me permita obtener un double 
        /// a partir del elemento genérico x
        /// </summary>
        /// <typeparam name="T">El tipo genérico de objetos que están en la colección</typeparam>
        /// <param name="coleccion">La colección de objetos de tipo T</param>
        /// <param name="valores">La Lambda que me permite obtener un double a partir de cada objeto de tipo T</param>
        /// <returns>Devuelve un double que es el desvío estándar de los valores obtenidos de T</returns>
        public static double DesvioEstandar<T>(this IEnumerable<T> coleccion, Func<T, double> valores)
        {
            // ref: https://stackoverflow.com/questions/2253874/linq-equivalent-for-standard-deviation
            // ref: http://warrenseen.com/blog/2006/03/13/how-to-calculate-standard-deviation/ 
            var media = 0.0;
            var suma = 0.0;
            var desvioEstandar = 0.0;
            var n = 0;
            foreach (var valor in coleccion.Select(valores))
            {
                n++;
                var delta = valor - media;
                media += delta / n;
                suma += delta * (valor - media);
            }
            if (1 < n)
                desvioEstandar = Math.Sqrt(suma / (n - 1));

            return desvioEstandar;
        }

    }
}
