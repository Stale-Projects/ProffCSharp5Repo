using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    class Program
    {
        static void Main(string[] args)
        {
            HisBaseClass myInstanceClass = new MyDerivedClass("Ponele");
            myInstanceClass.HisMethod1();
            //myInstanceClass.MyMethod1(); No se puede llamar 
            myInstanceClass.MyGroovyMethod();
            myInstanceClass.UnMetodoVirtual();

            MyDerivedClass myDerivedInstanceClass = new MyDerivedClass("Fijate");
            myDerivedInstanceClass.HisMethod1();
            myDerivedInstanceClass.MyMethod1();
            myDerivedInstanceClass.MyGroovyMethod();
            myDerivedInstanceClass.UnMetodoVirtual();
        }
    }
}
