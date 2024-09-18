using HHTestTask2.Domain;
using HHTestTask2.Domain.Repositories;
using HHTestTask2.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        public ITreeRepository TreeRepository { get; set; }
        public INodeRepository NodeRepository { get; set; }
        public IRequestRepository RequestRepository { get; set; }
        public IJournalRepository JournalRepository { get; set; }

        private readonly ApplicationContext _applicationContext;

        public UnitOfWork(ApplicationContext applicationContext, 
                          ITreeRepository treeRepository,
                          INodeRepository nodeRepository,
                          IRequestRepository requestRepository,
                          IJournalRepository journalRepository)
        {
            _applicationContext = applicationContext;

            TreeRepository = treeRepository;
            NodeRepository = nodeRepository;
            RequestRepository = requestRepository;
            JournalRepository = journalRepository;
        }

        public async Task<int> SaveAsync() => await _applicationContext.SaveChangesAsync();

        public void Dispose() => _applicationContext.Dispose();
    }
}
