namespace Capitulo10_Diccionarios
{
    class Alumno
    {
        public string Nombre { get; private set; }
        public string Apellido { get; private set; }
        public float Calificacion { get; private set; }

        public Alumno(string nombre, string apellido, float calificacion)
        {
            Nombre = nombre;
            Apellido = apellido;
            Calificacion = calificacion;

        }

        public override string ToString()
        {
            return Nombre + " " + Apellido + " tiene un " + Calificacion.ToString() +
                " en el examen. Eso nos dice algo sobre su capacidad?";
        }


    }


}
