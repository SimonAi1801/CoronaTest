using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaTest.Core.Contracts;
using CoronaTest.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoronaTest.Web.Pages.Registration
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ExaminationDto[] Examinations { get; set; }

        public async Task OnGet()
        {
            var examinations = await _unitOfWork.Examinations.GetAllAsync();

            Examinations = examinations.Select(e => new ExaminationDto
            {
                Campaign = e.Campaign,
                ExaminationAt = e.ExaminationAt,
                TestCenter = e.TestCenter,
                TestResult = e.TestResult
            }).ToArray();

        }
    }
}
