using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.App.Services.Interfaces
{
    public interface IProjectScheduleService
    {
        Task<IReadOnlyList<ProgramItemInfoApi>> GetSchedule();

        event Action OnScheduleUpdated;

        Task ForceUpdate();
    }
}
