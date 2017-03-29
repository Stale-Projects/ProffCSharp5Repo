namespace EventoEditorial
{
    class Program
    {
        static void Main(string[] args)
        {
            Editorial unaEditorial = new Editorial("Nueva Editorial");
            unaEditorial.NuevoLibro("Franz Kafka", "La Metamorfosis", Editorial.GenerosLiterarios.Fantástico, 88.12F);

            Suscriptor Michael = new Suscriptor("Michael Jackson", Editorial.GenerosLiterarios.Auto_ayuda);
            unaEditorial.NuevoLibroEvent += Michael.OnNuevoLibroEvent;
            Suscriptor Frank = new Suscriptor("Frank Zappa", Editorial.GenerosLiterarios.Musical);
            unaEditorial.NuevoLibroEvent += Frank.OnNuevoLibroEvent;

            unaEditorial.NuevoLibro("Ira Challup", "Diesel Espiritual", Editorial.GenerosLiterarios.Auto_ayuda, 999.99F);

            unaEditorial.NuevoLibro("Luis Alberto Spinetta", "Guitarra Negra", Editorial.GenerosLiterarios.Musical, 455.12F);
        }
    }
}
