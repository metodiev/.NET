using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    class ExceptionsInCsharp : Exception
    {
        public static void Main(String [] args)
        {
            //int pi = 2.56;
            //sumPi();
            //uncheckedContext();
            //tryChatchFinally();

            //tryFinally();
            //stackOverFlowException(100);
            //catchException();
            nullArgumentException();
			
			
        }

        private static void nullArgumentException()
        {
            int? p = null;
            try
            {
                int num = p.Value;
                for (int i = 0; i < p; i++)
                {
                    Console.WriteLine(i);
                }

            }
            catch (Exception)
            {

                throw new ArgumentNullException("Parameter p is null");
            }
           
            

            
        }

        public static void catchException()
        {
            try
            {
                int[] piNumbers = { 2, 3, 4 };

                for (int i = 0; i < 41; i++)
                {
                    Console.WriteLine(piNumbers[i]);
                }
            }
            catch (Exception e)
            {
                
                throw new Exception("Error Message");
                
            }
        }

        public static void stackOverFlowException( int parameter)
        {
         
                try
                {
                    int par = 1;
                    stackOverFlowException( par + 1);
                }
                catch (Exception)
                {

                    //throw new  StackOverflowException("@@@@@@@@@@StackeOverflow exception@@@@@@@@@@@") ;
                    throw new Exception("Error");
                }
               
            
        }
        public static void sumPi()
        {
            int [] piNumbers = { 2, 3, 4 };

            for (int i = 0; i < 41; i++)
            {
                Console.WriteLine(piNumbers[i]);
            }

        }

        public static void tryChatchFinally()
        {
            try
            {
                int[] piNumbers = { 2, 3, 4 };

                for (int i = 0; i < 41; i++)
                {
                    Console.WriteLine(piNumbers[i]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());

            }
            finally
            {
                Console.WriteLine("This is going to be executed always no mather what is the condition in try or catch");
            }
        }

        public static void tryFinally()
        {
            FileStream F = null ;
            FileStream F1 = null;
            try
            {
                 F = new FileStream("test.dat", FileMode.OpenOrCreate,
                FileAccess.ReadWrite);
               

                for (int i = 1; i <= 20; i++)
                {
                    F.WriteByte((byte)i);
                    //F.Write("");
                }
                F.Position = 0;
                for (int i = 0; i <= 20; i++)
                {
                    Console.Write(F.ReadByte() + " ");
                }
               // F.Close();
                Console.ReadKey();
                F.Close();

                F1 = new FileStream("test.dat", FileMode.OpenOrCreate,
              FileAccess.ReadWrite);
                F1.Position = 0;
                for (int i = 0; i <= 20; i++)
                {
                    Console.Write(F1.ReadByte() + "F1@@@ ");
                }

            }
            finally
            {
                //FieldAccessException.
                F.Close();
            }
        }

        public virtual Exception GetBaseException(string astr)
        {
            Exception p1q3e = new IndexOutOfRangeException();
            return p1q3e;
        }


        public static void uncheckedContext()
        {
            // The first short will overflow after the second short does.
            short a = 0;
            short b = 100;
            try
            {
                //
                // Keep incrementing the shorts until an exception is thrown.
                // ... This is terrible program design.
                //
                while (true)
                {
                //There are two types of exceptions checked and uncheked
                //checked - compile time exception 
                //unchecked - runtime exception
                    checked
                    {
                        a++;
                    }
                    unchecked
                    {
                        b++;
                    }
                }
            }
            catch
            {
                // Display the value of the shorts when overflow exception occurs.
                Console.WriteLine(a);
                Console.WriteLine(b);
            }
        }
    }
}
