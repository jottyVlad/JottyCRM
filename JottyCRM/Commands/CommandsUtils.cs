using System;
using System.Collections.Generic;

namespace JottyCRM.Commands
{
    public static class CommandsUtils
    {
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for(var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }    
    }
}