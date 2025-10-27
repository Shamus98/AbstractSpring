using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractModel;

namespace SpringForce
{
    public class Bird : FastAbstractObject
    {
        
        double x;
        double v;
        double m;
        SpringWrapper springWrapper;
        public Bird(string ID, double x, double v, double m, SpringWrapper springWrapper) 
        { 
            uid = ID;
            this.x = x;
            this.v = v;
            this.m= m;
            this.springWrapper = springWrapper;
        }

        public override (double, FastAbstractEvent) getNearestEvent()
        {
            return (double.MaxValue, null);
        }

        public override void Update(double timeSpan)
        {
            throw new NotImplementedException();
        }
    }
}
