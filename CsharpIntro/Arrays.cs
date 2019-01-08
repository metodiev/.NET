using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    class Arrays
    {
        public static void Main(string[] args)
        {
            int[] numbers = { 1, 29, 3, 54 };

            //numbers.Length - size
            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }

            Console.WriteLine(numbers[3]);

            int[] numbersLong = { 105050, 250059, 32962, 5462 };
            foreach (var item in numbersLong)
            {
                Console.WriteLine(item);
            }


            foreach (int item in numbersLong)
            {
                Console.WriteLine(item);
            }

            int[,] matrix = { {1, 2, 3, 4},
                              {1, 2, 3, 4}
                            };
            Console.WriteLine(matrix.Length);
            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    Console.WriteLine(matrix[row, col]);
                }
            }

            //home work play with matrix
            int [,,] matrixTripleDimension = {
                                                 {{ 1, 2, 3, 4 }},
                                                 {{ 1, 2, 3, 4 }},
                                                 {{ 1, 2, 3, 4 }}
            };


            for (int row = 0; row < matrixTripleDimension.GetLength(0); row++)
            {
                for (int col = 0; col < matrixTripleDimension.GetLength(1); col++)
                {
                    for (int z = 0; z < matrixTripleDimension.GetLength(2); z++)
                    {
                        Console.Write(matrixTripleDimension[row, col, z]);
                    }
                    Console.WriteLine();
                }
            }


        }   
    }
}
