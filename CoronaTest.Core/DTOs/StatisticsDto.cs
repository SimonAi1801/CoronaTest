using System;
using System.Collections.Generic;
using System.Text;

namespace CoronaTest.Core.DTOs
{
    public class StatisticsDto
    {
        public int Year { get; set; }
        public int CalenderWeek { get; set; }
        public int CountOfExaminations { get; set; }
        public int CountOfUnkownResults { get; set; }
        public int CountofPositiveResults { get; set; }
        public int CountOfNegativeResults { get; set; }
    }
}
