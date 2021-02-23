using CoronaTest.Core.Entities;
using CoronaTest.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaTest.API.DTOs
{
    public class ExaminationDto
    {
        public string Identifier { get; set; }
        public Participant Participant { get; set; }
        public int ParticipantId { get; set; }
        public TestCenter TestCenter { get; set; }
        public int TestCenterId { get; set; }
        public DateTime ExaminationAt { get; set; }
        public TestResults TestResult { get; set; }
    }
}
