using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    class Strings
    {
        public static void Main(string [] args)
        {
            string str = "Pesho Gosho Miro Kiro";
            //Console.WriteLine( str);

            //remove whitespaces
           str = str.Replace(" ", "");
            Console.WriteLine(str);
            

            int number = 5;
            string stringNumber = "10";
            string castStringToNumber = Convert.ToString(number);
            int castIntToString = Convert.ToInt32(stringNumber);

            //check if convertion is posible
            int anotherCast = 0;
            bool canStringcastToInt = Int32.TryParse(stringNumber,  out anotherCast);

            string specialCharacters = @"kalsjdflkasd \kiro\, ";
            string specialCharacters1 = "kalsjdflkasd \\kiro\\ \"asd\", ";
            Console.WriteLine(specialCharacters);
            Console.WriteLine(specialCharacters1);

            Console.WriteLine(castStringToNumber, castIntToString);

            string[] strArr = { "kiro", "pesho", "gosho" };
            for (int i = 0; i < strArr.Length; i++)
            {
                Console.WriteLine(strArr[i]);
            }

            foreach(string items in strArr)
            {
                Console.WriteLine(items);
            }

            string test = "This is a test string";
            /// test = test.Trim();
            //this is a way to print test string with charachters
            int countTLetter = 0;
            for (int i = 0; i < test.Length; i++)
            {
                if (test.ToLower()[i] == 't')
                {
                    countTLetter++;
                }
               
            }
            //Homew work play with to upper and to lower
            //check ASCII code table
            Console.WriteLine(countTLetter);

            char[] charArr = { 'A', 'B', 'a', 'b' };

            for (int i = 0; i < charArr.Length; i++)
            {
                //this is called concatenation if you want to concatenate some string and int use + sign
                Console.WriteLine("Charachter " + charArr[i] +  "is: " + (int)charArr[i]);
                //Console.WriteLine((int)charArr[i]);
            }

            //home work write with switch statement when type a number to show you letter from ASCII
            Console.ReadLine();
        }
    }
}
