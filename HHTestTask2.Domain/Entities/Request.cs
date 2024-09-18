using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.Entities
{
    public class Request : BaseEntity
    {
        public required string Path { get; set; }
        public string? QueryParameters { get; set; }
        public string? BodyParameters { get; set; }
        public required DateTime CreatedAt { get; set; }
    }
}
