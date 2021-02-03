using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaTest.Core.Entities
{
    public class TestCenter : EntityObject
    {
        public ICollection<Campaign> AvailableCampaigns { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Postalcode { get; set; }
        public string Name { get; set; }
        public int SlotCapacity { get; set; }
    }
}
