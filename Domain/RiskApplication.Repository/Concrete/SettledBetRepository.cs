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
       
        private readonly IFileManager _fileManager;
        private GenericBetRepository<SettledBet> _betRepository;
        private readonly string _filePath = DataFilePathFinder.GetSettledBetsDataFilePath();

        public SettledBetRepository(IFileManager fileManager)
        {
        
            _fileManager = fileManager;
        }

        public IEnumerable<SettledBet> GetAll()
        {
            _betRepository = new GenericBetRepository<SettledBet>(_filePath, new DataLoader<SettledBet>(_fileManager));
            return _betRepository.GetAll();
        }

        public IEnumerable<SettledBet> GetAll(int customerId)
        {
            _betRepository = new GenericBetRepository<SettledBet>(_filePath, new DataLoader<SettledBet>(_fileManager));
            return _betRepository.GetAll(customerId);
        }
    }
}
