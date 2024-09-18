using HHTestTask2.Domain.Entities;
using HHTestTask2.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Infrastructure.Repositories
{
    public class TreeRepository : Repository<Tree>, ITreeRepository
    {
        public TreeRepository(DbSet<Tree> trees) : base(trees)
        {
        }

        public async Task<Tree> GetByNameAsync(string name)
        {
            return await _entities.FirstOrDefaultAsync(e => e.Name == name);
        }
    }

}
