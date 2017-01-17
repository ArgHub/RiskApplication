using System;
using System.IO;
using RiskApplication.Repository.Abstract;

namespace RiskApplication.Repository.Concrete
{
    public class FileManager : IFileManager
    {
        public string[] ReadRecords(string filePath)
        {
            return File.ReadAllText(filePath).Split(',');
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
