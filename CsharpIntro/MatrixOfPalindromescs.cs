using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    class MatrixOfPalindromescs
    {
        public static void Main(string [] args)
        {
            int row = 3;
            int col = 2;
            matrixOfPalindromescs(row, col);
            Console.ReadLine();
        }

        private static void matrixOfPalindromescs(int row, int col)
        {
            //int unicode = 97;
            //char character = (char)unicode;
            //string text = character.ToString();

            int[] unicodeArr = new int[col+4];
            for (int i = 0; i < unicodeArr.Length; i++)
            {
                unicodeArr[i] = 97 + i;
            }
           // Console.WriteLine(text);

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    for (int p = 0; p < 3; p++)
                    {
                        //if (j == 0)
                        //{
                        //    char character = (char)unicodeArr[i];
                        //    string text = character.ToString();
                        //    Console.Write(text);

                        //}

                        if (p != 1)
                        {
                            char character = (char)unicodeArr[i];
                            string text = character.ToString();
                            Console.Write(text);

                        }

                        if (p == 1)
                        {
                            char character = (char)unicodeArr[i+j];
                            string text = character.ToString();
                            Console.Write(text);
                        }
                      

                    }

                    Console.Write(" ");
                }
                Console.WriteLine();
            }


        }
    }
}
