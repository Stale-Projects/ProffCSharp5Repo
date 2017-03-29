using System;

namespace EventoEditorial
{
    class Suscriptor
    {
        public string Nombre { get; private set; }
        public Editorial.GenerosLiterarios GeneroPreferido { get; private set; }

        public Suscriptor(string nombre, Editorial.GenerosLiterarios generoPreferido)
        {
            Nombre = nombre;
            GeneroPreferido = generoPreferido;
        }


        public void OnNuevoLibroEvent(object sender, NuevoLibroEventArgs args)
        {
            Console.WriteLine("Soy {0} y te digo:", Nombre);
            Console.WriteLine("Llegó un libro. Titulo: {0}, Autor: {1}, Género: {2}, Precio: {3}", args.Titulo, args.Autor, args.Genero.ToString(), args.Precio.ToString());
            if (args.Genero == GeneroPreferido)
            {
                Console.WriteLine("Me gustan los libros del género {0}, lo voy a comprar", args.Genero.ToString());
            }
            else
            {
                Console.WriteLine("No me gustan los libros del género {0}, paso!", args.Genero.ToString());
            }
        }
    }
}
