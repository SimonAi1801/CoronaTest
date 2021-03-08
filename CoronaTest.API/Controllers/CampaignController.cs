using CoronaTest.Core.Contracts;
using CoronaTest.Core.DTOs;
using CoronaTest.Core.Entities;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Campaigns.GetAllAsync());
        }


        /// <summary>
        /// Liefert die Kampagne mit der übergebenen Id.
        /// </summary>
        /// <param name="id">CampaignId</param>
        /// <returns>CampaignDto</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var campaign = await _unitOfWork.Campaigns.GetDtoByIdAsync(id.Value);

            if (campaign == null)
            {
                return NotFound();
            }

            return Ok(campaign);
        }

        /// <summary>
        /// Abfrage der Untersuchungen einer bestimmten Kampagne
        /// </summary>
        /// <param name="id">campaignId</param>
        /// <returns>ExaminationDto</returns>
        [HttpGet("{id}/Examinations")]
        [Authorize(Roles = "Admin")]
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

            return Ok(examinations);
        }

        /// <summary>
        /// Abfrage des TestCenters einer bestimmten Kampagne
        /// </summary>
        /// <param name="id">campaignId</param>
        /// <returns>TestCenterDto</returns>
        [HttpGet("{id}/TestCenters")]
        [Authorize(Roles = "Admin")]
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

            return Ok(testCenter);
        }

        /// <summary>
        /// Erstellt eine neue Kampagne
        /// </summary>
        /// <param name="campaign">Campaign</param>
        /// <returns>CampaignDto</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCampaign(CampaignDto campaign)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newCampaign = new Campaign
            {
                Name = campaign.Name,
                From = campaign.From,
                To = campaign.To
            };

            try
            {
                await _unitOfWork.Campaigns.AddAsync(newCampaign);
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddCampaignToTestCenter(int? id, int? testCenterIdToAdd)
        {
            if (id == null || testCenterIdToAdd == null)
            {
                return BadRequest();
            }

            var campaign = await _unitOfWork.Campaigns.GetByIdAsync(id.Value);
            var testCenter = await _unitOfWork.TestCenters.GetByIdAsync(testCenterIdToAdd.Value);

            if (campaign != null && testCenter != null)
            {
                try
                {
                    testCenter.AvailableCampaigns.Add(campaign);
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
        /// Entfernt ein Testcenter von einer bestimmten Kampagne
        /// </summary>
        /// <param name="id">campaignId</param>
        /// <param name="testCenterIdToRemove">testCenterId</param>
        /// <returns></returns>
        [HttpDelete("{id}/TestCenters/{testCenterIdToRemove}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCampaignFromTestCener(int? id, int? testCenterIdToRemove)
        {
            if (id == null || testCenterIdToRemove == null)
            {
                return BadRequest();
            }

            var campaign = await _unitOfWork.Campaigns.GetByIdAsync(id.Value);
            var testCenter = await _unitOfWork.TestCenters.GetByIdAsync(testCenterIdToRemove.Value);

            if (campaign != null && testCenter != null)
            {
                try
                {
                    testCenter.AvailableCampaigns.Remove(campaign);
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
    }
}
