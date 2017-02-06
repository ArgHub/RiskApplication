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
    [RoutePrefix("apimvc")]
    public class UnsttledBetsController : ApiController
    {
        private readonly IUnsettledBettingService _unSettledBettingService;

        public UnsttledBetsController(IUnsettledBettingService unSettledBettingService)
        {
            _unSettledBettingService = unSettledBettingService;
        }

        [Route("unsettledbets")]
        public List<RiskyUnsettledBets> GetSettledBets()
        {
            try
            {
                return _unSettledBettingService.GetRiskyUnsettledBets();
            }
            catch (Exception ex)
            {
                throw new Exception("Server Error!");
            }
        }
    }
}
