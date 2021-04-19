using CoronaTest.Core.Entities;
using CoronaTest.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaTest.Web.Dtos
{
    public class ExaminationDto
    {
        public Campaign Campaign { get; set; }

        public DateTime ExaminationAt { get; set; }

        public TestCenter TestCenter { get; set; }

        public TestResults TestResult { get; set; }
    }
}
