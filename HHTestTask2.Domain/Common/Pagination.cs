using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.Common
{
    public class Pagination
    {
        public int Skip { get; set; } = 0;
        public int Limit { get; set; } = 10;
    }
}
