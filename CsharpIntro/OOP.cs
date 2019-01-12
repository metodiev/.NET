using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    //encapsulation of OOP class is public
   public class OOP : Abstraction
    {

        //Fields
        //Each field has encapsulation - there are three types of encapsulation private, protected, public
        //protected - current namcespaces and project
        //private is only for current class
        private static int number;
        private int number2;
        protected int protectedNumber;

        protected int ProtectedNumber { get => protectedNumber; set => protectedNumber = value; }

        public static void Main()
        {

            //OOP is paradigm
            //There are four principles of OOP (Object oriented programing)

            //1.Encapsulation 
            // 2.Inheritance Class a ingerit class b, a:b;
            // 3. Abstraction - check abstract class, class which has one or more than one abstract class
            // 4. Polymorphism - two types of polymorphism dynamic and static
            //4a. Static - overload - one function with the same name but different arguments in the input body
            //4b Dynamic - when you override some function rerwrite inherit function for your needs

            
            //static filed
            number = 100;

            //dinamyc field
            OOP dinamicFieldNumber = new OOP();
            dinamicFieldNumber.number2 = 102;
           

            //create object with default constructor
            Monitors dell = new Monitors();
            dell.Brand = "Dell";
            dell.Inches = 24;
            dell.Matrix = "IPS";
            dell.TechnicalSpecs = "ARM processor";
            dell.printMonitorInfo();
            
            //create another objects with populated fields
            Monitors acer = new Monitors("Acer", 24, "Very Good", "TN");
            Console.WriteLine(acer.Matrix);
            acer.printMonitorInfo();

            Console.ReadLine();


        }

        public override void showOnMonitor()
        {
            Console.WriteLine("Play Games");
        }

        public override double returnPiNumberVirtual()
        {
            return base.returnPiNumberVirtual();
        }

        //overload polymorphism - static 
        public int sum(int a)
        {
            return a;
        }
        public int sum(int a, int b)
        {
            return a;
        }
        public int sum()
        {
            return Math.Abs(1230);
        }
    }

    //with two dots : you're telling the compiler that you want class testProtected to inherint OOP class
    public class testProtected : OOP
    {
        public void testProtectedMethod()
        {
            testProtected testProtectedN = new testProtected();
            testProtectedN.protectedNumber = 3;
            Console.WriteLine(protectedNumber);
        }
    }
}
