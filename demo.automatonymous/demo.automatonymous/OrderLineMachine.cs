using Automatonymous;

namespace demo.auto
{
    public class OrderLineMachine : AutomatonymousStateMachine<LineState>
    {
        public OrderLineMachine()
        {
            InstanceState(x => x.CurrentState);

            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            Event(() => ImportLine);
            Event(() => Complete);
            Event(() => ReceiveUpdate);
            Event(() => Cancel);

            State(() => Completed);
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }

        public Event ImportLine { get; set; }
        public Event ReceiveUpdate { get; set; }
        public Event Complete { get; set; }
        public Event Cancel { get; set; }

        //Implicit States are: `Initial` and `Final?`
        public State Open { get; set; }
        public State Cancelled { get; set; }
        public State Completed { get; set; }
    }
}
