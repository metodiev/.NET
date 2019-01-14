using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    class Matrix_exercise
    {
        public static void Main(String [] args)
        {
           // patternA(4);
            patternB(4);
            //Console.ReadLine();
            patternMitko(4);
            Console.ReadLine();
        }

        public static void patternA(int numOfMatrix)
        {
            int[,] matrix = new int [numOfMatrix, numOfMatrix];
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            

            for (int i = 1; i <= matrix.GetLength(0); i++)
            {
                Console.Write(i+ " ");
                for (int j = 1; j < matrix.GetLength(1); j++)
                {

                    Console.Write(i +(j*numOfMatrix) + " ");
                   
                }
                Console.WriteLine();
            }
        }

        public static void patternB(int numOfMatrix)
        {
            int[,] matrix = new int[numOfMatrix, numOfMatrix];
            int row = matrix.GetLength(0);
            int col = matrix.GetLength(1);
            int[,] printArr = new int[numOfMatrix, numOfMatrix];
            int counterrow = 0;
            int counterCol = 0;

            for (int i = 1; i <= row; i++)
            {
                //Console.WriteLine(i + " ");
               
                if(i % 2 ==0)
                {
                    for (int j = col * i; j > (col * i) - col; j--)
                    {
                       // Console.Write(j + " ");
                        printArr[counterrow, counterCol] = j;
                        counterrow++;

                    }
                    Console.WriteLine();
                }
                
                if (i % 3 == 0 || i == 1)
                {
                    for (int j = (((col) * (i - 1) + 1)); j <= col * i; j++)
                    {
                        //Console.Write(j + " ");
                        printArr[counterrow, counterCol] = j;
                        counterrow++;
                    }
                    counterrow++;
                    Console.WriteLine();
                }

                counterCol++;
                counterrow = 0;
            }

            for (int i = 0; i < printArr.GetLength(0); i++)
            {
                for (int j = 0; j < printArr.GetLength(1); j++)
                {
                    Console.Write(printArr[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void patternMitko(int numberOfMatrix)
        {
            int arraySize = 4; //int.Parse(Console.ReadLine());
            int[,] arr = new int[arraySize, arraySize];
            int counter = 1;

            for (int i = 0; i < arraySize; i++)

            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < arraySize; j++)
                    {
                        arr[j, i] = counter;
                        counter++;
                    }
                }
                if (i % 2 != 0)
                {
                    for (int j = arraySize - 1; j > 0; j--)
                    {
                        arr[j, i] = counter;
                        counter++;
                    }
                }
               

            }
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(0); j++)
                {
                    Console.Write(arr[i, j]);
                }
            }

        }
   
    }
}
