using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using RiskApplication.Repository.Abstract;

namespace RiskApplication.Repository.Concrete
{
    public class DataPathFinder : IDataPathFinder
    {
        public string GetSettledBetsDataFilePath()
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "settled.csv");
        }

        public string GetUnsettledBetsDataFilePath()
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "unsettled.csv");
        }
    }
}
