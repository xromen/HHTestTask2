using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.DTOs
{
    public class RequestDto
    {
        public required int Id { get; set; }
        public required string Path { get; set; }
        public string? QueryParameters { get; set; }
        public string? BodyParameters { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
