using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApplication.Repository.Model;

namespace RiskApplication.Repository.ViewModel
{
    public class CustomerBettingHistory : SettledBet
    {
        public bool UnusualRateWinner { get; set; }
        public int WinsCount { get; set; }
        public int BetsCount { get; set; }
        public double AverageBet { get; set; }
    }
}
