﻿using System.Linq;
using JoinRpg.Web.XGameApi.Contract.Schedule;

namespace Joinrpg.Schedule.App.ViewModels
{
    public class ProgramItemViewModel
    {
        public string Name { get; }

        public string Description { get; }

        public string StartTime { get; }

        public string EndTime { get; }

        public string Authors { get; }
        public string Room { get;  }

        public ProgramItemViewModel(ProgramItemInfoApi model)
        {
            Name = model.Name;
            Description = model.Description;
            StartTime = model.StartTime.ToLocalTime().TimeOfDay.ToString();
            EndTime = model.EndTime.ToLocalTime().TimeOfDay.ToString();
            Authors = string.Join(", ", model.Authors.Select(author => author.Name));
            Room = string.Join(", ", model.Rooms.Select(room => room.Name));
        }

        public ProgramItemViewModel()
        {
            Name = "test item";

        }
    }
}
