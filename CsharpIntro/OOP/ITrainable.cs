using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
   public interface ITrainable
    {
        //inteface always has abstract classes
         void startTrain();
        void stopTrain();

        void turnTrain();
        void accelerateTrain();

    }
}
