using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation.Framework.Utilities
{
    public static class DateTimeHelper
    {
        public static bool IsTimeInBetween(TimeSpan start, TimeSpan end, TimeSpan now)
        {
            return (start <= now && end >= now);
        }
    }
}
