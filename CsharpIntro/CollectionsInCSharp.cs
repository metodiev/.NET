using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class CollectionsInCSharp
    {
        static void Main(string[] args)
        {
            //float numbers
            int peshoApples = 416516515;
            float devideApples= (float)peshoApples / (float)10.0000000;
            float floatNumber = 101.415151f;
            Console.WriteLine(devideApples);
            float pi = 3.14f; // - the precision is 7 digits
            Console.WriteLine(pi);
            ////the precision is 14 digits
            double doubleNumber = 12.4151616516516511;
            Console.WriteLine(doubleNumber);

            Console.WriteLine("kiro e {1} zashtoto ima {0}", floatNumber, doubleNumber);

            //List
            int[] numbers = new int[100];

            var list = new List<String>();
            list.Add("Str");

            for (int i = 0; i < 100; i++)
            {
                list.Add("Test: " + i);
            }

            foreach (string item in list)
            {
                //Console.WriteLine(item);
            }
            //  int p = 10;

         
            //var list1 = CreateList(o, o1);

            //var listGenerics = new List<>();

            Console.ReadLine();  
        }

        public static List<T> CreateList<T>(params T[] elements)
        {
            return new List<T>(elements);
        }

    }
}

