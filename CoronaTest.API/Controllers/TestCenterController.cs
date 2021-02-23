using CoronaTest.API.DTOs;
using CoronaTest.Core.Contracts;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Liefert ein Testcenter zur Id
        /// </summary>
        /// <param name="id">testCenterId</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Liefert alle Untersuchungen zu einer Postleitzahl
        /// </summary>
        /// <param name="postalCode">Postleitzahl</param>
        /// <returns>TestCenterDto[]</returns>
        [HttpGet("byPostalCode/{postalCode}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByPostalCode(string postalCode)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Liefert alle Untersuchungen zu einem Testcenter
        /// </summary>
        /// <param name="id">testCenterId</param>
        /// <returns></returns>
        [HttpGet("{id}/Examinations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExaminationsByTestCenterId(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ein TestCenter wird erstellt
        /// </summary>
        /// <param name="testCenterDto">TestCenterDto</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateTestCenter(TestCenterDto testCenterDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Ändert ein Testcenter
        /// </summary>
        /// <param name="id">testCenterId</param>
        /// <param name="testCenterDto">testCenterDto</param>
        /// <returns>testCenterId</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTestCenter(int id, TestCenterDto testCenterDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Löscht ein Testcenter
        /// </summary>
        /// <param name="id">testCenterId</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult>RemoveTestCenter(int id)
        {
            throw new NotImplementedException();
        }
    }
}
