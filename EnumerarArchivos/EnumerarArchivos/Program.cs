using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EnumerarArchivos
{

    class Program
    {

        // This query will produce the full path for all .txt files  
        // under the specified folder including subfolders.  
        // It orders the list according to the file name.  
        static void Main()
        {
            FileInfo[] todosMisArchivos = RecuperarTodosMisArchivos();


            //Uso de Operador de Filtro Where: 
            //Recupero todos los archivos txt
            var archivosTXT = from archivo in todosMisArchivos
                              where archivo.Extension == ".txt"
                              select archivo;

            Console.WriteLine("Todos los archivos TXT");
            foreach (var archivoTXT in archivosTXT)
            {
                Console.WriteLine(archivoTXT.Name);
            }


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

            var todosMisDirectorios = RecuperarTodosMisDirectoriosYArchivos();
            var cuentaDeDirectorios = todosMisDirectorios.Distinct().Count();
            Console.WriteLine("Son {0} directorios", cuentaDeDirectorios.ToString());
            foreach (var directorio in todosMisDirectorios.Distinct())
            {
                Console.WriteLine("Directorio: <{0}>", directorio.FullName);
            }




            return;

            ////Sólo al llegar aquí se ejecuta el query  
            //Console.WriteLine("Resultado Inicial");

            //foreach (FileInfo archivo in buscarArchivosTXT)
            //{
            //    Console.WriteLine(archivo.FullName);
            //}


            ////Creemos un nuevo archivo para complicar las cosas
            //CrearArchivo(misDocumentos);

            ////Reconstruyamos el array para traer los archivos nuevos
            //todosMisArchivos = dir.GetFiles("*.*", SearchOption.AllDirectories);

            ////Ejecutemos el query nuevamente, sin tocar la definición
            //Console.WriteLine("Resultado luego de crear un archivo nuevo");
            //foreach (FileInfo archivo in buscarArchivosTXT)
            //{
            //    Console.WriteLine(archivo.FullName);
            //}

            ////Un modo alternativo de ejecutar el mismo query
            //var buscarUnaVezMas = dir.GetFiles("*.*", SearchOption.AllDirectories).Where(f => f.Extension == ".txt").OrderBy(f => f.Name).Select(f => f);

            ////Ejecutar el query  
            //foreach (FileInfo archivo in buscarUnaVezMas)
            //{
            //    Console.WriteLine(archivo.FullName);
            //}


            //// Usando como base la búsqueda anterior, creamos y ejecutamos otra búsqueda 
            //// que nos diga: cuántos son, y cuál es el más nuevo
            //// Aquí no se ejecuta la búsqueda hasta que no se produce la llamada a Last()

            //var archivoMasReciente =
            //    (from archivo in buscarArchivosTXT
            //     orderby archivo.CreationTime
            //     select new { archivo.FullName, archivo.CreationTime })
            //    .Last();

            //Console.WriteLine("\r\nEl archivo más reciente de tipo .txt es {0}. Creado en: {1}",
            //    archivoMasReciente.FullName, archivoMasReciente.CreationTime);

            ////Creamos un nuevo archivo TXT para producir un resultado diferente en el query
            //CrearArchivo(misDocumentos);

            //var archivoRecienCreado =
            //    (from archivo in buscarArchivosTXT
            //     orderby archivo.CreationTime
            //     select new { archivo.FullName, archivo.CreationTime })
            //    .Last();

            //Console.WriteLine("\r\nEste es el archivo que acabamos de crear: {0}. Creado en: {1}",
            //    archivoRecienCreado.FullName, archivoRecienCreado.CreationTime);




            //string ejemplo = "murciélago";
            //int vocales = ejemplo.CuantasVocalesTienes();
            ////int vocales = StringExtension.CuantasVocalesTienes(ejemplo);

            //Console.WriteLine(@"'murciélago' tiene {0} vocales", vocales.ToString());

        }


        private static FileInfo[] RecuperarTodosMisArchivos()
        {
            //Primero obtenemos la localización del directorio Mis Documentos 
            string misDocumentos = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            FileInfo[] todosMisArchivos = null;

            //Tomamos una imagen instantánea del directorio My Documents
            var dir = new DirectoryInfo(misDocumentos);

            try
            {
                //El método GetFiles devuelve un array de objetos de tipo FileInfo
                //Separamos la recuperación de los archivos de la definición del query
                //para lograr la ejecución diferida 
                //Creamos un query que lista todos los archivos txt  
                //el segundo argumento le indica a GetFiles que busque en todos los 
                //subdirectorios de Mis Documentos

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


        private static IEnumerable<DirectoryInfo> RecuperarTodosMisDirectoriosYArchivos()
        {
            //Primero obtenemos la localización del directorio Mis Documentos 
            string misDocumentos = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            //Tomamos una imagen instantánea del directorio My Documents
            var dir = new DirectoryInfo(misDocumentos);


            //Ahora, obtengo recursivamente todos los subdirectorios 
            IEnumerable<DirectoryInfo> todosMisSubdirectorios = RecuperarTodosLosSubdirectorios(dir);
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


}
