using AutoMapper;
using HHTestTask2.Domain.Common;
using HHTestTask2.Domain.DTOs;
using HHTestTask2.Domain.Models;
using HHTestTask2.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HHTestTask2.API.Controllers
{
    [Produces("application/json")]
    [Route("api.user.[controller].[action]")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        private readonly INodeTreeService _nodeTreeService;
        private readonly IMapper _mapper;

        public TreeController(INodeTreeService nodeTreeService, IMapper mapper)
        {
            _nodeTreeService = nodeTreeService;
            _mapper = mapper;
        }

        [ProducesResponseType(typeof(TreeDto), StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> get([FromQuery][Required] string treeName)
        {
            var tree = await _nodeTreeService.GetOrCreateTreeByName(treeName);
            return new JsonResult(tree);
        }
    }
}
