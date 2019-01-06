using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    class MathematicalOperations
    {
        public static void Main(string[] args)
        {
            //this is comment in C#
            /***
             * Write a program to sum two numbers
             */
            Console.WriteLine("Enter number one:");
            int numberOne = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter a number two:");
            int numberTwo = int.Parse(Console.ReadLine());
            //generate new method with ctrl and dot shorcut
            Console.WriteLine(sumTwoNumbers(numberOne, numberTwo) );

            //Home work write logics with another operations
            //use methods
        }

        
        private static int sumTwoNumbers(int numberOne, int numberTwo)
        {
            if ((numberOne < 0 || numberTwo < 0) )
            {
                Console.WriteLine("Invalid input");
                return 0;
            }
            return numberTwo + numberOne;
        }
    }

  
}
