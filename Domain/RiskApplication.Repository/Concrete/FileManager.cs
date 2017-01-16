using System;
using System.IO;

namespace RiskApplication.Repository.Concrete
{
    public class FileManager
    {
        public string[] ReadAllLines(string filePath)
        {
            return File.ReadAllText(filePath).Split(',');
        }

        public bool FileExists(string filePath)
        {
            throw new NotImplementedException();
        }
    }
}
