using System;
using System.Linq.Expressions;

namespace VisualizadorDeArbolesDeExpresion
{
    class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int, int, int>> expresion = (x, y) => x + y;
            Console.WriteLine(expresion);

            for (int i = 0; i < expresion.Parameters.Count; i++)
            {
                Console.WriteLine("Parámetro {0}, Nombre: {1} Tipo: {2}", i.ToString(),
                    expresion.Parameters[i].Name, expresion.Parameters[i].Type);
            }

            int resultado = expresion.Compile()(3, 5);
            Console.WriteLine(resultado.ToString());

            var query = from c in db.Clientes
                        where c.Ciudad == "Caracas"
                        select new { c.Ciudad, c.NombreDeEmpresa };

        }
    }
}
