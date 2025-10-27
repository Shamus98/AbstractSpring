using AbstractModel;

namespace SpringForce
{
    public class ChangeForce : FastAbstractEvent
    {
        string ID;
        double x;
        double v;
        double m;
        string ID1;
        string ID2;
        double l;
        double dl;
        double f;
        public ChangeForce(string ID, double x, double v, double m, string ID1, string ID2, double l, double dl, double f) 
        { 
            this.objId = ID;
            this.x = x;
            this.v = v;
            this.m = m;
            this.ID1 = ID1;
            this.ID2 = ID2;
            this.l = l;
            this.dl = dl;
            this.f = f;
        }
        public override void runEvent(FastAbstractWrapper wrapper, double timeSpan)
        {
            throw new NotImplementedException();
        }
    }
}