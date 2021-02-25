using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaTest.Core.DTOs
{
    public class CampaignDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
