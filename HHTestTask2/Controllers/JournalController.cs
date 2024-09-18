using AutoMapper;
using HHTestTask2.Domain;
using HHTestTask2.Domain.Common;
using HHTestTask2.Domain.Entities;
using HHTestTask2.Domain.Exceptions;
using HHTestTask2.Domain.Models;
using HHTestTask2.Domain.Services;
using HHTestTask2.Infrastructure.Database;
using HHTestTask2.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace HHTestTask2.Controllers
{
    [Produces("application/json")]
    [Route("api.user.[controller].[action]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;
        private readonly IMapper _mapper;

        public JournalController(IJournalService journalService, IMapper mapper) 
        {
            _journalService = journalService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(MRange<MJournalInfo>), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> getRange([FromQuery] [Required] Pagination pagination, [FromBody] [Required] Filter filter)
        {
            var journals = await _journalService.GetJournals(pagination, filter);
            var count = await _journalService.GetJournalsCount(filter);

            MRange<MJournalInfo> range = new MRange<MJournalInfo>()
            {
                Skip = pagination.Skip,
                Count = count,
                Items = _mapper.Map<IEnumerable<MJournalInfo>>(journals)
            };

            return new JsonResult(range);
        }

        [ProducesResponseType(typeof(MJournal), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> getSingle([FromQuery][Required] int id)
        {
            var journal = await _journalService.GetJournal(id);

            return new JsonResult(_mapper.Map<MJournal>(journal));
        }
    }
}
