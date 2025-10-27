using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractModel;

namespace SpringForce
{
    public class Spring:FastAbstractObject
    {
        string Bird_ID1;  
        string Bird_ID2; 
        double lengthSpring;
        double changeLengthSpring;
        double force;
        SpringWrapper springWrapper;

        public Spring(string ID,string Bird_ID1, string Bird_ID2, double lengthSpring, double changeLengthSpring, double force, SpringWrapper springWrapper) 
        
        {
            uid = ID;
            this.Bird_ID1 = Bird_ID1;
            this.Bird_ID2 = Bird_ID2;
            this.lengthSpring = lengthSpring;
            this.changeLengthSpring = changeLengthSpring;
            this.force = force;
            this.springWrapper = springWrapper;
        }

        public override (double, FastAbstractEvent) getNearestEvent()
        {
            throw new NotImplementedException();
        }

        public override void Update(double timeSpan)
        {
            throw new NotImplementedException();
        }
    }
}
