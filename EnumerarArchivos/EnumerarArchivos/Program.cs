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
            //Primero obtenemos la localización del directorio Mis Documentos 
            string misDocumentos = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            //Tomamos una imagen instantánea del directorio My Documents
            var dir = new DirectoryInfo(misDocumentos);

            try
            {

                var nombres = new List<string> { "Alberto", "Carlos", "Cintia", "Pedro", "Laura" };
                var nombresConA = (from n in nombres
                                   where n.StartsWith("A")
                                   orderby n
                                   select n).ToList<string>();

                Console.WriteLine("Primera pasada");
                foreach (string name in nombresConA)
                {
                    Console.WriteLine(name);
                }
                Console.WriteLine();
                nombres.Add("Alejandra");
                nombres.Add("Pablo");
                nombres.Add("Andrés");
                nombres.Add("David");
                Console.WriteLine("Segunda Pasada");


                foreach (string name in nombresConA)
                {
                    Console.WriteLine(name);


                }
                return;
                //El método GetFiles devuelve un array de objetos de tipo FileInfo
                //Separamos la recuperación de los archivos de la definición del query
                //para lograr la ejecución diferida 
                //Creamos un query que lista todos los archivos txt  
                //el segundo argumento le indica a GetFiles que busque en todos los 
                //subdirectorios de Mis Documentos

                FileInfo[] todosMisArchivos = dir.GetFiles("*.*", SearchOption.AllDirectories);
                var buscarArchivosTXT =
                    from archivo in todosMisArchivos
                    where archivo.Extension == ".txt"
                    orderby archivo.Name
                    select archivo;

                //Sólo al llegar aquí se ejecuta el query  
                Console.WriteLine("Resultado Inicial");

                foreach (FileInfo archivo in buscarArchivosTXT)
                {
                    Console.WriteLine(archivo.FullName);
                }

                //Creemos un nuevo archivo para complicar las cosas
                CrearArchivo(misDocumentos);

                //Reconstruyamos el array para traer los archivos nuevos
                todosMisArchivos = dir.GetFiles("*.*", SearchOption.AllDirectories);

                //Ejecutemos el query nuevamente, sin tocar la definición
                Console.WriteLine("Resultado luego de crear un archivo nuevo");
                foreach (FileInfo archivo in buscarArchivosTXT)
                {
                    Console.WriteLine(archivo.FullName);
                }



                //Un modo alternativo de ejecutar el mismo query
                var buscarUnaVezMas = dir.GetFiles("*.*", SearchOption.AllDirectories).Where(f => f.Extension == ".txt").OrderBy(f => f.Name).Select(f => f);

                //Ejecutar el query  
                foreach (FileInfo archivo in buscarUnaVezMas)
                {
                    Console.WriteLine(archivo.FullName);
                }


                // Usando como base la búsqueda anterior, creamos y ejecutamos otra búsqueda 
                // que nos diga: cuántos son, y cuál es el más nuevo
                // Aquí no se ejecuta la búsqueda hasta que no se produce la llamada a Last()

                var archivoMasReciente =
                    (from archivo in buscarArchivosTXT
                     orderby archivo.CreationTime
                     select new { archivo.FullName, archivo.CreationTime })
                    .Last();

                Console.WriteLine("\r\nEl archivo más reciente de tipo .txt es {0}. Creado en: {1}",
                    archivoMasReciente.FullName, archivoMasReciente.CreationTime);

                //Creamos un nuevo archivo TXT para producir un resultado diferente en el query
                CrearArchivo(misDocumentos);

                var archivoRecienCreado =
                    (from archivo in buscarArchivosTXT
                     orderby archivo.CreationTime
                     select new { archivo.FullName, archivo.CreationTime })
                    .Last();

                Console.WriteLine("\r\nEste es el archivo que acabamos de crear: {0}. Creado en: {1}",
                    archivoRecienCreado.FullName, archivoRecienCreado.CreationTime);


            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }

            string ejemplo = "murciélago";
            int vocales = ejemplo.CuantasVocalesTienes();
            //int vocales = StringExtension.CuantasVocalesTienes(ejemplo);

            Console.WriteLine(@"'murciélago' tiene {0} vocales", vocales.ToString());

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

    public static class StringExtension
    {
        public static int CuantasVocalesTienes(this string s)
        {
            //Código aquí para retornar el número de vocales
            return 5;
        }
    }
}
