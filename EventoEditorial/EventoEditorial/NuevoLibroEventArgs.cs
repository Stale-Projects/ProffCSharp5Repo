using System;

namespace EventoEditorial
{
    class NuevoLibroEventArgs : EventArgs
    {
        public string Autor { get; private set; }
        public string Titulo { get; private set; }
        public Editorial.GenerosLiterarios Genero { get; private set; }
        public float Precio { get; private set; }

        public NuevoLibroEventArgs(string autor, string titulo, Editorial.GenerosLiterarios genero, float precio)
        {
            Autor = autor;
            Titulo = titulo;
            Genero = genero;
            Precio = precio;
        }
    }
}
