using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    class HisBaseClass
    {
        public HisBaseClass(string stringToInsert)
        {
            Console.WriteLine("Esta es la string insertada: {0}", stringToInsert);
        }
        public void HisMethod1()
        {
            Console.WriteLine("This is a call to HisBaseClass.Method1");
        }

        public void MyGroovyMethod()
        {
            Console.WriteLine("HisBaseClass.MyGroovyMethod");
        }

        public virtual void UnMetodoVirtual()
        {
            Console.WriteLine("Un metodo virtual llamado desde la base class");
        }
    
    }

}
