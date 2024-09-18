using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HHTestTask2.Domain.Models
{
    public class MJournal
    {
        public string Text {  get; set; }
        public int Id { get;set; }
        public int Eventid { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
