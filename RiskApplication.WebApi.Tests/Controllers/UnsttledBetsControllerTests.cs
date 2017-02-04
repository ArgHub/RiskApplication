using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using RiskApplication.Repository.ViewModel;
using RiskApplication.Service.Abstract;
using RiskApplication.WebApi.Controllers;

namespace RiskApplication.WebApi.Tests.Controllers
{
    [TestFixture]
    public class UnsttledBetsControllerTests
    {
        private Mock<IUnsettledBettingService> _mockUnsettledBettingService;
        private UnsttledBetsController _unsttledBetsController;

        [SetUp]
        public void Setup()
        {
            _mockUnsettledBettingService = new Mock<IUnsettledBettingService>();
            _unsttledBetsController = new UnsttledBetsController(_mockUnsettledBettingService.Object);
        }

        [Test]
        public void GetSettledBets_returns_expected_data()
        {
            //Arrange
            List<RiskyUnsettledBets> testBets = new List<RiskyUnsettledBets>();
 
            //Setup
            _mockUnsettledBettingService.Setup(x => x.GetRiskyUnsettledBets()).Returns(testBets);

            //Act
            var result =_unsttledBetsController.GetSettledBets();
                      
            //Assert
            Assert.AreEqual(result.GetType(), typeof(List<RiskyUnsettledBets>));
            
            //Verify
            _mockUnsettledBettingService.Verify(x => x.GetRiskyUnsettledBets(), Times.Once, "IUnsettledBettingService - GetRiskyUnsettledBets() not get called!");
        }

        [Test]
        public void GetSettledBets_throws_expected_exception_when_error_occurs()
        {
            //Setup
            _mockUnsettledBettingService.Setup(x => x.GetRiskyUnsettledBets()).Throws<Exception>();

            //Act
            try
            {
                var result = _unsttledBetsController.GetSettledBets();
            }
            catch (Exception ex)
            {
                if (ex.Message == "Server Error!") return;
            }
           
            //Assert
            Assert.Fail("Server Error not thrown!");
        }

        [TearDown]
        public void TearDown()
        {
            _mockUnsettledBettingService = null;
            _unsttledBetsController = null;
        }
    }
}
