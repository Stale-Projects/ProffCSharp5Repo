using System;

namespace Chapter4
{
    class Program
    {
        static void Main(string[] args)
        {
            EncabezadoYPieConsola Escritor = new EncabezadoYPieConsola();

            HisBaseClass myInstanceClass = new MyDerivedClass("Ponele");
            myInstanceClass.HisMethod1();
            //myInstanceClass.MyMethod1(); No se puede llamar 
            myInstanceClass.MyGroovyMethod();
            myInstanceClass.UnMetodoVirtual();

            MyDerivedClass myDerivedInstanceClass = new MyDerivedClass("Fijate");
            myDerivedInstanceClass.HisMethod1();
            myDerivedInstanceClass.MyMethod1();
            myDerivedInstanceClass.MyGroovyMethod();
            myDerivedInstanceClass.UnMetodoVirtual();

            Escritor.EscribirEncabezado("Ejemlo 4.1: Jeraraquía de Constructores");
            TrianguloRegular UnTriangulo = new TrianguloRegular(12.5f);
            Console.WriteLine("Las siguientes propiedades vienen de la clase base");
            Console.WriteLine("Número de lados del polígono regular: {0}", UnTriangulo.NumeroDeLados.ToString());
            Console.WriteLine("Largo del lado del polígono regular: {0}", UnTriangulo.LargoDelLado.ToString());
            Console.WriteLine("Este método también viene de la clase base:");
            Console.WriteLine("Perímetro del polígono regular: {0}", UnTriangulo.Perimetro().ToString());
            Console.WriteLine("El siguiente método depende de la clase derivada:");
            Console.WriteLine("Área del Rectángulo: {0}", UnTriangulo.Area().ToString());
            Escritor.EscribirPie("Fin Ejemplo 4.1");


            Escritor.EscribirEncabezado("Ejemlo 4.2: Jeraraquía de Constructores Responsables");
            TrianguloRegularDespreocupado OtroTriangulo = new TrianguloRegularDespreocupado(15.5f);
            Console.WriteLine("Las siguientes propiedades vienen de la clase base");
            Console.WriteLine("Número de lados del polígono regular: {0}", OtroTriangulo.NumeroDeLados.ToString());
            Console.WriteLine("Largo del lado del polígono regular: {0}", OtroTriangulo.LargoDelLado.ToString());
            Console.WriteLine("Este método también viene de la clase base:");
            Console.WriteLine("Perímetro del polígono regular: {0}", OtroTriangulo.Perimetro().ToString());
            Console.WriteLine("El siguiente método depende de la clase derivada:");
            Console.WriteLine("Área del Rectángulo: {0}", OtroTriangulo.Area().ToString());
            Escritor.EscribirPie("Fin Ejemplo 4.2");



        }
    }

    struct EncabezadoYPieConsola
    {
        public void EscribirEncabezado(string Titulo)
        {
            string encabezado = PrepararString(Titulo, '*');
            Console.WriteLine(encabezado);
            Console.WriteLine("");
        }

        public void EscribirPie(string Titulo)
        {
            string pie = PrepararString(Titulo, '-');
            Console.WriteLine("");
            Console.WriteLine(pie);
            Console.WriteLine("");
            Console.WriteLine("");
        }

        private string PrepararString(string Literal, char Caracter)
        {
            if (Literal.Length > 80)
                Literal = Literal.Substring(1, 80);
            int numeroDeAsteriscos = 80 - Literal.Length;
            string preparada = new string(Caracter, (int)(numeroDeAsteriscos / 2)) + Literal +
            new string(Caracter, (int)(numeroDeAsteriscos / 2));
            return preparada;
        }
    }

}
