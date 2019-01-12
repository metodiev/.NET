using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
   public class Monitors
    {
        //fields of Monitors class - advert
        private string brand;
        private int inches;
        private string technicalSpecs;
        private string matrix;

        //this is the Default constructor
        public Monitors()
        {

        }

        public Monitors(string brand, int inches, string technicalSpecs, string matrix)
        {
            this.Brand = brand;
            this.Inches = inches;
            this.TechnicalSpecs = technicalSpecs;
            this.Matrix = matrix;
        }

        //these properties are to get and set values to fields
        public string Brand { get; set; }
        public int Inches { get; set; }
        public string TechnicalSpecs { get; set; }
        public string Matrix { get; set; }


        //methods - verbs
        public void showText()
        {
            Console.WriteLine("Monitor show text");

        }

        public void printMonitorInfo()
        {
            Console.WriteLine(Brand);
            Console.WriteLine(Inches);
            Console.WriteLine(TechnicalSpecs);
            Console.WriteLine(Matrix);
        }

    }
}
