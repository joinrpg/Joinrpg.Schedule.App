using System;

using Joinrpg.Schedule.App.Models;

namespace Joinrpg.Schedule.App.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ProgramItemModel Item { get; set; }

        public ItemDetailViewModel() : this(new ProgramItemModel())
        {
            
        }
        public ItemDetailViewModel(ProgramItemModel item)
        {
            Title = item?.Name;
            Item = item;
        }
    }
}
