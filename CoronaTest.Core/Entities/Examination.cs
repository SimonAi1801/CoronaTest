using CoronaTest.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaTest.Core.Entities
{
    public class Examination : EntityObject
    {
        public Campaign Campaign { get; set; }
        public Participant Participant { get; set; }
        public TestCenter TestCenter { get; set; }
        public TestResults TestResult { get; set; }
        public ExaminationStates ExaminationState { get; set; }
        public DateTime ExaminationAt { get; set; }
        public string Identifier { get; set; }

        public static Examination CreateNex()
        {
            return new Examination();
        }
    }
}
