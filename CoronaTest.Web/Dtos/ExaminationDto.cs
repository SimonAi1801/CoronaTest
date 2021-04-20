using CoronaTest.Core.Entities;
using CoronaTest.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaTest.Web.Dtos
{
    public class ExaminationDto
    {
        public Campaign Campaign { get; set; }

        [DisplayName("Datum der Testung")]
        [Required]
        public DateTime ExaminationAt { get; set; }

        public TestCenter TestCenter { get; set; }

        public TestResults TestResult { get; set; }
    }
}
