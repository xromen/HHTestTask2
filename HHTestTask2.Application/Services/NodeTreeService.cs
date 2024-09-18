using AutoMapper;
using HHTestTask2.Domain;
using HHTestTask2.Domain.Common;
using HHTestTask2.Domain.DTOs;
using HHTestTask2.Domain.Entities;
using HHTestTask2.Domain.Exceptions;
using HHTestTask2.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Application.Services
{
    public class NodeTreeService : INodeTreeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NodeTreeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<NodeDto> CreateNode(string treeName, int? parentNodeId, string nodeName)
        {
            var treeExist = await _unitOfWork.TreeRepository.ExistAsync(c => c.Name == treeName);
            if (!treeExist)
                throw new SecureException("This tree doesn't exist.");

            var parentNodeExist = await _unitOfWork.NodeRepository.ExistAsync(c => c.Tree.Name == treeName && c.Id == parentNodeId, entitiesToInclude: ["Tree"]);
            if (parentNodeId.HasValue && !parentNodeExist)
                throw new SecureException("This node doesn't exist in this tree.");

            var nodeNameExist = await _unitOfWork.NodeRepository.ExistAsync(c => c.Tree.Name == treeName && c.Name == nodeName, entitiesToInclude: ["Tree"]);
            if (nodeNameExist)
                throw new SecureException("This node name already in use.");

            var tree = await _unitOfWork.TreeRepository.GetByNameAsync(treeName);

            var newNode = new Node()
            {
                Name = nodeName,
                Tree = tree,
                NodeId = parentNodeId
            };

            await _unitOfWork.NodeRepository.AddAsync(newNode);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<NodeDto>(newNode);
        }

        public async Task<NodeDto> DeleteNode(string treeName, int nodeId)
        {
            var treeExist = await _unitOfWork.TreeRepository.ExistAsync(c => c.Name == treeName);
            if (!treeExist)
                throw new SecureException("This tree doesn't exist.");

            var nodeExist = await _unitOfWork.NodeRepository.ExistAsync(c => c.Id == nodeId);
            if (!nodeExist)
                throw new SecureException("This node doesn't exist.");

            Node node = await _unitOfWork.NodeRepository.GetWithChildrenByNodeId(nodeId);
            if (node.Children != null && node.Children.Count != 0)
                throw new SecureException("You have to delete all children nodes first.");

            await _unitOfWork.NodeRepository.DeleteAsync(node);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<NodeDto>(node);
        }

        public async Task<IEnumerable<NodeDto>> GetNodesByTreeName(string treeName)
        {
            var exists = await _unitOfWork.TreeRepository.ExistAsync(c => c.Name == treeName);

            if (!exists)
                throw new SecureException("This tree doesn't exist.");

            var nodes = await _unitOfWork.NodeRepository.GetAllByTreeName(treeName);
            
            return _mapper.Map<IEnumerable<NodeDto>>(nodes);
        }

        public async Task<TreeDto> GetOrCreateTreeByName(string name)
        {
            var exists = await _unitOfWork.TreeRepository.ExistAsync(c => c.Name == name);

            if (!exists)
            {
                await _unitOfWork.TreeRepository.AddAsync(new Tree() { Name = name });
                await _unitOfWork.SaveAsync();
            }

            var tree = await _unitOfWork.TreeRepository.GetByNameAsync(name);
            var treeDto = _mapper.Map<TreeDto>(tree);

            treeDto.Children = await GetNodesByTreeName(name);

            return treeDto;
        }

        public async Task<NodeDto> RenameNode(string treeName, int nodeId, string newName)
        {
            var treeExist = await _unitOfWork.TreeRepository.ExistAsync(c => c.Name == treeName);
            if (!treeExist)
                throw new SecureException("This tree doesn't exist.");

            var parentNodeExist = await _unitOfWork.NodeRepository.ExistAsync(c => c.Id == nodeId);
            if (!parentNodeExist)
                throw new SecureException("This node doesn't exist.");

            var nodeNameExist = await _unitOfWork.NodeRepository.ExistAsync(c => c.Tree.Name == treeName && c.Name == newName, entitiesToInclude: ["Tree"]);
            if (nodeNameExist)
                throw new SecureException("This node name already in use.");

            var node = await _unitOfWork.NodeRepository.GetByIdAsync(nodeId);
            node.Name = newName;

            await _unitOfWork.NodeRepository.UpdateAsync(node);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<NodeDto>(node);
        }
    }
}
