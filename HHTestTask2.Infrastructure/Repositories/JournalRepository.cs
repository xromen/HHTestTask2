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
    public class JournalRepository : Repository<Journal>, IJournalRepository
    {
        public JournalRepository(DbSet<Journal> journal) : base(journal)
        { 
        }
    }
}
