using System;
namespace Chapter7
{
    class Person
    {
        private string _firstName;
        private HashType _tipoDeHash;
        public enum HashType
        {
            Algoritmo = 1,
            Fijo = 2,
            Aleatorio = 3

        }


        public Person(HashType tipoDeHash)
        {
            _tipoDeHash = tipoDeHash;
        }
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public override bool Equals(object obj)
        {
            //Si comparo peras con melones retornar false
            if (!(obj is Person))
            {
                return false;
            }

            Person comparando = (Person)obj;
            if (this.FirstName == comparando.FirstName && this.LastName == comparando.LastName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //Cuando se hace un override de Equals conviene hacer un override de GetHashCode para asegurarnos que 
        //dos objetos que son iguales según Equals tengan el mismo hash
        //Usamos dos enteros primos y computamos el hashcode como la suma de los hashcodes multiplicados por cada uno de estos enteros
        public override int GetHashCode()
        {
            int i = 13;
            int j = 7;
            Random hasheador = new Random(DateTime.Now.Millisecond);

            switch (_tipoDeHash)
            {
                case HashType.Algoritmo:
                    return (this.FirstName.GetHashCode() * i) + (this.LastName.GetHashCode() * j);
                    break;
                case HashType.Fijo:
                    return 20;
                    break;
                case HashType.Aleatorio:
                    return hasheador.Next(100000);
                    break;
                default:
                    return 0;
                    break;
            }


        }

    }
}
