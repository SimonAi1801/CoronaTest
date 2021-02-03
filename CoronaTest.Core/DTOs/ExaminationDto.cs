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
        public string ParticipantFullname { get; set; }
        public TestResults TestResult { get; set; }
        public DateTime ExaminationAt { get; set; }
        public string Identifier { get; set; }
        public ExaminationDto(Examination examination)
        {
            Id = examination.Id;
            ParticipantFullname = $"{examination.Participant.Firstname} {examination.Participant.Lastname}";
            TestResult = examination.TestResult;
            ExaminationAt = examination.ExaminationAt;
            Identifier = examination.Identifier;
        }
    }
}
