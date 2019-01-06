using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    class Statments
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Enter your current age");
            int age = int.Parse(Console.ReadLine());

            checkUserAge(age);

            switch (checkUserAge(age))
            {
                case true:
                    printAllowSiteMessage();
                    break;

                case false:
                    printWarningSiteMessage();
                    break;

                default:
                    break;
            }

            //switch statment choice number
            Console.WriteLine("Enter a number:");
            int number = int.Parse(Console.ReadLine());

            switch (number)
            {
                case 1:
                    Console.WriteLine("one");
                    break;

                case 2:
                    Console.WriteLine("Two");
                   // Console.ReadLine();
                    break;

                case 3:
                    Console.WriteLine("Three");
                    break;

                case 4:
                    Console.WriteLine("Four");
                    break;

                default:
                    break;
            }

            switch (number)
            {
                case 1:
                    Console.WriteLine("One");
                    break;
                case 2:

                case 3:
                 

                case 4:
                    Console.WriteLine("Four");
                    Console.ReadLine();
                    break;
      
            }

           //Home work write a coupe if and switch statments

        }

        //warning site message
        private static void printWarningSiteMessage()
        {
            Console.WriteLine("Warning you dont have enougn age to be here please visit www.cartoonnetwork.com");
        }

        //sucessfull site message
        private static void printAllowSiteMessage()
        {
            Console.WriteLine("Sucessfull login");
        }

        //check current age if the user
        private static bool checkUserAge(int age)
        {
            if (age >= 18)
            {
                return true;
            }

            return false;
        }
    }
}
