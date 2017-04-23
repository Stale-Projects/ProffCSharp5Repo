using System;
using System.Collections.Generic;
using System.Linq;

namespace Chapter10
{
    class Program
    {
        static void Main(string[] args)
        {
            //Para escribir las separaciones
            EncabezadoYPieConsola separador = new EncabezadoYPieConsola();

            #region Ejemplo 0: Incrementos de Capacity
            //En este ejemplo se ven las diferencias de comportamiento entre dos colecciones declaradas de distinto modo en cuanto a su capacity
            //El compilador trata de buscar un equilibrio entre la mínima cantidad de re-definiciones de capacity con el mínimo exceso de capacity
            separador.EscribirEncabezado("Ejemplo 0: Cambios de Capacity");

            List<int> mvEnteros = new List<int>(10);
            for (int i = 0; i < 21; i++)
            {
                mvEnteros.Add(i);
                Console.WriteLine("Agregado el elemeto: {0}. Ahora la capacity es: {1}", (i + 1).ToString(), mvEnteros.Capacity.ToString());
            }

            List<int> mvEnterosIneficientes = new List<int>();
            for (int i = 0; i < 21; i++)
            {
                mvEnterosIneficientes.Add(i);
                Console.WriteLine("Agregado el elemeto: {0}. Ahora la capacity es: {1}", (i + 1).ToString(), mvEnterosIneficientes.Capacity.ToString());
            }

            separador.EscribirPie("Fin de Ejemplo 0");
            #endregion

            #region Ejemplo 1: Instanciación de List con constructor y agregado manual de elementos
            //Primer modo de declarar
            //Ejemplo de Capacity y Count
            separador.EscribirEncabezado("Ejemplo 1: Instanciar List con constructor y agregar elementos manualmente");
            List<Guitarist> guitarristasDePunk = new List<Guitarist>();
            Console.WriteLine("guitarristasDePunk: Capacity: {0}, Count: {1}", guitarristasDePunk.Capacity.ToString(), guitarristasDePunk.Count.ToString());
            Guitarist david = new Guitarist("David", "Jones", "Gibson Les Paul", 40);
            guitarristasDePunk.Add(david);
            Console.WriteLine("Antes de TrimExcess - guitarristasDePunk: Capacity: {0}, Count: {1}", guitarristasDePunk.Capacity.ToString(), guitarristasDePunk.Count.ToString());
            guitarristasDePunk.TrimExcess();
            Console.WriteLine("Después de TrimExcess - guitarristasDePunk: Capacity: {0}, Count: {1}", guitarristasDePunk.Capacity.ToString(), guitarristasDePunk.Count.ToString());

            separador.EscribirPie("Fin Ejemplo 1");
            #endregion

            #region  Ejemplo 2: Instanciación en base a algunos objetos conocidos
            separador.EscribirEncabezado("Ejemplo 2: Instanciación en base a algunos objetos conocidos");

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

            separador.EscribirPie("Fin ejemplo 2");
            #endregion

            #region Ejemplo 3: Instanciación en base a objetos declarados dentro del llamado al contructor
            separador.EscribirEncabezado("Ejemplo 3: Instanciación en base a objetos anónimos");

            //Otro modo más, declarando los objetos sobre el pucho
            List<Guitarist> guitarristasDeFolklore = new List<Guitarist>
            {
                new Guitarist("Jose", "Larralde", "N/N", 100),
                new Guitarist("Eduardo", "Falu", "N/N", 71)
            };
            Console.WriteLine("guitarristasDeFolklore: Capacity: {0}, Count: {1}", guitarristasDeFolklore.Capacity.ToString(), guitarristasDeFolklore.Count.ToString());

            foreach (Guitarist guitarrista in guitarristasDeFolklore)
            {
                Console.WriteLine(guitarrista.ToString());
            }

            separador.EscribirPie("Fin Ejemplo 3");
            #endregion

            #region Ejemplo 4a: Agregar e insertar elementos 
            separador.EscribirEncabezado("Ejemplo 4a: Agregar e insertar elementos");
            //Primero agrego simplemente unos objetos anónimos o ya instanciados
            guitarristasDeRock.Add(new Guitarist("Norberto", "Nappolitano", "Gibson Flying V", 110));
            Guitarist alex = new Guitarist("Alex", "Lifeson", "Gibson Les Paul custom", 77);
            guitarristasDeRock.Add(alex);

            //Agregar múltiples elementos con AddRange()
            guitarristasDeRock.AddRange(new Guitarist[] {
            new Guitarist("Jack", "White", "Gretsch 6130", 25),
            new Guitarist("Jack", "Black", "Gibson SG", 25) });


            //Tener en cuenta, al proporcionar el index que son zero-based
            //En el código que sigue inserto para que el nuevo sea el primer elemento
            guitarristasDePunk.Insert(0, new Guitarist("Kevin John", "Wasserman", "Ibanez", 35));
            Console.WriteLine("Luego de insertar en Guitarristas de Punk");
            foreach (var guitarrista in guitarristasDePunk)
            {
                Console.WriteLine(guitarrista.ToString());
            }

            separador.EscribirPie("Fin Ejemplo 4a");
            #endregion

            #region Ejemplo 4b: Acceder a elementos 
            separador.EscribirEncabezado("Ejemplo 4c: Acceder a elementos");

            //Acceder de a un elemento por vez usando el índice
            Console.WriteLine("Enumeración en un loop for");
            for (int i = 0; i < guitarristasDeRock.Count; i++)
            {
                Console.WriteLine("Nombre completo: {0} - Guitarra Preferida: {1}",
                    guitarristasDeRock[i].FirstName + " " + guitarristasDeRock[i].LastName,
                    guitarristasDeRock[i].Guitar);
            }

            //Acceder a los elementos en forma secuencial con foreach()
            Console.WriteLine("Enumeración con foreach: un poco más conciso");
            foreach (var unG in guitarristasDeRock)
            {
                Console.WriteLine("Nombre completo: {0} - Guitarra Preferida: {1}",
                    unG.FirstName + " " + unG.LastName, unG.Guitar);

            }

            //Uso de ForEach
            Console.WriteLine("Mucho más económico: ForEach");
            guitarristasDeRock.ForEach(Guitarist.ImprimirDatos);

            //Un poco más elegante
            Console.WriteLine("Económico y elegante? Acá va:");
            guitarristasDeRock.ForEach(g => Console.WriteLine("{0:G}", g));


            separador.EscribirPie("Fin ejemplo 4c");
            #endregion

            #region Ejemplo #5.a Buscar elementos en listas simples
            separador.EscribirEncabezado("Ejemplo #5.a: Búsquedas en listas simples");

            List<int> enterosPrimos = new List<int>(new int[] {
                2,3,5,7,11,13,17,19,23,29,23,19,17,13,11,7,5,3,2,19 });

            int indiceDe19 = enterosPrimos.IndexOf(19);
            Console.WriteLine("Encontré a 19 en la posición {0}", indiceDe19);
            indiceDe19 = enterosPrimos.IndexOf(19, indiceDe19 + 1);
            Console.WriteLine("La segunda instancia de 19 está en la posición {0}", indiceDe19);
            indiceDe19 = enterosPrimos.LastIndexOf(19);
            Console.WriteLine("La última instancia de 19 está en la posición {0}", indiceDe19);
            indiceDe19 = enterosPrimos.LastIndexOf(19, 12);
            Console.WriteLine("La última instancia de 19 en el subgrupo de los 13 primeros elementos está en la posición {0}", indiceDe19);

            Guitarrista ricardoSoule = new Guitarrista("Ricardo", "Soule", "Gibson SG", 20);
            object obj = ricardoSoule;
            Console.WriteLine("Comparacion ricardoSoule.Equals(obj): {0}", ricardoSoule.Equals(obj));


            int diecinueve = enterosPrimos.Find(x => x - 19 == 0);
            Console.WriteLine("Esto encontré con Find: {0}", diecinueve);


            separador.EscribirPie("Fin de ejemplo 5.a");
            #endregion

            #region Ejemplo #5: Borrar Elementos de a uno
            separador.EscribirEncabezado("Ejemplo 5: Borrar Elementos de a uno");

            Console.WriteLine("Acceder al primer elemento antes de borrar");
            Console.WriteLine(guitarristasDePunk[0].ToString());
            //Borro
            guitarristasDePunk.RemoveAt(0);
            Console.WriteLine("Acceder al primer elemento después de borrar");
            Console.WriteLine(guitarristasDePunk[0].ToString());

            //Borrar por nombre del elemento
            //Guitarist borrarGuitarrista = new Guitarist("David", "Jones", "Gibson Les Paul", 40);
            var borrarGuitarrista = guitarristasDePunk.FindLast(g => g.FirstName == "David" && g.LastName == "Jones");
            if (borrarGuitarrista != null) guitarristasDePunk.Remove(borrarGuitarrista);

            Console.WriteLine("Ahora guitarristasDePunk tiene {0} elementos después de borrar a Jones a partir de un objeto creado igual al que quiero borrar", guitarristasDePunk.Count.ToString());
            guitarristasDePunk.Remove(david);
            Console.WriteLine("Ahora guitarristasDePunk tiene {0} elementos después de borrar a Jones con la referencia", guitarristasDePunk.Count.ToString());

            separador.EscribirPie("Fin Ejemplo 5");
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
            separador.EscribirEncabezado("Ejemplo 8: Busqueda de elementos");
            //Primero usando un predicate
            List<Guitarist> guitarristasConScoreNoNulo = guitarristasDeRock.FindAll(new SelectScore(0).SelectScorePredicate);
            foreach (Guitarist guitarristaEvaluado in guitarristasConScoreNoNulo)
            {
                Console.WriteLine("Este guitarrista tiene score no nulo: {0}", guitarristaEvaluado.ToString());
            }

            //Ahora con una Lambda
            guitarristasConScoreNoNulo = guitarristasDeRock.FindAll(g => g.Score > 0);
            var otrosGuitarristas = guitarristasDeRock.Except(guitarristasConScoreNoNulo);


            separador.EscribirPie("Fin Ejemplo 8");
            #endregion

            #region Ejemplo 9: Borro de una lista todos los elementos que están en otra lista
            separador.EscribirEncabezado("Ejemplo 9: Borrar de una lista los guitarristas que están en otra");

            //Comienzo creando un HashSet de elementos

            var GuitarristasQueNoEstan = new List<Guitarist>()
            {
                new Guitarist("James Marshall", "Hendrix", "Fender Stratocaster", 100)

            };

            //Ahora borro
            guitarristasDeRock.RemoveAll(g => GuitarristasQueNoEstan.Contains(g));
            Console.WriteLine("Veamos que Jimi no está");
            foreach (Guitarist gui in guitarristasDeRock)
            {
                Console.WriteLine(gui.ToString());
            }

            //Importante: Si no implementé Equals en la clase Guitarist, esto solo funciona si la lista a GuitarristasQueNoEstan se creó a partir de un subconjunto formado por 
            //LOS MISMOS OBJETOS
            //que los que están en la lista GuitarristasDeRock. 

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

        //Este método lo uso para ForEach en la lista
        private static void ImprimirDatos(Guitarist g)
        {
            string mensaje;
            mensaje = "Nombre: " + g.FirstName + g.LastName +
                " - Guitarra preferida: " + g.Guitar;
            Console.WriteLine(mensaje);
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
