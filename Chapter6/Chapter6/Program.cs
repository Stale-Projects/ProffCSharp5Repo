using System;
using System.Collections;
using System.Collections.Generic;

namespace Chapter6
{
    class Program
    {
        static void Main(string[] args)
        {
            EncabezadoYPieConsola separador = new EncabezadoYPieConsola();

            //Ejemplo 1: Dos maneras de iterar en un array
            #region Ejemplo 1
            separador.EscribirEncabezado("Ejemplo 1: Dos maneras de iterar en un array");
            int[] myArray = { 0, 1, 2, 3 };
            for (int i = 0; i < myArray.Length; i++)
            {
                Console.WriteLine(myArray[i].ToString());

            }

            foreach (var item in myArray)
            {
                Console.WriteLine(item.ToString());
            }
            separador.EscribirPie("Fin Ejemplo 1");
            #endregion

            //Ejemplo 2: Maneras de reservar espacio en memoria para un array de User-Defined Types (UDTs)
            #region Ejemplo 2
            separador.EscribirEncabezado("Ejemplo 2: Array de UDTs");
            Person[] myPersons = new Person[2];
            //Si intento acceder a los elmentos del array antes de incializarlos tengo un error
            //El ciclo forach ejecutado aqui antes de inicializar los elementos me daría NullReferenceException

            myPersons[0] = new Person { FirstName = "Albert", LastName = "Einstein" };
            myPersons[1] = new Person { FirstName = "Erwin", LastName = "Schrödinger" };

            foreach (Person item in myPersons)
            {
                Console.WriteLine(item.ToString());
            }


            Person[] newPersons = { new Person{FirstName = "Albert", LastName = "Einstein" },
            new Person { FirstName = "Erwin", LastName="Schrödinger"}};


            foreach (Person item in newPersons)
            {
                Console.WriteLine(item.ToString());

            }
            separador.EscribirPie("Fin de ejemplo 2");
            #endregion

            //Ejemplo 3: Arrays multidimensionales rectangulares (todas las filas tienen la misma cantidad de columnas)
            #region Ejemplo 3
            separador.EscribirEncabezado("Ejemplo 3: Arrays multidimensionales recatngulares");
            int[,] twoDim = new int[,] {
                { 0, 1 },
                { 2, 3 }
            };

            for (int row = 0; row < twoDim.Length / 2; row++)
            {
                for (int col = 0; col < twoDim.Length / 2; col++)
                {
                    Console.WriteLine(twoDim[row, col].ToString());

                }

            }

            //Tambien se podría escribir
            //int[,,] threeDim;
            //int[,,,] fourDim;
            //Pero en estos casos es mejor usar CreateInstance
            separador.EscribirPie("Fin Ejemplo 3");
            #endregion

            //Ejemplo 4: Jagged Arrays (arrays donde cada fila puede tener un número de columnas diferente)
            #region Ejemplo 4
            separador.EscribirEncabezado("Ejemplo 4: Jagged Arrays");
            int[][] jagged = new int[3][];
            jagged[0] = new int[2] { 0, 1 };
            jagged[1] = new int[3] { 2, 3, 4 };
            jagged[2] = new int[2] { 5, 6 };

            for (int row = 0; row < jagged.Length; row++)
            {
                for (int col = 0; col < jagged[row].Length; col++)
                {
                    Console.Write(jagged[row][col].ToString() + " ");
                    if (col == jagged[row].Length - 1)
                    {
                        Console.Write("\n");
                    }
                }

            }
            separador.EscribirPie("Fin Ejemplo 4");
            #endregion


            //Ejemplo 5: Sort simple. Los tipos primitivos proveen un método CompareTo y llamando al método Sort se ordenan
            #region Ejemplo 5
            separador.EscribirEncabezado("Ejemplo 5: Sort simple usando CompareTo de Tipos Primitivos");
            string[] cantantes = new string[] { "Glen Hughes", "Joe Linterna", "Jon Anderson", "Ian Gillan" };

            Array.Sort(cantantes);

            foreach (var cantante in cantantes)
            {
                Console.WriteLine(cantante);
            }
            separador.EscribirPie("Fin de Ejemplo 5");
            #endregion

            //Ejemplo 6: En un array de objetos tengo que hacer que el tipo definido x el usuario
            //implemente ISortable y provea una implementación de CompareTo (ver la definición de SortablePerson
            #region Ejemplo 6
            separador.EscribirEncabezado("Ejemplo 6: Ordenamiento usando método CompareTo del UDT");
            SortablePerson[] SortablePersons = new SortablePerson[]
            {
                new SortablePerson { FirstName = "Jon", LastName="Anderson"},
                new SortablePerson { FirstName="Jon", LastName="Bon Jovi"},
                new SortablePerson { FirstName="Ian", LastName="Anderson"},
                new SortablePerson { FirstName= "Ian", LastName="Gillan"}
            };

            Array.Sort(SortablePersons);
            foreach (var sortablePerson in SortablePersons)
            {
                Console.WriteLine(sortablePerson.ToString());
            }
            separador.EscribirPie("Fin de Ejemplo 6");
            #endregion


            //Ejemplo 7: Inicializo un array usando CreateInstance. Ver que todo el código referido al array usa Object
            //tanto para setear el tipo de elemento, como para recuperarlo. Es decir, el código del array es independiente
            //del tipo que guardo en el array. GetValue devuelve un Object. CreateInstance tiene varias sobrecargas
            //para crear arrays multidimensionales y crear arrays que no son zero-based
            #region Ejemplo 7
            separador.EscribirEncabezado("Ejemplo 7: Uso de CreateInstance");

            object myObject;
            int myInt = 5;
            myObject = myInt;
            Array thisArray = Array.CreateInstance(myObject.GetType(), 5); //Si supiera el tipo pondría por ejemplo typeof(int), pero la idea aquí es que desconozco el tipo, por eso uso el GetType
            object valor;
            int entero = 12;
            valor = entero;
            thisArray.SetValue(valor, 0); //Uso de SetValue para fijar el valor. De nuevo uso un Object para recibir el valor
            entero = 69;
            valor = entero;
            thisArray.SetValue(valor, 1);

            for (int i = 0; i < thisArray.Length; i++)
            {
                valor = thisArray.GetValue(i);
                Console.WriteLine("Este es el valor: {0}", valor.ToString()); //No inicialicé los valores, pero como esta boxed, esto no me da error
            }

            separador.EscribirPie("Fin de Ejemplo 7");
            #endregion


            //Ejemplo 7b: Casteo el Array obtenido con CreateInstance a un array de enteros. A ver:
            #region Ejemplo 7b
            separador.EscribirEncabezado("Ejemplo 7b: Casteo del objeto Array");

            int[] arrayDeEnteros = (int[])thisArray;
            //A pesar de tener objetos sin inicializar, se castean al valor default del int que es cero
            for (int i = 0; i < arrayDeEnteros.Length; i++)
            {
                Console.WriteLine("Después de castearlo, valor: {0}", arrayDeEnteros[i].ToString());
            }

            separador.EscribirPie("Fin de Ejemplo 7b)");
            #endregion


            //Ejemplo 8: Arrays multidimensionales con CreateInstance
            //Primero creo un array unidimensional que especifica el número de elementos en cada coordenada. Lo llamo coordenadas
            //Es un objeto Array, creado con CreateInstance y esto es diferente a un int[]. Cuando lo pase como argumento 
            //en la segunda llamada a CreateInstance, lo tenga que castear a int[]. Es un poco excesivo de mi parte. Este array que 
            //representa las coordenadas se puede crear de tipo int directamente
            #region Ejemplo 8
            separador.EscribirEncabezado("Ejemplo 8: Arrays multidimensionales con CreateInstance");

            Array coordenadas = Array.CreateInstance(typeof(int), 2); //Creación del array de coordenadas
            coordenadas.SetValue(1, 0); //Valor del primer elemento: el array que voy a crear tendrá 1 fila
            coordenadas.SetValue(2, 1); //Valor del segundo elemento: el array que voy a crear tendrá 2 columnas
            Array otroArrayMas = Array.CreateInstance(myObject.GetType(), (int[])coordenadas); //usé myObject creado en el ejemplo 7
            //A partir de acá puedo fijar y obtener valores como antes
            otroArrayMas.SetValue(11, new int[] { 0, 0 }); //Fijo el valor del primer elemento
            //Fijate que uso un array para indicar las coordenadas
            otroArrayMas.SetValue(12, new int[] { 0, 1 });
            Console.WriteLine("Valores de otroArrayMas: {0}", otroArrayMas.GetValue(new int[] { 0, 0 }));
            Console.WriteLine("Valores de otroArrayMas: {0}", otroArrayMas.GetValue(new int[] { 0, 1 }));

            separador.EscribirPie("Fin Ejemplo 8");
            #endregion


            //Ejemplo 8b: Arrays Multidimensionales usando CreateInstance
            //Un ejemplo con un poco más de detalle
            #region Ejemplo 8.b)
            separador.EscribirEncabezado("Ejemplo 8b: CreateInstance, más detalles");

            //Array de Coordenadas (2 x 3)
            Array miArrayDeInts = Array.CreateInstance(typeof(int), new int[3] { 2, 2, 2 });
            Random miRandom = new Random(DateTime.Now.Millisecond);
            int randomInt;
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        randomInt = miRandom.Next(9);
                        miArrayDeInts.SetValue(randomInt, x, y, z);
                    }
                }

            }

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    for (int z = 0; z < 2; z++)
                    {
                        Console.WriteLine("Valor en: {0},{1},{2}: {3}", x.ToString(), y.ToString(), z.ToString(),
                            miArrayDeInts.GetValue(x, y, z).ToString());
                    }
                }

            }

            separador.EscribirPie("Fin Ejemplo 8.b");
            #endregion

            //Ejemplo 8.c) Crear un array que no es zero-based
            #region Ejemplo 8.c)
            separador.EscribirEncabezado("Ejemplo 8.c) Crear un array que no es zero-based");

            //Primero creamos un array para fijar las coordenadas
            //Este array va a tener 5 dimensiones
            int[] coordenadasArray = new int[5] { 1, 2, 3, 4, 5 };
            //Ahora creamos un array para el límite inferior de cada coordenada
            //Las coordenadas impares tienen un límite inferior de uno (1), las pares de cero (0)
            int[] limitesInferiores = new int[5] { 1, 0, 1, 0, 1 };
            //Ahora creo un array
            Array arrayEspecial = Array.CreateInstance(typeof(int), coordenadasArray, limitesInferiores);
            //Por ultimo puedo fijar los valores
            arrayEspecial.SetValue(5, new int[] { 1, 0, 1, 0, 1 });
            arrayEspecial.SetValue(6, new int[] { 1, 1, 1, 0, 1 });
            Console.WriteLine("El valor 10101 es: {0}", arrayEspecial.GetValue(new int[] { 1, 0, 1, 0, 1 }));
            Console.WriteLine("El valor 11101 es: {0}", arrayEspecial.GetValue(new int[] { 1, 1, 1, 0, 1 }));

            separador.EscribirPie("Fin Ejemplo 8.c)");
            #endregion


            //Ejemplo 9: Usando el array SortablePersons del ejemplo 6, creo una copia usando Clone, y otra usando Copy
            //Ojo que al crear el Clone hay que hacer un casting porque de lo contrario se crean de tipo Object[]
            //Luego hago lo mismo, pero usando Copy. Con respecto a Copy recordar que es un método estático
            #region Ejemplo 9
            separador.EscribirEncabezado("Ejemplo 9: Shallow Copy de Arrays");

            SortablePerson[] sortablePersonsClone = (SortablePerson[])SortablePersons.Clone();
            foreach (var unaPersona in sortablePersonsClone)
            {
                Console.WriteLine("Nombre del Clon: {0}", unaPersona.FirstName + " " + unaPersona.LastName);
            }

            SortablePerson[] sortablePersonsCopy = new SortablePerson[4];
            Array.Copy(SortablePersons, sortablePersonsCopy, 4);

            foreach (var unaPersona in sortablePersonsCopy)
            {
                Console.WriteLine("Nombre de la Copia: {0}", unaPersona.FirstName + " " + unaPersona.LastName);
            }

            separador.EscribirPie("Fin de Ejemplo 9");
            #endregion

            //Ejemplo 10: La característica de Covarianza de los Arrays puede llevar a un error en runtime
            //del cual el compilador no se percata
            //La última línea de este ejemplo da un Type Mismatch
            #region Ejemplo 10
            object[] objectArray;
            SortablePerson[] PersonArrayTest = new SortablePerson[3];
            objectArray = PersonArrayTest;
            objectArray[0] = new SortablePerson();
            //La siguiente línea daría un error
            //objectArray[1] = "Una string de prueba";
            #endregion

            //Ejemplo 11: Segmentos de Arrays. En este caso creo un segmento pasando dos segmentos
            //uno de cada array de enteros creado al principio
            //Es decir, el segmento está creado por la unión de dos segmentos
            //Esto me muestra el constructor que supongo es más habitual usar: 
            //new ArraySegment<int>(arrayOriginal, offset, count)
            //En este ejemplo, se crea un array de segmentos de array
            //new ArraySegment<int>[]{new Arraysegment<int>(enterosPares, 1, 2), 
            //new Arraysegment<int>(enterosImpares, 1, 2)}
            #region Ejemplo 11
            separador.EscribirEncabezado("Ejemplo 11: Segmentos de Arrays");

            int[] enterosImpares = { 1, 3, 5, 7, 9 };
            int[] enterosPares = { 0, 2, 4, 6, 8 };
            var segmentoDeEnteros = new ArraySegment<int>[]
                {new ArraySegment<int> (enterosImpares, 1, 2),
                 new ArraySegment<int>(enterosPares, 1, 2)};

            Console.WriteLine("La manera siguiente de enumerar los elementos del segmento de array no funciona");
            for (int i = 0; i < segmentoDeEnteros.Length; i++)
            {
                Console.WriteLine("Valor de Elemento {0}: {1}", i.ToString(), segmentoDeEnteros[i].ToString());
            }
            Console.WriteLine("Esta es la correcta:");

            int segmentOffset;
            int segmentCount;

            segmentOffset = segmentoDeEnteros[0].Offset;
            segmentCount = segmentoDeEnteros[0].Count;
            for (int i = segmentOffset; i < segmentOffset + segmentCount; i++)
            {
                Console.WriteLine("Este es el valor del elemento {0} del primer segmento: {1}", i.ToString(), segmentoDeEnteros[0].Array[i].ToString());
            }


            segmentOffset = segmentoDeEnteros[1].Offset;
            segmentCount = segmentoDeEnteros[1].Count;
            for (int i = segmentOffset; i < segmentOffset + segmentCount; i++)
            {
                Console.WriteLine("Este es el valor del elemento {0} del segundo segmento: {1}", i.ToString(), segmentoDeEnteros[1].Array[i].ToString());
            }


            separador.EscribirPie("Fin Ejemplo 11");
            #endregion

            //Ejemplo 12: Comparaciones
            //Para comparar dos arrays lo mejor es usar IStructuralEquatable 
            //que me permite comparar dos arrays en secuencia. Es decir, la secuencia de elementos en el array tiene
            //que ser la misma. Si están desordenados, aunque los valores sean iguales, la comparación indica que son distintos
            //Con las tuplas puedo usar Equals también, lo cual con arrays no funciona porque compara por referencia
            //De todos modos, usar siempre IStructuralEquatable
            #region Ejemplo 12
            separador.EscribirEncabezado("Ejemplo 12: Comparaciones de Arrays");

            int[] enterosComparacion = { 1, 3, 5, 7, 9 };
            int[] enterosComparacionInvertida = { 9, 7, 5, 3, 1 };

            Console.WriteLine("Comparación usando: StructuralComparisons.StructuralEqualityComparer.Equals(arg1, arg2)");
            Console.WriteLine("Comparación entre enterosImpares y enterosComparacion: {0}",
                StructuralComparisons.StructuralEqualityComparer.Equals(enterosImpares, enterosComparacion));

            Console.WriteLine("Comparación entre enterosPares y enterosComparacion: {0}",
                StructuralComparisons.StructuralEqualityComparer.Equals(enterosPares, enterosComparacion));

            Console.WriteLine("Comparación entre enterosPares y enterosComparacionInvertida: {0}",
                StructuralComparisons.StructuralEqualityComparer.Equals(enterosPares, enterosComparacionInvertida));

            //Otro modo de comparar los arays de enteros
            Console.WriteLine("Otro modo de comparar: enterosImpares as IStructuralEquatable).Equals(enterosComparacion, EqualityComparer<int>.Default");
            if ((enterosImpares as IStructuralEquatable).Equals(enterosComparacion, EqualityComparer<int>.Default))
                Console.WriteLine("Siguen siendo iguales: enterosImpares y EnterosComparacion");
            else
                Console.WriteLine("enterosImpares y EneterosComparacion no son iguales!?");

            //Ahora hago un sort de los arrays enterosImpares y enterosComparacionInvertida para que me den iguales
            //Si no quiero alterar los arrays, debería implementar un método que los clone a ambos y que los ordene
            //y comparo los elementos clonados
            Array.Sort<int>(enterosComparacion);
            Array.Sort<int>(enterosComparacionInvertida);

            Console.WriteLine("Comparación entre enterosImpares y enterosComparacionInvertida luego de hacer un Sort: {0}",
                StructuralComparisons.StructuralEqualityComparer.Equals(enterosImpares, enterosComparacionInvertida));


            //Comparación de Tuplas

            var t1 = Tuple.Create<int, string>(1, "tonto");
            var t2 = Tuple.Create<int, string>(1, "tonto");
            var t3 = Tuple.Create<string, int>("tonto", 1);

            Console.WriteLine("Comparación entre t1 y t2: {0}",
                StructuralComparisons.StructuralEqualityComparer.Equals(t1, t2));

            Console.WriteLine("Comparación entre t1 y t3: {0}", StructuralComparisons.StructuralEqualityComparer.Equals(t1, t3));


            //Comparación de Referencias. Esto da True
            int unEntero = 5;
            object unObjeto = unEntero;
            object otroObjeto = unEntero;
            Console.WriteLine(unObjeto.Equals(otroObjeto));

            separador.EscribirPie("Fin Ejemplo 12");
            #endregion


            //Ejemplo 13: Enumeración y Enumeración inversa
            //La manera sencilla de enumerar Arrays es con foreach
            //foreach no permite enumerar en forma inversa, para eso tengo que crear mi propia clase
            #region Ejemplo 13
            separador.EscribirEncabezado("Ejemplo 13; Enumeraciones");

            //Primero definimos un array con números random del 0 al 100
            int[] arrayAleatorio = new int[25];
            Random asignador = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < arrayAleatorio.Length; i++)
            {
                arrayAleatorio[i] = asignador.Next(100);
            }


            //Ahora enumeramos
            //Primero en forma directa
            Console.WriteLine("Enumeración directa");
            foreach (var enteroAleatorio in arrayAleatorio)
            {
                Console.WriteLine(enteroAleatorio.ToString());
            }



            separador.EscribirPie("Fin Ejemplo 13");
            #endregion


            //Ejemplo 14: Enumeradores personalizados
            //Se muestra el uso de Yield y la definición de enumeradores personalizados
            #region Ejemplo 14
            separador.EscribirEncabezado("Ejemplo 14: Enumeradores personalizados");

            SerieDeEnteros miSerie = new SerieDeEnteros(50, 10);
            Console.WriteLine("Enumeración Directa:");
            string linea = "";
            foreach (var miEntero in miSerie)
            {
                linea += miEntero.ToString() + ", ";
            }
            Console.WriteLine(linea);
            Console.WriteLine("Enumeración inversa:");
            linea = "";
            foreach (var miEntero in miSerie.EnumeracionReversa())
            {
                linea += miEntero.ToString() + ", ";
            }
            Console.WriteLine(linea);

            Console.WriteLine("Enumerar Pares");
            linea = "";
            foreach (var miEnteroPar in miSerie.EnumerarPares())
            {
                linea += miEnteroPar.ToString() + ", ";
            }
            Console.WriteLine(linea);

            separador.EscribirPie("Fin de Ejemplo 14");
            #endregion


            //Ejemplo 15: Comparo dos arrays de UDTs usando la clase PersonasComparables
            //que está mal nombrada, debería haber sido en singlular
            #region Ejemplo 15
            separador.EscribirEncabezado("Ejemplo 15: Comparación estructural de Arrays de UDTs");

            Console.WriteLine("Comparamos dos arrays de UDTs estructuralmente");
            var jlb = new PersonasComparables { Apellido = "Borges", Nombre = "Jorge Luis" };

            PersonasComparables[] unasPersonas =
            {
                new PersonasComparables {Apellido = "Asimov", Nombre = "Isaac" },
                jlb
            };

            PersonasComparables[] otrasPersonas =
            {
                jlb,
                new PersonasComparables { Nombre = "Isaac", Apellido="Asimov"}
            };

            Console.WriteLine("Primero comparo con un simple '=='. Esto compara referencias, debe dar distinto");
            if (unasPersonas == otrasPersonas)
            {
                Console.WriteLine("Las referencias son iguales! (WTF)");
            }
            else
            {
                Console.WriteLine("Las referencias son distintas, como cabía esperar");
            }

            Console.WriteLine("Ahora usamos una comparación con el código de Stack Overflow");
            Console.WriteLine("Recorda que va using System.Cllections");

            bool sonIguales = StructuralComparisons.StructuralEqualityComparer.Equals(unasPersonas, otrasPersonas);

            Console.WriteLine("Son iguales? Respuesta: {0}", sonIguales.ToString());
            //Recordemos que hay que ordenarlos para compararlos
            Array.Sort<PersonasComparables>(unasPersonas);
            Array.Sort<PersonasComparables>(otrasPersonas);
            sonIguales = StructuralComparisons.StructuralEqualityComparer.Equals(unasPersonas, otrasPersonas);
            Console.WriteLine("Comparación luego de ordenar. Son iguales? Respuesta: {0}", sonIguales.ToString());

            //Ahora usamos la comparación del libro
            sonIguales = false;
            sonIguales = (unasPersonas as IStructuralEquatable).Equals(otrasPersonas as IStructuralEquatable,
                EqualityComparer<PersonasComparables>.Default);
            Console.WriteLine("Comparación según el libro. Son iguales? Respuesta: {0}", sonIguales.ToString());


            Console.WriteLine("Este es el primero: {0}", unasPersonas[0].ToString());
            Console.WriteLine("Este es el segundo: {0}", unasPersonas[1].ToString());


            separador.EscribirPie("Fin Ejemplo 15");
            #endregion


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
