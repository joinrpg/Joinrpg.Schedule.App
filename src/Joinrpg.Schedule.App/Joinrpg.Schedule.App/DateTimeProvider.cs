using System;
using System.Collections.Generic;
using System.Text;
using Joinrpg.Schedule.App.Services.Interfaces;

namespace Joinrpg.Schedule.App
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now()
        {
            return new DateTimeOffset(2019, 10, 19, 15, 00, 00, TimeSpan.FromHours(3));
            //return DateTimeOffset.Now;
        }
    }
}

