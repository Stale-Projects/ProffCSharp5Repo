// ==++==
// 
//   Copyright (c) S. Marcelo Volta.  Todos los derechos reservados.
// 
// ==--==
/*============================================================
** Proyecto: Capitulo11_LINQ
** Clase:  Premio
** 
** <OWNER>MarceVolta</OWNER>
**
** Propósito: Modelar premios Nobel otorgados desde 1901 
** para servir de ejemplo en el libro "De Cabeza a C#"
** Esta clase se usa para proveer ejemplos de sentencias LINQ
** Descripciones debajo en el summary
===========================================================*/


namespace Cap11_LINQ
{
    /// <summary>
    /// Esta clase encapsula propiedades básicas de premios Nobel
    /// otorgados desde 1901
    /// </summary>
    class Premio
    {
        public int Año { get; private set; }
        public string Categoria { get; private set; }
        public int NumeroDeLaureados { get; private set; }

        public Premio(int año, string categoria, int numeroDeLaureados)
        {
            Año = año;
            Categoria = categoria;
            NumeroDeLaureados = numeroDeLaureados;
        }

    }
}
