using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RiskApplication.Repository.Abstract;
using RiskApplication.Repository.Model;
using RiskApplication.Repository.Tools;

namespace RiskApplication.Repository.Concrete
{
    public class SettledBetRepository : ISettledBetRepository
    {
        private readonly IDataLoader<SettledBet> _dataLoader;
        private GenericBetRepository<SettledBet> _betRepository;
        private readonly string _filePath = DataFilePathFinder.GetSettledBetsDataFilePath();

        public SettledBetRepository(IDataLoader<SettledBet> dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public IEnumerable<SettledBet> GetAll()
        {
            _betRepository = new GenericBetRepository<SettledBet>(_filePath, _dataLoader);
            return _betRepository.GetAll();
        }

        public IEnumerable<SettledBet> GetAll(int customerId)
        {
            _betRepository = new GenericBetRepository<SettledBet>(_filePath, _dataLoader);
            return _betRepository.GetAll(customerId);
        }
    }
}
