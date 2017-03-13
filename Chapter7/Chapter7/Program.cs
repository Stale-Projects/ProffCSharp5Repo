using System;

namespace Chapter7
{
    class Program
    {

        static void Main(string[] args)
        {
            EncabezadoYPieConsola Separador = new EncabezadoYPieConsola();

            //Ejemplo 1: Diferencia entre prefix y postfix
            //Con el prefijo, primero se suma 1 a x y luefo se evalúa el if
            //con el sufijo primero se evalúa el if y luego se suma 1 a x
            #region Ejemplo 1
            Separador.EscribirEncabezado("Ejemplo 1: Operador prefix y postfix");

            int x = 5;
            if (++x == 6)
                Console.WriteLine("x es igual a 6 con el prefix");
            if (x++ == 7)
                Console.WriteLine("x es igual a 7 con el postfix");
            else
            {
                Console.WriteLine("El operador postfix no incrementó el valor!");
            }

            Separador.EscribirPie("Fin Ejemplo 1");
            #endregion

            //Ejemplo 2: Overflow y como prevenirlo con checked
            #region Ejemplo 2
            Separador.EscribirEncabezado("Ejemplo 2: Overflow y prevención");

            //Aquí unByte hace overflow y termina valiendo cero
            byte unByte = 255;
            Console.WriteLine("Antes del operador, el Byte vale: {0}", unByte.ToString());
            unByte++;
            Console.WriteLine("Ahora el byte vale {0}", unByte.ToString());

            //Aquí otroByte sigue valiendo 255
            //Ver la técnica de encerrar el bloque checked dentro de un try
            //Esto permite atrapar el error eventual que purde producirse al realizar la 
            //operación

            byte otroByte = 255;
            try
            {
                checked
                {
                    otroByte++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ocurrió la siguiente excepción: {0}", e.Message);

            }
            Console.WriteLine("El valor de otroByte es: {0}", otroByte.ToString());

            Separador.EscribirPie("Fin Ejemplo 2");
            #endregion

            //Ejemplo 3: Conversión explícita usando "as"
            //Ojo sólo se usa para Reference Types
            #region Ejemplo 3
            Separador.EscribirEncabezado("Ejemplo 3: Conversión explícita usando 'as'");

            object miEntero = "5";
            object str = 12;

            Console.WriteLine("Conversión de object a string: {0}", (miEntero as string) + " es ahora una string");

            if (str as int? != null)
            {
                Console.WriteLine("La conversión de object a 'int?' me anuló la variable");
            }
            else
            {
                Console.WriteLine("Conversión de object a int: {0}", ((str as int?) + 5).ToString());
            }




            Separador.EscribirPie("Fin ejemplo 3");
            #endregion

            //Ejemplo 4: Pérdida de precisión al convertir de long a float
            #region Ejemplo 4
            Separador.EscribirEncabezado("Ejemplo 4: Pérdida de precisión en conversiones");

            long enteroLargo = 922337293685477580;
            Console.WriteLine("EnteroLargo vale: {0}", enteroLargo.ToString());
            float flotante = enteroLargo;
            Console.WriteLine("Flotante vale: {0}", flotante.ToString());
            Console.WriteLine("Prestar atención al redondeo que hizo: 922337293685477580 pasó a ser: 92233730000000000");

            Separador.EscribirPie("Fin Ejemplo 4");
            #endregion

            //Ejemplo 5: Parseo de strings usando el método Parse. 
            //Notar que Parse es un método estático de los tipos a los cuales 
            //quiero convertir la string. En el ejemplo, de bool y de int
            #region Ejemplo 5
            Separador.EscribirEncabezado("Ejemplo 5: Parseo de Strings usando el método Parse de los tipos primitivos");

            string strBooleano = "true";
            string strEntero = "16";

            //Probemos conversiones explícitas por medio de parse
            if (bool.Parse(strBooleano))
            {
                Console.WriteLine("El booleano se parseó correctamente");
            }

            if (int.Parse(strEntero) == 16)
            {
                Console.WriteLine("El entero se parseó correctamente");
            }

            Separador.EscribirPie("Fin Ejemplo 5");
            #endregion


            //Ejemplo 6: Un overflow en unboxing
            //Fijate que en la línea donde asigno byteGrandeUnboxed tengo un doble cast: 
            //El primero es para pasar byteGrandeBoxed a byte y poder sumarle 1
            //El segundo es para pasar el resultado de la suma a Byte, ya que el compilador lo convierte a int
            //En la segunda parte del ejemplo obtenemos un InvalidCastException al hacer unboxing con un tipo diferente al inicial 
            #region Ejemplo 6
            Separador.EscribirEncabezado("Ejemplo 6: Overflow en Unbozing");

            byte byteGrande = 255;
            object byteGrandeBoxed = (object)byteGrande;
            byte byteGrandeUnboxed = (byte)((byte)byteGrandeBoxed + 1);
            Console.WriteLine("Sorpresa! byteGrandeUnboxed vale: {0}", byteGrandeUnboxed.ToString());

            long unLongCualquiera = 333333423;
            object unLongCualquieraBoxed = (object)unLongCualquiera;
            //La siguiente línea da una excpción, por eso está comentada
            //int aDisney = (int)unLongCualquieraBoxed;

            Separador.EscribirPie("Fin ejemplo 6");
            #endregion

            //Ejemplo 7: Comparando objetos
            //En el primer caso comparo por referencia usando el método static de System.Object ReferenceEquals
            //En la segunda y tercera comparaciones uso un método virtual overrideado en la clase Person
            //El override es simple cuando comparo objeto por objeto, no hace falta implementar interfases 
            //como se vio en Arrays
            #region Ejemplo 7
            Separador.EscribirEncabezado("Ejemplo 7: Comparaciones de Objetos");

            Person curly = new Person(Person.HashType.Fijo);
            curly.FirstName = "Curly";
            curly.LastName = "Howard";
            Person otraRef = curly;
            Console.WriteLine("Las dos variables: curly y otraRef son referencias a un mismo objeto de tipo Person");
            Console.WriteLine("Pregunta: las referencias de curly y otraRef son iguales (por medio de ReferenceEquals)? Respuesta: {0}", object.ReferenceEquals(curly, otraRef));
            Console.WriteLine("Comparación por medio de Equals. Son iguales? Respuesta: {0}", (curly.Equals(otraRef)).ToString());
            Console.WriteLine("Comparación por medio de '=='. Son iguales? Respuesta: {0}", (curly == otraRef).ToString());


            Person shemp = new Person(Person.HashType.Fijo);
            shemp.FirstName = "Shemp";
            shemp.LastName = "Howard";
            Console.WriteLine("En este caso las variables: curly y shemp apuntan a dos instancias distintas de la clase Person");
            Console.WriteLine("Comparación de curly vs shemp por medio de Equals. Son iguales? {0}", (shemp.Equals(curly)));
            Console.WriteLine("Comparación de curly vs shemp por medio de '=='. Son iguales? {0}", (shemp == curly).ToString());
            Console.WriteLine("Hash de Curly: {0}", curly.GetHashCode().ToString());
            Console.WriteLine("Hash de Shemp: {0}", shemp.GetHashCode().ToString());

            Person curlyClon = new Person(Person.HashType.Algoritmo);
            curlyClon.FirstName = "Curly";
            curlyClon.LastName = "Howard";

            Console.WriteLine("Comparación de Curly vs CurlyClon por medio de Equals. Son iguales? {0}", (curlyClon.Equals(curly)));
            Console.WriteLine("Comparación de Curly vs CurlyClon por medio de '=='. Son iguales? {0}", (curlyClon == curly).ToString());
            Console.WriteLine("Hash de Curly: {0}", curly.GetHashCode().ToString());
            Console.WriteLine("Hash de CurlyClon: {0}", curlyClon.GetHashCode().ToString());

            Separador.EscribirPie("Fin Ejemplo 7");
            #endregion

            //Ejemplo 8: Comparación de value types usando Equals de instancia (no se precisa castear, se hace boxing)
            #region Ejemplo 8
            Separador.EscribirEncabezado("Ejemplo 8: Comparación de Value Types");

            int primerEntero = 5;
            int segundoEntero = 5;
            Console.WriteLine("Comparación de dos enteros usando Equals de instancia, equals de ReferenceType y '=='");
            Console.WriteLine("P: Son iguales (Equals de instancia)? R: {0}", (primerEntero.Equals(segundoEntero)).ToString());
            Console.WriteLine("P: Son iguales (Equals de ValueType)? R: {0}", (ValueType.Equals(primerEntero, segundoEntero)).ToString());
            Console.WriteLine("P: Son iguales (==)? R: {0}", (primerEntero == segundoEntero).ToString());


            //Segunda parte del ejemplo: Comparando structs con el Equals de instancia
            Console.WriteLine("Segunda parte: comparación de Structs (son value types). Dos instancias distintas con el mismo contenido");
            MisMascotas mascotasReales = new MisMascotas();
            mascotasReales.NombreDelGato = "Wanda";
            mascotasReales.NombreDelPerro = "Barbie";

            MisMascotas mascotasImaginarias = new MisMascotas();
            mascotasImaginarias.NombreDelGato = "Wanda";
            mascotasImaginarias.NombreDelPerro = "Barbie";

            Console.WriteLine("P: Son iguales las dos instancias (utilizando Equals de instancia)? R: {0}", (mascotasReales.Equals(mascotasImaginarias)).ToString());
            Console.WriteLine("P: Son iguales las dos instancias (utilizando Equals de ValueType)? R: {0}", (ValueType.Equals(mascotasImaginarias, mascotasReales)).ToString());



            Separador.EscribirPie("Fin Ejemplo 8");
            #endregion

            //Ejemplo 9: Operator Overloading para un vector en 3D
            //EN verdad lo más importante está en la definición del struct vector, que es donde está el overload
            #region Ejemplo 9
            Separador.EscribirEncabezado("Ejemplo 9: Sobrecarga de operadores en clases");

            Vector velocidad = new Vector(1, 2, 3);
            Vector velocidadAdicional = new Vector(4, 5, 6);
            Vector velocidadFinal = velocidad + velocidadAdicional;
            Console.WriteLine("El resultado es: {0}", velocidadFinal.ToString());

            Separador.EscribirPie("Fin de ejemplo 9");
            #endregion

            //Ejemplo 9b: Este código muestra una peculiaridad del producto con respecto a la operación XOR
            //Multiplicar por cero un valor no nulo y después hacer un XOR no es lo mismo que hacer un XOR con cero
            #region Ejemplo 9b
            var result = 0;
            var result2 = 0;
            result = (result * 397) ^ 41;
            result = (result * 397) ^ 37;
            Console.WriteLine(result.ToString());
            Console.WriteLine((result2 * 397).ToString());
            #endregion

            //Ejemplo 10: Conversión con System.Convert
            //Usando System.Convert no se pierden centavos, mientras que usando una conversión explícita de float a unint sí se pierde
            #region Ejemplo 10
            float amount = 0.63f;
            uint cents = System.Convert.ToUInt16(amount * 100.0f);
            float dollars = cents / 100.0f;
            Console.WriteLine("Valor float inicial: {0}", amount.ToString());
            Console.WriteLine("Cents: {0}", cents.ToString());
            Console.WriteLine("Dollars: {0}", dollars.ToString());

            cents = (uint)(amount * 100.0f);
            dollars = cents / 100.0f;
            Console.WriteLine("Cents: {0}", cents.ToString());
            Console.WriteLine("Dollars: {0}", dollars.ToString());
            #endregion


            //Ejemplo 11: En este caso, es una definción de Caste ntre dos clases creadas por mí. 
            //Ver en Chapter7.Classes las definicones de OneMan y AnotherMan. 
            //La conversión puede definrise en cualquiera de las dos clases (origen y target) con exactamente el mismo código,
            //y funcionará de las dos maneras
            #region Ejemplo 11
            OneMan unHombre = new OneMan("Juan Carlos", "Pelotudo");
            AnotherMan otroHombre = (AnotherMan)unHombre;
            Console.WriteLine("El nombre completo es: {0}", otroHombre.FullName);
            #endregion



            //Ejemplo 12: En estos ejemplos se ve que el casteo de una clase derivada a una clase base no sirve de mucho
            //En ninguno de estos casos recupero el comportamiento de la clase base. Esto es porque el casting implícito 
            //provisto por el compilador sólo cambia el tipo de referencia pero el objeto creado en Managed Heap es el mismo
            //Por eso siempre obtengo 4 divisores sin importar los pasajes que haga entre tipo derivado y tipo base
            //El único modo de obtener dos divisores es que el tipo base sea puro
            #region Ejemplo 12
            //Primer escenario: Casteo un EnteroPar para convertirlo en un Entero. Recibo el cast en una variable Entero
            EnteroPar dos = new EnteroPar(3); //Aunque sea un contrasentido acá, sirve para los ejemplos de casteo con EnteroPrimo abajo para ver si tuvo éxito o no
            Entero uno = (Entero)dos;
            int[] divisores = uno.Divisores();
            for (int i = 0; i < divisores.Length; i++)
            {
                Console.WriteLine("Divisores[{0}]={1}", i.ToString(), divisores[i].ToString());
            }

            //Segundo escenario: Declaro con el tipo base, pero uso el contructor del tipo derivado
            //Después casteo al tipo base recibiendo el cast en una variable del tipo base. Acá tengo dos escanrios distintos
            Entero cuatro = new EnteroPar(4);
            Entero cinco = (Entero)cuatro;
            int[] DivisoresDe4 = cuatro.Divisores();
            for (int i = 0; i < DivisoresDe4.Length; i++)
            {
                Console.WriteLine("DivisoresDe4[{0}]={1}", i.ToString(), DivisoresDe4[i].ToString());
            }
            int[] DivisoresDe5 = cinco.Divisores();
            for (int i = 0; i < DivisoresDe5.Length; i++)
            {
                Console.WriteLine("DivisoresDe5[{0}]={1}", i.ToString(), DivisoresDe5[i].ToString());
            }


            //Aquí tengo un objeto puero de la clase base declarado y construido como de la clase base
            Entero entero6 = new Entero(6);
            int[] DivisoresDe6 = entero6.Divisores();
            for (int i = 0; i < DivisoresDe6.Length; i++)
            {
                Console.WriteLine("DivisoresDe6[{0}]={1}", i.ToString(), DivisoresDe6[i].ToString());
            }

            //Ahora van escenarios entre EnteroPrimo y EnteroPar
            //Tomando el mismo objeto dos creado arriba
            EnteroPrimo tres = (EnteroPrimo)dos;
            int[] divisoresDe3 = tres.Divisores();
            for (int i = 0; i < divisoresDe3.Length; i++)
            {
                Console.WriteLine("divisoresDe3[{0}]={1}", i.ToString(), divisoresDe3[i].ToString());
            }

            //Y al revés, desde un primo genero un par
            EnteroPar seis = (EnteroPar)tres;
            int[] divisoresDe6 = seis.Divisores();

            for (int i = 0; i < divisoresDe6.Length; i++)
            {
                Console.WriteLine("divisoresDe6[{0}]={1}", i.ToString(), divisoresDe6[i].ToString());
            }

            #endregion




            //int i = 5;

            //if (i is object)
            //{
            //    Console.WriteLine(@"i is an object :)");
            //}
            //else
            //{
            //    Console.WriteLine(@"i is not an object :(");
            //}

            //Console.WriteLine("Un int necesita {0} bytes", sizeof(int).ToString());
            //Console.WriteLine("Un long necesita {0} bytes", sizeof(long).ToString());

            //int? a = null;
            //Nullable<int> b = null;
            //Console.WriteLine(b.HasValue);
            //b = a;

            //int entero = 5;
            //int? enteroNulo = null;
            //int enteroFinal = enteroNulo ?? entero;
            //Console.WriteLine("El valor de enteroFinal es {0}", enteroFinal.ToString());

            //long val = 30000000000;

            //try
            //{
            //    int otroVal = checked((int)val);
            //    Console.WriteLine("El valor de otroVal es: {0}", otroVal.ToString());
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Ocurrió una excepción de tipo {0}", e.ToString());

            //}

            //Person Curly = new Person();
            //Person Shemp = Curly;
            //bool test = ReferenceEquals(Shemp, Curly);
            //Console.WriteLine(test.ToString());
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
}


