namespace Chapter7
{
    class Person
    {
        private string _firstName;

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
        public override int GetHashCode()
        {
            int i = 13;
            int j = 7;
            return (this.FirstName.GetHashCode() * i) + (this.LastName.GetHashCode() * j);

        }

    }
}
