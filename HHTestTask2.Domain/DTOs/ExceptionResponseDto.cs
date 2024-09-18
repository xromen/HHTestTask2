using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.DTOs
{
    public class ExceptionResponseDto
    {
        public string Type { get;set; }
        public string Id { get; set; }
        public DataDto Data { get; set; }
        public class DataDto
        {
            public string Message { get; set; }
        }
    }
}
