using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Moq;
using NUnit.Framework;
using RiskApplication.Repository.Abstract;
using RiskApplication.Repository.Model;
using RiskApplication.Repository.ViewModel;
using RiskApplication.Service.Abstract;
using RiskApplication.Service.Concrete;

namespace RiskApplication.Service.Tests
{
    [TestFixture]
    public class UnsetlledBetServiceTests
    {
        private Mock<IUnsettledBetRepository> _mockRepository;
        private Mock<ISettledBettingService> _mockSettledBettingServiceservice;
        private IUnsettledBettingService unsettledBettingServiceservice;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<IUnsettledBetRepository>();
            _mockSettledBettingServiceservice = new Mock<ISettledBettingService>();
            unsettledBettingServiceservice = new UnsettledBettingService(_mockRepository.Object, _mockSettledBettingServiceservice.Object);
        }

        [Test]
        public void GetRiskyUnsettledBets_returns_data()
        {
            //Arrange
            List<CustomerBettingHistory> bettingSummary = new List<CustomerBettingHistory>()
            {
                new CustomerBettingHistory()
                {
                  CustomerId =  1, EventId = 1,Participant = 6, AverageBet = 30,UnusualRateWinner = true, WinsCount = 3, BetsCount = 3                        
                },
                new CustomerBettingHistory()
                {
                  CustomerId =  2, EventId = 1,Participant = 6, AverageBet = 5,UnusualRateWinner = false, WinsCount = 1, BetsCount = 2                        
                },
                new CustomerBettingHistory()
                {
                  CustomerId =  3, EventId = 1,Participant = 6, AverageBet = 50,UnusualRateWinner = true, WinsCount = 7, BetsCount = 10                        
                },
                new CustomerBettingHistory()
                {
                  CustomerId =  4, EventId = 1,Participant = 6, AverageBet = 10,UnusualRateWinner = false, WinsCount = 3, BetsCount = 7                        
                },
            };

            List<UnsettledBet> unsettledBets = new List<UnsettledBet>()
            {
                new UnsettledBet(){CustomerId = 1,EventId = 1,Participant = 6,Stake = 400,ToWin = 700}, 
                new UnsettledBet(){CustomerId = 1,EventId = 1,Participant = 6,Stake = 1000,ToWin = 5000},  
                new UnsettledBet(){CustomerId = 1,EventId = 1,Participant = 6,Stake = 250,ToWin = 600}, 
                new UnsettledBet(){CustomerId = 2,EventId = 1,Participant = 6,Stake = 100,ToWin = 700},  
                new UnsettledBet(){CustomerId = 2,EventId = 1,Participant = 6,Stake = 30,ToWin = 100},
                new UnsettledBet(){CustomerId = 2,EventId = 1,Participant = 6,Stake = 160,ToWin = 300},       
                new UnsettledBet(){CustomerId = 3,EventId = 1,Participant = 6,Stake = 140,ToWin = 1000},
                new UnsettledBet(){CustomerId = 3,EventId = 1,Participant = 6,Stake = 1800,ToWin = 4000},
                new UnsettledBet(){CustomerId = 3,EventId = 1,Participant = 6,Stake = 550,ToWin = 900},
                new UnsettledBet(){CustomerId = 4,EventId = 1,Participant = 6,Stake = 350,ToWin = 2000} ,
                new UnsettledBet(){CustomerId = 4,EventId = 1,Participant = 6,Stake = 120,ToWin = 850} 
            };



            //Setup
            _mockRepository.Setup(a => a.GetAll()).Returns(unsettledBets);
            _mockSettledBettingServiceservice.Setup(a => a.GetBettingsSummary()).Returns(bettingSummary);

            //Act            
            var result = unsettledBettingServiceservice.GetRiskyUnsettledBets();

            //Assert
            Assert.AreEqual(11, result.Count());
            Assert.AreEqual(4, result.Count(a => a.IsHighlyUnusualBet));
            Assert.AreEqual(8, result.Count(a => a.IsUnusualBet));
            Assert.AreEqual(6, result.Count(b => b.IsBetFromUnusualWinner));

            //Verify
            _mockRepository.Verify(a => a.GetAll(), Times.Once, "IUnsettledBetRepository - GetAll not get called!");
            _mockSettledBettingServiceservice.Verify(a => a.GetBettingsSummary(), Times.Once, "ISettledBettingService - GetBettingsSummary not get called!");
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository = null;
            _mockSettledBettingServiceservice = null;
            unsettledBettingServiceservice = null;
        }
    }
}
