using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RiskApplication.Repository.Abstract;
using RiskApplication.Repository.ViewModel;
using RiskApplication.Service.Abstract;
using RiskApplication.Service.Concrete;
using RiskApplication.WebApi.Controllers;

namespace RiskApplication.WebApi.Tests.Controllers
{
    [TestFixture]
    public class SetteledBetsControllerTests
    {
        private Mock<ISettledBettingService> _mockSettledBettingService;
        private SetteledBetsController _settledBetsController;

        [SetUp]
        public void Setup()
        {
            _mockSettledBettingService = new Mock<ISettledBettingService>();
            _settledBetsController = new SetteledBetsController(_mockSettledBettingService.Object);
        }

        [Test]
        public void GetSettledBets_returns_expected_data()
        {
            //Arrange
            List<CustomerBettingHistory> testBets = new List<CustomerBettingHistory>();

            //Setup
            _mockSettledBettingService.Setup(x => x.GetBettingsSummary()).Returns(testBets);

            //Act
            var result = _settledBetsController.GetSettledBets();

            //Assert
            Assert.AreEqual(result.GetType(), typeof(List<CustomerBettingHistory>));
            
            //Verify
            _mockSettledBettingService.Verify(x => x.GetBettingsSummary(), Times.Once, "ISettledBettingService - GetBettingsSummary() not get called!");
        }

        [Test]
        public void GetSettledBets_throws_expected_exception_when_error_occurs()
        {
            //Setup
            _mockSettledBettingService.Setup(x => x.GetBettingsSummary()).Throws<Exception>();

            //Act
            try
            {
                var result = _settledBetsController.GetSettledBets();
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
            _mockSettledBettingService = null;
            _settledBetsController = null;
        }
    }
}
