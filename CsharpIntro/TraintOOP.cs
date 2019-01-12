using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpIntro
{
    class TraintOOP : ITrainable, ITrainaBle2
    {
        public void accelerateTrain()
        {
            Console.WriteLine("Accelerate traint");
        }

        public void startTrain()
        {
            Console.WriteLine("Start the train");
        }

        public void stopTrain()
        {
            Console.WriteLine("Stop Train");
        }

        public void testinterface()
        {
            throw new NotImplementedException();
        }

        public void turnTrain()
        {
            Console.WriteLine("Turn train");
        }
    }
}
