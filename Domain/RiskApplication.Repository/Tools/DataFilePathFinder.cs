using System.IO;
using System.Web;

namespace RiskApplication.Repository.Tools
{
    public static class DataFilePathFinder
    {
        public static string GetSettledBetsDataFilePath()
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "settled.csv");
        }

        public static string GetUnsettledBetsDataFilePath()
        {
            return Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "unsettled.csv");
        }
    }
}
