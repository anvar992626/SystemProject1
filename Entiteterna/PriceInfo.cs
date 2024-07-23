using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entiteterna
{
    public class PriceInfo
    {
        public Dictionary<int, int> WeeklyPrices { get; set; }

        public int GetPriceForWeek(int week)
        {
            return WeeklyPrices.TryGetValue(week, out int price) ? price : 0;
        }
    }

}
