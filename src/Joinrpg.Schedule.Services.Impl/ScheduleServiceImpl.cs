using System.Collections.Concurrent;
using System.Net.Http;
using Joinrpg.Schedule.App.Services.Interfaces;
using Joinrpg.Web.XGameApi.Client;

namespace Joinrpg.Schedule.Services.Impl
{
    public class ScheduleServiceImpl : IScheduleService
    {
        private ConcurrentDictionary<int, IProjectScheduleService> services =
            new ConcurrentDictionary<int, IProjectScheduleService>();

        public IProjectScheduleService GetProjectScheduleService(int projectId)
        {
            //TODO DI
            return services.GetOrAdd(projectId,
                id => new ProjectScheduleServiceImpl(projectId, new ProjectScheduleWebClient(new HttpClient())));
        }
    }
}