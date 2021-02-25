using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaTest.Core.DTOs
{
    public class TestCenterDto
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string Postalcode { get; set; }
        public string Name { get; set; }
        public int SlotCapacity { get; set; }
    }
}
