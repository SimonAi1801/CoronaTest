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
    public class StatisticsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticsController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }

        /// <summary>
        /// Liefert alle Statistiken zurück
        /// </summary>
        /// <returns>StatistikDTO[]</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Liefert die Statistik über die Id
        /// </summary>
        /// <param name="id">statisticsId</param>
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
        /// Liefert alle Statistiken in dem einegegebenen Zeitraum
        /// </summary>
        /// <param name="from">Startzeit</param>
        /// <param name="to">Endzeit</param>
        /// <returns></returns>
        [HttpGet("{from}/{to}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByTimeSpan(DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Liefert alle Statistiken zur Postleitzahl und zu einem Zeitraum
        /// </summary>
        /// <param name="postalCode">Postleitzahl</param>
        /// <param name="from">Startzeit</param>
        /// <param name="to"></param>
        /// <returns></returns>
        [HttpGet("byPostalCode/{postalCode}/{from}/{to}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByPostalCodeAndTimeSpan(string postalCode, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }
    }
}
