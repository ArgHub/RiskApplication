using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApplication.Repository.Model;

namespace RiskApplication.Repository.Abstract
{
    public interface IUnsettledBetRepository
    {
        IEnumerable<UnsettledBet> GetAll();
        IEnumerable<UnsettledBet> GetAll(int customerId);
    }
}
