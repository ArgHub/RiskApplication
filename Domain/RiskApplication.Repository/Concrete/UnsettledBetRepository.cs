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
        private readonly string _filePath = DataFilePathFinder.GetUnsettledBetsDataFilePath();

        public UnsettledBetRepository(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public IEnumerable<UnsettledBet> GetAll()
        {
            _betRepository = new GenericBetRepository<UnsettledBet>(_filePath, new DataLoader<UnsettledBet>(_fileManager));
            return _betRepository.GetAll();
        }

        public IEnumerable<UnsettledBet> GetAll(int customerId)
        {
            _betRepository = new GenericBetRepository<UnsettledBet>(_filePath, new DataLoader<UnsettledBet>(_fileManager));
            return _betRepository.GetAll(customerId);
        }
    }
}
