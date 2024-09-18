using HHTestTask2.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        ITreeRepository TreeRepository { get; set; }
        INodeRepository NodeRepository { get; set; }
        IRequestRepository RequestRepository { get; set; }
        IJournalRepository JournalRepository { get; set; }
        Task<int> SaveAsync();
    }
}
