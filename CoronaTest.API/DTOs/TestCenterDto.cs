using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaTest.API.DTOs
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
