using System;
using Automatonymous;

namespace demo.auto
{
    public class OrderState
    {
        public OrderState()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public State CurrentState { get; set; }
        public int TheTrackedOrder { get; set; }
    }

    public class LineState
    {
        public Guid Id { get; set; }
        public State CurrentState { get; set; }
        public int TheTrackedOrder { get; set; }
        public int TheLineNumber { get; set; }
    }
}
