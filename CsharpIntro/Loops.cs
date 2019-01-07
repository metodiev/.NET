using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    class Loops
    {
        public static void Main(string [] args)
        {
            Console.WriteLine("Enter number: ");
            int number = int.Parse(Console.ReadLine());
            forLoop(number);
            whileCycle(number);
            doWhileCycle(number);
            Console.ReadLine();

            //Home work reverse all cycles
            
        }

        private static void doWhileCycle(int number)
        {
            do
            {
                Console.WriteLine(number);
                number--;
            } while (number > 0);
        }

        private static void whileCycle(int number)
        {
            while (number > 0)
            {
                Console.WriteLine(number);
                number--;
            }
        }

        private static void forLoop(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Console.Write(i);
            }
        }
    }
}
