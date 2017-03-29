using System;

namespace EventoEditorial
{
    class Editorial
    {
        public string Nombre { get; private set; }

        public Editorial(string nombre)
        {
            Nombre = nombre;
        }
        public enum GenerosLiterarios
        {
            Historico,
            Policial,
            Fantástico,
            Politico,
            Auto_ayuda,
            Musical,
            Artistico
        }

        public event EventHandler<NuevoLibroEventArgs> NuevoLibroEvent;


        protected virtual void RaiseNuevoLibroEvent(string autor, string titulo, Editorial.GenerosLiterarios genero, float precio)
        {
            EventHandler<NuevoLibroEventArgs> _eventoNuevoLibro = NuevoLibroEvent;

            if (_eventoNuevoLibro != null)
            {
                _eventoNuevoLibro(this, new NuevoLibroEventArgs(autor, titulo, genero, precio));
            }
            else
            {
                Console.WriteLine("No hay nadie suscripto a los alertas de nuevas ediciones");
            }
        }

        public void NuevoLibro(string autor, string titulo, Editorial.GenerosLiterarios genero, float precio)
        {
            Console.WriteLine("La editorial {0} anuncia que salió un nuevo título de {1} llamado {2}", Nombre, autor, titulo);
            RaiseNuevoLibroEvent(autor, titulo, genero, precio);
        }



    }
}
