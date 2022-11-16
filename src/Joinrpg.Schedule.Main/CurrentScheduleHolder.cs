using System;
using System.Collections.Generic;
using System.Text;
using Joinrpg.Schedule.App.Services.Interfaces;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace Joinrpg.Schedule.Main
{
    public class CurrentScheduleHolder
    {
        private readonly IScheduleService scheduleService;

        public CurrentScheduleHolder(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }
        public IProjectScheduleService GetCurrentScheduleService() => scheduleService.GetProjectScheduleService(589);
    }
}
