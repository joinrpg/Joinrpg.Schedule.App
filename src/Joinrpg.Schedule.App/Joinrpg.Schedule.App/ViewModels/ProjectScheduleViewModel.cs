using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Joinrpg.Schedule.App.Services.Interfaces;
using JoinRpg.Web.XGameApi.Contract.Schedule;
using Xamarin.Forms;

namespace Joinrpg.Schedule.App.ViewModels
{
    public class ProjectScheduleViewModel : ScheduleViewModelBase
    {
        public ProjectScheduleViewModel()
        {

        }

        public ProjectScheduleViewModel(IProjectScheduleService scheduleService)
         : base(scheduleService)
        {
        }

        protected override IOrderedEnumerable<ProgramItemInfoApi> SelectProgramItems(IReadOnlyList<ProgramItemInfoApi> items)
        {
            return items.OrderBy(item => item.StartTime);
        }
    }
}
