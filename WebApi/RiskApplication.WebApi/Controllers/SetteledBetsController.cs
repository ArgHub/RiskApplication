using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RiskApplication.Repository.ViewModel;
using RiskApplication.Service.Abstract;

namespace RiskApplication.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class SetteledBetsController : ApiController
    {
        private readonly ISettledBettingService _settledBettingService;

        public SetteledBetsController(ISettledBettingService settledBettingService)
        {
            _settledBettingService = settledBettingService;
        }

        [Route("settledbets")]
        public List<CustomerBettingHistory> GetSettledBets()
        {
            try
            {
                return _settledBettingService.GetBettingsSummary();
            }
            catch (Exception ex)
            {               
                throw new Exception("Server Error!");
            }
        }
    }
}
