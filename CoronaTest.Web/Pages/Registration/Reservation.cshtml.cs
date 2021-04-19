using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoronaTest.Core.Contracts;
using CoronaTest.Core.Entities;
using CoronaTest.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CoronaTest.Web.Pages.Registration
{
    public class ReservationModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task OnGet()
        {
           
        }
    }
}
