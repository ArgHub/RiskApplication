using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApplication.Repository.ViewModel;

namespace RiskApplication.Service.Abstract
{
    public interface ISettledBettingService
    {
        List<CustomerBettingHistory> GetBettingsSummary();
    }
}
