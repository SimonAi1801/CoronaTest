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
    public class CreateUserModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ParticipantDto Participant { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var newParticipant = new Participant
            {
                Firstname = Participant.Firstname,
                Lastname = Participant.Lastname,
                SocialSecurityNumber = Participant.SocialSecurityNumber,
                Birthdate = Participant.Birthdate,
                Stair = Participant.Stair,
                Street = Participant.Street,
                City = Participant.City,
                Door = Participant.Door,
                Gender = Participant.Gender,
                HouseNumber = Participant.HouseNumber,
                Mobilphone = Participant.Mobilephone,
                Postalcode = Participant.Postalcode
            };

            try
            {
                await _unitOfWork.Participants.AddAsync(newParticipant);
                await _unitOfWork.SaveChangesAsync();
                RedirectToPage("/Registartion/UserOverview");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                return Page();
            }
            return Page();
        }
    }
}
