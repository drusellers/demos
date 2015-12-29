using System;
using Automatonymous;
using Automatonymous.Binders;
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
            theMachine.RaiseEvent(theInstance, theMachine.Greet);
            Console.WriteLine(theInstance.CurrentState);


            theMachine.RaiseEvent(theInstance, theMachine.Goodbye);
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
            InstanceState(x=>x.CurrentState);

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Event(()=> Greet);
            Event(()=> Goodbye);

            State(()=>Greeted);
            // ReSharper restore DoNotCallOverridableMethodsInConstructor

            Initially(
                When(Greet)
                .Then(x =>
                {
                    
                    throw new Exception("BOB");
                    Console.WriteLine("ID:" + x.Instance.Id);
                    
                })
                .TransitionTo(Greeted)
                .Catch<Exception>(OnError)
                );

            During(Greeted, When(Goodbye)
                .Finalize());
        }

        private ExceptionActivityBinder<OrderState, Exception> OnError(ExceptionActivityBinder<OrderState, Exception> arg)
        {
            return arg
                .TransitionTo(Errored);
        }

        public Event Greet { get; set; }
        public Event Goodbye { get; set; }

        public State Greeted { get; set; }
        public State Bill { get; set; }
        public State Errored { get; set; }
    }
}
