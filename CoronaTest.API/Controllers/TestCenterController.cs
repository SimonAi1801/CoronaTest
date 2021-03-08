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
    [Route("api/[controller]")]
    [ApiController]
    public class TestCenterController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TestCenterController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        /// Liefert alle Testcenter
        /// </summary>
        /// <returns>TestCenterDto[]</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        => Ok(await _unitOfWork.TestCenters.GetAllAsync());

        /// <summary>
        /// Liefert ein Testcenter zur Id
        /// </summary>
        /// <param name="id">testCenterId</param>
        /// <returns></returns>
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

            var testCenter = await _unitOfWork.TestCenters.GetDtoByIdAsync(id.Value);

            if (testCenter == null)
            {
                return NotFound();
            }

            return Ok(testCenter);
        }

        /// <summary>
        /// Liefert alle Untersuchungen zu einer Postleitzahl
        /// </summary>
        /// <param name="postalCode">Postleitzahl</param>
        /// <returns>TestCenterDto[]</returns>
        [HttpGet("byPostalCode/{postalCode}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByPostalCode(string postalCode)
        {
            if (postalCode == null)
            {
                return BadRequest();
            }

            var testCenters = await _unitOfWork.TestCenters.GetByPostalCodeAsync(postalCode);

            if (testCenters == null)
            {
                return NotFound();
            }

            return Ok(testCenters);
        }

        /// <summary>
        /// Liefert alle Untersuchungen zu einem Testcenter
        /// </summary>
        /// <param name="id">testCenterId</param>
        /// <returns></returns>
        [HttpGet("{id}/Examinations")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExaminationsByTestCenterId(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var examinations = await _unitOfWork.Examinations.GetByTestCenterIdAsync(id.Value);

            if (examinations == null)
            {
                return NotFound();
            }

            return Ok(examinations);
        }

        /// <summary>
        /// Ein TestCenter wird erstellt
        /// </summary>
        /// <param name="testCenterDto">TestCenterDto</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateTestCenter(TestCenterDto testCenterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newTestCenter = new TestCenter
            {
                City = testCenterDto.City,
                SlotCapacity = testCenterDto.SlotCapacity,
                Street = testCenterDto.Street,
                Name = testCenterDto.Name,
                Postalcode = testCenterDto.Postalcode
            };

            try
            {
                await _unitOfWork.TestCenters.AddAsync(newTestCenter);
                await _unitOfWork.SaveChangesAsync();

                return CreatedAtAction(
                    nameof(Get),
                    new { id = newTestCenter.Id },
                    newTestCenter);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Ändert ein Testcenter
        /// </summary>
        /// <param name="id">testCenterId</param>
        /// <param name="testCenterDto">testCenterDto</param>
        /// <returns>testCenterId</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTestCenter(int? id, TestCenterDto testCenterDto)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var tesCenterInDb = await _unitOfWork.TestCenters.GetByIdAsync(id.Value);

            if (tesCenterInDb != null)
            {
                tesCenterInDb.City = testCenterDto.City;
                tesCenterInDb.SlotCapacity = testCenterDto.SlotCapacity;
                tesCenterInDb.Street = testCenterDto.Street;
                tesCenterInDb.Name = testCenterDto.Name;
                tesCenterInDb.Postalcode = testCenterDto.Postalcode;

                try
                {
                    _unitOfWork.TestCenters.Update(tesCenterInDb);
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
        /// Löscht ein Testcenter
        /// </summary>
        /// <param name="id">testCenterId</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>RemoveTestCenter(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var testCenter = await _unitOfWork.TestCenters.GetByIdAsync(id.Value);

            if (testCenter != null)
            {
                try
                {
                    _unitOfWork.TestCenters.Remove(testCenter);
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
