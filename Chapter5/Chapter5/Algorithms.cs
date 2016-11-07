using System.Collections.Generic;

namespace Chapter5
{
    class Algorithms
    {
        //Esta función static toma una colección de objetos Account 
        //y calcula el total de los balances
        //con la condición que implemente IEnumerable para poder
        //usar foreach(). Aquí lo que es genérico es la colección en sí
        //pero hay coupling con el objeto Account
        public static decimal SimpleAccumulate(IEnumerable<Account> accounts)
        {
            decimal amount = 0;
            foreach (Account account in accounts)
            {
                amount += account.Balance;
            }
            return amount;
        }
    }
}
