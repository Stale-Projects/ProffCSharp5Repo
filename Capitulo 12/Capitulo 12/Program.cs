// ==++==
// 
//   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
// 
// ==--==
/*============================================================
    ** Proyecto: Capitulo10_ColeccionesObservables
    ** Clase:  Program
    ** 
    ** <OWNER>MarceVolta</OWNER>
    **
    ** Propósito: Proveer ejemplos de código para el capítulo 12 del libro
    ** "De Cabeza a C#"
    ** Este proyecto provee ejemplos de Código Asincrónico o Asíncrono
    ** Descripciones debajo en los summary
    ===========================================================*/

using System;
using System.Net;
using System.Threading.Tasks;

namespace Capitulo_12
{

    class Program
    {
        private static EncabezadoYPieConsola separador = new EncabezadoYPieConsola();
        static void Main(string[] args)
        {

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

            //await SmtpClient.SendMailAsync(mailMessage);
        }


        /// <summary>
        /// Código bloqueante o sincrónico
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
