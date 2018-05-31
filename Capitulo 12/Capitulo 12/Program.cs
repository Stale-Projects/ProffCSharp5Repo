// ==++==
// 
//   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
// 
// ==--==
/*============================================================
    ** Proyecto: Capitulo12
    ** 
    ** <OWNER>MarceVolta</OWNER>
    **
    ** Propósito: Proveer ejemplos de código para el capítulo 12 del libro
    ** "De Cabeza a C#"
    ** Este proyecto provee ejemplos de Código Asincrónico
    ** Descripciones debajo
    ===========================================================*/

using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Capitulo_12
{
    // ==++==
    // 
    //   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
    // 
    // ==--==
    /*============================================================
            ** Proyecto: Capitulo12_Async
            ** Clase:  Program
            ** 
            ** <OWNER>MarceVolta</OWNER>
            **
            ** Propósito: Proveer ejemplos de código para el capítulo 12 del libro
            ** "De Cabeza a C#"
            ** Este proyecto provee ejemplos de código asincrónico en C#
            * Ten en cuenta que debes referenciar los namespaces System.Threading y 
            * Sysm.Threading.Tasks
            * La funcionalidad está provista por el assembly mscorlib
            * Además debemos referenciar System.Net para los ejemplos 
            * que usan funcionalidad de red como ejemplo de código I/O - bound
            ** Descripciones debajo en el summary
         ===========================================================*/

    class Program
    {
        private static EncabezadoYPieConsola separador = new EncabezadoYPieConsola();

        /// <summary>
        /// Esta función Main se usa de dos maneras: 
        /// Por un lado ejecutamos código asincrónico
        /// usando los patrones de .NET anteriores a 4.0 
        /// Para usar TAP sólo delegamos en MainAsync
        /// ya que necesitamos un Main asincrónico
        /// para poder llamar a await
        /// MainAsync se ejecuta en forma asincrónica
        /// Para eso usa un AsyncContext que está definido 
        /// en la biblioteca Nito.AsyncEx (instalar con NuGet)
        /// Cualquier excepción es capturada y reportada
        /// Observa que nos alejamos del clásico retorno void para 
        /// Main de modo que podamos realizar el llamado a AsyncContext
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static int Main(string[] args)
        {
            //Patrones asincrónicos anteriores a .NET 4.0

            #region Ejemplo #01 - Patrón EAP (Event Asynchronous Pattern)
            separador.EscribirEncabezado("Ejemplo #01 - Patrón EAP (Event Asynchronous Pattern)");

            //Descarga de una página web en modo asincrónico
            //Aquí utilizamos EAP
            string url = "http://www.teatrocolon.org.ar/es/historia";
            Uri uri = new Uri(url);
            Console.WriteLine("Iniciamos la descarga de una página web");
            DescargarPaginaWeb(uri);
            Console.WriteLine("Esto debería verse aún antes de que finalice la descarga de la página");
            Console.WriteLine("Aguarda a que se descargue la página y luego oprime ENTER");
            Console.ReadLine();

            separador.EscribirPie("Fin Ejemplo #01");
            #endregion

            #region Ejemplo #02 - Patrón IAsynResult
            separador.EscribirEncabezado("Ejemplo #02 - Patrón IAsynResult");

            //Búsqueda de Host en modo asincrónico
            //Aquí utilizamos IAsyncResult
            url = "www.teatrocolon.org.ar";
            BuscarHostPorNombre(url);
            Console.WriteLine("Aguarda a que se ejecute la búsqueda por DNS y luego oprime ENTER");
            Console.ReadLine();

            separador.EscribirPie("Fin Ejemplo #02");
            #endregion

            #region Ejemplo #03 - El Patrón más simple: Usando un Callback
            separador.EscribirEncabezado("Ejemplo #03 - El Patrón más simple: Usando un Callback");

            //El modo más sencillo de ejecutar en forma asincrónica
            //Usar un Callback, en este caso lo pasamos como una Lambda
            int unValorCualquiera = 3;
            UnaOperacionLarga(20000000, cuantos =>
            {
                System.Media.SystemSounds.Beep.Play();
                unValorCualquiera++;
                int porciento = (int)(100 * cuantos / 200000000);


            });

            separador.EscribirPie("Fin Ejemplo #03");
            #endregion

            #region Ejemplo #04 - Evitando desdoblamiento con Task<T>
            separador.EscribirEncabezado("Ejemplo #04 - Evitando desdoblamiento con Task<T>");

            //Con esta única llamada y la declaración del método puedo ejecutar
            //código asíncrono
            BuscarHostPorNombre_v2(url);

            separador.EscribirPie("Fin Ejemplo #04");
            #endregion


            //Llamado a MainAsync, donde ejecuto el resto de los ejemplos escritos siguiendo el TAP
            try
            {
                return AsyncContext.Run(() => MainAsync(args));
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Esta es una versión async de Main
        /// La usamos como si fuera Main y nos da 
        /// la posibilidad de usar await
        /// </summary>
        /// <param name="args">El mismo parámetro que llega a Main: colección de argumentos</param>
        /// <returns></returns>
        /// 

        static async Task<int> MainAsync(string[] args)
        {
            //ToDo: Escribir ejemplos básicos de TAP

            #region Ejemplo #7: Conversión a TAP "a mano"
            separador.EscribirEncabezado("Ejemplo #7: Conversión a TAP 'a mano'");

            string hostName = "www.google.com";
            IPHostEntry iphost = await (ReturnHostEntryAsync(hostName));
            Console.WriteLine("Esta es la lista de IPs del host {0}", hostName);
            foreach (IPAddress ip in iphost.AddressList)
            {
                Console.WriteLine(ip.ToString());
            }

            separador.EscribirPie("Fin Ejemplo #7");
            #endregion

            #region Ejemplo #8: Conversión a TAP usando FromAsync
            separador.EscribirEncabezado("Ejemplo #8: Conversión a TAP usando FromAsync");

            string hostNameOrAddress = "www.facebook.com";
            Task<IPHostEntry> t = Task<IPHostEntry>.Factory.FromAsync<string>(Dns.BeginGetHostEntry,
                Dns.EndGetHostEntry, hostNameOrAddress, null);

            IPHostEntry ipHostFB = await (t);

            Console.WriteLine("Esta es la lista de IPs del host {0}", hostNameOrAddress);
            foreach (IPAddress ip in ipHostFB.AddressList)
            {
                Console.WriteLine(ip.ToString());
            }

            separador.EscribirPie("Fin Ejemplo #8");
            #endregion

            #region Ejemplo #9: Tres modos de Espera
            separador.EscribirEncabezado("Ejemplo #9: Tres modos de Espera");

            //Ejemplo 9: Tres modos de espera. El único que no es aceptable es Thread.Sleep()
            //ya que éste ocupa un thread del pool sólo para ahecrlo esperar

            Console.WriteLine("Esperamos tres segundos usando un timer async...");
            await Esperar(3000);
            Console.WriteLine("Listo!");

            Console.WriteLine("Esperamos dos segundos usando Task.Delay...");
            await Task.Delay(2000);
            Console.WriteLine("Listo!");

            Console.WriteLine("Esperamos dos segundos usando Thread.Sleep...");
            await Task.Run(() => Thread.Sleep(2000));
            Console.WriteLine("Listo!");

            separador.EscribirPie("Fin Ejemplo #9");
            #endregion

            #region Ejemplo #10: Uso de WhenAll
            separador.EscribirEncabezado("Ejemplo #10: Uso de WhenAll");

            //tasks es la colección detasks que vamos a crear
            var tasks = new List<Task<long>>();

            for (int ctr = 1; ctr <= 10; ctr++)
            {
                int intervaloDePausa = 18 * ctr;
                tasks.Add(Task.Run(async () =>
                {
                    long total = 0;
                    await Task.Delay(intervaloDePausa);
                    var rnd = new Random();
                    // Generar 1.000 enteros aleatorios
                    for (int n = 1; n <= 1000; n++)
                        total += rnd.Next(0, 1000);
                    return total;
                }));
            }
            var todosLosTasks = Task.WhenAll(tasks);
            try
            {
                //todosLosTasks.Wait();
                long[] valores = await todosLosTasks;
            }
            catch (AggregateException)
            { }

            if (todosLosTasks.Status == TaskStatus.RanToCompletion)
            {
                long totalGeneral = 0;

                foreach (var resultado in todosLosTasks.Result)
                {
                    totalGeneral += resultado;
                    Console.WriteLine("Promedio: {0:N2}, n = 1.000", resultado / 1000.0);
                }

                Console.WriteLine("\nPromedio General: {0:N2}, n = 10.000",
                                  totalGeneral / 10000);
            }
            // Mostrar info de los tasks con errores
            else
            {
                foreach (var tsk in tasks)
                {
                    Console.WriteLine("Task {0}: {1}", tsk.Id, tsk.Status);
                }
            }

            separador.EscribirPie("Fin Ejemplo #10");
            #endregion

            #region Ejemplo 11: Uso de WhenAny
            separador.EscribirEncabezado("Ejemplo 11: Uso de WhenAny");

            CancellationTokenSource cts = new CancellationTokenSource();

            Task<Task<long>> cualquierTarea = Task.WhenAny(tasks);
            Task<long> primerTareaCompleta = await cualquierTarea;

            Console.WriteLine("Primer tarea completa: ID= {0} - Resultado= {1}", primerTareaCompleta.Id.ToString(),
                primerTareaCompleta.Result.ToString());

            foreach (var unaTarea in tasks)
            {
                if (unaTarea != primerTareaCompleta)
                {
                    await unaTarea;

                }
            }


            separador.EscribirPie("Fin Ejemplo #11");
            #endregion



            return 0;

        }



        /// <summary>
        /// Esta es la primera mitad del código para descargar una página web 
        /// usando callbacks. Lo que hago aquí es fijar el callback para que 
        /// llame a OnDownloadStringCompleted e invocar el 
        /// método asincrónico DownloadStringAsync
        /// </summary>
        /// <param name="uri">URI que identifica la página web a descargar</param>
        private static void DescargarPaginaWeb(Uri uri)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += OnDownloadStringDataCompleted;
            webClient.DownloadStringAsync(uri);

        }

        /// <summary>
        /// Esta función se usa como manejador del evento DownloadStringCompleted 
        /// de la clase WebClient
        /// </summary>
        /// <param name="sender">El objeto que envió el evento</param>
        /// <param name="args">Los argumentos del evento</param>
        private static void OnDownloadStringDataCompleted(object sender, DownloadStringCompletedEventArgs args)
        {
            //Console.WriteLine("Descarga finalizada");
            //Console.WriteLine("La página web descargada tiene {0} caracteres", args.Result.Length);
            System.Media.SystemSounds.Beep.Play();

        }

        /// <summary>
        /// Este método busca un Host por nombre
        /// Se utiliza el patrón de IAsyncResult
        /// </summary>
        /// <param name="url">La URL del host cuyas IP buscamos</param>
        private static void BuscarHostPorNombre(string url)
        {
            //Este objeto se usa para enviarle al manejador de evento
            //información contextual del punto del programa donde 
            //se inició la operación asincrónica 
            object estadoEnviado = DateTime.Now;
            Dns.BeginGetHostAddresses(url, OnNombreDeHostResuelto,
                estadoEnviado);
        }



        /// <summary>
        /// Este es el manejador de Evento. La clave aquí es el objeto
        /// de tipo IAsyncResult que contiene la información contextual
        /// que permite recibir estado desde el inicio de la ejecución
        /// para poder identificar el punto del código desde el cual se 
        /// ejecutó el método asuncrónico, y también se usa para poder 
        /// recuperar el resultado correspondiente a esa ejecución
        /// de la tarea asincrónica y también utilizar 
        /// </summary>
        /// <param name="ar"></param>
        private static void OnNombreDeHostResuelto(IAsyncResult ar)
        {
            //Aquí recibo el objeto que creé para contener estado 
            //su valor es la hora a la que se inció la operación
            object estadoRecibido = ar.AsyncState;
            //Aquí recupero información del fin de la ejecución del método estático BeginHostAddresses
            //pasando como argumento el objeto IAsyncResult generado
            IPAddress[] direcionesIP = Dns.EndGetHostAddresses(ar);
            //Aquí podría usar las direcciones IP obtenidas
            System.Media.SystemSounds.Beep.Play();

        }

        /// <summary>
        /// Este método toma como argumento un callback (pasado como Action)
        /// Lo que el método hace es determinar cuantos números enteros son divisibles
        /// por 3 y por 5 a la vez, y luego invoca el callback pasándole este número
        /// </summary>
        /// <param name="limiteSuperior">Busco enteros divisibles por 3 y por 5 a la vez, desde el 1 hasta el número pasado en este parámetro</param>
        /// <param name="callback">Un método que toma como parámetro un la cuenta de enteros que son divisibles por 3 y 5 a la vez</param>
        private static void UnaOperacionLarga(int limiteSuperior, Action<int> callback)
        {
            int cuantosDivisibles = 0;
            for (int i = 0; i < limiteSuperior; i++)
            {
                if (Math.IEEERemainder(i, 3) == 0 & Math.IEEERemainder(i, 5) == 0)
                {
                    cuantosDivisibles++;
                }
            }
            callback(cuantosDivisibles);
        }

        /// <summary>
        /// Este método usa la clase Task&lt;IPAdress[]&gt; como una promesa
        /// de un array de direcciones IP que se obtendrá como resultado 
        /// de correr Dns.GetHostAdressesAsync
        /// El callback se provee en la propiedad ContinueWith como una lambda
        /// para evitar desdoblamiento
        /// </summary>
        /// <param name="url">La url para la cual buscamos los hosts</param>
        private static void BuscarHostPorNombre_v2(string url)
        {
            Task<IPAddress[]> promesaDeIPAdress = Dns.GetHostAddressesAsync(url);
            promesaDeIPAdress.ContinueWith(_ =>
            {
                IPAddress[] direccionesIP = promesaDeIPAdress.Result;
                //Aquí puedo hacer algo con las direcciones IP 
            });
        }


        /// <summary>
        /// Este método encapsula un método async implementado por medio del patrón IAsyncResult
        /// (que usa callbacks al estilo de .NET 4.0), utilizando un Puppet Task por medio 
        /// TaskCompletionSource. De este modo le evito al código cliente, tener que desdoblarse
        /// para definir un callback. Aquí devolvemos un Task que puede ser esperado por medio de
        /// await usando el patrón TAP
        /// </summary>
        /// <param name="hostNameOrAddress"></param>
        /// <returns></returns>
        public static Task<IPHostEntry> ReturnHostEntryAsync(string hostNameOrAddress)
        {
            TaskCompletionSource<IPHostEntry> tcs = new TaskCompletionSource<IPHostEntry>();
            Dns.BeginGetHostEntry(hostNameOrAddress,
                asyncResult =>
                {
                    try
                    {
                        IPHostEntry resultado = Dns.EndGetHostEntry(asyncResult);
                        tcs.SetResult(resultado);
                    }
                    catch (Exception e)
                    {
                        tcs.SetException(e);

                    }
                }
                , null);

            return tcs.Task;
        }

        /// <summary>
        /// Implementa una espera asincrónica por medio de un Timer.
        /// Preferida a Thread.Sleep() pero reemplazada por Task.Wait()
        /// </summary>
        /// <param name="milisegundos">Tiempo de espera</param>
        /// <returns></returns>
        private static Task Esperar(int milisegundos)
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            Timer timer = new Timer(_ => tcs.SetResult(null), null, milisegundos, Timeout.Infinite);
            tcs.Task.ContinueWith(delegate { timer.Dispose(); });
            return tcs.Task;
        }

        /// <summary>
        /// Identifica el thread escribiendo su ID en la consola
        /// </summary>
        static void IdentificarThread()
        {
            Console.WriteLine("Este es el thread [{0}] ", Thread.CurrentThread.ManagedThreadId.ToString());
        }

        static async Task<T> TaskConTimeout<T>(Task<T> task, int milisegundos)
        {

            Task espera = Task.Delay(milisegundos);
            //WhenAny puede manejra distintos tipos de Tasks
            Task primeroCompleto = await Task.WhenAny(task, espera);

            if (primeroCompleto == espera)
            {
                //Si el TimeOut se completó primero puede ser porque hubo excepciones
                //ToDo: El compilador me tira un warning acá
                //ToDo: Ojo que ContinueWith le manda el argumento al action!
                task.ContinueWith(ManejarExcepcion);
                throw new TimeoutException();
            }

            //ToDo: no entiendo esto
            return await task;
        }

        static void ManejarExcepcion<T>(Task<T> task)
        {
            if (task.Exception != null)
            {
                Console.WriteLine(task.Exception.ToString());
            }

        }
    }

    /// <summary>
    /// Esta struct encapsula dos métodos para mostrar un encabezado y un pie 
    /// para cada ejemplo, de modo que podamos verlos por separado en la salida de 
    /// consola e identificar los resultados de cada ejemplo
    /// </summary>
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
