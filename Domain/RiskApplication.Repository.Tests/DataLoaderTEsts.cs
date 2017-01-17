using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using RiskApplication.Repository.Abstract;
using RiskApplication.Repository.Concrete;
using RiskApplication.Repository.Model;

namespace RiskApplication.Repository.Tests
{
    [TestFixture]
    public class DataLoaderTests
    {
        private Mock<IFileManager> _mockFileManager;
        private DataLoader<GenericBet> dataLoader;
        private string[] _testFile;

        [SetUp]
        public void SetUp()
        {
            _mockFileManager = new Mock<IFileManager>();
            dataLoader = new DataLoader<GenericBet>(_mockFileManager.Object);

            _testFile = new string[0];
        }

        [Test]
        public void Data_loader_returns_valid_data()
        {
            //Arrange
            _testFile = new[]
            {
                "CId,EId,P,S,W",
                "1,2,3,4,5",
                "1,2,3,4,5"
            };
            string filePath = "Path";        

            //Act
            _mockFileManager.Setup(a => a.ReadRecords(filePath)).Returns(_testFile);
            _mockFileManager.Setup(a => a.FileExists(filePath)).Returns(true);

            //Assert
            NUnit.Framework.Assert.AreEqual(2, dataLoader.LoadDataFile(filePath).Count());

            //Verify
            _mockFileManager.Verify(a => a.FileExists(filePath), Times.Once, "IFileManager - FileExists not get called!");
            _mockFileManager.Verify(a => a.ReadRecords(filePath), Times.Once, "IFileManager - ReadRecords not get called!");
        }
    }
}
