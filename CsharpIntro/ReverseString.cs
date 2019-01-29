using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collections
{
    class ReverseString
    {
        public static void Main(string [] args)
        {
            //Learning Java
            string [] input = Console.ReadLine().Split(' ');
            
            Stack<string> stack = new Stack<string>();
            foreach (var item in input)
            {
                stack.Push(item);
            }

            int stackSize = stack.Count();
            for (int i = 0; i < stackSize; i++)
            {
                Console.Write(reverseWord(stack.Pop()));
                Console.Write(" ");
            }

            Console.ReadLine();
            
        }

        private static string reverseWord(string v)
        {
            char[] arr = new char[v.Length];
            int counter = 0;
            for (int i = v.Length-1; i >= 0; i--)
            {
                arr[counter] = v[i];
                counter++;
            }

            string retrunStr = new string(arr);
            return retrunStr;
        }
    }
}
