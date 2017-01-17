using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskApplication.Repository.Abstract
{
    public interface IFileManager
    {
        string[] ReadRecords(string filePath);
        bool FileExists(string filePath);
    }
}
