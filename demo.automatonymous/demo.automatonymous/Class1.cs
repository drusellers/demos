using System;
using Automatonymous;
using Automatonymous.Binders;
using Automatonymous.Events;
using Automatonymous.Graphing;
using NUnit.Framework;

namespace demo.auto
{
    public class Class1
    {
        [Test]
        public void X()
        {
            var theMachine = new OrderMachine();
            var theInstance = new OrderState();
            theMachine.RaiseEvent(theInstance, theMachine.DetectedOrder, new OrderData
            {
                OrderId = 2
            });
            Console.WriteLine(theInstance.CurrentState);


            theMachine.RaiseEvent(theInstance, theMachine.Complete);
            Console.WriteLine(theInstance.CurrentState);

            var x = theMachine.GetGraph();
        }
    }

    public class OrderState
    {
        public OrderState()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }
        public State CurrentState { get; set; }
    }

    public class OrderMachine : AutomatonymousStateMachine<OrderState>
    {
        public OrderMachine()
        {
            InstanceState(x => x.CurrentState);

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Event(() => DetectedOrder);
            Event(() => Complete);

            State(() => Unprocessed);
            // ReSharper restore DoNotCallOverridableMethodsInConstructor

            Initially(
                When(DetectedOrder)
                    .Then(cxt =>
                    {
                        //throw new Exception("BOB");
                        Console.WriteLine("ID:" + cxt.Data.OrderId);
                    })
                    .TransitionTo(Unprocessed)
                    .Catch<Exception>(OnError)
                );

            During(Unprocessed, When(Complete)
                .Finalize());
        }

        public Event<OrderData> DetectedOrder { get; set; }
        public Event ReceiveUpdate { get; set; }
        public Event Complete { get; set; }

        //Implicit States are: `Initial` and `Complete?`
        public State Unprocessed { get; set; }
        public State AtDropShipper { get; set; }
        public State ShippedComplete { get; set; }
        public State ProcessedIntoErp { get; set; }
        public State Cancelled { get; set; }
        public State ErroredSending { get; set; }

        ExceptionActivityBinder<OrderState, OrderData, Exception> OnError(ExceptionActivityBinder<OrderState, OrderData, Exception> arg)
        {
            return arg
                .TransitionTo(ErroredSending);
        }
    }

    public class OrderData
    {
        public int OrderId { get; set; }
    }
}
