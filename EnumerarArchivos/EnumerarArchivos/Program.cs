// ==++==
// 
//   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
// 
// ==--==
/*============================================================
** Proyecto: EnumerarArchivos
** Capítulo 11: LINQ 
** Clase:  Program
** 
** <OWNER>MarceVolta</OWNER>
**
** Propósito: Proveer ejemplos de código para el capítulo 11 del libro
** "De Cabeza a C#"
** Este proyecto provee ejemplos de Queries LINQ sobre objetos
* del tipo Directorio y otras colecciones creadas para ejemplos más
* sencillos.
** Descripciones debajo
===========================================================*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnumerarArchivos
{

    class Program
    {

        static void Main()
        {
            //Separador
            EncabezadoYPieConsola separador = new EncabezadoYPieConsola();



            #region Ejemplo #01: Operador Where
            separador.EscribirEncabezado("Ejemplo #01: Uso de Operador Where");

            FileInfo[] todosMisArchivos = RecuperarTodosMisArchivos();
            var archivosTXT = from archivo in todosMisArchivos
                              where archivo.Extension == ".txt"
                              select archivo;

            Console.WriteLine("Todos los archivos TXT");
            foreach (var archivoTXT in archivosTXT)
            {
                Console.WriteLine(archivoTXT.Name);
            }

            separador.EscribirPie("Fin Ejemplo #01");
            #endregion

            #region Ejemplo #02: Operador Where con métodos de Extensión
            separador.EscribirEncabezado("Ejemplo #02: Operador Where con métodos de Extensión");
            var buscarUnaVezMas = todosMisArchivos.Where(f => f.Extension == ".txt").OrderBy(f => f.Name).Select(f => f);

            //Ejecutar el query  
            foreach (FileInfo archivo in buscarUnaVezMas)
            {
                Console.WriteLine(archivo.FullName);
            }
            separador.EscribirPie("Fin Ejemplo #02");
            #endregion

            #region Ejemplo #03: Ejecución diferida
            separador.EscribirEncabezado("Ejemplo #03: Ejecución diferida");

            var nombres = new List<string> { "Alberto", "Carlos", "Cintia", "Pedro", "Laura" };
            var nombresConA = from n in nombres
                              where n.StartsWith("A")
                              orderby n
                              select n;

            Console.WriteLine("Primera pasada");
            foreach (string name in nombresConA)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine();
            nombres.Add("Alejandra");
            nombres.Add("Pablo");
            nombres.Add("Andrés");
            nombres.Add("David");
            Console.WriteLine("Segunda Pasada");


            foreach (string name in nombresConA)
            {
                Console.WriteLine(name);
            }

            separador.EscribirPie("Fin Ejemplo #03");
            #endregion

            #region Ejemplo 04: Ejecución parcialmente diferida
            separador.EscribirEncabezado("Ejemplo #04: Ejecución parcialmente diferida");

            //En lugar de separar la búsqueda de archivos en otra función lo incluimos en el query 
            //aún a riesgo de una excepción, para que sea más evidente el problema que queremos mostrar 
            string misDocumentos = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            //Tomamos una imagen instantánea del directorio My Documents
            var dir = new DirectoryInfo(misDocumentos);


            //Creamos un query que lista todos los archivos txt  
            //El método GetFiles devuelve un array de objetos de tipo FileInfo
            //e incluimos su ejecución directamente en la definición del query
            archivosTXT =
            from archivo in dir.GetFiles("*.*", SearchOption.AllDirectories)
            where archivo.Extension == ".txt"
            orderby archivo.Name
            select archivo;

            //Sólo al llegar aquí se ejecuta el query  
            Console.WriteLine("Resultado Inicial");
            Console.WriteLine("Encontramos {0} archivos", archivosTXT.ToArray<FileInfo>().Length.ToString());


            //Creemos un nuevo archivo para complicar las cosas
            CrearArchivo(misDocumentos);


            //Ejecutemos el query nuevamente, sin tocar la definición
            Console.WriteLine("Resultado luego de crear un archivo nuevo");
            Console.WriteLine("Encontramos {0} archivos", archivosTXT.ToArray<FileInfo>().Length.ToString());

            separador.EscribirPie("Fin Ejemplo #04");
            #endregion


            #region Ejemplo 04b: Ejecución completamente diferida
            separador.EscribirEncabezado("Ejemplo #04b: Ejecución completamente diferida");

            todosMisArchivos = dir.GetFiles("*.*", SearchOption.AllDirectories);
            archivosTXT =
            from archivo in todosMisArchivos
            where archivo.Extension == ".txt"
            orderby archivo.Name
            select archivo;

            Console.WriteLine("Resultado Inicial");
            Console.WriteLine("Encontramos {0} archivos", archivosTXT.ToArray<FileInfo>().Length.ToString());
            //Creemos un nuevo archivo para complicar las cosas
            CrearArchivo(misDocumentos);

            //Refresquemos el array de archivos, para traer todo lo nuevo 
            //Es necesario hacerlo desde la creación del DirectoryInfo
            dir = new DirectoryInfo(misDocumentos);
            todosMisArchivos = dir.GetFiles("*.*", SearchOption.AllDirectories);

            //Ejecutemos el query nuevamente, sin tocar la definición
            Console.WriteLine("Resultado luego de crear un archivo nuevo");
            Console.WriteLine("Encontramos {0} archivos", archivosTXT.ToArray<FileInfo>().Length.ToString());

            separador.EscribirPie("Fin Ejemplo #04b");
            #endregion






            // Usando como base la búsqueda anterior, creamos y ejecutamos otra búsqueda 
            // que nos diga: cuántos son, y cuál es el más nuevo
            // Aquí no se ejecuta la búsqueda hasta que no se produce la llamada a Last()

            var archivoMasReciente =
                (from archivo in archivosTXT
                 orderby archivo.CreationTime
                 select new { archivo.FullName, archivo.CreationTime })
                .Last();

            Console.WriteLine("\r\nEl archivo más reciente de tipo .txt es {0}. Creado en: {1}",
                archivoMasReciente.FullName, archivoMasReciente.CreationTime);

            //Creamos un nuevo archivo TXT para producir un resultado diferente en el query
            CrearArchivo(misDocumentos);

            var archivoRecienCreado =
                (from archivo in archivosTXT
                 orderby archivo.CreationTime
                 select new { archivo.FullName, archivo.CreationTime })
                .Last();

            Console.WriteLine("\r\nEste es el archivo que acabamos de crear: {0}. Creado en: {1}",
                archivoRecienCreado.FullName, archivoRecienCreado.CreationTime);





            //Recupero todos los archivos txt cuyo nombre comienza con A (ó a)
            var archivosTXTConA = from archivo in todosMisArchivos
                                  where archivo.Extension == ".txt" &&
                                  archivo.Name.Substring(0, 1).ToLower() == "a"
                                  select archivo;

            Console.WriteLine("Archivos TXT que comienzan con A ó a");
            foreach (var archivoTXTConA in archivosTXTConA)
            {
                Console.WriteLine(archivoTXTConA.Name);
            }

            //El primer query anterior escrito con sintaxis de métodos de extensión
            var archivosTXT_V2 = todosMisArchivos.Where(x => x.Extension == ".txt").Select(r => r);
            var cuentaDeArchivosTXT = todosMisArchivos.Where(x => x.Extension == ".txt").Count();
            Console.WriteLine("Todos los archivos TXT en Sintaxis de métodos de extensión");
            Console.WriteLine("Son {0} archivos", cuentaDeArchivosTXT.ToString());

            foreach (var archivoTXT in archivosTXT_V2)
            {
                Console.WriteLine(archivoTXT.Name);
            }


            //Un query donde utilizamos un índice
            var archivosTXT_IndicePar = todosMisArchivos.Where((x, indice) => x.Extension == ".txt" && indice % 2 == 0).Select(r => r);
            Console.WriteLine("Todos los archivos TXT cuyo índice sea par");
            var cuentaDeArchivosTXT_IndicePar = todosMisArchivos.Where((x, indice) => x.Extension == ".txt" && indice % 2 == 0).Count();
            Console.WriteLine("Son {0} archivos", cuentaDeArchivosTXT_IndicePar.ToString());
            foreach (var archivoTXT in archivosTXT_IndicePar)
            {
                Console.WriteLine(archivoTXT.Name);
            }

            //Filtrado por tipo
            //Este ejemplo se hizo demasiado complejo para algo simple
            //var todosMisDirectorios = RecuperarTodosMisDirectoriosYArchivos();
            //var cuentaDeDirectorios = todosMisDirectorios.Distinct().Count();
            //Console.WriteLine("Son {0} directorios", cuentaDeDirectorios.ToString());
            //foreach (var directorio in todosMisDirectorios.Distinct())
            //{
            //    Console.WriteLine("Directorio: <{0}>", directorio.FullName);
            //}


            //Filtrado por tipo
            Console.WriteLine("Filtrado por tipo");
            object[] datos = { "uno", 2, 3, "cuatro", "cinco", 6 };
            var query = datos.OfType<string>();
            foreach (var s in query)
            {
                Console.WriteLine(s);
            }


            //Operador From compuesto
            //Primero creamos una lista de todos los subdirectorios de Mis Documentos
            List<DirectoryInfo> misDirectoryInfo = RecuperarTodosMisDirectorios().ToList<DirectoryInfo>();
            List<Directorio> misDirectorios = new List<Directorio>();
            foreach (var directorio in misDirectoryInfo)
            {
                misDirectorios.Add(new Directorio(directorio));
            }

            foreach (var directorio in misDirectorios)
            {
                Console.WriteLine("Nombre: {0}", directorio.Nombre);
            }

            //Ahora listamos solo los directorios que tienen archivos TXT dentro
            IEnumerable<string> directoriosConTXT =
                from directorio in misDirectorios
                from archivo in directorio.Archivos
                where archivo.Extension == ".txt"
                select (directorio.Nombre + " - " + archivo.Name);

            Console.WriteLine("Nombres de Directorios que contienen TXTs");
            foreach (var descripcion in directoriosConTXT)
            {
                Console.WriteLine(descripcion);
            }


            //Para ejemplificar el uso de OrderBy modificamos ligeramente 
            //el query anterior
            //Ahora listamos solo los directorios que tienen archivos TXT dentro
            IEnumerable<string> directoriosConTXTOrdenados =
                from unDirectorio in misDirectorios
                from archivo in unDirectorio.Archivos
                where archivo.Extension == ".txt"
                orderby archivo.CreationTime
                select (unDirectorio.Nombre + " - " + archivo.Name + "Creado en: " + archivo.CreationTime.ToLongDateString());

            Console.WriteLine("Nombres de Directorios que contienen TXTs");
            foreach (var descripcion in directoriosConTXTOrdenados)
            {
                Console.WriteLine(descripcion);
            }

            return;



        }


        /// <summary>
        /// Esta función obtiene un array de objetos FileInfo que representan 
        /// a los archivos contenidos en el directorio Mis Documentos
        /// Para ello utilizamos la función GetDirectoryName para obtener el nombre del 
        /// directorio. Esto permite que utilicemos el código en cualquier sistema de 
        /// archivos Windows. Recuerda que no se deben usar variables explícitas a menos 
        /// que sea estrictamente necesario; si usara directamente el nombre "Mis Documentos"
        /// no podría ejecutar este código en un equipo con el sistema operativo en otro 
        /// idioma distinto al Español
        /// La creación del objeto de tipo DirectoryInfo equivale a tomar una foto del estado del 
        /// directorio en ese momento
        /// El método GetFiles devuelve un array de objetos de tipo FileInfo
        /// Separamos la recuperación de los archivos de la definición del query
        /// para lograr la ejecución diferida 
        /// Creamos un query que lista todos los archivos txt  
        /// el segundo argumento le indica a GetFiles que busque en todos los 
        /// subdirectorios de Mis Documentos
        /// </summary>
        /// <returns>FileInfo[]</returns>
        private static FileInfo[] RecuperarTodosMisArchivos()
        {
            string misDocumentos = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            FileInfo[] todosMisArchivos = null;

            var dir = new DirectoryInfo(misDocumentos);

            try
            {
                todosMisArchivos = dir.GetFiles("*.*", SearchOption.AllDirectories);

            }

            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
            catch (Exception excepcionGeneral)
            {
                Console.WriteLine("Ocurrió un error al tratar de recueprar los archivos de Mis Documentos: {0}", excepcionGeneral.Message);
            }

            return todosMisArchivos;
        }



        private static IEnumerable<DirectoryInfo> RecuperarTodosMisDirectorios()
        {
            //Primero obtenemos la localización del directorio Mis Documentos 
            string misDocumentos = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            //Tomamos una imagen instantánea del directorio My Documents
            var dir = new DirectoryInfo(misDocumentos);


            //Ahora, obtengo recursivamente todos los subdirectorios 
            IEnumerable<DirectoryInfo> todosMisSubdirectorios = dir.GetDirectories("*.*", SearchOption.AllDirectories);
            //IEnumerable<DirectoryInfo> todosMisSubdirectorios = RecuperarTodosLosSubdirectorios(dir);
            return todosMisSubdirectorios;

            //IEnumerable todosMisDirectoriosYArchivos = null;


            //try
            //{
            //    //El método GetFiles devuelve un array de objetos de tipo FileInfo
            //    //Separamos la recuperación de los archivos de la definición del query
            //    //para lograr la ejecución diferida 
            //    //Creamos un query que lista todos los archivos txt  
            //    //el segundo argumento le indica a GetFiles que busque en todos los 
            //    //subdirectorios de Mis Documentos

            //    var todosMisArchivos = dir.GetFiles("*.*", SearchOption.AllDirectories);
            //    var todosMisDirectorios = dir.GetDirectories();
            //}

            //catch (UnauthorizedAccessException UAEx)
            //{
            //    Console.WriteLine(UAEx.Message);
            //}
            //catch (PathTooLongException PathEx)
            //{
            //    Console.WriteLine(PathEx.Message);
            //}
            //catch (Exception excepcionGeneral)
            //{
            //    Console.WriteLine("Ocurrió un error al tratar de recueprar los archivos de Mis Documentos: {0}", excepcionGeneral.Message);
            //}

            //return todosMisArchivos;
        }

        /// <summary>
        /// Esta función la escribí para obtener los sub-directorios en forma recursiva porque 
        /// no sabía que habia una sobrecarga de GetDirectories() que me permitía obtener todos los
        /// ubdirectorios en forma recursiva
        /// </summary>
        /// <param name="directorio"></param>
        /// <returns></returns>
        private static IEnumerable<DirectoryInfo> RecuperarTodosLosSubdirectorios(DirectoryInfo directorio)
        {
            IEnumerable<DirectoryInfo> _subdirectorios = directorio.GetDirectories();

            foreach (var subdirectorio in _subdirectorios)
            {
                _subdirectorios = _subdirectorios.Concat(RecuperarTodosLosSubdirectorios(subdirectorio));
            }
            return _subdirectorios;
        }

        private static void CrearArchivo(string path)
        {
            string nombreDeArchivo = path + @"\txt_" + DateTime.Now.ToString(format: "yyyymmdd hhmmss") + ".txt";
            string contenido = "Un contenido de prueba para poner en el arvhivo";
            BinaryWriter bw = new BinaryWriter(new FileStream(nombreDeArchivo, FileMode.Create), System.Text.Encoding.ASCII);
            bw.Write(contenido);
            bw.Dispose();

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
