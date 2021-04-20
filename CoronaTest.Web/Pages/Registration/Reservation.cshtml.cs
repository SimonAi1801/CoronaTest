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


        public TestCenterDto[] TestCenters{ get; set; }

        [BindProperty]
        public int SelectedTestCenterId { get; set; }

        public ParticipantDto[] Participants { get; set; }


        [BindProperty]
        public int SelectedParticipantId { get; set; }

        [BindProperty]
        public ExaminationDto Examination { get; set; }


        public CampaignDto[] Campaigns { get; set; }

        [BindProperty]
        public int SelectedCampaignId { get; set; }

        public async Task OnGet()
        {
            var testCenters = await _unitOfWork.TestCenters.GetAllAsync();

            TestCenters = testCenters.Select(t => new TestCenterDto
            {
                Id = t.Id,
                Street = t.Street,
                City = t.City,
                Name = t.Name,
                Postalcode = t.Postalcode
            }).ToArray();

            var participants = await _unitOfWork.Participants.GetAllAsync();

            Participants = participants.Select(p => new ParticipantDto
            {
                Id = p.Id,
                Birthdate = p.Birthdate,
                SocialSecurityNumber = p.SocialSecurityNumber,
                Stair = p.Stair,
                Street = p.Street,
                City = p.City,
                Door = p.Door,
                Firstname = p.Firstname,
                Lastname = p.Lastname,
                Gender = p.Gender,
                HouseNumber = p.HouseNumber,
                Mobilephone = p.Mobilphone,
                Postalcode = p.Postalcode
            }).ToArray();

            var campaigns = await _unitOfWork.Campaigns.GetAllAsync();

            Campaigns = campaigns.Select(c => new CampaignDto
            {
                Id = c.Id,
                From = c.From,
                To = c.To,
                Name = c.Name
            }).ToArray();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var participant = await _unitOfWork.Participants.GetByIdAsync(SelectedParticipantId);
            var testCenter = await _unitOfWork.TestCenters.GetByIdAsync(SelectedTestCenterId);
            var campaign = await _unitOfWork.Campaigns.GetByIdAsync(SelectedCampaignId);

            var newExamination = new Examination
            {
                Campaign = campaign,
                TestCenter = testCenter,
                ExaminationAt = Examination.ExaminationAt,
                Participant = participant,
            };


            try
            {
                await _unitOfWork.Examinations.AddAsync(newExamination);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(String.Empty, ex.Message);
                RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
