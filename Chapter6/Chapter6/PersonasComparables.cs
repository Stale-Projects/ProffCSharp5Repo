using System;

namespace Chapter6
{
    /// <summary>
    /// Esta clase muestra un ejemplo de como comparar arrays o tuplas de UDTs
    /// Es una comparación estructural para decidir si dos arrays 
    /// o tuplas tienen los mismos elementos
    /// Una parte importante de la comparación estructural es ordenar
    /// por lo tanto también implemento la interfaz IComparable y hago override de 
    /// CompareTo
    /// </summary>
    class PersonasComparables : IEquatable<PersonasComparables>, IComparable<PersonasComparables>
    {
        public int ID { get; private set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }


        public PersonasComparables()
        {
            Random _asignador = new Random(DateTime.Now.Millisecond);
            ID = _asignador.Next(10000000);
        }

        //Override ToString para mostrar un objeto
        public override string ToString()
        {
            return String.Format("{0} {1}, ID: {2}", Nombre, Apellido, ID);

        }

        //La versión de Equals que overrideo acepta un Object como parametro
        //Lo único que hace mi override es llamar a otra función que acepta 
        //un objeto de la misma clase para comparar
        public override bool Equals(object obj)
        {
            if (obj == null)
                return base.Equals(obj);
            return Equals(obj as PersonasComparables);

        }

        //Siempre que overrideo Equals
        //tengo que overridear GetHashCode
        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        //Aquí está la comparación real. Comparo por ID, Nombre y Apellido
        public bool Equals(PersonasComparables OtraPersona)
        {
            if (OtraPersona == null)
                return base.Equals(OtraPersona);

            return (ID == OtraPersona.ID && Nombre == OtraPersona.Nombre && Apellido == OtraPersona.Apellido);
        }

        public int CompareTo(PersonasComparables otraPersona)
        {
            if (Apellido == otraPersona.Apellido)
            {
                if (Nombre == otraPersona.Nombre)
                {
                    return ID.CompareTo(otraPersona.ID);
                }
                else
                {
                    return Nombre.CompareTo(otraPersona.Nombre);
                }

            }
            else
            {
                return Apellido.CompareTo(otraPersona.Apellido);
            }
        }
    }
}
