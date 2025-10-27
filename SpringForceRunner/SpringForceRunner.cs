using SpringForce;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpringForceRunner
{
    class SeaRunner
    {
        static void Main(string[] args)
        {

            //TODO read skvorecList
            //TODO read springList
            


            var springWrapper = new SpringWrapper(skvorecList, springList);


            while (springWrapper.Next())
            {

            }


        }



    }
}

