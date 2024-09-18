using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.Models
{
    public class MRange<TItem>
    {
        public required int Skip { get; set; }
        public required int Count { get; set; }
        public required IEnumerable<TItem> Items { get; set; }
    }
}
