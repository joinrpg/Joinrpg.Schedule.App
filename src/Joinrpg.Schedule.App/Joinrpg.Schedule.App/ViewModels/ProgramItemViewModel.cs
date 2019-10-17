using System.Linq;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.App.ViewModels
{
    public class ProgramItemViewModel
    {
        private ProgramItemInfoApi Model { get; }

        public string Name => Model.Name;
        public string Description => Model.Description;

        public string StartTime => Model.StartTime.ToLocalTime().TimeOfDay.ToString();

        public string EndTime => Model.EndTime.ToLocalTime().TimeOfDay.ToString();

        public string Authors { get; }
        public string Room { get;  }

        public ProgramItemViewModel(ProgramItemInfoApi model)
        {
            Model = model;
            Authors = string.Join(", ", model.Authors.Select(author => author.Name));
            Room = string.Join(", ", model.Rooms.Select(room => room.Name));
        }
    }
}
