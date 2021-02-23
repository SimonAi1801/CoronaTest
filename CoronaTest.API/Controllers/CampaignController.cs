using CoronaTest.API.DTOs;
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
            throw new NotImplementedException();
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
        public async Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
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
        public async Task<IActionResult> GetExaminationsByCampaignId(int id)
        {
            throw new NotImplementedException();
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
        public ActionResult<TestCenterDto[]> GetTestCentersByCampaignId(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Erstellt eine neue Kampagne
        /// </summary>
        /// <param name="campaignDto">CampaignDto</param>
        /// <returns>CampaignDto</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCampaign(CampaignDto campaignDto)
        {
            throw new NotImplementedException();
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
        /// Ändert eine Kampagne
        /// </summary>
        /// <param name="id">campaignId</param>
        /// <param name="campaignDto">campaignDto</param>
        /// <returns>campaignId</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCampaign(int id, CampaignDto campaignDto)
        {
            throw new NotImplementedException();
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
        public async Task<IActionResult> DeleteCampaign(int id)
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
