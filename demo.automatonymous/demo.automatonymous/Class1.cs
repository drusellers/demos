using System;
using Automatonymous;
using Automatonymous.Graphing;
using NUnit.Framework;

namespace demo.auto
{
    public class Class1
    {
        [Test]
        public void TestRun()
        {
            var theMachine = new OrderMachine();
            var theInstance = new OrderState();
            theMachine.RaiseEvent(theInstance, theMachine.ImportOrder);
            Console.WriteLine(theInstance.CurrentState);


            theMachine.RaiseEvent(theInstance, theMachine.Complete);
            Console.WriteLine(theInstance.CurrentState);

            var x = theMachine.GetGraph();
        }
    }
}
