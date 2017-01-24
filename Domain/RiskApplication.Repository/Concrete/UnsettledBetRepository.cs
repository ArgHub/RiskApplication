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
        private readonly IFileManager _fileManager;
        private GenericBetRepository<UnsettledBet> _betRepository;
        private readonly IDataPathFinder _dataPathFinder;

        public UnsettledBetRepository(IFileManager fileManager, IDataPathFinder dataPathFinder)
        {
            _fileManager = fileManager;
            _dataPathFinder = dataPathFinder;
        }

        public IEnumerable<UnsettledBet> GetAll()
        {
            _betRepository = new GenericBetRepository<UnsettledBet>(_dataPathFinder.GetUnsettledBetsDataFilePath(), new DataLoader<UnsettledBet>(_fileManager));
            return _betRepository.GetAll();
        }

        public IEnumerable<UnsettledBet> GetAll(int customerId)
        {
            _betRepository = new GenericBetRepository<UnsettledBet>(_dataPathFinder.GetUnsettledBetsDataFilePath(), new DataLoader<UnsettledBet>(_fileManager));
            return _betRepository.GetAll(customerId);
        }
    }
}
