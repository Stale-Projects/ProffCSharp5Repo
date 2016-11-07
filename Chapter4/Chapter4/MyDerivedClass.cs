using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    class MyDerivedClass : HisBaseClass
    {
        public MyDerivedClass(string stringToInsert): base(stringToInsert)
        {
            Console.WriteLine("Constructor de MyDerivedClass con esta string insertada: {0}", stringToInsert);
        }
        public void MyMethod1()
        {
            Console.WriteLine("A call to MyDerivedClass.MyMethod1");
        }

        public new void MyGroovyMethod()
        {
            Console.WriteLine("Groovy method from MyDerivedClass");
        }
    }
}
