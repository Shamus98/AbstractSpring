using AbstractModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringForce
{
    public class SpringWrapper : FastAbstractWrapper
    {
        public SpringWrapper(List<(string ID, double x, double v, double m)> skvorecList, List<(string ID, string ID1, string ID2, double l, double dl, double f)> springList) : base()
        {

            foreach (var scvoec in skvorecList) 
            {
                addObject(new Bird(scvoec.ID, scvoec.x, scvoec.v, scvoec.m, this));
            }
            List<Spring> springLst = new List<Spring>();
            foreach (var spring in springList)
            {
                var spr = new Spring(spring.ID, spring.ID1, spring.ID2, spring.l, spring.dl, spring.f, this);
                springLst.Add(spr);
                addObject(spr);
            }

            foreach(var spring in springLst)
            {
                spring.SetupAcceleration();
            }
            
        }
    }
}
