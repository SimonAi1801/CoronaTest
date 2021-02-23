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
        public bool Get()
        {
            return true;
        }

    }
}
