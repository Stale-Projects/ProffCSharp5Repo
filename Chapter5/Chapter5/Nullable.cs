namespace Chapter5
{
    //Definición de clase Nullable para poder usar con bases de datos, XML, etc., donde un valor
    //que representa un value type puede ser nulo (no asignado). 
    class Nullable<T> where T : struct
    {
        private object _value;
        public Nullable()
        {

        }



        public Nullable(T value)
        {
            _value = value;
        }

        public bool HasValue
        {
            get { return _value != null; }

        }

        public T GetValueOrDefault()
        {
            if (this.HasValue)
                return (T)_value;
            else
                return default(T);
        }

    }

}
