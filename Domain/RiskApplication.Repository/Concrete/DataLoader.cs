using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using RiskApplication.Repository.Model;
using System.Threading.Tasks;
using RiskApplication.Repository.Abstract;
using RiskApplication.Repository.Tools;

namespace RiskApplication.Repository.Concrete
{
    public class DataLoader<TGenericBet> : IDataLoader<TGenericBet>
        where TGenericBet : GenericBet, new()
    {
        private readonly IFileManager _fileManager;
        private readonly List<TGenericBet> _genericBets = new List<TGenericBet>();

        public DataLoader(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }

        public IEnumerable<TGenericBet> LoadDataFile(string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                    throw new ArgumentException("The path specified is invalid.");

                if (!_fileManager.FileExists(filePath))
                    throw new FileNotFoundException("File not found as per the path specified. Path: " + filePath);

                var lines = _fileManager.ReadAllLines(filePath);

                foreach (var betDataTuple in lines.Skip(1).Select(line => line.ParseData()))
                {
                    var bet = new TGenericBet()
                    {
                        CustomerId = betDataTuple.Item1,
                        EventId = betDataTuple.Item2,
                        Participant = betDataTuple.Item3,
                        Stake = betDataTuple.Item4,
                        WinIndex = betDataTuple.Item5
                    };
                    _genericBets.Add(bet);
                }

                return _genericBets;
            }
            catch (Exception exception)
            {
                throw new Exception("An error occured when trying to read the file content; File path: " + filePath,
                    exception);
            }
        }
    }
}
