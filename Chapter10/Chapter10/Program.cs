using System;
using System.Collections.Generic;
using System.Linq;

namespace Chapter10
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Ejemplo 1: Instanciación de List con constructor y agregado manual de elementos
            //Primer modo de declarar
            //Ejemplo de Capacity y Count
            List<Guitarist> guitarristasDePunk = new List<Guitarist>();
            Console.WriteLine("guitarristasDePunk: Capacity: {0}, Count: {1}", guitarristasDePunk.Capacity.ToString(), guitarristasDePunk.Count.ToString());
            Guitarist david = new Guitarist("David", "Jones", "Gibson Les Paul", 40);
            guitarristasDePunk.Add(david);
            Console.WriteLine("guitarristasDePunk: Capacity: {0}, Count: {1}", guitarristasDePunk.Capacity.ToString(), guitarristasDePunk.Count.ToString());
            #endregion


            #region  Ejemplo 2: Instanciación en base a algunos objetos conocidos
            //Otro modo de declarar en base a algunos objetos conocidos
            Guitarist ritchie = new Guitarist("Ritchie", "Blackmore", "Fender Stratocaster", 100);
            Guitarist jimmy = new Guitarist("Jimmy", "Page", "Gibson Les Paul", 99);
            List<Guitarist> guitarristasDeRock = new List<Guitarist>(10) { ritchie, jimmy };
            //Ahora agrego 1
            Guitarist eddie = new Guitarist("Eddie", "Van Halen", "Frankenstrat", 85);
            guitarristasDeRock.Add(eddie);

            Guitarist jimi = new Guitarist("James Marshall", "Hendrix", "Fender Stratocaster", 100);
            guitarristasDeRock.Add(jimi);
            Console.WriteLine("guitarristasDeRock: Capacity: {0}, Count: {1}", guitarristasDeRock.Capacity.ToString(), guitarristasDeRock.Count.ToString());
            #endregion


            #region Ejemplo 3: Instanciación en base a objetos declarados dentro del llamado al contructor
            //Otro modo más, declarando los objetos sobre el pucho
            List<Guitarist> guitarristasDeFolklore = new List<Guitarist>
            {
                new Guitarist("Jose", "Larralde", "N/N", 100),
                new Guitarist("Eduardo", "Falu", "N/N", 71)
            };
            Console.WriteLine("guitarristasDeFolklore: Capacity: {0}, Count: {1}", guitarristasDeFolklore.Capacity.ToString(), guitarristasDeFolklore.Count.ToString());

            foreach (Guitarist guitarrista in guitarristasDeRock)
            {
                Console.WriteLine(guitarrista.ToString());
            }
            #endregion


            #region Ejemplo 4: Insertar elementos en una posición definida y uso de foreach
            //Tener en cuenta, al proporcionar el index que son zero-based
            //En el código que sigue inserto para que el nuevo sea el primer elemento
            guitarristasDePunk.Insert(0, new Guitarist("Kevin John", "Wasserman", "Ibanez", 35));
            Console.WriteLine("Luego de insertar en Guitarristas de Punk");
            foreach (var guitarrista in guitarristasDePunk)
            {
                Console.WriteLine(guitarrista.ToString());
            }

            #endregion


            #region Ejemplo 5: Acceder a un elemento en particular y borrar un elemento
            //Se accede como si fuera un array
            Console.WriteLine("Acceder al primer elemento antes de borrar");
            Console.WriteLine(guitarristasDePunk[0].ToString());
            //Borro
            guitarristasDePunk.RemoveAt(0);
            Console.WriteLine("Acceder al primer elemento después de borrar");
            Console.WriteLine(guitarristasDePunk[0].ToString());

            //Borrar por nombre del elemento
            Guitarist borrarGuitarrista = new Guitarist("David", "Jones", "Gibson Les Paul", 40);
            guitarristasDePunk.Remove(borrarGuitarrista);
            Console.WriteLine("Ahora guitarristasDePunk tiene {0} elementos después de borrar a Jones a partir de un objeto creado igual al que quiero borrar", guitarristasDePunk.Count.ToString());
            guitarristasDePunk.Remove(david);
            Console.WriteLine("Ahora guitarristasDePunk tiene {0} elementos después de borrar a Jones con la referencia", guitarristasDePunk.Count.ToString());
            #endregion


            #region Ejemplo 6: Busco un elemento
            //Primero busco usando un Predicate
            int index = guitarristasDeRock.FindIndex(new SelectScore(80).SelectScorePredicate);
            //Ahora busco usando una Lambda
            int index2 = guitarristasDeRock.FindIndex(g => g.Score > 80);
            Console.WriteLine("Este guitarrista encontré con Predicates: {0}", guitarristasDeRock[index].ToString());
            Console.WriteLine("Este guitarrista encontré con Lambda: {0}", guitarristasDeRock[index2].ToString());
            #endregion

            #region Ejemplo 6b: Busco un guitarrista específico y lo borro
            //Ahora busco un guitarrista y lo borro (ejemplo más realista)
            index = -1;

            index = guitarristasDeRock.FindIndex(0, new SelectNameAndGuitar("Ritchie", "Blackmore", "Fender Stratocaster").SelectNameAndGuitarPredicate);
            if (index != -1)
            {
                guitarristasDeRock.RemoveAt(index);
                Console.WriteLine("Quité el guitarrista de la posición {0}", index.ToString());
            }
            foreach (Guitarist guitarristaEvaluado in guitarristasDeRock)
            {
                Console.WriteLine("Este guitarrista sobrevivió: {0}", guitarristaEvaluado.ToString());
            }
            #endregion

            #region Ejemplo 7: Objetos nulos
            //Este ejemplo muestra los errorres que se producen cuando hay al menos un objeto de la colección que es nulo
            //Descomentar las dos lineas siguientes
            //Guitarist GuitarristaNulo = null;
            //guitarristasDeRock.Add(GuitarristaNulo);
            //Descomentar alguna de las siguientes para ver los errores
            //int index3 = guitarristasDeRock.FindIndex(new SelectScore(500).SelectScorePredicate);
            //int index4 = guitarristasDeRock.FindIndex(g => g.Score > 500);
            #endregion

            #region Ejemplo 8: Busco todos los elementos con una determinada condicion

            //Primero usando un predicate
            List<Guitarist> guitarristasConScoreNoNulo = guitarristasDeRock.FindAll(new SelectScore(0).SelectScorePredicate);
            foreach (Guitarist guitarristaEvaluado in guitarristasConScoreNoNulo)
            {
                Console.WriteLine("Este guitarrista tiene score no nulo: {0}", guitarristaEvaluado.ToString());
            }

            //Ahora con una Lambda
            guitarristasConScoreNoNulo = guitarristasDeRock.FindAll(g => g.Score > 0);

            #endregion

            #region Ejemplo 9: Borro de una lista todos los elementos que están en otra lista
            //Comienzo creando un HashSet de elementos
            var GuitarristasQueNoEstan = new HashSet<Guitarist>()
            {
                jimi

            };

            //Ahora borro
            guitarristasDeRock.RemoveAll(g => GuitarristasQueNoEstan.Contains(g));
            foreach (Guitarist gui in guitarristasDeRock)
            {
                Console.WriteLine(gui.ToString());
            }

            //Importante: Esto solo funciona si la lista a GuitarristasQueNoEstan se creó a partir de un subconjunto formado por LOS MISMOS OBJETOS
            //que los que están en la lista GuitarristasDeRock. Es decir, si hubiera creado un nuevo objeto Guitarist usando los mismos valores de FirstName, LastName, etc.
            //no se hubiera borrado nada. Ver que la creé usando un objeto pre-existente
            //Para borrar por valor, tendría que definir lo qe es igualdad en el objeto Guitarist (sobrecarga del operador ==)

            #endregion



            #region Ejemplo 10: Crear una nueva lista a partir de otra pre-existente con una condición
            //Para que esto funcione hay que agregar la referencia a System.Linq
            var GuitarristasQueTocanLesPaul = guitarristasDeRock.Where(g => g.Guitar == "Gibson Les Paul").ToList();
            Console.WriteLine("Ejemplo 10: Guitarristas que tocan Les Paul");
            foreach (Guitarist guit in GuitarristasQueTocanLesPaul)
                Console.WriteLine(guit.ToString());
            Console.WriteLine("==================================");
            #endregion


            #region Ejemplo11: Primera sobrecarga de Sort
            Guitarist Ace = new Guitarist("Ace", "Frehley", "Gibson Les Paul", 80);
            Guitarist Martin = new Guitarist("Martin", "Barre", "Gibson Les Paul", 100);
            Guitarist Alex = new Guitarist("Alex", "Lifeson", "Gibson Les Paul", 100);
            guitarristasDeRock.Add(Ace);
            guitarristasDeRock.Add(Martin);
            guitarristasDeRock.Add(Alex);

            Console.WriteLine("========Ejemplo 11: Orden usando Sort sin parámetros=========");
            Console.WriteLine("Antes de ordenar");
            ImprimirLista(guitarristasDeRock);
            guitarristasDeRock.Sort();
            Console.WriteLine("Después de ordenar");
            ImprimirLista(guitarristasDeRock);
            Console.WriteLine("--------------Fin de Ejemplo 11-------------");
            #endregion

            #region Ejemplo 12: Segunda Sobrecarga de Sort
            Console.WriteLine("==========Ejemplo 12: Sort segunda sobrecarga");
            Console.WriteLine("Antes de ordenar");
            ImprimirLista(guitarristasDeRock);
            guitarristasDeRock.Sort(new GuitaristComparer());
            Console.WriteLine("Después de ordenar");
            ImprimirLista(guitarristasDeRock);
            Console.WriteLine("--------------Fin de Ejemplo 12-------------");

            #endregion


            #region Ejemplo 13: Cuarta Sobrecarga de Sort
            Console.WriteLine("==========Ejemplo 13: Sort cuarta sobrecarga");
            Console.WriteLine("Antes de ordenar");
            //En este caso proporciono una Lambda para ordenar y luego uso el método Reverse para dar orden inverso
            //Ver que usé el método CompareTo del tipo int32 para proporcionar el valor que espera Sort: un entero que indica el 
            //resultado de la comparación
            ImprimirLista(guitarristasDeRock);
            guitarristasDeRock.Sort((x, y) => x.Score.CompareTo(y.Score));
            guitarristasDeRock.Reverse();
            Console.WriteLine("Después de ordenar");
            ImprimirLista(guitarristasDeRock);
            Console.WriteLine("--------------Fin de Ejemplo 13-------------");
            #endregion







        }

        private static void ImprimirLista(List<Guitarist> lista)
        {
            foreach (var item in lista)
                Console.WriteLine(item.ToString());
        }
    }
}
