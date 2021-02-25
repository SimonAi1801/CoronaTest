using CoronaTest.Core.Entities;
using CoronaTest.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaTest.Core.DTOs
{
    public class ExaminationDto
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public Participant Participant { get; set; }
        public int ParticipantId { get; set; }
        public TestCenter TestCenter { get; set; }
        public Campaign Campaign { get; set; }
        public int CampaignId { get; set; }
        public int TestCenterId { get; set; }
        public DateTime ExaminationAt { get; set; }
        public TestResults TestResult { get; set; }
        public ExaminationStates ExaminationState { get; set; }
    }
}
