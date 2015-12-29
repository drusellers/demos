using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace demo.automapper.Advanced._01_SelectMany
{
    public class SelectMany
    {
        [Test]
        public void SelectManyProjection()
        {
            var x = new List<WithMany>();
            var result = new List<Output>();

            var b = x.SelectMany(a =>
            {
                var c = a.Groups.Select(d => new Output
                {
                    Bob = a.Bob,
                    Bill = d.Bill,
                    Mary = d.Mary
                });
                return c;
            });
        }
    }


    public class Output
    {
        public string Bob { get; set; }
        public string Bill { get; set; }
        public string Mary { get; set; }
    }

    public class WithMany
    {
        public string Bob { get; set; }
        public List<ASingle> Groups { get; set; }
    }

    public class ASingle
    {
        public string Bill { get; set; }
        public string Mary { get; set; }
    }
}