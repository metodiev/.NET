using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
  public  abstract class Abstraction 
    {
        private int testNumber;

        public double returnPiNumber()
        {
            return Math.PI;
        }

        public virtual double returnPiNumberVirtual()
        {
            return Math.PI;
        }

        //this is and abstract class
        public abstract void showOnMonitor();
    }
}
