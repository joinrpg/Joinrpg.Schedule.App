using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Joinrpg.Schedule.App.PresentationMode.PresentationSuggesters;
using Joinrpg.Schedule.App.Services.Interfaces;
using Xamarin.Forms;

namespace Joinrpg.Schedule.App.PresentationMode
{
    public class PresentationSelecter
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CurrentScheduleHolder _holder;
        private readonly IList<IPresentationSuggester> _suggesters;

        public PresentationSelecter()
        {
            _dateTimeProvider = DependencyService.Get<DateTimeProvider>();
            _holder = DependencyService.Get<CurrentScheduleHolder>();

            //TODO inject enumerable from DI
            _suggesters = new List<IPresentationSuggester>()
            {
                new TodayPresentations(),
                new TomorrowPresentations(),
                new NowGoingListPresentations(),
                new NowGoingOnePresentations(),
                new ProudlyPresentsPresentation(),
            };
        }

        public async Task<NavigationArgs> GetNextPresentation()
        {
            var currentScheduleService = _holder.GetCurrentScheduleService();
            var schedule = await currentScheduleService.GetSchedule();
            var now = _dateTimeProvider.Now();
            return _suggesters.Select(s => s.GetSuggestions(now, schedule).ToList().SelectRandom()).Where(l => l != null).ToList().SelectRandom();
        }
    }

    public static class RandomSelector
    {
        private static readonly Random Random = new Random();

        public static T SelectRandom<T>(this List<T> options) where T:class
        {
            if (!options.Any())
            {
                return null;
            }
            if (options.Count == 1)
            {
                return options.Single();
            }
            var index = Random.Next(0, options.Count - 1);
            return options[index];
        }
    }
}
