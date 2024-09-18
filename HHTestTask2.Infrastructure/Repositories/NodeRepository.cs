using HHTestTask2.Domain.Entities;
using HHTestTask2.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Infrastructure.Repositories
{
    public class NodeRepository : Repository<Node>, INodeRepository
    {
        public NodeRepository(DbSet<Node> nodes) : base(nodes)
        {
        }

        public async Task<IEnumerable<Node>> GetAllByTreeName(string treeName)
        {
            await _entities.Include(c => c.Tree).Where(c => c.Tree.Name == treeName).LoadAsync();
            return await _entities.Where(c => c.Tree.Name == treeName && c.NodeId == null).ToListAsync();
        }

        public async Task<Node?> GetWithChildrenByNodeId(int nodeId)
        {
            await _entities.Include(c => c.Tree).Where(c => c.NodeId == nodeId).LoadAsync();
            return await _entities.FirstOrDefaultAsync(c => c.Id == nodeId);
        }
    }
}
