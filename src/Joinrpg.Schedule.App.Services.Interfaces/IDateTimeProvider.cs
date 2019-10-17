using System;
using System.Collections.Generic;
using System.Text;

namespace Joinrpg.Schedule.App.Services.Interfaces
{
    /// <summary>
    /// Allows injecting of current DateTime
    /// </summary>
    public interface IDateTimeProvider
    {
        DateTimeOffset Now();
    }
}
