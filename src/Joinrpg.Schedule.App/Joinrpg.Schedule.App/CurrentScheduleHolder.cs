using System;
using System.Collections.Generic;
using System.Text;
using Joinrpg.Schedule.App.Services.Interfaces;
using Xamarin.Forms;

namespace Joinrpg.Schedule.App
{
    public class CurrentScheduleHolder
    {
        public IProjectScheduleService GetCurrentScheduleService()
        {
            var scheduleService = DependencyService.Get<IScheduleService>();
            return scheduleService.GetProjectScheduleService(589);
        }
    }
}
