using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HHTestTask2.Domain;
using HHTestTask2.Domain.Common;
using HHTestTask2.Domain.DTOs;
using HHTestTask2.Domain.Entities;
using HHTestTask2.Domain.Exceptions;
using HHTestTask2.Domain.Services;

namespace HHTestTask2.Application.Services
{
    public class JournalService : IJournalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public JournalService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<JournalDto> AddJournal(JournalDto journalDto)
        {
            var journal = _mapper.Map<Journal>(journalDto);
            journal.Request = await _unitOfWork.RequestRepository.GetByIdAsync(journal.Request.Id);

            await _unitOfWork.JournalRepository.AddAsync(journal);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<JournalDto>(journal);
        }

        public async Task<JournalDto> GetJournal(int journalId)
        {
            var exists = await _unitOfWork .JournalRepository.ExistAsync(c => c.Id == journalId);
            if (!exists)
                throw new SecureException("The journal doesn't exist.");

            var journal = _unitOfWork.JournalRepository.GetByIdAsync(journalId, entitiesToInclude: ["Request"]);
            var journalDto = _mapper.Map<JournalDto>(journal);

            return journalDto;
        }

        public async Task<IEnumerable<JournalDto>> GetJournals()
        {
            var journals = await _unitOfWork.JournalRepository.GetAllAsync(entitiesToInclude: ["Request"]);
            var journalsDto = _mapper.Map<IEnumerable<JournalDto>>(journals);

            return journalsDto;
        }

        public async Task<IEnumerable<JournalDto>> GetJournals(Pagination pagination, Filter filter)
        {
            IEnumerable<Expression<Func<Journal, bool>>> predicates = GetPredicates(filter);

            var journals = await _unitOfWork.JournalRepository.GetAllAsync(pagination.Skip, pagination.Limit, predicates, entitiesToInclude: ["Request"]);
            var journalsDto = _mapper.Map<IEnumerable<JournalDto>>(journals);

            return journalsDto;
        }

        public async Task<int> GetJournalsCount(Filter filter)
        {
            IEnumerable<Expression<Func<Journal, bool>>> predicates = GetPredicates(filter);

            var count = await _unitOfWork.JournalRepository.Count(predicates, entitiesToInclude: ["Request"]);

            return count;
        }

        private IEnumerable<Expression<Func<Journal, bool>>> GetPredicates(Filter filter)
        {
            List<Expression<Func<Journal, bool>>> predicates = new List<Expression<Func<Journal, bool>>>();

            if (filter != null)
            {
                if (filter.From.HasValue)
                    predicates.Add(c => c.Request.CreatedAt >= filter.From);

                if (filter.To.HasValue)
                    predicates.Add(c => c.Request.CreatedAt <= filter.To);

                if (!string.IsNullOrEmpty(filter.Search))
                    predicates.Add(c => c.StackTrace.ToLower().Contains(filter.Search.ToLower()));
            }

            return predicates;
        }
    }
}
