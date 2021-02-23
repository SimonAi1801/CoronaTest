using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaTest.API.DTOs
{
    public class StatisticsDto
    {
        public string Year { get; set; }
        public int CalenderWeek { get; set; }
        public int CountOfExaminations { get; set; }
        public int CountOfUnkownResults { get; set; }
        public int CountofPositiveResults { get; set; }
        public int CountOfNegativeResults { get; set; }
    }
}
