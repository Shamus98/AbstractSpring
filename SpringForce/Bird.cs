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



        public double x { get; private set; }
        public double v { get; private set; }
        public double m { get; private set; }

        public double a { get; private set; }

        SpringWrapper springWrapper;
        internal string springRightID;
        internal string springLeftID;

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
            double deltaT = lastUpdated - timeSpan;

            x = x + v * deltaT + a / 2 * deltaT * deltaT;
            v = v + a * deltaT;



            lastUpdated = timeSpan;
            Console.WriteLine($"{uid}: {x} {v} {a} {lastUpdated}");
        }

        internal void SetupAcceleration(double a)
        {
            this.a = a;
        }
    }
}
