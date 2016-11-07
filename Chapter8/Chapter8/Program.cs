using System;
using System.Collections.Generic;

namespace Chapter8
{
    //Usada para el lame example
    delegate string AStringFuntion();
    //Usada para el ejemplo de Array de Delegados
    delegate long Operacion(int x);
    class Program
    {

        static void Main(string[] args)
        {
            //Ejemplo 1: Creo una función, le apunto con un delegado
            //y luego llamo al delegado que es lo mismo que llamar a la función
            //Aquí uso Delegate Inference, la  línea 18 pudo haber sido: AStringFunction firstStringFunction = new AStringFunction(ThisFunction);
            #region Ejemplo 1
            AStringFuntion firstStringFunction = ThisFunction;
            string mensaje = firstStringFunction();
            Console.WriteLine(mensaje);
            #endregion

            //Ejemplo 2: Usar un array de delegates
            //Ver que el delegado se declara sin tener en cuenta el array
            #region Ejemplo 2
            Operacion[] Operaciones =
            {
                new Operacion(MathOperations.MultiplicarPorDos),
                new Operacion(MathOperations.Cuadrado)
            };

            for (int i = 0; i < Operaciones.Length; i++)
            {
                CalcularYMostrar(Operaciones[i]);
            }
            #endregion


            //Ejemplo 3: Lo mismo que en ejemplo 2, pero usando un Delegate
            //Vemos que no hace falta declarar el delegado específico, porque uso la declaración Genérica que ya está provista por C#
            #region Ejemplo 3
            Func<int, long>[] OperacionesConGeneric =
            {
                MathOperations.MultiplicarPorDos, MathOperations.Cuadrado
            };
            for (int i = 0; i < OperacionesConGeneric.Length; i++)
            {
                CalcularConGenericYMostrar(OperacionesConGeneric[i]);
            }
            #endregion


            //Ejemplo 4: Aquí veo el poder real de los Generics y de los Delegate.
            #region Ejemplo 4
            //En el primer caso ordeno un array de enteros usando un BubbleSorter que está definido 
            //sólo para el caso en que tenga que ordenar enteros, es una implementación específica
            //Ordenar un array de enteros
            int[] array = PrepareArray();
            BubbleSorter.BubbleSort(array);
            PrintArray(array);

            //Ahora, voy a ordenar una lista de objetos definidos por mí
            //En este caso, la definición del BubbleSorter no está atada a la clase sino que es completamente genérica

            //Ordenar una List de Employees
            List<Employee> empleados = PrepareEmployees();
            BubbleSorter.GenericBubbleSort(empleados, Employee.CompareTo);
            PrintEmployees(empleados);


            //Ordenar un Array de Employees
            //Como el BubbleSorter tiene un parámetro de tipo 
            //IList<T> en List<T> puedo usar también un array
            Employee[] arrayDeEmpleados = PrepareEmployeeArray();
            BubbleSorter.GenericBubbleSort(arrayDeEmpleados, Employee.CompareTo);
            PrintEmployees(arrayDeEmpleados);
            #endregion


            //Ejemplo 5: Multicast Delegate
            //Como se ve, no hay necesidad de declarar el Multicast Delegate ya que uso el Generic Delegate Action<T>
            //Y con una sola llamada se invocan todas las funciones de la lista
            #region Eejemplo 5
            Action<int> OperacionesNulas = MathOperations.CuadradoYMostrar;
            OperacionesNulas += MathOperations.Dividir5PorX;
            OperacionesNulas += MathOperations.MutiplicarPorDosyMostrar;
            OperacionesNulas(2);
            OperacionesNulas(500);
            //El problema potencial es que si llega a fallar algo en el medio (una excepción en alguna invocación)
            //pueden no invocarse todas las funciones. Para eso uso la enumeración con un try catch
            Delegate[] OperacionesPorLista = OperacionesNulas.GetInvocationList();
            foreach (Action<int> accion in OperacionesPorLista)
            {
                accion(7);

                try
                {
                    accion(0);
                }
                catch (Exception e)
                {
                    ReportarExcepcion(e);
                }

                accion(1000);

            }
            #endregion


            //Ejemplo 6: Anonymous Delegate
            //Recordemos todas las instancias de definción de delegados
            //Primero la más explícita y verbosa: 
            //delegate string GetAstring();
            //int x = 5;
            //GetAstring StringDeEntero = new GetAString(x.toString());
            //****
            //Despsués le saqueé la llamada explícita al constructor con Inferencia. Reemplazo la última línea por:
            //GetAString StringDentero = x.ToString();
            //Después usé Generics para no tener que declarar (me libro de la primera línea de arriba):
            //Func<string> StringDeEntero = x.ToString():
            //Y ahora finalmente uso método anónimo: 
            //Func<string> StringDeEntero = delegate()
            //{
            //  return x.ToString();
            //}
            //Incluso puedo usar la variable x declarada fuera del Delegate
            #region Ejemplo 6
            //Usar Anonymous Delegate
            Func<int, int> anonDel = delegate (int param)
            {
                param += 10;
                return param;
            };
            Console.WriteLine("El valor retornado usando un Anonymous Delagate es: {0}", anonDel(50));

            //Usar Lambda Expression - Ejemplo simple
            Func<int, int> lambda = param =>
            {
                param += 10;
                return param;
            };
            Console.WriteLine("El valor retornado usando una Lambda Expression es: {0}", lambda(50));
            #endregion

            //Ejemplo 6: Lambda Expression
            #region Ejemplo 7
            //Usar Lambda expression inline
            Func<int, int> LambdaSingle = param => param += 10;
            Console.WriteLine("El valor retornado usando una Single Line Lambda es: {0}", LambdaSingle(50));
            #endregion


        }


        //Función Usada para el Ejemplo 1
        public static string ThisFunction()
        {
            return "What a lame funtion!";
        }

        //Función usada para el ejemplo 2
        public static void CalcularYMostrar(Operacion operacion)
        {
            long result = operacion(2);
            Console.WriteLine("El resultado para 2 es: {0}", result.ToString());
            result = operacion(10);
            Console.WriteLine("El resultado para 10 es: {0}", result.ToString());
            return;
        }

        //Función usada para el Ejemplo 2b
        public static void CalcularConGenericYMostrar(Func<int, long> funcion)
        {
            long result = funcion(2);
            Console.WriteLine("El resultado para 2 es: {0}", result.ToString());
            result = funcion(10);
            Console.WriteLine("El resultado para 10 es: {0}", result.ToString());
            return;
        }

        public static int[] PrepareArray()
        {
            int[] array =
            {
                1, 100,75, 189,44, 33,2, 11, 37, 152, 800,73
            };
            return array;
        }

        public static void PrintArray(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i].ToString());
            }
        }

        public static List<Employee> PrepareEmployees()
        {
            List<Employee> lista = new List<Employee>();

            lista.Add(new Employee("Curly", 800.15M));
            lista.Add(new Employee("Shemp", 200.50M));
            lista.Add(new Employee("Moe", 715.25M));

            return lista;
        }

        public static Employee[] PrepareEmployeeArray()
        {
            Employee[] employees =
            {
                new Employee("Curly", 800.15M),
                new Employee("Shemp", 200.50M),
                new Employee("Moe", 715.25M)
            };

            return employees;
        }


        public static void PrintEmployees(IList<Employee> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                Console.WriteLine("Nombre: {0}, Salario: {1}", lista[i].Name, lista[i].Salary.ToString());
            }
        }

        public static void ReportarExcepcion(Exception e)
        {
            Console.WriteLine("Ocurrió una excepción: {0}", e.Message);
        }

    }
}
