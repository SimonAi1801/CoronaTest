﻿using CoronaTest.API.DTOs;
using CoronaTest.Core.Contracts;
using CoronaTest.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaTest.API.Controllers
{
    /// <summary>
    /// Dieser Controller ist für die Datenbehandlung der Kamapgnen zuständig.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CampaignController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CampaignController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        /// Liefert alle Kampagnen zurück.
        /// </summary>
        /// <returns>CampaignDTO</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var campaigns = await _unitOfWork.Campaigns.GetAllAsync();

            return Ok(campaigns.Select(c => new CampaignDto 
            {
                Id = c.Id,
                Name = c.Name,
                From = c.From,
                To = c.To
            }).ToArray());
        }


        /// <summary>
        /// Liefert die Kampagne mit der übergebenen Id.
        /// </summary>
        /// <param name="id">CampaignId</param>
        /// <returns>CampaignDto</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var campaign = await _unitOfWork.Campaigns.GetByIdAsync(id.Value);

            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(new CampaignDto
            {
                Id = campaign.Id,
                Name = campaign.Name,
                From = campaign.From,
                To = campaign.To
            });
        }

        /// <summary>
        /// Abfrage der Untersuchungen einer bestimmten Kampagne
        /// </summary>
        /// <param name="id">campaignId</param>
        /// <returns>ExaminationDto</returns>
        [HttpGet("{id}/Examinations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExaminationsByCampaignId(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var examinations = await _unitOfWork.Examinations.GetByCamapignIdAsync(id.Value);

            if (examinations == null)
            {
                return NotFound();
            }

            var examinationsDto = examinations.Select(e => new ExaminationDto
            {
                Identifier = e.Identifier,
                ExaminationAt = e.ExaminationAt,
                Participant = e.Participant,
                ParticipantId = e.Participant.Id,
                TestCenter = e.TestCenter,
                TestCenterId = e.TestCenter.Id,
                TestResult = e.TestResult
            }).ToArray();

            return Ok(examinationsDto);
        }

        /// <summary>
        /// Abfrage des TestCenters einer bestimmten Kampagne
        /// </summary>
        /// <param name="id">campaignId</param>
        /// <returns>TestCenterDto</returns>
        [HttpGet("{id}/TestCenters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTestCentersByCampaignId(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var testCenter = await _unitOfWork.TestCenters.GetByCampaignIdAsync(id.Value);

            if (testCenter == null)
            {
                return NotFound();
            }

            var testCenterDto = testCenter.Select(t => new TestCenterDto
            {
                Name = t.Name,
                SlotCapacity = t.SlotCapacity,
                Street = t.Street,
                City = t.City,
                Postalcode = t.Postalcode
            }).ToArray();

            return Ok(testCenterDto);
        }

        /// <summary>
        /// Erstellt eine neue Kampagne
        /// </summary>
        /// <param name="campaign">Campaign</param>
        /// <returns>CampaignDto</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCampaign(Campaign campaign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _unitOfWork.Campaigns.AddAsync(campaign);
                await _unitOfWork.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(Get),
                    new { id = campaign.Id },
                    campaign);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ändert eine Kampagne
        /// </summary>
        /// <param name="id">campaignId</param>
        /// <param name="campaign">campaignDto</param>
        /// <returns>campaignId</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCampaign(int? id, Campaign campaign)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var campaignInDb = await _unitOfWork.Campaigns.GetByIdAsync(id.Value);

            if (campaignInDb != null)
            {
                campaignInDb.Name = campaign.Name;
                campaignInDb.From = campaign.From;
                campaignInDb.To = campaign.To;

                try
                {
                    _unitOfWork.Campaigns.Update(campaignInDb);
                    return Ok(await _unitOfWork.SaveChangesAsync());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Löscht eine bestimmte Kampagne
        /// </summary>
        /// <param name="id">campaignId</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCampaign(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var campaign = await _unitOfWork.Campaigns.GetByIdAsync(id.Value);

            if (campaign != null)
            {
                try
                {
                    _unitOfWork.Campaigns.Remove(campaign);
                    return Ok(await _unitOfWork.SaveChangesAsync());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else 
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Schaltet ein Testcenter für eine Kampagne frei
        /// </summary>
        /// <param name="id">campaignId</param>
        /// <param name="testCenterIdToAdd">testCenterId</param>
        /// <returns>CampaignDto</returns>
        [HttpPost("{id}/TestCenters/{testCenterIdToAdd}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddCampaignToTestCenter(int id, int testCenterIdToAdd)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Entfernt ein Testcenter von einer bestimmten Kampagne
        /// </summary>
        /// <param name="id">campaignId</param>
        /// <param name="testCenterIdToRemove">testCenterId</param>
        /// <returns></returns>
        [HttpDelete("{id}TTestCenters/{testCenterIdToRemove}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCampaignFromTestCener(int id, int testCenterIdToRemove)
        {
            throw new NotImplementedException();
        }
    }
}
