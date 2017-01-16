using System;
using System.Collections.Generic;
using System.Linq;
using RiskApplication.Repository.Model;
using System.Threading.Tasks;

namespace RiskApplication.Repository.Abstract
{
    public interface ISettledBetRepository
    {
        IEnumerable<SettledBet> GetAll();
        IEnumerable<SettledBet> GetAll(int customerId);
    }
}
