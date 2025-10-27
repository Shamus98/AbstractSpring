using AbstractModel;

namespace SpringForce
{
    public class ChangeForce : FastAbstractEvent
    {
        SpringWrapper springWrapper;
        
        public ChangeForce(string ID, SpringWrapper SpringWrapper) 
        { 
            this.objId = ID;
            this.springWrapper = SpringWrapper;
        }
        public override void runEvent(FastAbstractWrapper wrapper, double timeSpan)
        {
            var spring = (Spring) springWrapper.getObject(objId);
            spring.SetupAcceleration();
        }
    }
}