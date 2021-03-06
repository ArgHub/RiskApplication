﻿using System;
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
    [TestFixture, Description("These are unit tests related to UnsettledBet Repository"), Category("UnsettledBet Repository Unit Tests")]
    public class UnsettledBetRepositortTests
    {
        private Mock<IFileManager> _mockFileManager;
        private Mock<IDataPathFinder> _mockDataPathFinder;
        UnsettledBetRepository repository;
        private string _filePath = "Path";

        [SetUp]
        public void SetUp()
        {
            _mockFileManager = new Mock<IFileManager>();
            _mockDataPathFinder = new Mock<IDataPathFinder>();
            repository = new UnsettledBetRepository(_mockFileManager.Object, _mockDataPathFinder.Object);
        }

        [Test, Description("This is unit test for GetAll method when file has no record!"), Category("UnsettledBet Repository Unit Tests")]
        public void GetAll_method_returns_zero_record_when_file_has_no_record()
        {
            //Arrange    
            //Setup
            _mockDataPathFinder.Setup(a => a.GetUnsettledBetsDataFilePath()).Returns(_filePath);
            _mockFileManager.Setup(a => a.ReadRecords(_filePath)).Returns(new[] { "CId,EId,P,S,TW" });
            _mockFileManager.Setup(a => a.FileExists(_filePath)).Returns(true);

            //Act
            //Assert
            Assert.AreEqual(0, repository.GetAll().Count());

            //Verify
            _mockFileManager.Verify(a => a.FileExists(_filePath), Times.Once, "IFileManager - FileExists not get called!");
            _mockFileManager.Verify(a => a.ReadRecords(_filePath), Times.Once, "IFileManager - ReadRecords not get called!");
        }

        [Test, Description("This is unit test for GetAll method when file has records!"), Category("UnsettledBet Repository Unit Tests")]
        public void GetAll_method_returns_expected_records_when_file_has_records()
        {
            //Arrange
            //Setup
            _mockDataPathFinder.Setup(a => a.GetUnsettledBetsDataFilePath()).Returns(_filePath);
            _mockFileManager.Setup(a => a.ReadRecords(_filePath)).Returns(new[] { "CId,EId,P,S,TW", "1,2,3,4,5", "1,2,3,4,5" });
            _mockFileManager.Setup(a => a.FileExists(_filePath)).Returns(true);

            //Act
            //Assert
            Assert.AreEqual(2, repository.GetAll().Count());

            //Verify
            _mockFileManager.Verify(a => a.FileExists(_filePath), Times.Once, "IFileManager - FileExists not get called!");
            _mockFileManager.Verify(a => a.ReadRecords(_filePath), Times.Once, "IFileManager - ReadRecords not get called!");
        }

        [Test, Description("This is unit test for GetAll method for specific customerId!"), Category("UnsettledBet Repository Unit Tests")]
        public void GetAll_method_returns_data_for_specific_customerId()
        {
            //Arrange
            //Setup
            _mockDataPathFinder.Setup(a => a.GetUnsettledBetsDataFilePath()).Returns(_filePath);
            _mockFileManager.Setup(a => a.ReadRecords(_filePath)).Returns(new[] { "CId,EId,P,S,TW", "1,2,3,4,5", "1,2,3,4,6", "3,2,3,4,7" });
            _mockFileManager.Setup(a => a.FileExists(_filePath)).Returns(true);

            //Act
            //Assert
            Assert.AreEqual(2, repository.GetAll(1).Count());

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
