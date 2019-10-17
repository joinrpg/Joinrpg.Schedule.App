using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Joinrpg.Schedule.App.Services.Interfaces;
using Joinrpg.Web.XGameApi.Client;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.Services.Impl
{
    public class ProjectScheduleServiceImpl : IProjectScheduleService
    {
        private int projectId;
        private ProjectScheduleWebClient projectScheduleWebClient;

        private SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        private IReadOnlyList<ProgramItemInfoApi> _list = null;

        public ProjectScheduleServiceImpl(int projectId, ProjectScheduleWebClient projectScheduleWebClient)
        {
            this.projectId = projectId;
            this.projectScheduleWebClient = projectScheduleWebClient;
        }

        public async Task<IReadOnlyList<ProgramItemInfoApi>> GetSchedule()
        {
            if (!(_list is null))
            {
                return _list;
            }
            await semaphoreSlim.WaitAsync();
            try
            {
                if (!(_list is null))
                {
                    return _list;
                }
                _list = (await projectScheduleWebClient.GetSchedule(projectId)).AsReadOnly();
                return _list;
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        public event Action OnScheduleUpdated;
        public Task ForceUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
