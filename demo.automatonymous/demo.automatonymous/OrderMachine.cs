using System;
using Automatonymous;
using Automatonymous.Binders;

namespace demo.auto
{
    public class OrderMachine : AutomatonymousStateMachine<OrderState>
    {
        public OrderMachine()
        {
            InstanceState(x => x.CurrentState);

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Event(() => ImportOrder);
            Event(() => Complete);
            Event(() => ReceiveUpdate);
            Event(() => Cancel);

            State(() => Imported);
            // ReSharper restore DoNotCallOverridableMethodsInConstructor

            Initially(
                When(ImportOrder)
                    .Then(cxt =>
                    {

                        // import the order into the database
                        // if no error transition to `Imported`

                        // get a service ImportService.Import(cxt.Data)
                        Console.WriteLine("ID: {0}", cxt.Instance.Id);
                    })
                    .TransitionTo(Imported)
                    .Catch<Exception>(OnError)
                );

            During(Imported, When(SendOut).Then(cxt =>
            {
                var x= cxt.Data.GetMyData;
                // send to distributor
            }).TransitionTo(Submitted));


            During(Submitted, When(ReceiveUpdate).Then(cxt =>
            {
                // Shred the shipment data
                // Deliver updates to the correct line
                // this is acting as a router to *its* children
            }));

            During(Submitted, When(LineUpdated).Then(cxt =>
            {
                // if this line is complete
                // check other lines, if all complete
                // transition order to ship complete
                // otherwise do nothing
            }));


            During(Submitted, When(Cancel).Then(cxt =>
            {
                // attempt to cancel all lines
            }));
        }

        public Event ImportOrder { get; set; }
        public Event<SomeData> SendOut { get; set; }
        public Event ReceiveUpdate { get; set; }
        public Event Complete { get; set; }
        public Event Cancel { get; set; }
        public Event LineUpdated { get; set; }

        //Implicit States are: `Initial` and `Final?`
        public State Imported { get; set; }
        public State Submitted { get; set; }
        public State ShippedComplete { get; set; }
        public State InvoiceComplete { get; set; }
        public State ErroredSending { get; set; }
        public State ErroredImporting { get; set; }
        public State ErroredUpdating { get; set; }

        ExceptionActivityBinder<OrderState, Exception> OnError(ExceptionActivityBinder<OrderState, Exception> arg)
        {
            return arg
                .TransitionTo(ErroredImporting);
        }
    }

    public class SomeData
    {
        public string GetMyData { get; set; }
    }
}
