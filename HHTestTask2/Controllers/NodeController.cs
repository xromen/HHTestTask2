using AutoMapper;
using HHTestTask2.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HHTestTask2.API.Controllers
{
    [Produces("application/json")]
    [Route("api.user.Tree.[controller].[action]")]
    [ApiController]
    public class NodeController : ControllerBase
    {
        private readonly INodeTreeService _nodeTreeService;
        private readonly IMapper _mapper;

        public NodeController(INodeTreeService nodeTreeService, IMapper mapper)
        {
            _nodeTreeService = nodeTreeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> create(string treeName, int? parentNodeId, string nodeName)
        {
            await _nodeTreeService.CreateNode(treeName, parentNodeId, nodeName);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> delete(string treeName, int nodeId)
        {
            await _nodeTreeService.DeleteNode(treeName, nodeId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> rename(string treeName, int nodeId, string newNodeName)
        {
            await _nodeTreeService.RenameNode(treeName, nodeId, newNodeName);

            return Ok();
        }
    }
}
