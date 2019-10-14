using System.Collections.Generic;
using System.Threading.Tasks;

namespace JoinRpg.Web.XGameApi.Contract.Schedule
{
    public interface IProjectScheduleWebClient
    {
        Task<List<ProgramItemInfoApi>> GetSchedule(int projectId);
    }
}
