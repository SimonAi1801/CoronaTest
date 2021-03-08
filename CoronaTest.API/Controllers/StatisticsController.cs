using CoronaTest.Core.Contracts;
using CoronaTest.Core.DTOs;
using CoronaTest.Core.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> Get()
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Liefert die Statistik über die Id
        ///// </summary>
        ///// <param name="id">statisticsId</param>
        ///// <returns></returns>
        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Liefert alle Statistiken in dem einegegebenen Zeitraum
        ///// </summary>
        ///// <param name="from">Startzeit</param>
        ///// <param name="to">Endzeit</param>
        ///// <returns></returns>
        //[HttpGet("{from}/{to}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetByTimeSpan(DateTime from, DateTime to)
        //{
        //    throw new NotImplementedException();
        //}

        ///// <summary>
        ///// Liefert alle Statistiken zur Postleitzahl und zu einem Zeitraum
        ///// </summary>
        ///// <param name="postalCode">Postleitzahl</param>
        ///// <param name="from">Startzeit</param>
        ///// <param name="to"></param>
        ///// <returns></returns>
        [HttpGet("byPostalCode/{postalCode}/{from}/{to}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByPostalCodeAndTimeSpan(string postalCode, DateTime from, DateTime to)
        {
            if (postalCode == null || from == null || to == null)
            {
                return BadRequest();
            }

            var testCenter = await _unitOfWork.TestCenters.GetByPostalCodeAsync(postalCode);
            var examinations = await _unitOfWork.Examinations.GetExaminationsWithFilterAsync(from, to);

            if (testCenter == null || examinations == null)
            {
                return NotFound();
            }

            return Ok(new StatisticsDto
            {
                CalenderWeek = Convert.ToInt32((from - to) / 7),
                CountOfExaminations = examinations.Count(),
                CountOfNegativeResults = examinations.Where(e => e.TestResult == TestResults.Negative).Count(),
                CountofPositiveResults = examinations.Where(e => e.TestResult == TestResults.Positive).Count(),
                CountOfUnkownResults = examinations.Where(e => e.TestResult != TestResults.Positive && e.TestResult == TestResults.Negative).Count(),
                Year = from.Year
            });
        }

        //[HttpGet("byCalenderWeek/{calenderWeek}/{from}/{to}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetByCalenderWeekAndTimeSpan(int calenderWeek, DateTime from, DateTime to)
        //{
        //    CultureInfo ciCurr = CultureInfo.CurrentCulture;
        //    int weekNum = ciCurr.Calendar.GetWeekOfYear(from, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

        //    throw new NotImplementedException();
        //}
    }
}
