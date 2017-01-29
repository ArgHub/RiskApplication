using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskApplication.Repository.Model
{
    public class SettledBet : GenericBet
    {
        public int Win {
            get { return WinIndex; }
            set { WinIndex = value; } 
        }

        public bool IsWin
        {
            get { return Win > 0; }
        }
    }
}
