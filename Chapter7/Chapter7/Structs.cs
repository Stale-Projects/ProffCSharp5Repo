using System;

namespace Chapter7
{
    struct MisMascotas
    {
        public string NombreDelPerro { get; set; }
        public string NombreDelGato { get; set; }
    }

    struct Vector
    {
        public long x;
        public long y;
        public long z;

        public Vector(long x, long y, long z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        //Sobrecarga de operador asignación (Vector2 = Vector1)?
        public Vector(Vector rhs)
        {
            this.x = rhs.x;
            this.y = rhs.y;
            this.z = rhs.z;
        }

        //Ejemplo de Operator overloading: Suma
        public static Vector operator +(Vector lhs, Vector rhs)
        {
            Vector result = new Vector(lhs);
            result.x += rhs.x;
            result.y += rhs.y;
            result.z += rhs.z;
            return result;
        }

        //Sobrecarga de Operador producto con tres versiones: 
        //Escalar por Vector, Vector por escalar, Vector por Vector
        public static Vector operator *(long lhs, Vector rhs)
        {
            return new Vector(rhs.x * lhs, rhs.y * lhs, rhs.z * lhs);
        }

        public static Vector operator *(Vector lhs, long rhs)
        {
            return rhs * lhs;
        }

        public static long operator *(Vector lhs, Vector rhs)
        {
            return (lhs.x * rhs.x) + (lhs.y * rhs.y) + (lhs.z * rhs.z);
        }

        //Sobrecarga de Operador comparación (==)
        public static bool operator ==(Vector lhs, Vector rhs)
        {
            return ((lhs.x == rhs.x) &&
                (lhs.y == rhs.y) && (lhs.z == rhs.z));
        }

        //Sobrecarga de operador comparación (!=) usando como base
        //el operador de comparación (==)
        public static bool operator !=(Vector lhs, Vector rhs)
        {
            return !(lhs == rhs);
        }


        public override string ToString()
        {
            string result;
            result = "(" + this.x.ToString() + ", " + this.y.ToString() + ", " + this.z.ToString() + ")";
            return result;

        }
    }

    struct Currency
    {
        public uint Pesos;
        public ushort Centavos;

        public Currency(uint pesos, ushort centavos)
        {
            Pesos = pesos;
            Centavos = centavos;
        }

        public static implicit operator float(Currency value)
        {
            return value.Pesos + (value.Centavos / 100.0f);
        }

        public static explicit operator Currency(float value)
        {
            checked
            {
                uint pesos = Convert.ToUInt16(value); //(uint)value;
                ushort centavos = Convert.ToUInt16((value - pesos) * 100);
                return new Currency(pesos, centavos);
            }

        }

    }
    class Structs
    {
    }
}
