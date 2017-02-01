using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using RiskApplication.Repository.Abstract;
using RiskApplication.Repository.Model;
using RiskApplication.Service.Abstract;
using RiskApplication.Service.Concrete;

namespace RiskApplication.Service.Tests
{
    [TestFixture]
    public class SettledBetServiceTests
    {
        private Mock<ISettledBetRepository> _mockRepository;
        private ISettledBettingService _service;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new Mock<ISettledBetRepository>();
            _service = new SettledBettingService(_mockRepository.Object);
        }


        [Test]
        public void GetBettingsSummary_returns_data()
        {
            //Arrange
            List<SettledBet> settledBets = new List<SettledBet>()
            {
                new SettledBet()
                {
                    CustomerId =  1,
                    EventId = 1,
                    Participant = 6,
                    Stake = 50,
                    Win = 250
                },
                new SettledBet()
                {
                    CustomerId =  2,
                    EventId = 1,
                    Participant = 3,
                    Stake = 5,
                    Win = 0
                },
                new SettledBet()
                {
                    CustomerId =  3,
                    EventId = 1,
                    Participant = 3,
                    Stake = 20,
                    Win = 0
                },
                new SettledBet()
                {
                    CustomerId =  1,
                    EventId = 1,
                    Participant = 6,
                    Stake = 200,
                    Win = 1000
                },
                new SettledBet()
                {
                    CustomerId =  1,
                    EventId = 2,
                    Participant = 1,
                    Stake = 20,
                    Win = 60
                },
                new SettledBet()
                {
                    CustomerId =  2,
                    EventId = 2,
                    Participant = 1,
                    Stake = 5,
                    Win = 15
                }
            };

            //Setup
            _mockRepository.Setup(a => a.GetAll()).Returns(settledBets);

            //Act            
            var result = _service.GetBettingsSummary();

            //Assert
            Assert.AreEqual(3, result.Count);
            Assert.IsTrue(result.Find(a => a.CustomerId == 1).UnusualRateWinner);
            //Checking the count of unusual rate winners
            Assert.AreEqual(1,result.Count(a => a.UnusualRateWinner));
            Assert.IsFalse(result.Find(a => a.CustomerId == 2).UnusualRateWinner);
            Assert.AreEqual(20,result.Find(a => a.CustomerId == 3).AverageBet);

            //Verify
            _mockRepository.Verify(a => a.GetAll(), Times.Once, "ISettledBetRepository - GetAll not get called!");
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository = null;
            _service = null;
        }
    }
}
