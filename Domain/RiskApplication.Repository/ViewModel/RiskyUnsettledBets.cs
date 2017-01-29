using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApplication.Repository.Model;

namespace RiskApplication.Repository.ViewModel
{
    public class RiskyUnsettledBets : UnsettledBet
    {
        //public int BetsCount { get; set; }
        public bool IsBetFromUnusualWinner { get; set; }
        public bool IsUnusualBet { get; set; }
        public bool IsHighlyUnusualBet { get; set; }
    }
}
