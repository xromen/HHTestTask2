using HHTestTask2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.DTOs
{
    public class JournalDto
    {
        public int Id { get; set; }
        public required string ExceptionType { get; set; }
        public required string ExceptionMessage { get; set; }
        public string? ExceptionData { get; set; }
        public required string StackTrace { get; set; }
        public required RequestDto Request { get; set; }
    }
}
