using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskApplication.Repository.Abstract
{
    public interface IDataPathFinder
    {
        string GetSettledBetsDataFilePath();
        string GetUnsettledBetsDataFilePath();
    }
}
