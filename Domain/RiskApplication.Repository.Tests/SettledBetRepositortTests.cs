using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using RiskApplication.Repository.Abstract;
using RiskApplication.Repository.Concrete;
using RiskApplication.Repository.Model;

namespace RiskApplication.Repository.Tests
{
    [TestFixture, Description("These are unit tests related to SettledBet Repository"), Category("SettledBet Repository Unit Tests")]
    public class SettledBetRepositortTests
    {
        private Mock<IFileManager> _mockFileManager;
        private Mock<IDataPathFinder> _mockDataPathFinder;
        SettledBetRepository repository;
        private string _filePath = "Path"; 

        [SetUp]
        public void SetUp()
        {
            _mockFileManager = new Mock<IFileManager>();
            _mockDataPathFinder = new Mock<IDataPathFinder>();
            repository = new SettledBetRepository(_mockFileManager.Object,_mockDataPathFinder.Object);
        }

        [Test, Description("This is unit test for GetAll method when file has no record!"), Category("SettledBet Repository Unit Tests")]
        public void GetAll_method_returns_zero_record_when_file_has_no_record()
        {
            //Arrange
            //Setup
            _mockDataPathFinder.Setup(a => a.GetSettledBetsDataFilePath()).Returns(_filePath);
            _mockFileManager.Setup(a => a.ReadRecords(_filePath)).Returns(new[] { "CId,EId,P,S,W" });
            _mockFileManager.Setup(a => a.FileExists(_filePath)).Returns(true);

            //Act
            //Assert
            Assert.AreEqual(0, repository.GetAll().Count());

            //Verify
            _mockFileManager.Verify(a => a.FileExists(_filePath), Times.Once, "IFileManager - FileExists not get called!");
            _mockFileManager.Verify(a => a.ReadRecords(_filePath), Times.Once, "IFileManager - ReadRecords not get called!");
        }

        [Test, Description("This is unit test for GetAll method when file has records!"), Category("SettledBet Repository Unit Tests")]
        public void GetAll_method_returns_expected_records_when_file_has_records()
        {
            //Arrange
            //Setup
            _mockDataPathFinder.Setup(a => a.GetSettledBetsDataFilePath()).Returns(_filePath);
            _mockFileManager.Setup(a => a.ReadRecords(_filePath)).Returns(new[] { "CId,EId,P,S,W", "1,2,3,4,5", "1,2,3,4,5" });                                
            _mockFileManager.Setup(a => a.FileExists(_filePath)).Returns(true);

            //Act
            //Assert
            Assert.AreEqual(2, repository.GetAll().Count());

            //Verify
            _mockFileManager.Verify(a => a.FileExists(_filePath), Times.Once, "IFileManager - FileExists not get called!");
            _mockFileManager.Verify(a => a.ReadRecords(_filePath), Times.Once, "IFileManager - ReadRecords not get called!");
        }

        [Test, Description("This is unit test for GetAll method for specific customerId!"), Category("SettledBet Repository Unit Tests")]
        public void GetAll_method_returns_data_for_specific_customerId()
        {
            //Arrange
            //Setup
            _mockDataPathFinder.Setup(a => a.GetSettledBetsDataFilePath()).Returns(_filePath);
            _mockFileManager.Setup(a => a.ReadRecords(_filePath)).Returns(new[] { "CId,EId,P,S,W", "1,2,3,4,5", "2,2,3,4,6", "3,2,3,4,7" });
            _mockFileManager.Setup(a => a.FileExists(_filePath)).Returns(true);

            //Act
            //Assert
            Assert.AreEqual(6, repository.GetAll(2).FirstOrDefault(a=> a.CustomerId == 2).Win);

            //Verify
            _mockFileManager.Verify(a => a.FileExists(_filePath), Times.Once, "IFileManager - FileExists not get called!");
            _mockFileManager.Verify(a => a.ReadRecords(_filePath), Times.Once, "IFileManager - ReadRecords not get called!");
        }

        [TearDown]
        public void TearDown()
        {
            _mockFileManager = null;
            _mockDataPathFinder = null;
            repository = null;
        }
    }
}
