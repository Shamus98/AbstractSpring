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

        }

        public override (double, FastAbstractEvent) getNearestEvent()
        {


            List<double> time = SquareSentenceTime();

            if (time.Count > 0)
            {

                double time_nearest = time.Min();
             
                return (time_nearest, new ChangeForce(uid, springWrapper));
            } else
            {
                return (double.MaxValue, null);
            }






        }

        public List<double> SquareSentenceTime()
        {
            double discriminant_one = zv * zv - 2 * za * z + 2 * za * (lengthSpring - changeLengthSpring);
            double discriminant_two = zv * zv - 2 * za * z + 2 * za * (lengthSpring + changeLengthSpring);

            List<double> time = new List<double>();

            // Случай 1: Применяем первое уравнение (discriminant_one)
            if (discriminant_one > 0)
            {
                // Два различных корня
                if (za != 0)
                {
                    if (zv != 0)
                    {
                        double t1 = (-zv + Math.Sqrt(discriminant_one)) / za;
                        double t2 = (-zv - Math.Sqrt(discriminant_one)) / za;
                        // Только положительные решения
                        if (t1 > 0) time.Add(t1);
                        if (t2 > 0) time.Add(t2);
                    }
                    else
                    {
                        // Если начальная скорость равна 0
                        double t1 = Math.Sqrt(2 * ((lengthSpring - changeLengthSpring) - z) / za);
                        if (t1 > 0) time.Add(t1);
                    }
                }
            }
            else if (discriminant_one == 0)
            {
                // Один корень
                if (za != 0)
                {
                    double t = -zv / za; // Решение при дискриминанте равном 0
                    if (t > 0) time.Add(t);
                }
            }

            // Случай 2: Применяем второе уравнение (discriminant_two)
            if (discriminant_two > 0)
            {
                // Два различных корня
                if (za != 0)
                {
                    if (zv != 0)
                    {
                        double t1 = (-zv + Math.Sqrt(discriminant_two)) / za;
                        double t2 = (-zv - Math.Sqrt(discriminant_two)) / za;
                        // Только положительные решения
                        if (t1 > 0) time.Add(t1);
                        if (t2 > 0) time.Add(t2);
                    }
                    else
                    {
                        double t1 = Math.Sqrt(2 * ((lengthSpring + changeLengthSpring) - z) / za);
                        if (t1 > 0) time.Add(t1);
                    }
                }
            }
            else if (discriminant_two == 0)
            {
                // Один корень
                if (za != 0)
                {
                    double t = -zv / za; // Решение при дискриминанте равном 0
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

            var springLeft = (Spring)springWrapper.getObject(birdLeft.springLeftID);
            var springRight = (Spring)springWrapper.getObject (birdRight.springRightID);

            if (springLeft != null)
            {
                forceLeft = springLeft.getForce();
            }

            double currentForce = this.getForce();

            double aLeft = (forceLeft - currentForce) / birdLeft.m;

            double forceRight = 0;

            if (springRight != null)
            {
                forceRight = springRight.getForce();
            }

            double aRight = (currentForce - forceRight) / birdRight.m;
            double za = aRight - aLeft;

            birdLeft.SetupAcceleration(aLeft);
            birdRight.SetupAcceleration(aRight); 

        }
    }
}
