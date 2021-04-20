using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaTest.Web.Dtos
{
    public class TestCenterDto
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Postalcode { get; set; }
        public string Name { get; set; }
    }
}
