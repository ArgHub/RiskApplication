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
    public class UnsettledBettingService : IUnsettledBettingService
    {
        private readonly IUnsettledBetRepository _betRepository;
        private readonly ISettledBettingService _settledBettingService;

        public UnsettledBettingService(IUnsettledBetRepository betRepository, ISettledBettingService settledBettingService)
        {
            _betRepository = betRepository;
            _settledBettingService = settledBettingService;
        }

        public List<RiskyUnsettledBets> GetRiskyUnsettledBets()
        {
            var betsHistory = _settledBettingService.GetBettingsSummary();
            var allBets = _betRepository.GetAll();

            /**Used GroupJoin to produce a list of entries in the first list (allBets), 
            each with a group of joined entries in the second list ().**/
            var unsettledBets = allBets.GroupJoin(betsHistory, ub => ub.CustomerId, sb => sb.CustomerId, (ub, bets) => new
                {
                    UnsettledBet = ub,
                    CustomerBettings = bets
                }).SelectMany(a => a.CustomerBettings.DefaultIfEmpty(),
                (a, bet) => new
                {
                    a.UnsettledBet,
                    CustomerBetting = bet
                });

            return unsettledBets.Select(ub => new RiskyUnsettledBets
            {
                CustomerId = ub.UnsettledBet.CustomerId,
                EventId = ub.UnsettledBet.EventId,
                Participant = ub.UnsettledBet.Participant,
                ToWin = ub.UnsettledBet.ToWin,
                Stake = ub.UnsettledBet.Stake,
                IsBetFromUnusualWinner = ub.CustomerBetting != null &&
                                            ub.CustomerBetting.UnusualRateWinner,
                IsUnusualBet = ub.CustomerBetting != null &&
                                            ub.UnsettledBet.Stake > (ub.CustomerBetting.AverageBet) * 10,
                IsHighlyUnusualBet = ub.CustomerBetting != null &&
                                            ub.UnsettledBet.Stake > (ub.CustomerBetting.AverageBet) * 30
            }).ToList();

        }
    }
}
