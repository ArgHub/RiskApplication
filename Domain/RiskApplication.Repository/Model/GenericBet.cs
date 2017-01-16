using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskApplication.Repository.Model
{
    public class GenericBet
    {
        public int CustomerId { get; set; }
        public int EventId { get; set; }
        public int Participant { get; set; }
        public int Stake { get; set; }
        public int WinIndex { get; set; }
    }
}
