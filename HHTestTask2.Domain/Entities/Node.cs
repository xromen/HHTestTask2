using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.Entities
{
    public class Node : BaseEntity
    {
        public required string Name { get; set; }
        public required Tree Tree { get; set; }

        public int? NodeId { get; set; }
        public List<Node>? Children { get; set; }
    }
}
