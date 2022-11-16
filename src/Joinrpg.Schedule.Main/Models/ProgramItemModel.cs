using System.Net;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.Main.ViewModels
{
    public class ProgramItemModel
    {
        public string Name { get; }

        public string Description { get; }

        public string StartTime { get; }

        public string EndTime { get; }

        public string Authors { get; }
        public string Room { get;  }

        public int Id { get; }

        public ProgramItemInfoApi Item { get; set; }

        public ProgramItemModel(ProgramItemInfoApi model)
        {
            Id = model.ProgramItemId;
            Name = model.Name;
            Description = WebUtility.HtmlDecode(model.Description);
            StartTime = model.StartTime.ToLocalTime().TimeOfDay.ToString();
            EndTime = model.EndTime.ToLocalTime().TimeOfDay.ToString();
            Authors = string.Join(", ", model.Authors.Select(author => author.Name));
            Room = string.Join(", ", model.Rooms.Select(room => room.Name));
            Item = model;
        }

        public ProgramItemModel()
        {
            Name = "test item";

        }
    }
}
