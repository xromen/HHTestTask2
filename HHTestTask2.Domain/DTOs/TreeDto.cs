using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.DTOs
{
    public class TreeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public IEnumerable<NodeDto>? Children { get; set; }
    }
}
