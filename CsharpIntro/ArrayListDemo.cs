using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    class ArrayListDemo
    {
        static void Main(string[] args)
        {
            //sample array
            //int[] numbers = { 1, 2, 2, 34, 5 };
            int[] numbers = new int [10];

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = i;//int.Parse(Console.ReadLine()); 
            }

            //for (int i = 0; i < numbers.Length; i++)
            //{
            //    Console.WriteLine(numbers[i]);
            //}



            //arraylist
            List<int> list = new List<int>();
            list.Add(1);
            list.Add(1);
            list.Add(1);
            list.Add(1);


            for (int i = 0; i < numbers.Length; i++)
            {
                list.Add(numbers[i]);
            }

            //count list elements
            Console.WriteLine("Szie of list is:" + list.Count);

            Console.WriteLine("Before sort");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }


            //sort the list
            list.Sort();
            Console.WriteLine("After sort");
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            //double [] test = new double [10];

            //foreach (var item in test)
            //{

            //}

            //insert range

            List<int> listRagne = new List<int>();
            listRagne.Add(90);
            listRagne.Add(80);
            listRagne.Add(70);
            listRagne.Add(60);
            listRagne.Add(50);

            listRagne.Add(40);
            listRagne.InsertRange(5,list);

            Console.WriteLine("Listragne example:");
            foreach (var item in listRagne)
            {
                Console.WriteLine(item);
            }

            listRagne.Clear();
            Console.WriteLine("Listragne after clear method");

            // Console.WriteLine(listRagne);
            Console.WriteLine(listRagne.Count);
            foreach (var item in listRagne)
            {
                
                Console.WriteLine(item);
            }

            //copy array into list

            List<int> copyArr = new List<int>(numbers);
            Console.WriteLine("Copy arr values:");
            foreach (var item in copyArr)
            {
                Console.WriteLine(item);
            }

            //index of list

            Console.WriteLine("Index of 3:");
            Console.WriteLine(copyArr.IndexOf(3));

            //insert element

            copyArr.Insert(10, 10);
            Console.WriteLine("insert element");
            Console.WriteLine(copyArr.IndexOf(10));


            Console.WriteLine("reverse arrays");
            copyArr.Reverse();

            foreach (var item in copyArr)
            {
                Console.WriteLine(item);
            }
        
       }

    }
}
