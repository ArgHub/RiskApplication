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
    public class UnsettledBetRepository : IUnsettledBetRepository
    {
        private readonly IDataLoader<UnsettledBet> _dataLoader;
        private GenericBetRepository<UnsettledBet> _betRepository;
        private readonly string _filePath = DataFilePathFinder.GetUnsettledBetsDataFilePath();

        public UnsettledBetRepository(IDataLoader<UnsettledBet> dataLoader)
        {
            _dataLoader = dataLoader;
        }

        public IEnumerable<UnsettledBet> GetAll()
        {
            _betRepository = new GenericBetRepository<UnsettledBet>(_filePath, _dataLoader);
            return _betRepository.GetAll();
        }

        public IEnumerable<UnsettledBet> GetAll(int customerId)
        {
            _betRepository = new GenericBetRepository<UnsettledBet>(_filePath, _dataLoader);
            return _betRepository.GetAll(customerId);
        }
    }
}
