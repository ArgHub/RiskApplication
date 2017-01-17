using System;
using System.IO;
using RiskApplication.Repository.Abstract;

namespace RiskApplication.Repository.Concrete
{
    public class FileManager : IFileManager
    {
        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllText(filePath).Split(',');
        }
    }
}
