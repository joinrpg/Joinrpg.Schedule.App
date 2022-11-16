using System;
using System.Collections.Generic;
using System.Text;
using Joinrpg.Schedule.App.Services.Interfaces;

namespace Joinrpg.Schedule.Main
{
    public class DateTimeProvider : IDateTimeProvider
    {
        private DateTimeOffset? _freezedTime = null;
        public DateTimeOffset Now()
        {
            return _freezedTime ?? DateTimeOffset.Now;
        }

        public void SetDebug()
        {
            _freezedTime = new DateTimeOffset(2019, 10, 19, 15, 00, 01, TimeSpan.FromHours(3));
        }
    }
}

