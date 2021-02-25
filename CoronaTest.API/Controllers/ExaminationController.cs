using CoronaTest.Core.Contracts;
using CoronaTest.Core.DTOs;
using CoronaTest.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoronaTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExaminationController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        /// Liefert alle Untersuchungen
        /// </summary>
        /// <returns>examinationDto[]</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _unitOfWork.Examinations.GetAllAsync());
        }

        /// <summary>
        /// Liefert eine Untersuchung
        /// </summary>
        /// <param name="id">examinationId</param>
        /// <returns>examinationDto</returns>
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

            var examination = await _unitOfWork.Examinations.GetDtoByIdAsync(id.Value);

            if (examination == null)
            {
                return NotFound();
            }

            return Ok(examination);
        }

        /// <summary>
        /// Liefert eine Untersuchung im Zeitraum
        /// </summary>
        /// <param name="from">Startpunkt</param>
        /// <param name="to">Endpunkt</param>
        /// <returns>examinationDto[]</returns>
        [HttpGet("{from}/{to}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExaminationByTimeSpan(DateTime from, DateTime to)
        {
            if (from == null || to == null)
            {
                return BadRequest();
            }

            var examinations = await _unitOfWork.Examinations.GetExaminationsWithFilterAsync(from, to);

            if (examinations == null)
            {
                return NotFound();
            }

            return Ok(examinations);
        }

        /// <summary>
        /// Erstellt eine Uuntersuchung
        /// </summary>
        /// <param name="examinationDto">examinationDto</param>
        /// <returns>examinationDto</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateExamination(ExaminationDto examinationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newExamination = new Examination
            {
                ExaminationAt = examinationDto.ExaminationAt,
                ExaminationState = examinationDto.ExaminationState,
                Campaign = examinationDto.Campaign,
                TestCenter = examinationDto.TestCenter,
                Identifier = examinationDto.Identifier,
                Participant = examinationDto.Participant,
                TestResult = examinationDto.TestResult
            };

            try
            {
                await _unitOfWork.Examinations.AddAsync(newExamination);
                await _unitOfWork.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(Get),
                    new { id = newExamination.Id },
                    newExamination);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ändert eine Untersuchung
        /// </summary>
        /// <param name="id">examinationId</param>
        /// <param name="examinationDto">examinationDto</param>
        /// <returns>examinationId</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateExamination(int? id, ExaminationDto examinationDto)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var examinationInDb = await _unitOfWork.Examinations.GetByIdAsync(id.Value);

            if (examinationInDb != null)
            {
                examinationInDb.Identifier = examinationDto.Identifier;
                examinationInDb.Participant = examinationDto.Participant;
                examinationInDb.TestCenter = examinationDto.TestCenter;
                examinationInDb.TestResult = examinationDto.TestResult;
                examinationInDb.Campaign = examinationDto.Campaign;
                examinationInDb.ExaminationAt = examinationDto.ExaminationAt;
                examinationInDb.ExaminationState = examinationDto.ExaminationState;
                try
                {
                    _unitOfWork.Examinations.Update(examinationInDb);
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
        /// Löscht eine Untersuchung
        /// </summary>
        /// <param name="id">examinationId</param>
        /// <returns>examinationId</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExamination(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var examination = await _unitOfWork.Campaigns.GetByIdAsync(id.Value);

            if (examination != null)
            {
                try
                {
                    _unitOfWork.Campaigns.Remove(examination);
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
