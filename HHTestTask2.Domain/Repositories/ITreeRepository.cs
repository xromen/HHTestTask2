using HHTestTask2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.Repositories
{
    public interface ITreeRepository : IRepository<Tree>
    {
        Task<Tree> GetByNameAsync(string name);
    }
}
