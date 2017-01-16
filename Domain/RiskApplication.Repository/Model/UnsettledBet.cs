using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiskApplication.Repository.Model
{
    public class UnsettledBet : GenericBet
    {
        public int ToWin
        {
            get { return WinIndex; }
            set { WinIndex = value; }
        }
    }
}
