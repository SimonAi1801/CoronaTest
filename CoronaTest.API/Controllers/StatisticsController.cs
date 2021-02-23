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
        /// Liefert die Statistik zurück, die bei diesem Zeitpunkt entsanden ist.
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns>StatistikDTO</returns>
        [HttpGet("timestamp")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<StatisticsDto> Get(DateTime timestamp)
        {
            if (timestamp >= DateTime.Now)
            {
                return NotFound("Der Zeitpunkt liegt in der Zukunft!");
            }
            else
            {
                return Ok(new StatisticsDto 
                {
                    
                });
            }
        }

    }
}
