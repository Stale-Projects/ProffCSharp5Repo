using System;
using System.Diagnostics.Contracts;

namespace Capitulo10_Diccionarios
{
    /// <summary>
    /// Clase utilizada para probar la funcionalidad de la clase genérica Dictionary
    /// El ID de fichaje del futbolista es una instancia de la struct <see cref="FichaDeFutbolista"/> 
    /// La clase futbolista no tiene más que un conjunto de propiedades, un constructor donde se 
    /// definen las propiedades, y un override de ToString() para mostrar toda esta información
    /// </summary>
    class Futbolista
    {
        public IDDeFutbolista ID { get; private set; }
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public string Equipo { get; private set; }
        public string Nacionalidad { get; private set; }
        public float SalarioAnual { get; private set; }

        public Futbolista(IDDeFutbolista id, string nombre, string apellido, string equipo, string nacionalidad, float salarioAnual)
        {
            ID = id;
            Nombre = nombre;
            Apellido = apellido;
            Equipo = equipo;
            Nacionalidad = nacionalidad;
            SalarioAnual = salarioAnual;
        }



        public override string ToString()
        {
            string resultado;
            resultado = Nombre + " " + Apellido + " " + "juega en " + Equipo +
                ", tiene nacionalidad " + Nacionalidad + " y gana Euros M " + SalarioAnual.ToString() +
                " al año. Su código de fichaje es: " + ID.CodigoDeFichaje;

            return resultado;
        }
    }

    /// <summary>
    /// Este struct se utiliza para proporcionar a nuestra clase Futbolista un ID estructurado
    /// El primer carácter debe ser una letra, y el resto un entero. Si el entero tiene menos de 
    /// 6 dígitos se insertan los ceros necesarios entre el primer caracter y el entero para completar
    /// un ID con 7 caracteres alfanuméricos
    /// En el constructor se proporciona el ID y se verifica la estructura. Si es incorrecta se lanza
    /// una excepción <see cref="IDDeFichajeDeFutbolistaNovalidoException"/>
    /// En esta clase usamos un Contrato para especificar una condición para el codigo de fichaje, que no debe ser nulo
    /// Si es nulo generamos una excepción estándar
    /// Un punto importante es el override de GetHashCode y la implementación de Equals y el operador == 
    /// Notar que en Equals no puedo usar la expresión (otroID == null) hasta que no hago una implementación del operador ==
    /// Esto sucede porque es un Tipo por Valor (es una struct)
    /// </summary>

    public struct IDDeFutbolista : IEquatable<IDDeFutbolista>
    {
        private readonly char _prefijo;
        private readonly int _numero;
        public string CodigoDeFichaje { get; private set; }


        public IDDeFutbolista(string codigoDeFichaje)
        {
            Contract.Requires<ArgumentNullException>(codigoDeFichaje != null);

            try
            {
                _prefijo = codigoDeFichaje.ToUpper()[0];
                if (!char.IsLetter(_prefijo))
                {
                    throw new IDDeFichajeDeFutbolistaNovalidoException("El primer caracter del código de fichaje debe ser una letra");
                }
                int.TryParse(codigoDeFichaje.Substring(1), out _numero);
                if (_numero == 0)
                {
                    throw new IDDeFichajeDeFutbolistaNovalidoException("El código de fichaje debe contener un número distinto de cero luego de la primer letra");
                }
                CodigoDeFichaje = _prefijo + string.Format("{0:000000}", _numero);
            }
            catch (FormatException)
            {
                throw new IDDeFichajeDeFutbolistaNovalidoException("El código de fichaje proporcionado no es válido");
            }

        }

        public override int GetHashCode()
        {
            int _codigoHash;
            _codigoHash = _numero.GetHashCode() * 0x15051505;
            _codigoHash = _codigoHash ^ (_prefijo.GetHashCode() * 0x15051505);
            return _codigoHash;
        }

        public static bool operator ==(IDDeFutbolista lhs, IDDeFutbolista rhs)
        {
            if (lhs == null && rhs == null)
            {
                return true;
            }
            else
            {
                return lhs.Equals(rhs);
            }
        }


        public static bool operator !=(IDDeFutbolista lhs, IDDeFutbolista rhs)
        {
            if (lhs == null && rhs == null)
            {
                return false;
            }
            else
            {
                return !(lhs.Equals(rhs));
            }
        }



        public bool Equals(IDDeFutbolista otroID)
        {
            return (otroID == null) ? false : (CodigoDeFichaje == otroID.CodigoDeFichaje);

        }

        public override bool Equals(object obj)
        {
            return Equals((IDDeFutbolista)obj);
        }

    }

    /// <summary>
    /// Esta clase se utiliza para crear una excepción personalizada
    /// para nuestra clase FichaDeFutbolista
    /// <param name="mensaje">El mensaje que se muestra cuando la excepción es lanzada</param> 
    /// </summary>
    public class IDDeFichajeDeFutbolistaNovalidoException : Exception
    {
        public IDDeFichajeDeFutbolistaNovalidoException(string mensaje) : base(mensaje) { }
    }

}
