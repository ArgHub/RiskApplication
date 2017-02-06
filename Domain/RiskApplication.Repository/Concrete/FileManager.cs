using System;
using System.Collections.Generic;
using System.IO;
using RiskApplication.Repository.Abstract;

namespace RiskApplication.Repository.Concrete
{
    public class FileManager : IFileManager
    {
        public string[] ReadRecords(string filePath)
        {
            var lines = new List<string>();
            using (StreamReader sr = new StreamReader(filePath))
            {
                string currentLine;
                while ((currentLine = sr.ReadLine()) != null)
                {
                   lines.Add(currentLine);
                }
            }
            return lines.ToArray();
        }

        public bool FileExists(string filePath)
        {
            return File.Exists(filePath);
        }
    }
}
