using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using AbstractModel;

namespace SpringForce
{
    public class Spring:FastAbstractObject
    {
        public string Bird_ID1 { get; private set; }
        public string Bird_ID2 { get; private set; }
        double lengthSpring;
        double changeLengthSpring;
        double force;
        public double z {get; private set;}
        public double zv { get; private set; }
        public double za {get; private set;}

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

            var birdLeft = (Bird)this.springWrapper.getObject(Bird_ID1);
            var birdRight = (Bird)this.springWrapper.getObject(Bird_ID2);

            birdLeft.springRightID = this.uid;
            birdRight.springLeftID = this.uid;

            z = birdRight.x - birdLeft.x;
            zv = birdRight.v - birdLeft.v;
        }

        public override (double, FastAbstractEvent) getNearestEvent()
        {


            List<double> time = SquareSentenceTime(changeLengthSpring);
            List<double> time1 = SquareSentenceTime(-changeLengthSpring);
            time.AddRange(time1);

            if (time.Count > 0)
            {

                double time_nearest = time.Min();
             
                return (time_nearest, new ChangeForce(uid, springWrapper));
            } else
            {
                return (double.MaxValue, null);
            }

    




        }

        public List<double> SquareSentenceTime(double dl)
        {
            List<double> time = new List<double>();
            double a = za / 2.0;
            double b = zv;
            double c = z - (lengthSpring + dl);
            double discriminant = b * b - 4 * a * c;
            if (a != 0)
            {
                if (discriminant > 0)
                {
                    // Два различных корня
                    double t1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
                    double t2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
                    // Только положительные решения
                    if (t1 > 0) time.Add(t1);
                    if (t2 > 0) time.Add(t2);

                }
                else if (discriminant == 0)
                {
                    double t = -b / (2 * a); // Решение при дискриминанте равном 0
                    if (t > 0) time.Add(t);
                }
            }
            else {
                if (b != 0)
                {
                    double t = -c / b;
                    if (t > 0) time.Add(t);
                }
            }
            
            return time;
        }

        public double getForce()
        {
            if (z<lengthSpring-changeLengthSpring) return -force;
            if (z > lengthSpring + changeLengthSpring) return force;
            if (z==lengthSpring - changeLengthSpring)
            {
                if (zv < 0) return -force;
                else return 0;
            }
            if (z==lengthSpring + changeLengthSpring)
            {
                if (zv > 0) return force;
                else return 0;
            }
            return 0;
        }


        public override void Update(double timeSpan)
        {
            double deltaT = timeSpan - lastUpdated;
            z = z + zv * deltaT + za / 2 * deltaT * deltaT;
            zv = zv + za * deltaT;

            lastUpdated = timeSpan;
        }

        internal void SetupAcceleration()
        {
            var birdLeft = (Bird)springWrapper.getObject(Bird_ID1);
            var birdRight = (Bird)springWrapper.getObject(Bird_ID2);

            double forceLeft = 0;

            Spring springLeft;
            Spring springRight;

            if (birdLeft.springLeftID != null)
            {
                springLeft = (Spring)springWrapper.getObject(birdLeft.springLeftID);
                forceLeft = springLeft.getForce();
            }

            double currentForce = this.getForce();

            double aLeft = (currentForce - forceLeft) / birdLeft.m;

            double forceRight = 0;

            if (birdRight.springRightID != null)
            {
                springRight = (Spring)springWrapper.getObject(birdRight.springRightID);
                forceRight = springRight.getForce();
            }

            double aRight = (forceRight - currentForce) / birdRight.m;
            za = aRight -  aLeft;

            birdLeft.SetupAcceleration(aLeft);
            birdRight.SetupAcceleration(aRight); 

        }
    }
}
