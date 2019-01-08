using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    class Recursion
    {
        public static void Main(string [] args)
        {
            int numberOfRecursion = 11;
            long sum = sumOfRecursion(numberOfRecursion);
            Console.WriteLine(sum);
            

            long fibonacci = fibonacciReqursion(numberOfRecursion);
            Console.WriteLine(fibonacci);
            Console.ReadLine();
        }

        private static long fibonacciReqursion(int numberOfRecursion)
        {
            if(numberOfRecursion == 0)
            {
                return 0;
            }
            if(numberOfRecursion ==1)
            {
                return 1;
            }
            else 
            {
                return  fibonacciReqursion(numberOfRecursion - 1) + fibonacciReqursion(numberOfRecursion - 2);
            }
            //home work check fibonacci formula and create it without recursion
        }

        private static long sumOfRecursion(int numberOfRecursion)
        {
            if (numberOfRecursion == 1)
            {
                return numberOfRecursion;
            }

            return numberOfRecursion + sumOfRecursion(numberOfRecursion - 1) ;
        }
    }
}
