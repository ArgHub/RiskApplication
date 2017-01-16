using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiskApplication.Repository.Model;

namespace RiskApplication.Repository.Abstract
{
    public class GenericBetRepository<TGenericBet>
        where TGenericBet : GenericBet
    {
        private readonly string _filePath;
        private readonly IDataLoader<TGenericBet> _dataLoader;

        public GenericBetRepository(string filePath, IDataLoader<TGenericBet> dataLoader)
        {
            _filePath = filePath;
            _dataLoader = dataLoader;
        }

        public IEnumerable<TGenericBet> GetAll()
        {
            return _dataLoader.LoadDataFile(_filePath);
        }

        public IEnumerable<TGenericBet> GetAll(int customerId)
        {
            return _dataLoader.LoadDataFile(_filePath).Where(a=> a.CustomerId == customerId);
        }
    }
}
