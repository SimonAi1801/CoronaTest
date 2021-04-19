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
    public class UserOverviewModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserOverviewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ParticipantDto[] Participants { get; set; }

        public async Task OnGet()
        {
            var participants = await _unitOfWork.Participants.GetAllAsync();

            Participants = participants.Select(p => new ParticipantDto
            {
                Id = p.Id,
                Birthdate = p.Birthdate,
                SocialSecurityNumber = p.SocialSecurityNumber,
                Stair = p.Stair,
                Street = p.Street,
                Door = p.Door,
                City = p.City,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Gender = p.Gender,
                HouseNumber = p.HouseNumber,
                Mobilephone = p.Mobilphone,
                Postalcode = p.Postalcode
            }).ToArray();
        }
    }
}
