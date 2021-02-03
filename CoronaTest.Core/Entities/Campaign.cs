using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaTest.Core.Entities
{
    public class Campaign : EntityObject
    {
        public string Name { get; set; }
        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public ICollection<TestCenter> AvailableTestCenters { get; set; }
    }
}
