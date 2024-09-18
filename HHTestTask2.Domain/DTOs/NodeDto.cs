using HHTestTask2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.DTOs
{
    public class NodeDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        public List<NodeDto>? Children { get; set; }
    }
}
