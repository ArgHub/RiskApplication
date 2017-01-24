using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using RiskApplication.Repository.Abstract;
using RiskApplication.Repository.Concrete;
using RiskApplication.Repository.Model;

namespace RiskApplication.Repository.Tests
{
    [TestFixture, Description("These are unit tests related to DataLoader"), Category("Repository General Unit Tests")]
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

        [Test, Description("This is unit test for ReadRecord method in DataLoader when data is valid"),
         Category("Feature Service Unit Tests")]
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
            Assert.AreEqual(2, dataLoader.LoadDataFile(filePath).Count());

            //Verify
            _mockFileManager.Verify(a => a.FileExists(filePath), Times.Once, "IFileManager - FileExists not get called!");
            _mockFileManager.Verify(a => a.ReadRecords(filePath), Times.Once, "IFileManager - ReadRecords not get called!");
        }

       [Test, Description("This is unit test for ReadRecord method in DataLoader when field numbers do not match in records"),
         Category("Feature Service Unit Tests")]
        public void ThrowsExceptionWhenTheNumberOfRowsElementsDoNotMatch()
        {
            //Arrange
            _testFile = new[]
            {
                "CId,EId,P,S,W",
                "1,2,3,4,5",
                "1,2,3,4"
            };
            string filePath = "Path";

            //Setup
            _mockFileManager.Setup(a => a.ReadRecords(filePath)).Returns(_testFile);
            _mockFileManager.Setup(a => a.FileExists(filePath)).Returns(true);

           //Act
           try
           {
              dataLoader.LoadDataFile(filePath);
           }
           catch (Exception exception)
           {
               if (exception.InnerException.Message == "An unexpected number of fields in data record") return;
           }

            //Assert
           Assert.Fail("Unexpected number of fields in data record not thrown!");
        }

        [Test, Description("This is unit test for ReadRecord method in DataLoader When data is invalid"),
         Category("Feature Service Unit Tests")]
       public void ThrowsExceptionWhenTheFieldsValuesAreNotNumeric()
       {
           //Arrange
           _testFile = new[]
            {
                "CId,EId,P,S,W",
                "1,2,3,4,5",
                "1,2,3,4,S",
                "a,b,c,d,e"
            };
           string filePath = "Path";

           //Setup
           _mockFileManager.Setup(a => a.ReadRecords(filePath)).Returns(_testFile);
           _mockFileManager.Setup(a => a.FileExists(filePath)).Returns(true);

           //Act
           try
           {
               dataLoader.LoadDataFile(filePath);
           }
           catch (Exception exception)
           {
               if (exception.InnerException.Message == "A non-numeric field value") return;
           }

           //Assert
           Assert.Fail("Non-numeric field value not thrown!");
       }

        [TearDown]
        public void TearDown()
        {
            _mockFileManager = null;
            dataLoader = null;
        }
    }
}
