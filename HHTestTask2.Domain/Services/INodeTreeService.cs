using HHTestTask2.Domain.Common;
using HHTestTask2.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.Services
{
    public interface INodeTreeService
    {
        Task<TreeDto> GetOrCreateTreeByName(string name);
        Task<IEnumerable<NodeDto>> GetNodesByTreeName(string treeName);
        Task<NodeDto> CreateNode(string treeName, int? parentNodeId, string nodeName);
        Task<NodeDto> DeleteNode(string treeName, int nodeId);
        Task<NodeDto> RenameNode(string treeName, int nodeId, string newName);
    }
}
