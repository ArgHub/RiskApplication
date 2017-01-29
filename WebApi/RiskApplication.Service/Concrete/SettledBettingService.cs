using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApplication.Repository.Abstract;
using RiskApplication.Repository.ViewModel;
using RiskApplication.Service.Abstract;

namespace RiskApplication.Service.Concrete
{
    public class SettledBettingService : ISettledBettingService
    {
        private readonly ISettledBetRepository _betRepository;

        public SettledBettingService(ISettledBetRepository betRepository)
        {
            _betRepository = betRepository;
        }

        public List<CustomerBettingHistory> GetBettingsSummary()
        {
            var allBets = _betRepository.GetAll();

            var groupedBets = allBets.GroupBy(c => c.CustomerId).Select(gb => new
            {
                CustomerId = gb.Key,
                WinsCount = gb.Count(a => a.IsWin),
                BetsCount = gb.Count(),
                AverageBet = gb.Average(a => a.Stake)
            }).Select(b => new CustomerBettingHistory
            {
                CustomerId = b.CustomerId,
                UnusualRateWinner = ((b.WinsCount/b.BetsCount) > 0.6),
                WinsCount = b.WinsCount,
                BetsCount = b.BetsCount,
                AverageBet = b.AverageBet
            });

            return groupedBets.ToList();
        }

    }
}
