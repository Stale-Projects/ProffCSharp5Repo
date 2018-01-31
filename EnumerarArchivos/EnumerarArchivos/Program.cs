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
** de tipo Directorio y otras colecciones creadas para ejemplos más
** sencillos.
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

            #region Ejemplo #01-b: Operador Where con dos condiciones
            separador.EscribirEncabezado("Ejemplo #01-b: Operador Where con dos condiciones");
            var archivosTXTGrandes = from archivo in todosMisArchivos
                                     where (archivo.Extension == ".txt"
                                     && archivo.Length > 1000)
                                     select archivo;
            foreach (var archivoTXTGrande in archivosTXTGrandes)
            {
                Console.WriteLine("{0}: {1} bytes", archivoTXTGrande.Name, archivoTXTGrande.Length.ToString());
            }


            separador.EscribirPie("Fin Ejemplo 01-b");
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

            #region Ejemplo #02-b: Operador Where con métodos de extensión y más de una condición
            separador.EscribirEncabezado("Ejemplo #02-b: Operador Where con métodos de Extensión y más de una condición");
            var queryConDosCondiciones = todosMisArchivos.Where(f => (f.Extension == ".txt" && f.Name.Substring(0, 1).ToLower() == "a")).Select(f => f);
            foreach (FileInfo archivo in queryConDosCondiciones)
            {
                Console.WriteLine(archivo.Name);
            }

            separador.EscribirPie("Fin Ejemplo #02-b");
            #endregion

            #region Ejemplo #02-c: Operador Where con índice
            separador.EscribirEncabezado("Ejemplo #02-c: Operador Where con índice");

            //Un query donde utilizamos un índice
            var archivosTXT_IndicePar = todosMisArchivos.Where((x, indice) => x.Extension == ".txt" && indice % 2 == 0).Select(r => r);
            Console.WriteLine("Todos los archivos TXT cuyo índice sea par");
            var cuentaDeArchivosTXT_IndicePar = archivosTXT_IndicePar.Count();
            Console.WriteLine("Son {0} archivos", cuentaDeArchivosTXT_IndicePar.ToString());
            foreach (var archivoTXT in archivosTXT_IndicePar)
            {
                Console.WriteLine(archivoTXT.Name);
            }

            separador.EscribirPie("Fin Ejemplo #02-c");
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
            Console.WriteLine("Crea un nuevo archivo TXT y luego oprime ENTER...");
            Console.ReadLine();
            //CrearArchivo(misDocumentos);

            //Refresquemos el array de archivos, para traer todo lo nuevo 
            //Es necesario hacerlo desde la creación del DirectoryInfo
            dir = new DirectoryInfo(misDocumentos);
            todosMisArchivos = dir.GetFiles("*.*", SearchOption.AllDirectories);

            //Ejecutemos el query nuevamente, sin tocar la definición
            Console.WriteLine("Resultado luego de crear un archivo nuevo");
            Console.WriteLine("Encontramos {0} archivos", archivosTXT.ToArray<FileInfo>().Length.ToString());

            separador.EscribirPie("Fin Ejemplo #04b");
            #endregion

            #region Ejemplo #05: Operador From compuesto 
            separador.EscribirEncabezado("Operador From compuesto");

            //Primero creamos una lista de todos los subdirectorios de Mis Documentos
            List<DirectoryInfo> misDirectoryInfo = RecuperarTodosMisDirectorios().ToList<DirectoryInfo>();
            //Luego creamos una lista para contener los objetos Directorio correspondientes
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
                select directorio.Nombre;

            Console.WriteLine("Nombres de Directorios que contienen TXTs");
            foreach (var descripcion in directoriosConTXT)
            {
                Console.WriteLine(descripcion);
            }


            separador.EscribirPie("Fin ejemplo #05");
            #endregion

            #region Ejemplo #06: Ordenar resultados de Query
            separador.EscribirEncabezado("Ejemplo #06: Ordenar resultados de Query");

            var archivoMasReciente =
                (from archivo in archivosTXT
                 orderby archivo.CreationTime
                 select new { archivo.FullName, archivo.CreationTime })
                .Last();

            Console.WriteLine("\r\nEl archivo más reciente de tipo .txt es {0}. Creado en: {1}",
                archivoMasReciente.FullName, archivoMasReciente.CreationTime);

            separador.EscribirPie("Fin ejemplo #06");
            #endregion

            #region Ejemplo #06-b Ordenar con métodos de extensión
            separador.EscribirEncabezado("Ejemplo #06.b Ordenar con métodos de extensión");

            var archivoMasAntiguo = (archivosTXT.OrderBy(a => a.CreationTime).Select(a => new { a.FullName, a.CreationTime })).First();

            Console.WriteLine("\r\nEl archivo más antiguo de tipo .txt es {0}. Creado en: {1}",
                archivoMasReciente.FullName, archivoMasAntiguo.CreationTime);

            separador.EscribirPie("Fin ejemplo #06.b");
            #endregion

            #region Ejemplo #06-c: Más de un criterio de orden
            separador.EscribirEncabezado("Ejemplo #06-c: Más de un criterio de orden");

            var archivosOrdenados =
                (from archivo in archivosTXT
                 orderby archivo.CreationTime.ToShortDateString(), archivo.Name
                 select archivo.Name + " - " + archivo.CreationTime.ToShortDateString());

            Console.WriteLine("Archivos ordenados por fecha y nombre");
            foreach (var archivo in archivosOrdenados)
            {
                Console.WriteLine(archivo);
            }

            separador.EscribirPie("Fin ejemplo 06-c");
            #endregion

            #region Ejemplo #06-d: Más de un criterio de orden con métodos de extensión
            separador.EscribirEncabezado("Ejemplo #06-d: Más de un criterio de orden con métodos de extensión");

            var archivosOrdenadosV2 = archivosTXT.OrderBy(f => f.CreationTime.ToShortDateString()).ThenBy(f => f.Name).Select(f => f.Name + " - " + f.CreationTime.ToShortDateString());

            Console.WriteLine("Archivos ordenados por fecha y nombre, usando métodos de extensión");
            foreach (var archivo in archivosOrdenadosV2)
            {
                Console.WriteLine(archivo);
            }


            separador.EscribirPie("Fin Ejemplo #06-d");
            #endregion

            #region Ejemplo #07: Agrupamiento con group by
            separador.EscribirEncabezado("Ejemplo #07: Agrupamiento con group by");

            var agrupadosPorNombre =
                (from archivo in archivosTXT
                 group archivo by archivo.Name.Substring(0, 1).ToLower() into grupos
                 orderby grupos.Count() descending, grupos.Key
                 select new { Inicial = grupos.Key, Cuenta = grupos.Count() });

            Console.WriteLine("Cuantos archivos hay en cada grupo por inicial de nombre");
            Console.WriteLine("Contamos sólo aquellos grupos que tengan al menos un archivo");
            foreach (var grupo in agrupadosPorNombre)
            {
                Console.WriteLine("Inicial: {0}, Cuenta: {1}", grupo.Inicial, grupo.Cuenta.ToString());
            }


            separador.EscribirPie("Fin ejemplo #07");
            #endregion

            #region Ejemplo #08: Agrupamiento con métodos de extensión
            separador.EscribirEncabezado("Ejemplo #08: Agrupamiento con métodos de extensión");

            var agrupadosPorNombre_v2 = archivosTXT.GroupBy(a => a.Name.Substring(0, 1).ToLower()).OrderByDescending(g => g.Count()).ThenBy(g => g.Key).Where(g => g.Count() > 0).Select(g => new { cuantos = g.Count(), inicial = g.Key });
            foreach (var grupo in agrupadosPorNombre_v2)
            {
                Console.WriteLine("Inicial: {0}, Cuenta: {1}", grupo.inicial, grupo.cuantos.ToString());
            }

            separador.EscribirPie("Fin Ejemplo #08");

            #endregion

            #region Ejemplo #09: Agrupamiento con objetos anidados
            separador.EscribirEncabezado("Ejemplo #09: Agrupamiento con objetos anidados");

            var agrupadosYAnidados =
                (from archivo in archivosTXT
                 group archivo by archivo.Name.Substring(0, 1).ToLower() into grupos
                 orderby grupos.Count() descending, grupos.Key
                 select new
                 {
                     Inicial = grupos.Key,
                     Cuenta = grupos.Count(),
                     Nombres = (from nombresDeArchivo in grupos
                                orderby nombresDeArchivo.Name
                                select nombresDeArchivo.Name)
                 });

            Console.WriteLine("Cuantos archivos hay en cada grupo por inicial de nombre");
            Console.WriteLine("Contamos sólo aquellos grupos que tengan al menos un archivo");
            foreach (var grupo in agrupadosYAnidados)
            {
                Console.WriteLine("Inicial: {0}, Cuenta: {1}", grupo.Inicial, grupo.Cuenta.ToString());
                Console.WriteLine("Nombres: ");
                foreach (var nombre in grupo.Nombres)
                {
                    Console.WriteLine(nombre);
                }
            }


            separador.EscribirPie("Fin ejemplo #09");
            #endregion

            #region Ejemplo #10: Uso de Join - Queries separados
            separador.EscribirEncabezado("Ejemplo #10: Uso de Join - Queries separados");

            var archivosDOCX = from cualquierArchivo in todosMisArchivos
                               where cualquierArchivo.Extension == ".docx"
                               select new { Nombre = NombreSinExtension(cualquierArchivo.Name), fecha = cualquierArchivo.CreationTime.ToShortDateString() };

            var archivosPDF = from cualquierArchivo in todosMisArchivos
                              where cualquierArchivo.Extension == ".pdf"
                              select new { Nombre = NombreSinExtension(cualquierArchivo.Name), fecha = cualquierArchivo.CreationTime.ToShortDateString() };

            var archivosHomonimos = from docx in archivosDOCX
                                    join pdf in archivosPDF on docx.Nombre equals pdf.Nombre
                                    select new { Nombre = docx.Nombre, FechaDOCX = docx.fecha, FechaPDF = pdf.fecha };

            foreach (var homonimo in archivosHomonimos)
            {
                Console.WriteLine("Nombre: '{0}' - Fecha DOCX: {1} - Fecha PDF: {2}", homonimo.Nombre, homonimo.FechaDOCX, homonimo.FechaPDF);
            }

            separador.EscribirPie("Fin ejemplo #10");
            #endregion

            #region Ejemplo #11 - Todo en el mismo query
            separador.EscribirEncabezado("Ejemplo #11 - Todo en el mismo query");

            var ArchivosCompartidos = from unArchivoDocx in
                                      (from unArchivoDOCX in todosMisArchivos
                                       where unArchivoDOCX.Extension == ".docx"
                                       select new { Nombre = NombreSinExtension(unArchivoDOCX.Name), fecha = unArchivoDOCX.CreationTime.ToShortDateString() })
                                      join unArchivoPdf in
                                      (from unArchivoPDF in todosMisArchivos
                                       where unArchivoPDF.Extension == ".pdf"
                                       select new { Nombre = NombreSinExtension(unArchivoPDF.Name), fecha = unArchivoPDF.CreationTime.ToShortDateString() })
                                      on unArchivoDocx.Nombre equals unArchivoPdf.Nombre
                                      select new { Nombre = unArchivoDocx.Nombre, FechaDOCX = unArchivoDocx.fecha, FechaPDF = unArchivoPdf.fecha };

            foreach (var archivo in ArchivosCompartidos)
            {
                Console.WriteLine("Nombre: '{0}' - Fecha DOCX: {1} - Fecha PDF: {2}", archivo.Nombre, archivo.FechaDOCX, archivo.FechaPDF);
            }

            separador.EscribirPie("Fin Ejemplo #11");
            #endregion

            #region Ejemplo #12 - Left Outer Join
            separador.EscribirEncabezado("Ejemplo #12 - Left Outer Join");

            Console.WriteLine("Todos los DOCX, y los PDF homónimos");
            Console.WriteLine();
            var todosLosDocxYSusPdf = from docx in archivosDOCX
                                      join pdf in archivosPDF on docx.Nombre equals pdf.Nombre into docx_pdf
                                      from pdf in docx_pdf.DefaultIfEmpty()
                                      select new { Nombre = docx.Nombre, FechaDOCX = docx.fecha, FechaPDF = (pdf == null ? "No hay PDF homónimo" : pdf.fecha) };

            foreach (var cadaArchivoDOCX in todosLosDocxYSusPdf)
            {
                Console.WriteLine("Nombre: '{0}' - Fecha DOCX: {1} - Fecha PDF: {2}", cadaArchivoDOCX.Nombre, cadaArchivoDOCX.FechaDOCX, cadaArchivoDOCX.FechaPDF);
            }



            Console.WriteLine();
            Console.WriteLine("Todos los PDFs y los DOCX homónimos");
            Console.WriteLine();

            var todosLosPdfYSusDocx = from pdf in archivosPDF
                                      join docx in archivosDOCX on pdf.Nombre equals docx.Nombre into docx_pdf
                                      from docx in docx_pdf.DefaultIfEmpty()
                                      select new { Nombre = pdf.Nombre, FechaPDF = pdf.fecha, FechaDOCX = (docx == null ? "No hay DOCX homónimo" : docx.fecha) };

            foreach (var cadaArchivoPDF in todosLosPdfYSusDocx)
            {
                Console.WriteLine("Nombre: '{0}' - Fecha PDF: {1} - Fecha DOCX: {2}", cadaArchivoPDF.Nombre, cadaArchivoPDF.FechaPDF, cadaArchivoPDF.FechaDOCX);
            }


            separador.EscribirPie("Fin Ejemplo #12");
            #endregion

            #region Ejemplo #13 - Group Join
            separador.EscribirEncabezado("Ejemplo #13 - Group Join");

            //Inferencia de Tipos para crear una lista de objetos sin clase
            //creados de forma anónima
            var valoresPadre = new[] { new { Id = 1, ValorPadre = "A" },
            new { Id = 2, ValorPadre = "B" },
            new { Id = 3, ValorPadre = "C" } }.ToList();

            var valoresHijo = new[] {new {Id = 1, ValorHijo="a1"},
            new {Id = 1, ValorHijo="a2"}, new {Id = 1, ValorHijo="a3"},
            new {Id = 2, ValorHijo="b1"}, new {Id = 2, ValorHijo="b2"}}.ToList();


            Console.WriteLine();
            Console.WriteLine("Inner Join");
            Console.WriteLine();


            //Inner Join
            var tablaPlana = from p in valoresPadre
                             join h in valoresHijo on p.Id equals h.Id
                             select new { Id = p.Id, ValorPadre = p.ValorPadre, ValorHijo = h.ValorHijo };

            foreach (var fila in tablaPlana)
            {
                Console.WriteLine("Id:{0} - ValorPadre:{1} - ValorHijo:{2}",
                  fila.Id, fila.ValorPadre, fila.ValorHijo);
            }

            Console.WriteLine();
            Console.WriteLine("Left Outer Join");
            Console.WriteLine();

            //Left Outer Join
            var tablaPlanaOJ = from p in valoresPadre
                               join h in valoresHijo on p.Id equals h.Id into g
                               from ph in g.DefaultIfEmpty()
                               select new { Id = p.Id, ValorPadre = p.ValorPadre, ValorHijo = (ph == null ? "Nulo" : ph.ValorHijo) };

            foreach (var fila in tablaPlanaOJ)
            {
                Console.WriteLine("Id:{0} - ValorPadre:{1} - ValorHijo:{2}",
                  fila.Id, fila.ValorPadre, fila.ValorHijo);
            }


            Console.WriteLine();
            Console.WriteLine("Group Join");
            Console.WriteLine();


            //Group Join
            var tablaGJ = from p in valoresPadre
                          join h in valoresHijo on p.Id equals h.Id into g
                          select new { Id = p.Id, ValorPadre = p.ValorPadre, ValorHijo = g };

            string grupoValoresHijo;
            foreach (var fila in tablaGJ)
            {
                if (fila.ValorHijo.Count() == 0)
                {
                    grupoValoresHijo = "Nulo";
                }
                else
                {
                    grupoValoresHijo = "[";
                    foreach (var hijo in fila.ValorHijo)
                    {
                        grupoValoresHijo += hijo.ValorHijo + ", ";
                    }
                    grupoValoresHijo = grupoValoresHijo.Substring(0, grupoValoresHijo.Length - 2);
                    grupoValoresHijo += "]";
                }
                Console.WriteLine("Id:{0} - Valor Padre:{1} - Valores Hijo:{2}",
                  fila.Id, fila.ValorPadre, grupoValoresHijo);
            }

            separador.EscribirPie("Fin Ejemplo #13");
            #endregion

            #region Ejemplo #14 - Operadores de Conjuntos
            separador.EscribirEncabezado("Ejemplo #14 - Operadores de Conjuntos");

            int[] multiplosDeDos = new int[] { 0, 2, 4, 6, 8, 10, 12, 14, 16, 18 };
            int[] multiplosDeTres = new int[] { 0, 3, 6, 9, 12, 15, 18, 21, 24, 27 };

            var multiplosDeDosYTres = multiplosDeDos.Intersect(multiplosDeTres);
            Console.WriteLine("Intersect - Múltiplos de 2 y 3");
            foreach (var entero in multiplosDeDosYTres)
            {
                Console.WriteLine(entero.ToString());
            }
            Console.WriteLine();


            var multiplosDeDosYMultiplosDeTres = multiplosDeDos.Union(multiplosDeTres);
            Console.WriteLine("Union - Múltiplos de 2 y Múltiplos de 3");
            foreach (var entero in multiplosDeDosYMultiplosDeTres)
            {
                Console.WriteLine(entero.ToString());
            }
            Console.WriteLine();

            var multiplosDeDosPeroNoDeTres = multiplosDeDos.Except(multiplosDeTres);
            Console.WriteLine("Except - Múltiplos de 2 que no son múltiplos de 3");

            foreach (var entero in multiplosDeDosPeroNoDeTres)
            {
                Console.WriteLine(entero.ToString());
            }
            Console.WriteLine();

            var multiplosDeTresPeroNoDeDos = multiplosDeTres.Except(multiplosDeDos);
            Console.WriteLine("Except - Múltiplos de 3 que no son múltiplos de 2");

            foreach (var entero in multiplosDeTresPeroNoDeDos)
            {
                Console.WriteLine(entero.ToString());
            }
            Console.WriteLine();

            separador.EscribirPie("Fin Ejemplo #14");
            #endregion

            #region Ejemplo #15 - ZIP
            separador.EscribirEncabezado("Ejemplo #15 - ZIP");

            string[] poligonos = new string[] { "triángulo", "rectángulo", "pentágono", "hexágono", "heptágono", "octágono", "eneágono" };
            int[] lados = new int[] { 3, 4, 5, 6, 7, 8, 9, 10 };

            var descripcionesDePoligonos = poligonos.Zip(lados,
                (p, l) => p + ": " + l.ToString() + " lados");

            foreach (var descripcion in descripcionesDePoligonos)
            {
                Console.WriteLine(descripcion);
            }

            separador.EscribirPie("Fin Ejemplo #15");
            #endregion

            #region Ejemplo #16 - Solución completa de Paginado!
            separador.EscribirEncabezado("Ejemplo #16 - Paginado");


            FileInfo[] misArchivos = RecuperarTodosMisArchivos();
            var misTXT = from archivo in todosMisArchivos
                         where archivo.Extension == ".txt"
                         select archivo;

            Console.WriteLine("Ejemplo de Paginado");
            Console.WriteLine("Oprime una tecla para comenzar");
            Console.ReadKey();
            var misTXTEnCache = misTXT.ToList();

            int largoDePagina = 5;
            int numeroDeItems = misTXT.Count();
            int paginaActual = 1;
            int numeroDePaginas = (int)Math.Ceiling(numeroDeItems / (double)largoDePagina);
            MostrarPagina(misTXT, largoDePagina, paginaActual);
            bool salir = false;
            while (salir == false)
            {
                ConsoleKeyInfo teclaIngresada = Console.ReadKey();
                switch (teclaIngresada.Key)
                {
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.PageDown:
                        if (paginaActual < numeroDePaginas)
                        {
                            paginaActual++;

                        }
                        else
                        {
                            Console.Beep();
                        }
                        break;

                    case ConsoleKey.UpArrow:
                    case ConsoleKey.PageUp:
                        if (paginaActual > 1)
                        {
                            paginaActual--;

                        }
                        else
                        {
                            Console.Beep();
                        }
                        break;

                    case ConsoleKey.Escape:
                        salir = true;
                        break;
                    default:
                        Console.Beep();
                        break;
                }
                MostrarPagina(misTXT, largoDePagina, paginaActual);
            }

            separador.EscribirPie("Fin Ejemplo #16");
            #endregion

            #region Ejemplo #17 - Operadores de Agregado dentro de un query
            separador.EscribirEncabezado("Ejemplo #17 - Operadores de Agregado dentro de un query");
            //La variable misDirectorios ya fue definida en el Ejempo #5 como una List<Directorios>
            //Una de las propiedades de la clase Directorio es la colección de archivos que contiene: Directorio.Archivos
            //la cual es del tipo IEnumerable<FileInfo>


            var cuentaDeTXT =
                from directorio in misDirectorios
                let cuantosTXT = (from archivo in directorio.Archivos
                                  where archivo.Extension == ".txt"
                                  select archivo.CreationTime).Count()
                where cuantosTXT > 0
                select new { Nombre = directorio.Ubicacion, CuantosTXT = cuantosTXT };

            foreach (var item in cuentaDeTXT)
            {
                Console.WriteLine("{0}: {1}", item.Nombre, item.CuantosTXT.ToString());
            }

            separador.EscribirPie("Fin Ejemplo #17");
            #endregion

            #region Ejemplo #18 - Ejemplo simple de Aggregate
            separador.EscribirEncabezado("Ejemplo #18 - Ejemplo simple de Aggregate");

            var semana = new string[] { "lunes", "martes", "miércoles", "jueves", "viernes" };
            string agregada = semana.Aggregate(
                    (x, y) =>
                    x + ", " + y);
            Console.WriteLine(agregada);


            separador.EscribirPie("Fin Ejemplo #18");
            #endregion

            return;

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


        /// <summary>
        /// Esta función recupera todos los directorios contenidos dentro del directorio Mis Documentos
        /// </summary>
        /// <returns>IEnumerable&lt;DirectoryInfo&gt; Una colección con todos los directorios</returns>
        private static IEnumerable<DirectoryInfo> RecuperarTodosMisDirectorios()
        {
            //Primero obtenemos la localización del directorio Mis Documentos 
            string misDocumentos = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

            //Tomamos una imagen instantánea del directorio My Documents
            var dir = new DirectoryInfo(misDocumentos);


            //Ahora, obtengo recursivamente todos los subdirectorios 
            IEnumerable<DirectoryInfo> todosMisSubdirectorios = dir.GetDirectories("*.*", SearchOption.AllDirectories);
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
        /// no sabía que habia una sobrecarga de GetDirectories() que tiene esa funcionalidad
        /// </summary>
        /// <param name="directorio">string. El directorio del cual quiero recuperar todos los subdirectorios</param>
        /// <returns>IEnumerable&lt;DirectoryInfo&gt; Una colección con todos los subdirectorios del directorio pasado como parámetro</returns>
        private static IEnumerable<DirectoryInfo> RecuperarTodosLosSubdirectorios(DirectoryInfo directorio)
        {
            IEnumerable<DirectoryInfo> _subdirectorios = directorio.GetDirectories();

            foreach (var subdirectorio in _subdirectorios)
            {
                _subdirectorios = _subdirectorios.Concat(RecuperarTodosLosSubdirectorios(subdirectorio));
            }
            return _subdirectorios;
        }

        /// <summary>
        /// Esta función permite crear un archivo de texto
        /// con un contenido fijo, sirve como auxiliar en los ejemplos
        /// </summary>
        /// <param name="path">string, el directorio donde se creará el archivo</param>
        private static void CrearArchivo(string path)
        {
            string nombreDeArchivo = path + @"\txt_" + DateTime.Now.ToString(format: "yyyymmdd hhmmss") + ".txt";
            string contenido = "Un contenido de prueba para poner en el arvhivo";
            BinaryWriter bw = new BinaryWriter(new FileStream(nombreDeArchivo, FileMode.Create), System.Text.Encoding.ASCII);
            bw.Write(contenido);
            bw.Dispose();

        }

        /// <summary>
        /// Esta función extrae el nombre de uun archivo quitándole la extensión. 
        /// Si el nombre no tiene extensión, retorna el mismo nombre sin modificar
        /// </summary>
        /// <param name="nombreDeArchivo">El nombre del archivo incluyendo la extensión</param>
        /// <returns></returns>
        private static string NombreSinExtension(string nombreDeArchivo)
        {
            int posicion = nombreDeArchivo.IndexOf(".");
            if (posicion >= 0)
            {
                nombreDeArchivo = nombreDeArchivo.Substring(0, posicion);
            }
            return nombreDeArchivo;
        }

        /// <summary>
        /// Esta función muestra como paginar usando Skip y Take
        /// a partir de una colección de objetos FileInfo. Se utiliza en el Ejemplo #16
        /// </summary>
        /// <param name="archivos">Una colección de objetos FileInfo obtenida de una query LINQ</param>
        /// <param name="largoDePagina">Cuántos archivos muestro en cada página</param>
        /// <param name="paginaActual">Cuál es la página que vamos a mostrar en este momento</param>
        private static void MostrarPagina(IEnumerable<FileInfo> archivos, int largoDePagina, int paginaActual)
        {
            int numeroDePaginas = (int)Math.Ceiling(archivos.Count() / (double)largoDePagina);
            Console.Clear();

            var pagina = archivos.Skip(largoDePagina * (paginaActual - 1)).Take(largoDePagina);
            Console.WriteLine("Página {0} de {1}", paginaActual.ToString(), numeroDePaginas.ToString());
            Console.WriteLine("-----------------------------------------");
            foreach (var archivo in pagina)
            {
                Console.WriteLine(archivo.Name);
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
