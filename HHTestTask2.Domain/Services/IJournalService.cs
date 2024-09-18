using HHTestTask2.Domain.Common;
using HHTestTask2.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.Services
{
    public interface IJournalService
    {
        Task<JournalDto> AddJournal(JournalDto journal);
        Task<JournalDto> GetJournal(int journalId);
        Task<IEnumerable<JournalDto>> GetJournals();
        Task<IEnumerable<JournalDto>> GetJournals(Pagination pagination, Filter filter);
        Task<int> GetJournalsCount(Filter filter);
    }
}
