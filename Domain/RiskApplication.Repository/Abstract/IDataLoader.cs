using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.Generic;
using RiskApplication.Repository.Model;

namespace RiskApplication.Repository.Abstract
{
    public interface IDataLoader<out TGenericBet> where TGenericBet : GenericBet
    {
        IEnumerable<TGenericBet> LoadDataFile(string filePath);
    }
}
