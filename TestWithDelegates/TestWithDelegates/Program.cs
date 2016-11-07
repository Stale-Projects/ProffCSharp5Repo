using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWithDelegates
{

    class Mathematics
    {
        public static double Quadrupled(double param)
        {
            return param * 4;
        }

        public static double Squared(double param)
        {
            return param * param;
        }


    }
    class Program
    {

        public delegate double Operation(double x);
        public Operation[] ops =
        {
            Mathematics.Squared,
            Mathematics.Quadrupled

        };


        public void ProcessOperations(Operation op, double param)
        {
            Console.WriteLine("The result for {0} is {1}", op(param));
        }


        private delegate string GetAString();
        static void Main(string[] args)
        {
            int x = 10;
            GetAString GetIntAsString = new GetAString(x.ToString);
        Console.WriteLine("The number is {0}", GetIntAsString.Invoke());
            Console.ReadLine();
            
            
        }
}
}
