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
    public class ExaminationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExaminationController(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
    }
}
