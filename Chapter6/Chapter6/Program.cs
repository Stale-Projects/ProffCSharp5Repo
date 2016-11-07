using System;
using System.Collections;
using System.Collections.Generic;

namespace Chapter6
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ejemplo 1: Dos maneras de iterar en un array
            #region Ejemplo 1
            int[] myArray = { 0, 1, 2, 3 };
            for (int i = 0; i < myArray.Length; i++)
            {
                Console.WriteLine(myArray[i].ToString());

            }

            foreach (var item in myArray)
            {
                Console.WriteLine(item.ToString());
            }
            #endregion

            //Ejemplo 2: Maneras de reservar espacio en memoria para un array de User-Defined Types (UDTs)
            #region Ejemplo 2
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

            #endregion

            //Ejemplo 3: Arrays multidimensionales rectangulares (todas las filas tienen la misma cantidad de columnas)
            #region Ejemplo 3
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
            #endregion

            //Ejemplo 4: Jagged Arrays (arrays donde no todas las filas tienen la misma cantidad de columnas)
            #region Ejemplo 4
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
            #endregion


            //Ejemplo 5: Sort simple. Los tipos proveen un método CompareTo y llamando al método Sort se ordenan
            #region Ejemplo 5
            string[] cantantes = new string[] { "Glen Hughes", "Joe Linterna", "Jon Anderson", "Ian Gillan" };

            Array.Sort(cantantes);

            foreach (var cantante in cantantes)
            {
                Console.WriteLine(cantante);
            }
            #endregion

            //Ejemplo 6: En un array de objetos tengo que hacer que el tipo definido x el usuario
            //implemente ISortable y provea una implementación de CompareTo (ver la definición de SortablePerson
            #region Ejemplo 6
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
            #endregion


            //Ejemplo 7: Inicializo un array usando CreateInstance. Ver que todo el código referido al array usa Object
            //tanto para setear el tipo de elemento, como para recuperarlo. Es decir, el código del array es independiente
            //del tipo que guardo en el array. GetValue devuelve un Object. CreateInstance tiene varias sobrecargas
            //para crear arrays multidimensionales y crear arrays que no son zero-based
            #region Ejemplo 7
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
            #endregion


            //Ejemplo 7b: Casteo el array a un array de enteros. A ver:
            #region Eejemplo 7b
            int[] arrayDeEnteros = (int[])thisArray;
            //A pesar de tener objetos sin inicializar, se castean al valor default del int que es cero
            for (int i = 0; i < arrayDeEnteros.Length; i++)
            {
                Console.WriteLine("Después de castearlo, valor: {0}", arrayDeEnteros[i].ToString());
            }
            #endregion


            //Ejemplo 8: Arrays multidimensionales con CreateInstance
            //Primero creo un array que especifica el número de elementos en cada coordenada. Lo llamo coordenadas
            //Ver que es un array de objetos, creado con CreateInstance. Eso hace que cuando lo use en CreateInstance del 
            //array que quiero crear, lo tenga que castear a int[]. Es un poco excesivo de mi parte. Este array que 
            //representa las coordenadas se puede crear de tipo int directamente
            #region Ejemplo 8
            Array coordenadas = Array.CreateInstance(typeof(int), 2);
            coordenadas.SetValue(1, 0); //Tiene una fila
            coordenadas.SetValue(2, 1); //Tiene dos columnas
            Array otroArrayMas = Array.CreateInstance(myObject.GetType(), (int[])coordenadas); //usé myObject creado en el ejemplo 7
            //A partir de acá puedo fijar y obtener valores como antes
            otroArrayMas.SetValue(11, new int[] { 0, 0 }); //Fijo el valor del primer elemento
            //Fijate que uso un array para indicar las coordenadas
            otroArrayMas.SetValue(12, new int[] { 0, 1 });
            Console.WriteLine("Valores de otroArrayMas: {0}", otroArrayMas.GetValue(new int[] { 0, 0 }));
            Console.WriteLine("Valores de otroArrayMas: {0}", otroArrayMas.GetValue(new int[] { 0, 1 }));
            #endregion


            //Ejemplo 9: Usando SortablePersons del ejemplo 6, creo una copia usando Clone, y otra usando Copy
            //Ojo que al crear el Clone hay que hacer un casting porque de lo contrario se crean de tipo Object[]
            //Con respecto a Copy recordar que es un método estático
            #region Ejemplo 9
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

            #endregion

            //Ejemplo 10: La característica de Covarianza de los Arrays puede llevar a un error en runtime
            //del cual el compilador no se percata
            //La última línea de este ejemplo da un Type Mismatch
            #region Ejemplo 10
            object[] objectArray;
            SortablePerson[] PersonArrayTest = new SortablePerson[3];
            objectArray = PersonArrayTest;
            objectArray[0] = new SortablePerson();
            //objectArray[1] = "Una string de prueba";
            #endregion

            //Ejemplo 11: Segmentos de Arrays. En este caso creo un segmento pasando dos segmentos
            //uno de cada array de enteros creado al principio
            //Es decir, el segmento está creado por la unión de dos segmentos
            //Esto me muestra lso dos constructores que tengo: 
            //new ArraySegment<int>[2]
            //new ArraySegment<int>[}{new Arraysegment<int>(enterosPares, 1, 2)}
            #region Ejemplo 11
            int[] enterosImpares = { 1, 3, 5, 7, 9 };
            int[] enterosPares = { 0, 2, 4, 6, 8 };
            var segmentoDeEnteros = new ArraySegment<int>[]
                {new ArraySegment<int> (enterosImpares,1,2) ,
                 new ArraySegment<int>(enterosPares, 1, 2)};
            #endregion

            //Ejemplo 12: Comparaciones
            //Para comparar dos arrays lo mejor es usar IStructuralEquatable 
            //que me permite comparar dos arrays en secuencia. Es decir, la secuencia de elementos en el array tiene
            //que ser la misma. Pero no compara elementos en desorden
            //Con las tuplas puedo usar Equals también, lo cual con arrays no funciona porque compara por referencia
            //De todos modos, usar siempre IStructuralEquatable
            #region Ejemplo 12
            int[] enterosComparacion = { 1, 3, 5, 7, 9 };
            int[] enterosComparacionInvertida = { 9, 7, 5, 3, 1 };

            Console.WriteLine("Comparación entre enterosImpares y enterosComparacion: {0}",
                StructuralComparisons.StructuralEqualityComparer.Equals(enterosImpares, enterosComparacion));

            Console.WriteLine("Comparación entre enterosPares y enterosComparacion: {0}",
                StructuralComparisons.StructuralEqualityComparer.Equals(enterosPares, enterosComparacion));

            Console.WriteLine("Comparación entre enterosPares y enterosComparacionInvertida: {0}",
                StructuralComparisons.StructuralEqualityComparer.Equals(enterosPares, enterosComparacionInvertida));

            //Otro modo de comparar los arays de enteros
            if ((enterosImpares as IStructuralEquatable).Equals(enterosComparacion, EqualityComparer<int>.Default))
                Console.WriteLine("Ahora sí son iguales enterosImpares y EnterosComparacion");
            else
                Console.WriteLine("enterosImpares y EneterosOcmparacion no son iguales!?");

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


            //Comparación de Referencias. Esto da True
            int unEntero = 5;
            object unObjeto = unEntero;
            object otroObjeto = unEntero;
            Console.WriteLine(unObjeto.Equals(otroObjeto));

            #endregion

        }
    }
}
