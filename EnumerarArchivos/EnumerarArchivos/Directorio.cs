// ==++==// //   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.// // ==--==/*============================================================** Proyecto: Capitulo10_ColeccionesObservables** Clase:  Directorio** ** <OWNER>MarceVolta</OWNER>**** Propósito: Proveer ejemplos de código para el capítulo 11 del libro** "De Cabeza a C#"** Este proyecto provee ejemplos de Búsquedas (Queries) * Esta clase se creó para mostrar ejemplos de queries* sobre una colección de objetos de tipo Directorio que * representan directorios reales en el sistema de archivos* del equipo donde se ejecuta el código* La clase es modelada en base a la clase DirectoryInfo del framework* Posee entre otras, una propiedad llamada Archivos que es una colección* de objetos de tipo FileInfo que representan cada uno a un * archivo contenido por este directorio** Descripciones debajo en el summary===========================================================*/

using System.Collections.Generic;
using System.IO;

namespace EnumerarArchivos
{
    class Directorio
    {
        public DirectoryInfo EsteDirectorio { get; private set; }

        private IEnumerable<FileInfo> archivos;


        public string Nombre
        {
            get
            {
                return EsteDirectorio.Name;
            }
        }

        public string Ubicacion
        {
            get
            {
                return EsteDirectorio.FullName;
            }
        }

        /// <summary>
        /// Para adherir a las buenas prácticas, el constructor
        /// no inicializa (como es habitual) la propiedad Archivos
        /// ya que ésta operación puede ser costosa en recursos
        /// </summary>
        /// <param name="esteDirectorio"></param>
        public Directorio(DirectoryInfo esteDirectorio)
        {
            EsteDirectorio = esteDirectorio;
        }
        /// <summary>
        /// Esta propiedad es una colección de todos los archivos 
        /// que están contenidos en el directorio
        /// Ojo: No son todos los archivos contenidos en todos
        /// los sub-directorios de este directorio, sino solo 
        /// los que están contenidos directamente en este directorio
        /// Esta propiedad se llena cuando es consultada por primera vez
        /// </summary>
        public IEnumerable<FileInfo> Archivos
        {
            get
            {
                if (archivos == null)
                {
                    archivos = EsteDirectorio.GetFiles("*.*", SearchOption.TopDirectoryOnly);

                }
                return archivos;
            }


        }

    }
}
